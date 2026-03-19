using System;
using System.Windows.Forms;
using Npgsql;

namespace PolyclinicApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            try
            {

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    lblStatus.Text = "Введите логин и пароль";
                    return;
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Ошибка логина и пароля");
            }

            if (!DbHelper.TestConnection())
            {
                lblStatus.Text = "Нет подключения к базе данных";
                return;
            }

            string sql = @"SELECT user_id, username, role
                           FROM users
                           WHERE username = @user
                             AND password_hash = md5(@pass)
                             AND is_active = true";

            var result = DbHelper.ExecuteQuery(sql,
                new NpgsqlParameter("@user", username),
                new NpgsqlParameter("@pass", password));

            if (result.Rows.Count == 0)
            {
                lblStatus.Text = "Неверный логин или пароль";
                return;
            }

            string role = result.Rows[0]["role"].ToString();
            this.Hide();
            var mainForm = new MainForm(username, role);
            mainForm.FormClosed += (s, args) => this.Close();
            mainForm.Show();
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            var form = new RegisterForm();
            form.ShowDialog();
        }
    }
}