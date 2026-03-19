namespace PolyclinicApp
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblSectionPersonal = new System.Windows.Forms.Label();
            this.label1LastName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label1FirstName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.label1MiddleName = new System.Windows.Forms.Label();
            this.txtMiddleName = new System.Windows.Forms.TextBox();
            this.label1BirthDate = new System.Windows.Forms.Label();
            this.dtpBirthDate = new System.Windows.Forms.DateTimePicker();
            this.label1Gender = new System.Windows.Forms.Label();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.label1Phone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblSectionAccount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSectionPersonal
            // 
            this.lblSectionPersonal.AutoSize = true;
            this.lblSectionPersonal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblSectionPersonal.ForeColor = System.Drawing.Color.DimGray;
            this.lblSectionPersonal.Location = new System.Drawing.Point(131, 9);
            this.lblSectionPersonal.Name = "lblSectionPersonal";
            this.lblSectionPersonal.Size = new System.Drawing.Size(131, 18);
            this.lblSectionPersonal.TabIndex = 0;
            this.lblSectionPersonal.Text = "Личные данные";
            // 
            // label1LastName
            // 
            this.label1LastName.AutoSize = true;
            this.label1LastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label1LastName.Location = new System.Drawing.Point(20, 45);
            this.label1LastName.Name = "label1LastName";
            this.label1LastName.Size = new System.Drawing.Size(83, 16);
            this.label1LastName.TabIndex = 1;
            this.label1LastName.Text = "Фамилия *";
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(175, 42);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(200, 22);
            this.txtLastName.TabIndex = 0;
            // 
            // label1FirstName
            // 
            this.label1FirstName.AutoSize = true;
            this.label1FirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label1FirstName.Location = new System.Drawing.Point(20, 78);
            this.label1FirstName.Name = "label1FirstName";
            this.label1FirstName.Size = new System.Drawing.Size(46, 16);
            this.label1FirstName.TabIndex = 2;
            this.label1FirstName.Text = "Имя *";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(175, 75);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(200, 22);
            this.txtFirstName.TabIndex = 1;
            // 
            // label1MiddleName
            // 
            this.label1MiddleName.AutoSize = true;
            this.label1MiddleName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label1MiddleName.Location = new System.Drawing.Point(20, 111);
            this.label1MiddleName.Name = "label1MiddleName";
            this.label1MiddleName.Size = new System.Drawing.Size(78, 16);
            this.label1MiddleName.TabIndex = 3;
            this.label1MiddleName.Text = "Отчество";
            // 
            // txtMiddleName
            // 
            this.txtMiddleName.Location = new System.Drawing.Point(175, 108);
            this.txtMiddleName.Name = "txtMiddleName";
            this.txtMiddleName.Size = new System.Drawing.Size(200, 22);
            this.txtMiddleName.TabIndex = 2;
            // 
            // label1BirthDate
            // 
            this.label1BirthDate.AutoSize = true;
            this.label1BirthDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label1BirthDate.Location = new System.Drawing.Point(20, 144);
            this.label1BirthDate.Name = "label1BirthDate";
            this.label1BirthDate.Size = new System.Drawing.Size(129, 16);
            this.label1BirthDate.TabIndex = 4;
            this.label1BirthDate.Text = "Дата рождения *";
            // 
            // dtpBirthDate
            // 
            this.dtpBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBirthDate.Location = new System.Drawing.Point(175, 141);
            this.dtpBirthDate.Name = "dtpBirthDate";
            this.dtpBirthDate.Size = new System.Drawing.Size(200, 22);
            this.dtpBirthDate.TabIndex = 3;
            // 
            // label1Gender
            // 
            this.label1Gender.AutoSize = true;
            this.label1Gender.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label1Gender.Location = new System.Drawing.Point(20, 177);
            this.label1Gender.Name = "label1Gender";
            this.label1Gender.Size = new System.Drawing.Size(46, 16);
            this.label1Gender.TabIndex = 5;
            this.label1Gender.Text = "Пол *";
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.Items.AddRange(new object[] {
            "М",
            "Ж"});
            this.cmbGender.Location = new System.Drawing.Point(175, 174);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(200, 24);
            this.cmbGender.TabIndex = 4;
            // 
            // label1Phone
            // 
            this.label1Phone.AutoSize = true;
            this.label1Phone.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label1Phone.Location = new System.Drawing.Point(20, 212);
            this.label1Phone.Name = "label1Phone";
            this.label1Phone.Size = new System.Drawing.Size(74, 16);
            this.label1Phone.TabIndex = 6;
            this.label1Phone.Text = "Телефон";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(175, 209);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 22);
            this.txtPhone.TabIndex = 5;
            // 
            // lblSectionAccount
            // 
            this.lblSectionAccount.AutoSize = true;
            this.lblSectionAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblSectionAccount.ForeColor = System.Drawing.Color.DimGray;
            this.lblSectionAccount.Location = new System.Drawing.Point(124, 243);
            this.lblSectionAccount.Name = "lblSectionAccount";
            this.lblSectionAccount.Size = new System.Drawing.Size(138, 18);
            this.lblSectionAccount.TabIndex = 7;
            this.lblSectionAccount.Text = "Учётные данные";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 278);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Логин *";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(175, 275);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(200, 22);
            this.txtUsername.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(3, 311);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Пароль *";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(175, 308);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(200, 22);
            this.txtPassword.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(3, 344);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Повторите пароль *";
            // 
            // txtConfirm
            // 
            this.txtConfirm.Location = new System.Drawing.Point(175, 341);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.PasswordChar = '*';
            this.txtConfirm.Size = new System.Drawing.Size(200, 22);
            this.txtConfirm.TabIndex = 8;
            // 
            // btnRegister
            // 
            this.btnRegister.AutoSize = true;
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.SystemColors.Control;
            this.btnRegister.Location = new System.Drawing.Point(20, 382);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(182, 35);
            this.btnRegister.TabIndex = 9;
            this.btnRegister.Text = "Зарегистрироваться";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.Color.Red;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Location = new System.Drawing.Point(220, 384);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 31);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(20, 430);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 16);
            this.lblStatus.TabIndex = 11;
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 460);
            this.Controls.Add(this.lblSectionPersonal);
            this.Controls.Add(this.label1LastName);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.label1FirstName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.label1MiddleName);
            this.Controls.Add(this.txtMiddleName);
            this.Controls.Add(this.label1BirthDate);
            this.Controls.Add(this.dtpBirthDate);
            this.Controls.Add(this.label1Gender);
            this.Controls.Add(this.cmbGender);
            this.Controls.Add(this.label1Phone);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblSectionAccount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "RegisterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Регистрация пациента";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSectionPersonal;
        private System.Windows.Forms.Label label1LastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label label1FirstName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label label1MiddleName;
        private System.Windows.Forms.TextBox txtMiddleName;
        private System.Windows.Forms.Label label1BirthDate;
        private System.Windows.Forms.DateTimePicker dtpBirthDate;
        private System.Windows.Forms.Label label1Gender;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Label label1Phone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblSectionAccount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblStatus;
    }
}