using System;
using System.Windows.Forms;

namespace PolyclinicApp
{
    // Форма смены роли пользователя
    public partial class UserRoleForm : Form
    {
        // Выбранная роль читается из MainForm после закрытия
        public string SelectedRole { get; private set; }

        public UserRoleForm(string username, string currentRole)
        {
            InitializeComponent();
            this.Text = $"Смена роли: {username}";
            lblUsername.Text = $"Пользователь: {username}";
            cmbRole.SelectedItem = currentRole;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbRole.SelectedItem == null) return;
            SelectedRole = cmbRole.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}