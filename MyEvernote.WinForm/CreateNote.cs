using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyEvernote.Model;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MyEvernote.WinForm
{
    public partial class CreateNote : Form
    {
        public CreateNote()
        {
            InitializeComponent();
            coBoxCategory.Items.AddRange(Variable.Categories.Select(x => x.Name).ToArray());
        }

        private void btnCancelCreation_Click(object sender, EventArgs e)
        {
            Close();
            Application.OpenForms[Variable.UserWindow].Show();
        }

        private  async void btnSaveNote_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtBoxTitleNote.Text) || Variable.Notes.Exists(x=>x.Title == TxtBoxTitleNote.Text) )
            {
                MessageBox.Show($"нет имени заметки или такое имя уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
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
            Note note = new Note()
            {
                Title = TxtBoxTitleNote.Text,
                Text = TxtBoxTextNote.Text,
                Creator = Variable.User.Id,
                Category = CategoryGuid,
                Id = Guid.NewGuid()
            };

            await MainForm.serviceClient.client.PostAsJsonAsync($"notes", note);
            Variable.Notes.Add(MainForm.serviceClient.client.GetAsync($"note/{note.Id}").Result.Content.ReadAsAsync<Note>().Result);

            ((UserWindow)Owner).RefreshWindow();

            btnCancelCreation_Click(new object(), null);
        }
    }
}
