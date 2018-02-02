using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MyEvernote.Model;
using Microsoft.AspNet.SignalR.Client;

namespace MyEvernote.WinForm
{
    public partial class MainForm : Form
    {
        public static string Token;
        bool flag = false;

        public MainForm()
        {
            InitializeComponent();
        }
    
        private void MainForm_Load(object sender, EventArgs e)
        {
            Variable.activeForm = ActiveForm;
            toolTipShowInfo.SetToolTip(ChBoxSignUp, "Создание нового пользователя.Имя пользователя должно быть уникальным");
        }

        private async void btnSignIn_Click(object sender, EventArgs e)
        {
            string info;
            if (!ChBoxSignUp.Checked)//log in
            {
                // срисовали юзера
                Variable.SelectedUser = new ApplicationUser() { UserName = tBoxNameUser.Text, Password = tBoxPassword.Text };

                // получить токен и установить токен MainForm.Token. если юзера нет в базе - в info придет информация об этом
                if (ServiceClient.GetToken(Variable.SelectedUser,out  info))
                {
                    Variable.Users = ServiceClient.GetAllUsers();
                    Variable.Categories = ServiceClient.GetCategories();
                    var userWindow = new UserWindow();
                    userWindow.Owner = this;
                    Hide();
                    userWindow.Show();
                }
                else
                    MessageBox.Show( info , "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else // log up
            {
                if (string.IsNullOrEmpty(tBoxNameUser.Text)|| string.IsNullOrEmpty(tBoxPassword.Text)) // если имя не введено
                    MessageBox.Show($"имя или пароль отсутствуют", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else 
                {
                    //зарегистрировать
                    var CreatedUser = await ServiceClient.Register(new ApplicationUser() { UserName = tBoxNameUser.Text , Password = tBoxPassword.Text });
                    if (CreatedUser == null)
                        MessageBox.Show($"Пользователь с таким именем уже сущетсвует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        ChBoxSignUp.Checked = false;
                        MessageBox.Show($"ОК", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
    public static class Variable // "глобальные" переменные
    {
        static public IEnumerable<ApplicationUser> Users;
        static public IEnumerable<Category> Categories;
        static public List<Note> Notes;
        static public Form activeForm;
        static public ApplicationUser SelectedUser;
        static public bool CommandToCreate = false;
        static public readonly int MainForm = 0; // индекс окна в Application.OpenForms
        static public readonly int UserWindow = 1;
    }
}
