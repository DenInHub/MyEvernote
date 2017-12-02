using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MyEvernote.Model;
using System.Net.Http;
using System.Net.Http.Headers;


namespace MyEvernote.WinForm
{
    
    public partial class UserWindow : Form
    {
        public  Note selectedNote;

        public UserWindow()
        {
            InitializeComponent();
        }

        private void UserWindow_Load(object sender, EventArgs e)
        {
            if (!((MainForm)Owner).ChBoxSignUp.Checked)
            {
                listBoxNotesOfUser.Items.AddRange(Variable.Notes.Select(x => x.Title).ToArray());
            }
            Text = Variable.SelectedUser.Name;

        }
        private void listBoxNotesOfUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(listBoxNotesOfUser.Text))
            {
                listBoxNotesOfUser.Text = string.Empty;
                selectedNote = null;
                return;
            }
            selectedNote = Variable.Notes.Single(x => x.Title == listBoxNotesOfUser.Text);
            tBoxNoteText.Text = selectedNote.Text; // показать текст выбранной заметки
        }

        private void btnViewInfo_Click(object sender, EventArgs e)
        {
            if (selectedNote == null)
                return;

            List<string> owners = new List<string>();
            for (int i = 0; i < selectedNote?.Shared?.Count; i++)
            {
                owners.Add(Variable.Users.First(x => x.Id == selectedNote.Shared[i]).Name);
            }
            var str = string.Format("{0,-25}\t{1,-25}\n{2,-25}\t{3,-25}\n{4,-25}\t{5,-25}\n{6,-25}\t{7,-25}\n{8,-25}\t{9,-25}\n{10,-30}\n{11,-25}\n",
                "Имя заметки:", selectedNote.Title,
                "Автор заметки:", Variable.Users.Single(x => x.Id == selectedNote.Creator).Name,
                "Категория заметки:", Variable.Categories?.First(x => x.Id == selectedNote.Category).Name,
                "Дата создания:", selectedNote.Created,
                "Дата изменения:", selectedNote.Changed,
                "Владельцы:", string.Join("\n", owners)
                );
            MessageBox.Show(str, "Note info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private  void btnChange_Click(object sender, EventArgs e)
        {
            if (selectedNote == null)
                return;
            if (selectedNote.Creator != Variable.SelectedUser.Id)
            {
                MessageBox.Show($"Недостаточно прав для редактирования .", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            btnCreateNote_Click(sender, null); // форма(NoteWindow) для изменения и создания замекти одна , разница в кнопке ее вызывающей. 
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Close();
            Application.OpenForms[Variable.MainForm].Show();
        }

        private void btnCreateNote_Click(object sender, EventArgs e)
        {
            Variable.CommandToCreate = (sender as Button).Name == "btnCreateNote"? true:false;

            var CreateNoteWindow = new NoteWindow();
            CreateNoteWindow.Owner = this;
            Hide();
            CreateNoteWindow.Show();
        }

        private void btnDeleteNote_Click(object sender, EventArgs e)
        {
            if (selectedNote == null)
                return;
            if (selectedNote.Creator != Variable.SelectedUser.Id)
            {
                MessageBox.Show($"Недостаточно прав для удаления .", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var NoteId = Variable.Notes.Single(x => x.Title == listBoxNotesOfUser.Text).Id;
            ServiceClient.DeleteNote(selectedNote.Id);
            Variable.Notes.Remove(Variable.Notes.Single(x => x.Title == listBoxNotesOfUser.Text));//удаляем из листа

            RefreshWindow();
        }

        public  void RefreshWindow()
        {
            //обновление бокса 
            if (Variable.Notes.Count != 0)
            {
                listBoxNotesOfUser.DataSource = Variable.Notes.Select(x => x.Title).ToArray();
                tBoxNoteText.Text = string.Empty;

                listBoxNotesOfUser.SelectedIndex = - 1;
            }
            else
            {
                listBoxNotesOfUser.DataSource = null;
                listBoxNotesOfUser.Items.Clear();
                listBoxNotesOfUser_SelectedIndexChanged(new object(), null);
                tBoxNoteText.Text = string.Empty;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Variable.Notes = ServiceClient.GetNotesOfUser(Variable.SelectedUser.Id);
            //listBoxNotesOfUser.Items.AddRange(Variable.Notes.Select(x => x.Title).ToArray());
            RefreshWindow();
        }
    }
}
