using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MyEvernote.Model;

namespace MyEvernote.WinForm
{
    public partial class MainForm : Form
    {

        public static ServiceClient serviceClient;
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            serviceClient = new ServiceClient("http://localhost:36932/api/");
            Variable.Users = serviceClient.GetAllUsers();
            Variable.Categories = serviceClient.GetCategories();
            Variable.activeForm = ActiveForm;

            toolTipShowInfo.SetToolTip(ChBoxSignUp, "Созданием нового пользователя./Имя пользователя должно быть уникальным");
#if DEBUG
            tBoxNameUser.Text = "юзер1";
#endif
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (!ChBoxSignUp.Checked)//log in
            {
                var SelectedUser = Variable.Users.Find(x => x.Name == tBoxNameUser.Text);
                if (SelectedUser != null) // такой юзер есть
                {
                    var userWindow = new UserWindow();
                    userWindow.Owner = this;
                    Variable.SelectedUser = SelectedUser; // сохраняем юзера
                    Variable.Notes = serviceClient.GetNotesOfUser(SelectedUser.Id); // сохраняем его заметки .
                    tBoxNameUser.Clear();
                    Hide();
                    userWindow.Show();    
                }
                else//юзера нет
                {
                    MessageBox.Show($"Пользователь: {tBoxNameUser.Text} не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tBoxNameUser.Clear();
                }
            }
            else // log up
            {
                
                if (string.IsNullOrEmpty(tBoxNameUser.Text)) // если имя не введено
                    MessageBox.Show($"Введите имя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else 
                {
                    if (Variable.Users.Exists(x=>x.Name== tBoxNameUser.Text)) // если такой пользователь ужесть
                    {
                        MessageBox.Show($"Такой пользователь уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tBoxNameUser.Clear();
                    }
                    else
                    {
                        var userWindow = new UserWindow();
                        userWindow.Owner = this;

                        Variable.SelectedUser = serviceClient.CreateUser(new User { Id = Guid.NewGuid(), Name = tBoxNameUser.Text });
                        Variable.Notes = new List<Note>();
                        Hide();
                        userWindow.Show();
                    }
                }
            }

        }
       
    }
    public static class Variable // "глобальные" переменные
    {
        static public Form activeForm;
        static public User SelectedUser;
        static public List<User> Users = new List<User>();
        static public List<Category> Categories;
        static public List<Note> Notes;
        static public readonly int MainForm = 0; // индекс окна в Application.OpenForms
        static public readonly int UserWindow = 1;
    }
}
