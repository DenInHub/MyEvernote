using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Net.Http;
using System.Linq;
using System.Windows.Forms;
using MyEvernote.Model;
using System.Drawing;

namespace MyEvernote.WinForm
{
    public partial class UserWindow : Form
    {
        public Note selectedNote;
        public UserWindow()
        {
            InitializeComponent();
        }

        private void UserWindow_Load(object sender, EventArgs e)
        {
            if (!((MainForm)Owner).ChBoxSignUp.Checked)
            {
                coBoxUserNotes.Items.AddRange(Variable.Notes.Select(x => x.Title).ToArray()); // заполнение ListBox именами заметок
            }
        }

        private void coBoxUserNotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(coBoxUserNotes.Text))
            {
                tBoxNoteText.Text = string.Empty;
                selectedNote = null;
                return;
            }
            selectedNote = Variable.Notes.Single(x => x.Title == coBoxUserNotes.Text);
            tBoxNoteText.Text = selectedNote.Text; // показать текст выбранной заметки
        }

        private void btnViewInfo_Click(object sender, EventArgs e)
        {
            selectedNote.ShowNoteInfoBox();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (selectedNote == null)
                return;
            // зпомнить индекс для следующего обновления списка заметок
            var i = Variable.Notes.IndexOf(selectedNote);
            // заносим изменения в локальную переменную
            selectedNote.Title = coBoxUserNotes.Text;
            selectedNote.Text = tBoxNoteText.Text;
            // заносим в базу 
            await MainForm.serviceClient.client.PostAsJsonAsync($"notes/{selectedNote.Id}", selectedNote);// потому что следующий метод иногда выполняется быстрее
            selectedNote = MainForm.serviceClient.client.GetAsync($"note/{selectedNote.Id}").Result.Content.ReadAsAsync<Note>().Result;// возвращаем из базы
            // обновление списка
            Variable.Notes[i] = selectedNote;
            RefreshWindow();
            
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Close();
            Application.OpenForms[Variable.MainForm].Show();
        }

        private void btnCreateNote_Click(object sender, EventArgs e)
        {
            var CreateNoteWindow = new CreateNote();
            CreateNoteWindow.Owner = this;
            Hide();
            CreateNoteWindow.Show();
        }

        private void btnDeleteNote_Click(object sender, EventArgs e)
        {
            if (selectedNote == null)
                return;

            var id = Variable.Notes.Single(x => x.Title == coBoxUserNotes.Text).Id;
            MainForm.serviceClient.client.DeleteAsync($"notes/{id}");//удаляем из базы 
            Variable.Notes.Remove(Variable.Notes.Single(x => x.Title == coBoxUserNotes.Text));//удаляем из листа
            RefreshWindow();
            
        }

        public  void RefreshWindow()
        {
            //обновление бокса 
            if (Variable.Notes.Count != 0)
            {
                coBoxUserNotes.DataSource = Variable.Notes.Select(x => x.Title).ToArray();
                coBoxUserNotes.SelectedIndex = -1;
            }
            else
            {
                //coBoxUserNotes.Text = string.Empty;
                coBoxUserNotes.DataSource = null;
                coBoxUserNotes.Items.Clear();
                coBoxUserNotes_SelectedIndexChanged(new object(), null);
            }
        }
    }
}
