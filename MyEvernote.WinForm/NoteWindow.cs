﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MyEvernote.Model;
using System.Threading;


namespace MyEvernote.WinForm
{
   
    public partial class NoteWindow : Form
    {
        Note selectedNote;
        int index;
        
        public NoteWindow() 
        {
            InitializeComponent();
        }

        private void NoteWindow_Load(object sender, EventArgs e)
        {
            coBoxCategory.Items.AddRange(Variable.Categories.Select(x => x.Name).ToArray()); // заполнить категории
            checkedListBoxShared.Items.AddRange(Variable.Users.Where(x => x.Name != Variable.SelectedUser.Name)?.Select(x => x.Name).ToArray());// заполнить лист бокс юзерами
            toolTipShowInfo.SetToolTip(coBoxCategory, "Выбрать категории из существующего списка.\nЕсли категория не выбрана будет установлена категория по умолчанию.\nДля создания новой категории введите ее имя");
            toolTipShowInfo.SetToolTip(checkedListBoxShared, "Если пользователи не отображаются значит их нет в базе");

            if (Variable.CommandToCreate != true) // если редактируем , то выводим информацию о заметке в окно
            {
                selectedNote = ((UserWindow)Owner).selectedNote;
                coBoxCategory.Text = Variable.Categories.First(x => x.Id == selectedNote.Category).Name;
                TxtBoxTitleNote.Text = selectedNote.Title;
                TxtBoxTextNote.Text = selectedNote.Text;
                // чекнуть тех кому пошарена
                foreach (var UserId in selectedNote.Shared)
                {
                    int index=0;
                    string UserName = Variable.Users.First(x => x.Id == UserId).Name;
                    if (UserName != Variable.SelectedUser.Name)
                        index = checkedListBoxShared.Items.IndexOf(UserName);
                    else
                        continue;
                    checkedListBoxShared.SetItemCheckState(index, CheckState.Checked);
                }
            }
        }


        private  async void btnSaveNote_Click(object sender, EventArgs e)
        {
            index = Variable.Notes.IndexOf(selectedNote);

            if ((string.IsNullOrEmpty(TxtBoxTitleNote.Text) || Variable.Notes.Exists(x => x.Title == TxtBoxTitleNote.Text)) && Variable.CommandToCreate)
            {
                MessageBox.Show($"нет имени заметки или такое имя уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //---------------------- CategoryGuid
            Guid CategoryGuid;
            if (string.IsNullOrEmpty(coBoxCategory.Text))
                CategoryGuid = Guid.Parse("00000000-0000-0000-0000-00000000FFFF"); // defult guid
            else
            {
                if (Variable.Categories.Exists(x => x.Name == coBoxCategory.Text)) // такая категория существует выбрать ее
                    CategoryGuid = Variable.Categories.First(x => x.Name == coBoxCategory.Text).Id;
                else // нетCategoryGuid - создать и добавить в лист 
                {
                    Category NewCategory = new Category { Id = Guid.NewGuid(), Name = coBoxCategory.Text };
                    Variable.Categories.Add(NewCategory);
                    CategoryGuid = ServiceClient.CreateCategory(NewCategory).Id;
                }
            }
            //---------------------- END CategoryGuid


            //---------------------- Create Note
            Note note = new Note()
            {
                Title = TxtBoxTitleNote.Text,
                Text = TxtBoxTextNote.Text,
                Creator = Variable.SelectedUser.Id,
                Category = CategoryGuid,
                Id = Variable.CommandToCreate ? Guid.NewGuid() : selectedNote.Id
            };

            if (Variable.CommandToCreate)
                // Create Note
                await ServiceClient.CreateNote(note);
            else
                // Change Note
                await ServiceClient.ChangeNote(note);
            //---------------------- END Create Note


            //---------------------- Shared
            if ( selectedNote?.Shared.Count != 0 ) // если заметка кому то пошарена , то удалить эту зависимость из таблицы
                ServiceClient.CancelShare(note.Id);

            List<string> SharedName = new List<string>();
            SharedName.AddRange(checkedListBoxShared.CheckedItems.Cast<string>().ToArray()); // забрать имена юзеров из checkedListBoxShared

            var requestUri = $"notes/share/{note.Id}";

            foreach (var UserName in SharedName)
            {
                var UserId = Variable.Users.First(x => x.Name == UserName).Id; // найти соответствие в users и забрать guid
                ServiceClient.ShareNote(note.Id, UserId);   

            }
            Thread.Sleep(500); // ну да , костыль ; без него иногда из базы забирается заметка в которой еще нет информации о том , кому она пошарена
            //---------------------- END Shared

            //---------------------- Забрать из базы

            if (Variable.CommandToCreate)
                Variable.Notes.Add(ServiceClient.GetNote(note.Id));
            else
                Variable.Notes[index] = ServiceClient.GetNote(note.Id);

            //------------------ END Забрать из базы

            ((UserWindow)Owner).RefreshWindow();
            Variable.CommandToCreate = false;
            btnCancelCreation_Click(new object(), null);
        }


        private void btnCancelCreation_Click(object sender, EventArgs e)
        {
            Variable.CommandToCreate = false;
            Close();
            Application.OpenForms[Variable.UserWindow].Show();
        }
        public void OnDependencyChange()
        {

        }
    }
}
