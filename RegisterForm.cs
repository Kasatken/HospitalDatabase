using System;
using System.Windows.Forms;
using Npgsql;

namespace PolyclinicApp
{
    // Форма самостоятельной регистрации пациента
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Валидация личных данных
            if (string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                ShowError("Заполните фамилию и имя");
                return;
            }

            if (cmbGender.SelectedIndex < 0)
            {
                ShowError("Выберите пол");
                return;
            }

            // Валидация учётных данных 
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirm = txtConfirm.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ShowError("Заполните логин и пароль");
                return;
            }

            if (password.Length < 4)
            {
                ShowError("Пароль должен быть не короче 4 символов");
                return;
            }

            if (password != confirm)
            {
                ShowError("Пароли не совпадают");
                return;
            }

            // Проверка уникальности логина 
            var existing = DbHelper.ExecuteQuery(
                "SELECT user_id FROM users WHERE username = @u",
                new NpgsqlParameter("@u", username));

            if (existing.Rows.Count > 0)
            {
                ShowError("Этот логин уже занят");
                return;
            }

            try
            {
                object ValueOrNull(string s) =>
                    string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : s.Trim();

                // 1. Создаём запись в таблице patients
                int newPatientId = Convert.ToInt32(DbHelper.ExecuteScalar(@"
                    INSERT INTO patients
                        (last_name, first_name, middle_name, birth_date, gender, phone, address, policy_number)
                    VALUES
                        (@ln, @fn, @mn, @bd, @g, @ph, NULL, NULL)
                    RETURNING patient_id",
                    new NpgsqlParameter("@ln", txtLastName.Text.Trim()),
                    new NpgsqlParameter("@fn", txtFirstName.Text.Trim()),
                    new NpgsqlParameter("@mn", ValueOrNull(txtMiddleName.Text)),
                    new NpgsqlParameter("@bd", dtpBirthDate.Value.Date),
                    new NpgsqlParameter("@g", cmbGender.SelectedItem.ToString()),
                    new NpgsqlParameter("@ph", ValueOrNull(txtPhone.Text))));

                // 2. Создаём пользователя с ролью registrar
                DbHelper.ExecuteNonQuery(@"
                 INSERT INTO users (username, password_hash, role, is_active, patient_id)
                 VALUES (@u, md5(@p), 'registrar', true, @pid)",
                 new NpgsqlParameter("@u", username),
                 new NpgsqlParameter("@p", password),
                 new NpgsqlParameter("@pid", newPatientId));

                lblStatus.ForeColor = System.Drawing.Color.Green;
                lblStatus.Text = "Регистрация прошла успешно!";

                var timer = new System.Windows.Forms.Timer { Interval = 1500 };
                timer.Tick += (s, args) =>
                {
                    timer.Stop();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                };
                timer.Start();
            }
            catch (Exception ex)
            {
                ShowError("Ошибка: " + ex.Message);
            }
        }

        private void ShowError(string message)
        {
            lblStatus.ForeColor = System.Drawing.Color.Red;
            lblStatus.Text = message;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}