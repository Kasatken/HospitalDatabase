using System;
using System.Windows.Forms;
using Npgsql;

namespace PolyclinicApp
{
    public partial class PatientEditForm : Form
    {
        private readonly int? patientId;

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
                object ValueOrNull(string s) =>
                    string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : s.Trim();

                if (patientId.HasValue)
                {
                    DbHelper.ExecuteNonQuery(@"
                        UPDATE patients SET
                            last_name     = @ln,
                            first_name    = @fn,
                            middle_name   = @mn,
                            birth_date    = @bd,
                            gender        = @g,
                            phone         = @ph,
                            address       = @addr,
                            policy_number = @pol
                        WHERE patient_id = @id",
                        new NpgsqlParameter("@ln", txtLastName.Text.Trim()),
                        new NpgsqlParameter("@fn", txtFirstName.Text.Trim()),
                        new NpgsqlParameter("@mn", ValueOrNull(txtMiddleName.Text)),
                        new NpgsqlParameter("@bd", dtpBirthDate.Value.Date),
                        new NpgsqlParameter("@g", cmbGender.SelectedItem?.ToString() ?? "М"),
                        new NpgsqlParameter("@ph", ValueOrNull(txtPhone.Text)),
                        new NpgsqlParameter("@addr", ValueOrNull(txtAddress.Text)),
                        new NpgsqlParameter("@pol", ValueOrNull(txtPolicy.Text)),
                        new NpgsqlParameter("@id", patientId.Value));
                }
                else
                {
                    DbHelper.ExecuteNonQuery(@"
                        INSERT INTO patients
                            (last_name, first_name, middle_name, birth_date, gender, phone, address, policy_number)
                        VALUES
                            (@ln, @fn, @mn, @bd, @g, @ph, @addr, @pol)",
                        new NpgsqlParameter("@ln", txtLastName.Text.Trim()),
                        new NpgsqlParameter("@fn", txtFirstName.Text.Trim()),
                        new NpgsqlParameter("@mn", ValueOrNull(txtMiddleName.Text)),
                        new NpgsqlParameter("@bd", dtpBirthDate.Value.Date),
                        new NpgsqlParameter("@g", cmbGender.SelectedItem?.ToString() ?? "М"),
                        new NpgsqlParameter("@ph", ValueOrNull(txtPhone.Text)),
                        new NpgsqlParameter("@addr", ValueOrNull(txtAddress.Text)),
                        new NpgsqlParameter("@pol", ValueOrNull(txtPolicy.Text)));
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
    }
}