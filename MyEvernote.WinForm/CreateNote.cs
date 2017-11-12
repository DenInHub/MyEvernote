using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MyEvernote.Model;
using System.Net.Http;


namespace MyEvernote.WinForm
{
   
    public partial class CreateNote : Form
    {
            string action;

        public CreateNote(Button btn , Form own) : this() 
        {
            if (btn.Name == "btnCreateNote")
            {
                coBoxCategory.Items.AddRange(Variable.Categories.Select(x => x.Name).ToArray()); // заполнить категории
                checkedListBoxShared.Items.AddRange(Variable.Users.Where(x => x.Name != Variable.User.Name)?.Select(x => x.Name).ToArray());// заполнить лист бокс юзерами*/
                action = "create";
                Owner = own;
            }
            if (btn.Name == "btnChange")
            {
                Owner = own;
                action = "change";
                var selectedNote = ((UserWindow)Owner).selectedNote;
                coBoxCategory.Text = Variable.Categories.First(x => x.Id == selectedNote.Category).Name;
                TxtBoxTitleNote.Text = selectedNote.Title;
                TxtBoxTextNote.Text = selectedNote.Text;
                //кому пошарена 
                checkedListBoxShared.Items.AddRange(Variable.Users.Where(x => x.Name != Variable.User.Name)?.Select(x => x.Name).ToArray()); // добавить всех юзеров
                // чекнуть тех кому пошарена
                foreach (var UserId in selectedNote.Shared)
                {
                    string UserName = Variable.Users.First(x => x.Id == UserId).Name;
                    int index = checkedListBoxShared.Items.IndexOf(UserName);
                    checkedListBoxShared.SetItemCheckState(index, CheckState.Checked);
                }
            }
        }
        public CreateNote() 
        {
            InitializeComponent();
            toolTipShowInfo.SetToolTip(coBoxCategory, "Выбрать категории из существующего списка.\nЕсли категория не выбрана будет установлена категория по умолчанию.\nДля создания новой категории введите ее имя");
            toolTipShowInfo.SetToolTip(checkedListBoxShared,"Если пользователи не отображаются значит их нет в базе");
        }

        private void btnCancelCreation_Click(object sender, EventArgs e)
        {
            Close();
            Application.OpenForms[Variable.UserWindow].Show();
        }

        private  async void btnSaveNote_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(TxtBoxTitleNote.Text) || Variable.Notes.Exists(x => x.Title == TxtBoxTitleNote.Text))
            {
                MessageBox.Show($"нет имени заметки или такое имя уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //---------------------- CategoryGuid
            Guid CategoryGuid;
            if (string.IsNullOrEmpty(coBoxCategory.Text))
                CategoryGuid = Guid.Parse("00000000-0000-0000-0000-00000000FFFF");
            else
            {
                if (Variable.Categories.Exists(x => x.Name == coBoxCategory.Text)) // такая категория существует выбрать ее
                    CategoryGuid = Variable.Categories.First(x => x.Name == coBoxCategory.Text).Id;
                else // нетCategoryGuid - создать 
                    CategoryGuid = MainForm.serviceClient.client.PostAsJsonAsync("Categories/", new Category { Id = Guid.NewGuid(), Name = coBoxCategory.Text }).Result.Content.ReadAsAsync<Category>().Result.Id;
            }
            //---------------------- CategoryGuid

            Note note = new Note()
            {
                Title = TxtBoxTitleNote.Text,
                Text = TxtBoxTextNote.Text,
                Creator = Variable.User.Id,
                Category = CategoryGuid,
                Id = Guid.NewGuid()
            };


            //---------------------- Create Note
            await MainForm.serviceClient.client.PostAsJsonAsync($"notes", note);
            //---------------------- Create Note

            //---------------------- Shared
            //List<Guid> SharedGuid = new List<Guid>();
            List<string> SharedName = new List<string>();
            SharedName.AddRange(checkedListBoxShared.CheckedItems.Cast<string>().ToArray()); // забрать имена юзеров из checkedListBoxShared

            var requestUri = $"notes/share/{note.Id}";

            foreach (var UserName in SharedName)
            {
                var UserId = Variable.Users.First(x => x.Name == UserName).Id; // найти соответствие в users и забрать guid
                StringContent content = new StringContent(string.Empty);
                await MainForm.serviceClient.client.PostAsJsonAsync($"notes/share/{note.Id}/{UserId}", content); // 
            }

            //---------------------- Shared

            //ЗАБРАТЬ ИЗ БАЗЫ
            Variable.Notes.Add(MainForm.serviceClient.client.GetAsync($"note/{note.Id}").Result.Content.ReadAsAsync<Note>().Result);

            ((UserWindow)Owner).RefreshWindow();

            btnCancelCreation_Click(new object(), null);
    

        }

    }
}
