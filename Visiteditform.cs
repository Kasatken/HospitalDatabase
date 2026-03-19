using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace PolyclinicApp
{
    // Форма добавления / редактирования приёма
    public partial class VisitEditForm : Form
    {
        // null = новый приём, число = редактирование существующего
        private readonly int? visitId;

        public VisitEditForm(int? id)
        {
            InitializeComponent();
            visitId = id;
            this.Text = id.HasValue ? "Редактирование приёма" : "Новый приём";

            LoadComboBoxes();

            if (visitId.HasValue)
                LoadVisitData();
        }

        // Заполняем выпадающие списки пациентов, врачей и диагнозов
        private void LoadComboBoxes()
        {
            var patients = DbHelper.ExecuteQuery(@"
                SELECT patient_id,
                       last_name || ' ' || first_name || ' ' || COALESCE(middle_name,'') AS full_name
                FROM patients ORDER BY last_name");
            cmbPatient.DataSource = null;          
            cmbPatient.DisplayMember = "full_name";
            cmbPatient.ValueMember = "patient_id";
            cmbPatient.DataSource = patients;

            var doctors = DbHelper.ExecuteQuery(@"
                SELECT d.doctor_id,
                       d.last_name || ' ' || d.first_name || ' (' || s.specialty_name || ')' AS full_name
                FROM doctors d
                JOIN specialties s ON d.specialty_id = s.specialty_id
                ORDER BY d.last_name");
            cmbPatient.DataSource = null;
            cmbDoctor.DisplayMember = "full_name";
            cmbDoctor.ValueMember = "doctor_id";
            cmbDoctor.DataSource = doctors;

            var diagnoses = DbHelper.ExecuteQuery(
                "SELECT diagnosis_id, icd_code || ' - ' || diagnosis_name AS diag_name FROM diagnoses ORDER BY icd_code");

            // Добавляем пустую строку в начало — диагноз необязателен
            var emptyRow = diagnoses.NewRow();
            emptyRow["diagnosis_id"] = DBNull.Value;
            emptyRow["diag_name"] = "(не указан)";
            diagnoses.Rows.InsertAt(emptyRow, 0);
 
            cmbPatient.DataSource = null;
            cmbDiagnosis.DisplayMember = "diag_name";
            cmbDiagnosis.ValueMember = "diagnosis_id";
            cmbDiagnosis.DataSource = diagnoses;
        }

        // Загружаем данные существующего приёма в поля формы
        private void LoadVisitData()
        {
            var table = DbHelper.ExecuteQuery(
                "SELECT * FROM visits WHERE visit_id = @id",
                new NpgsqlParameter("@id", visitId.Value));

            if (table.Rows.Count == 0) return;
            var row = table.Rows[0];

            cmbPatient.SelectedValue = row["patient_id"];
            cmbDoctor.SelectedValue = row["doctor_id"];
            dtpVisitDate.Value = Convert.ToDateTime(row["visit_date"]);
            txtComplaints.Text = row["complaints"].ToString();
            txtTreatment.Text = row["treatment"].ToString();
            txtNotes.Text = row["notes"].ToString();

            if (row["diagnosis_id"] != DBNull.Value)
                cmbDiagnosis.SelectedValue = row["diagnosis_id"];

            // Загружаем назначения (рецепты) для этого приёма
            var prescriptions = DbHelper.ExecuteQuery(
                "SELECT medicine_name, dosage, duration_days, instructions FROM prescriptions WHERE visit_id = @id",
                new NpgsqlParameter("@id", visitId.Value));

            dgvPrescriptions.Rows.Clear();
            foreach (DataRow r in prescriptions.Rows)
            {
                dgvPrescriptions.Rows.Add(
                    r["medicine_name"],
                    r["dosage"],
                    r["duration_days"] == DBNull.Value ? "" : r["duration_days"].ToString(),
                    r["instructions"]);
            }
        }

        // Кнопка "+" — добавить пустую строку в таблицу назначений
        private void btnAddPrescription_Click(object sender, EventArgs e)
        {
            dgvPrescriptions.Rows.Add("", "", "", "");
        }

        // Кнопка "-" — удалить выбранную строку из таблицы назначений
        private void btnRemovePrescription_Click(object sender, EventArgs e)
        {
            if (dgvPrescriptions.CurrentRow != null && !dgvPrescriptions.CurrentRow.IsNewRow)
                dgvPrescriptions.Rows.Remove(dgvPrescriptions.CurrentRow);
        }

        // Кнопка "Сохранить"
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbPatient.SelectedValue == null || cmbDoctor.SelectedValue == null)
            {
                MessageBox.Show("Выберите пациента и врача.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int savedVisitId = SaveVisit();
                SavePrescriptions(savedVisitId);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения:\n" + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // INSERT или UPDATE приёма, возвращает visit_id
        private int SaveVisit()
        {
            object diagnosisId = DBNull.Value;
            if (cmbDiagnosis.SelectedValue != null && cmbDiagnosis.SelectedValue != DBNull.Value)
                diagnosisId = cmbDiagnosis.SelectedValue;

            object TextOrNull(string s) =>
                string.IsNullOrEmpty(s) ? (object)DBNull.Value : s;

            if (visitId.HasValue)
            {
                DbHelper.ExecuteNonQuery(@"
                    UPDATE visits SET
                        patient_id   = @pid,
                        doctor_id    = @did,
                        visit_date   = @vd,
                        diagnosis_id = @diag,
                        complaints   = @c,
                        treatment    = @t,
                        notes        = @n
                    WHERE visit_id = @id",
                    new NpgsqlParameter("@pid", cmbPatient.SelectedValue),
                    new NpgsqlParameter("@did", cmbDoctor.SelectedValue),
                    new NpgsqlParameter("@vd", dtpVisitDate.Value.Date),
                    new NpgsqlParameter("@diag", diagnosisId),
                    new NpgsqlParameter("@c", TextOrNull(txtComplaints.Text)),
                    new NpgsqlParameter("@t", TextOrNull(txtTreatment.Text)),
                    new NpgsqlParameter("@n", TextOrNull(txtNotes.Text)),
                    new NpgsqlParameter("@id", visitId.Value));

                return visitId.Value;
            }
            else
            {
                return Convert.ToInt32(DbHelper.ExecuteScalar(@"
                    INSERT INTO visits
                        (patient_id, doctor_id, visit_date, diagnosis_id, complaints, treatment, notes)
                    VALUES
                        (@pid, @did, @vd, @diag, @c, @t, @n)
                    RETURNING visit_id",
                    new NpgsqlParameter("@pid", cmbPatient.SelectedValue),
                    new NpgsqlParameter("@did", cmbDoctor.SelectedValue),
                    new NpgsqlParameter("@vd", dtpVisitDate.Value.Date),
                    new NpgsqlParameter("@diag", diagnosisId),
                    new NpgsqlParameter("@c", TextOrNull(txtComplaints.Text)),
                    new NpgsqlParameter("@t", TextOrNull(txtTreatment.Text)),
                    new NpgsqlParameter("@n", TextOrNull(txtNotes.Text))));
            }
        }

        // Сохраняем назначения: сначала удаляем старые, потом записываем новые
        private void SavePrescriptions(int savedVisitId)
        {
            DbHelper.ExecuteNonQuery("DELETE FROM prescriptions WHERE visit_id = @id",
                new NpgsqlParameter("@id", savedVisitId));

            foreach (DataGridViewRow row in dgvPrescriptions.Rows)
            {
                if (row.IsNewRow) continue;

                string medName = row.Cells["colMedicine"].Value?.ToString()?.Trim();
                if (string.IsNullOrEmpty(medName)) continue;

                object duration = DBNull.Value;
                if (int.TryParse(row.Cells["colDays"].Value?.ToString(), out int days))
                    duration = days;

                DbHelper.ExecuteNonQuery(@"
                    INSERT INTO prescriptions
                        (visit_id, medicine_name, dosage, duration_days, instructions)
                    VALUES
                        (@vid, @med, @dos, @dur, @inst)",
                    new NpgsqlParameter("@vid", savedVisitId),
                    new NpgsqlParameter("@med", medName),
                    new NpgsqlParameter("@dos", (object)row.Cells["colDosage"].Value?.ToString() ?? DBNull.Value),
                    new NpgsqlParameter("@dur", duration),
                    new NpgsqlParameter("@inst", (object)row.Cells["colInstructions"].Value?.ToString() ?? DBNull.Value));
            }
        }

        // Кнопка "Отмена"
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}