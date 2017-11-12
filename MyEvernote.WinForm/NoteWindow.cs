using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MyEvernote.Model;
using System.Net.Http;


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
                    string UserName = Variable.Users.First(x => x.Id == UserId).Name;
                    int index = checkedListBoxShared.Items.IndexOf(UserName);
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
                    CategoryGuid = MainForm.serviceClient.client.PostAsJsonAsync("Categories/", NewCategory).Result.Content.ReadAsAsync<Category>().Result.Id;
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
                await MainForm.serviceClient.client.PostAsJsonAsync($"notes", note);
            else
               // Change Note
                await MainForm.serviceClient.client.PostAsJsonAsync($"notes/{note.Id}", note);
            //---------------------- END Create Note


            //---------------------- Shared
            if (selectedNote?.Shared != null )
                await MainForm.serviceClient.client.DeleteAsync($"notes/share/{note.Id}");

            List<string> SharedName = new List<string>();
            SharedName.AddRange(checkedListBoxShared.CheckedItems.Cast<string>().ToArray()); // забрать имена юзеров из checkedListBoxShared

            var requestUri = $"notes/share/{note.Id}";

            foreach (var UserName in SharedName)
            {
                var UserId = Variable.Users.First(x => x.Name == UserName).Id; // найти соответствие в users и забрать guid
                StringContent content = new StringContent(string.Empty);
                await MainForm.serviceClient.client.PostAsJsonAsync($"notes/share/{note.Id}/{UserId}", content); // 
            }
            //---------------------- END Shared

            //---------------------- Забрать из базы

            if (Variable.CommandToCreate)
                Variable.Notes.Add(MainForm.serviceClient.client.GetAsync($"note/{note.Id}").Result.Content.ReadAsAsync<Note>().Result);
            else
                Variable.Notes[index] = MainForm.serviceClient.client.GetAsync($"note/{note.Id}").Result.Content.ReadAsAsync<Note>().Result;
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
    }
}
