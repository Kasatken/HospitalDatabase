using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace PolyclinicApp
{
    public partial class VisitEditForm : Form
    {
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private ComboBox cmbPatient;
        private ComboBox cmbDoctor;
        private DateTimePicker dtpVisitDate;
        private ComboBox cmbDiagnosis;
        private TextBox txtComplaints;
        private TextBox txtTreatment;
        private TextBox txtNotes;
        private Button btnAddPrescription;
        private Button btnRemovePrescription;
        private DataGridView dgvPrescriptions;
        private DataGridViewTextBoxColumn colMedicine;
        private DataGridViewTextBoxColumn colDosage;
        private DataGridViewTextBoxColumn colDays;
        private DataGridViewTextBoxColumn colInstructions;
        private Button btnSave;
        private Button btnCancel;
        private int? visitId;

        public VisitEditForm(int? id)
        {
            InitializeComponent();
            visitId = id;
            this.Text = id.HasValue ? "Редактирование приёма" : "Новый приём";

            LoadComboBoxes();

            if (visitId.HasValue)
                LoadVisitData();
        }

        private void LoadComboBoxes()
        {
            var patients = DbHelper.ExecuteQuery(
                @"SELECT patient_id,
                         last_name || ' ' || first_name || ' ' || COALESCE(middle_name,'') AS full_name
                  FROM patients ORDER BY last_name");
            cmbPatient.DisplayMember = "full_name";
            cmbPatient.ValueMember = "patient_id";
            cmbPatient.DataSource = patients;

            var doctors = DbHelper.ExecuteQuery(
                @"SELECT d.doctor_id,
                         d.last_name || ' ' || d.first_name || ' (' || s.specialty_name || ')' AS full_name
                  FROM doctors d
                  JOIN specialties s ON d.specialty_id = s.specialty_id
                  ORDER BY d.last_name");
            cmbDoctor.DisplayMember = "full_name";
            cmbDoctor.ValueMember = "doctor_id";
            cmbDoctor.DataSource = doctors;

            var diagnoses = DbHelper.ExecuteQuery(
                "SELECT diagnosis_id, icd_code || ' - ' || diagnosis_name AS diag_name FROM diagnoses ORDER BY icd_code");
            var emptyRow = diagnoses.NewRow();
            emptyRow["diagnosis_id"] = DBNull.Value;
            emptyRow["diag_name"] = "(не указан)";
            diagnoses.Rows.InsertAt(emptyRow, 0);
            cmbDiagnosis.DisplayMember = "diag_name";
            cmbDiagnosis.ValueMember = "diagnosis_id";
            cmbDiagnosis.DataSource = diagnoses;
        }

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

            if (row["diagnosis_id"] != DBNull.Value)
                cmbDiagnosis.SelectedValue = row["diagnosis_id"];

            txtComplaints.Text = row["complaints"].ToString();
            txtTreatment.Text = row["treatment"].ToString();
            txtNotes.Text = row["notes"].ToString();

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

        private void btnAddPrescription_Click(object sender, EventArgs e)
        {
            dgvPrescriptions.Rows.Add("", "", "", "");
        }

        private void btnRemovePrescription_Click(object sender, EventArgs e)
        {
            if (dgvPrescriptions.CurrentRow != null && !dgvPrescriptions.CurrentRow.IsNewRow)
                dgvPrescriptions.Rows.Remove(dgvPrescriptions.CurrentRow);
        }

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
                object diagnosisId = DBNull.Value;
                if (cmbDiagnosis.SelectedValue != null && cmbDiagnosis.SelectedValue != DBNull.Value)
                    diagnosisId = cmbDiagnosis.SelectedValue;

                int savedVisitId;

                if (visitId.HasValue)
                {
                    string sql = @"UPDATE visits SET
                                    patient_id = @pid, doctor_id = @did, visit_date = @vd,
                                    diagnosis_id = @diag, complaints = @c, treatment = @t, notes = @n
                                   WHERE visit_id = @id";

                    DbHelper.ExecuteNonQuery(sql,
                        new NpgsqlParameter("@pid", cmbPatient.SelectedValue),
                        new NpgsqlParameter("@did", cmbDoctor.SelectedValue),
                        new NpgsqlParameter("@vd", dtpVisitDate.Value.Date),
                        new NpgsqlParameter("@diag", diagnosisId),
                        new NpgsqlParameter("@c", (object)txtComplaints.Text ?? DBNull.Value),
                        new NpgsqlParameter("@t", (object)txtTreatment.Text ?? DBNull.Value),
                        new NpgsqlParameter("@n", (object)txtNotes.Text ?? DBNull.Value),
                        new NpgsqlParameter("@id", visitId.Value));

                    savedVisitId = visitId.Value;

                    DbHelper.ExecuteNonQuery("DELETE FROM prescriptions WHERE visit_id = @id",
                        new NpgsqlParameter("@id", savedVisitId));
                }
                else
                {
                    string sql = @"INSERT INTO visits
                                    (patient_id, doctor_id, visit_date, diagnosis_id, complaints, treatment, notes)
                                   VALUES (@pid, @did, @vd, @diag, @c, @t, @n)
                                   RETURNING visit_id";

                    savedVisitId = Convert.ToInt32(DbHelper.ExecuteScalar(sql,
                        new NpgsqlParameter("@pid", cmbPatient.SelectedValue),
                        new NpgsqlParameter("@did", cmbDoctor.SelectedValue),
                        new NpgsqlParameter("@vd", dtpVisitDate.Value.Date),
                        new NpgsqlParameter("@diag", diagnosisId),
                        new NpgsqlParameter("@c", (object)txtComplaints.Text ?? DBNull.Value),
                        new NpgsqlParameter("@t", (object)txtTreatment.Text ?? DBNull.Value),
                        new NpgsqlParameter("@n", (object)txtNotes.Text ?? DBNull.Value)));
                }

                foreach (DataGridViewRow row in dgvPrescriptions.Rows)
                {
                    if (row.IsNewRow) continue;
                    string medName = row.Cells["colMedicine"].Value?.ToString()?.Trim();
                    if (string.IsNullOrEmpty(medName)) continue;

                    string sqlP = @"INSERT INTO prescriptions
                                    (visit_id, medicine_name, dosage, duration_days, instructions)
                                   VALUES (@vid, @med, @dos, @dur, @inst)";

                    object duration = DBNull.Value;
                    if (int.TryParse(row.Cells["colDays"].Value?.ToString(), out int days))
                        duration = days;

                    DbHelper.ExecuteNonQuery(sqlP,
                        new NpgsqlParameter("@vid", savedVisitId),
                        new NpgsqlParameter("@med", medName),
                        new NpgsqlParameter("@dos", (object)row.Cells["colDosage"].Value?.ToString() ?? DBNull.Value),
                        new NpgsqlParameter("@dur", duration),
                        new NpgsqlParameter("@inst", (object)row.Cells["colInstructions"].Value?.ToString() ?? DBNull.Value));
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
            this.cmbPatient = new System.Windows.Forms.ComboBox();
            this.cmbDoctor = new System.Windows.Forms.ComboBox();
            this.dtpVisitDate = new System.Windows.Forms.DateTimePicker();
            this.cmbDiagnosis = new System.Windows.Forms.ComboBox();
            this.txtComplaints = new System.Windows.Forms.TextBox();
            this.txtTreatment = new System.Windows.Forms.TextBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnAddPrescription = new System.Windows.Forms.Button();
            this.btnRemovePrescription = new System.Windows.Forms.Button();
            this.dgvPrescriptions = new System.Windows.Forms.DataGridView();
            this.colMedicine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDosage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInstructions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrescriptions)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "Пациент:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 16);
            this.label2.TabIndex = 18;
            this.label2.Text = "Врач:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(106, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "Дата:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(106, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "Диагноз:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(106, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "Жалобы:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(106, 220);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "Лечение:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(106, 264);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "Примечание:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(32, 290);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 17);
            this.label8.TabIndex = 12;
            this.label8.Text = "Рецепты:";
            // 
            // cmbPatient
            // 
            this.cmbPatient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatient.FormattingEnabled = true;
            this.cmbPatient.Location = new System.Drawing.Point(189, 27);
            this.cmbPatient.Name = "cmbPatient";
            this.cmbPatient.Size = new System.Drawing.Size(400, 24);
            this.cmbPatient.TabIndex = 11;
            // 
            // cmbDoctor
            // 
            this.cmbDoctor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDoctor.FormattingEnabled = true;
            this.cmbDoctor.Location = new System.Drawing.Point(189, 65);
            this.cmbDoctor.Name = "cmbDoctor";
            this.cmbDoctor.Size = new System.Drawing.Size(400, 24);
            this.cmbDoctor.TabIndex = 10;
            // 
            // dtpVisitDate
            // 
            this.dtpVisitDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVisitDate.Location = new System.Drawing.Point(189, 103);
            this.dtpVisitDate.Name = "dtpVisitDate";
            this.dtpVisitDate.Size = new System.Drawing.Size(200, 22);
            this.dtpVisitDate.TabIndex = 9;
            // 
            // cmbDiagnosis
            // 
            this.cmbDiagnosis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiagnosis.FormattingEnabled = true;
            this.cmbDiagnosis.Location = new System.Drawing.Point(189, 138);
            this.cmbDiagnosis.Name = "cmbDiagnosis";
            this.cmbDiagnosis.Size = new System.Drawing.Size(400, 24);
            this.cmbDiagnosis.TabIndex = 8;
            // 
            // txtComplaints
            // 
            this.txtComplaints.Location = new System.Drawing.Point(189, 173);
            this.txtComplaints.Multiline = true;
            this.txtComplaints.Name = "txtComplaints";
            this.txtComplaints.Size = new System.Drawing.Size(400, 38);
            this.txtComplaints.TabIndex = 7;
            // 
            // txtTreatment
            // 
            this.txtTreatment.Location = new System.Drawing.Point(189, 217);
            this.txtTreatment.Multiline = true;
            this.txtTreatment.Name = "txtTreatment";
            this.txtTreatment.Size = new System.Drawing.Size(400, 38);
            this.txtTreatment.TabIndex = 6;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(205, 261);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(384, 22);
            this.txtNotes.TabIndex = 5;
            // 
            // btnAddPrescription
            // 
            this.btnAddPrescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAddPrescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnAddPrescription.Location = new System.Drawing.Point(22, 319);
            this.btnAddPrescription.Name = "btnAddPrescription";
            this.btnAddPrescription.Size = new System.Drawing.Size(37, 25);
            this.btnAddPrescription.TabIndex = 4;
            this.btnAddPrescription.Text = "+";
            this.btnAddPrescription.UseVisualStyleBackColor = false;
            this.btnAddPrescription.Click += new System.EventHandler(this.btnAddPrescription_Click);
            // 
            // btnRemovePrescription
            // 
            this.btnRemovePrescription.BackColor = System.Drawing.Color.Red;
            this.btnRemovePrescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.8F, System.Drawing.FontStyle.Bold);
            this.btnRemovePrescription.Location = new System.Drawing.Point(82, 319);
            this.btnRemovePrescription.Name = "btnRemovePrescription";
            this.btnRemovePrescription.Size = new System.Drawing.Size(37, 25);
            this.btnRemovePrescription.TabIndex = 3;
            this.btnRemovePrescription.Text = "-";
            this.btnRemovePrescription.UseVisualStyleBackColor = false;
            this.btnRemovePrescription.Click += new System.EventHandler(this.btnRemovePrescription_Click);
            // 
            // dgvPrescriptions
            // 
            this.dgvPrescriptions.AllowUserToAddRows = false;
            this.dgvPrescriptions.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvPrescriptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPrescriptions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMedicine,
            this.colDosage,
            this.colDays,
            this.colInstructions});
            this.dgvPrescriptions.Location = new System.Drawing.Point(155, 304);
            this.dgvPrescriptions.Name = "dgvPrescriptions";
            this.dgvPrescriptions.RowHeadersWidth = 30;
            this.dgvPrescriptions.Size = new System.Drawing.Size(493, 140);
            this.dgvPrescriptions.TabIndex = 2;
            // 
            // colMedicine
            // 
            this.colMedicine.HeaderText = "Препарат";
            this.colMedicine.MinimumWidth = 6;
            this.colMedicine.Name = "colMedicine";
            this.colMedicine.Width = 140;
            // 
            // colDosage
            // 
            this.colDosage.HeaderText = "Дозировка";
            this.colDosage.MinimumWidth = 6;
            this.colDosage.Name = "colDosage";
            this.colDosage.Width = 120;
            // 
            // colDays
            // 
            this.colDays.HeaderText = "Дней";
            this.colDays.MinimumWidth = 6;
            this.colDays.Name = "colDays";
            this.colDays.Width = 60;
            // 
            // colInstructions
            // 
            this.colInstructions.HeaderText = "Указания";
            this.colInstructions.MinimumWidth = 6;
            this.colInstructions.Name = "colInstructions";
            this.colInstructions.Width = 140;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSave.Location = new System.Drawing.Point(22, 382);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 28);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Red;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Location = new System.Drawing.Point(22, 416);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 28);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // VisitEditForm
            // 
            this.ClientSize = new System.Drawing.Size(660, 460);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvPrescriptions);
            this.Controls.Add(this.btnRemovePrescription);
            this.Controls.Add(this.btnAddPrescription);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.txtTreatment);
            this.Controls.Add(this.txtComplaints);
            this.Controls.Add(this.cmbDiagnosis);
            this.Controls.Add(this.dtpVisitDate);
            this.Controls.Add(this.cmbDoctor);
            this.Controls.Add(this.cmbPatient);
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
            this.Name = "VisitEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrescriptions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}