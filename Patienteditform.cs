using System;
using System.Windows.Forms;
using Npgsql;

namespace PolyclinicApp
{
    public partial class PatientEditForm : Form
    {
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox txtLastName;
        private TextBox txtFirstName;
        private TextBox txtMiddleName;
        private DateTimePicker dtpBirthDate;
        private ComboBox cmbGender;
        private TextBox txtPhone;
        private TextBox txtAddress;
        private TextBox txtPolicy;
        private Button btnSave;
        private Button btnCancel;
        private int? patientId;

        public PatientEditForm(int? id)
        {
            InitializeComponent();
            patientId = id;

            if (patientId.HasValue)
            {
                this.Text = "Редактирование пациента";
                LoadPatientData();
            }
            else
            {
                this.Text = "Новый пациент";
            }
        }

        private void LoadPatientData()
        {
            var table = DbHelper.ExecuteQuery(
                "SELECT * FROM patients WHERE patient_id = @id",
                new NpgsqlParameter("@id", patientId.Value));

            if (table.Rows.Count == 0) return;

            var row = table.Rows[0];
            txtLastName.Text = row["last_name"].ToString();
            txtFirstName.Text = row["first_name"].ToString();
            txtMiddleName.Text = row["middle_name"].ToString();
            dtpBirthDate.Value = Convert.ToDateTime(row["birth_date"]);
            cmbGender.SelectedItem = row["gender"].ToString().Trim();
            txtPhone.Text = row["phone"].ToString();
            txtAddress.Text = row["address"].ToString();
            txtPolicy.Text = row["policy_number"].ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Заполните фамилию и имя.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (patientId.HasValue)
                {
                    string sql = @"UPDATE patients SET
                                    last_name = @ln, first_name = @fn, middle_name = @mn,
                                    birth_date = @bd, gender = @g, phone = @ph,
                                    address = @addr, policy_number = @pol
                                   WHERE patient_id = @id";

                    DbHelper.ExecuteNonQuery(sql,
                        new NpgsqlParameter("@ln", txtLastName.Text.Trim()),
                        new NpgsqlParameter("@fn", txtFirstName.Text.Trim()),
                        new NpgsqlParameter("@mn", (object)txtMiddleName.Text.Trim() ?? DBNull.Value),
                        new NpgsqlParameter("@bd", dtpBirthDate.Value.Date),
                        new NpgsqlParameter("@g", cmbGender.SelectedItem.ToString()),
                        new NpgsqlParameter("@ph", (object)txtPhone.Text.Trim() ?? DBNull.Value),
                        new NpgsqlParameter("@addr", (object)txtAddress.Text.Trim() ?? DBNull.Value),
                        new NpgsqlParameter("@pol", string.IsNullOrEmpty(txtPolicy.Text) ? DBNull.Value : (object)txtPolicy.Text.Trim()),
                        new NpgsqlParameter("@id", patientId.Value));
                }
                else
                {
                    string sql = @"INSERT INTO patients
                                    (last_name, first_name, middle_name, birth_date, gender, phone, address, policy_number)
                                   VALUES (@ln, @fn, @mn, @bd, @g, @ph, @addr, @pol)";

                    DbHelper.ExecuteNonQuery(sql,
                        new NpgsqlParameter("@ln", txtLastName.Text.Trim()),
                        new NpgsqlParameter("@fn", txtFirstName.Text.Trim()),
                        new NpgsqlParameter("@mn", (object)txtMiddleName.Text.Trim() ?? DBNull.Value),
                        new NpgsqlParameter("@bd", dtpBirthDate.Value.Date),
                        new NpgsqlParameter("@g", cmbGender.SelectedItem.ToString()),
                        new NpgsqlParameter("@ph", (object)txtPhone.Text.Trim() ?? DBNull.Value),
                        new NpgsqlParameter("@addr", (object)txtAddress.Text.Trim() ?? DBNull.Value),
                        new NpgsqlParameter("@pol", string.IsNullOrEmpty(txtPolicy.Text) ? DBNull.Value : (object)txtPolicy.Text.Trim()));
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения:\n" + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtMiddleName = new System.Windows.Forms.TextBox();
            this.dtpBirthDate = new System.Windows.Forms.DateTimePicker();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtPolicy = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "Фамилия:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "Имя:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "Отчество:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Дата рождения:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Пол:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(59, 254);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Телефон:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(60, 306);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "Адрес:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(59, 352);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 16);
            this.label8.TabIndex = 10;
            this.label8.Text = "Полис:";
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(172, 30);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(197, 22);
            this.txtLastName.TabIndex = 9;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(172, 68);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(197, 22);
            this.txtFirstName.TabIndex = 8;
            // 
            // txtMiddleName
            // 
            this.txtMiddleName.Location = new System.Drawing.Point(172, 118);
            this.txtMiddleName.Name = "txtMiddleName";
            this.txtMiddleName.Size = new System.Drawing.Size(197, 22);
            this.txtMiddleName.TabIndex = 7;
            // 
            // dtpBirthDate
            // 
            this.dtpBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBirthDate.Location = new System.Drawing.Point(172, 161);
            this.dtpBirthDate.Name = "dtpBirthDate";
            this.dtpBirthDate.Size = new System.Drawing.Size(197, 22);
            this.dtpBirthDate.TabIndex = 6;
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Items.AddRange(new object[] {
            "М",
            "Ж"});
            this.cmbGender.Location = new System.Drawing.Point(172, 198);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(197, 24);
            this.cmbGender.TabIndex = 5;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(172, 254);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(197, 22);
            this.txtPhone.TabIndex = 4;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(172, 303);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(197, 22);
            this.txtAddress.TabIndex = 3;
            // 
            // txtPolicy
            // 
            this.txtPolicy.Location = new System.Drawing.Point(172, 346);
            this.txtPolicy.Name = "txtPolicy";
            this.txtPolicy.Size = new System.Drawing.Size(197, 22);
            this.txtPolicy.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSave.Location = new System.Drawing.Point(59, 392);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 28);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Location = new System.Drawing.Point(280, 392);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 28);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // PatientEditForm
            // 
            this.ClientSize = new System.Drawing.Size(426, 449);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPolicy);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.cmbGender);
            this.Controls.Add(this.dtpBirthDate);
            this.Controls.Add(this.txtMiddleName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "PatientEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}