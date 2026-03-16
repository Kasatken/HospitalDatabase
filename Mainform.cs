using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace PolyclinicApp
{
    public partial class MainForm : Form
    {
        private string currentUser;
        private TabControl tabControl;
        private TabPage tabPatients;
        private TabPage tabMedCard;
        private TabPage tabReferences;
        private Panel panel1;
        private Button btnSearchPatient;
        private TextBox txtSearchPatient;
        private TabPage tabReports;
        private DataGridView dgvPatients;
        private Button btnDeletePatient;
        private Button btnEditPatient;
        private Button btnAddPatient;
        private TabPage tabVisits;
        private DataGridView dgvVisits;
        private Panel panel2;
        private Button btnDeleteVisit;
        private Button btnEditVisit;
        private Button btnAddVisit;
        private Button btnFilterVisits;
        private DateTimePicker dtpVisitsTo;
        private Label label2;
        private DateTimePicker dtpVisitsFrom;
        private Label label1;
        private Panel panel3;
        private Label lblMedCardInfo;
        private Button btnShowMedCard;
        private ComboBox cmbMedCardPatient;
        private Label label3;
        private DataGridView dgvMedCardHistory;
        private DataGridView dgvMedCardPrescriptions;
        private DataGridView dgvDoctors;
        private DataGridView dgvDiagnoses;
        private Panel panel4;
        private Label lblReportTotal;
        private Button btnReportDoctors;
        private Button btnReportDisease;
        private Label label5;
        private DateTimePicker dtpReportTo;
        private DateTimePicker dtpReportFrom;
        private Label label4;
        private DataGridView dgvReport;
        private string currentRole;

        public MainForm(string username, string role)
        {
            InitializeComponent();
            currentUser = username;
            currentRole = role;

            this.Text = "Электронная мед. книжка — " + username + " (" + GetRoleName(role) + ")";

            if (role != "admin")
            {
                tabControl.TabPages.Remove(tabReferences);
            }
        }

        private string GetRoleName(string role)
        {
            if (role == "admin") return "Администратор";
            if (role == "doctor") return "Врач";
            if (role == "registrar") return "Регистратор";
            return role;
        }

        // При загрузке формы
        private void MainForm_Load(object sender, EventArgs e)
        {
            dtpVisitsFrom.Value = DateTime.Now.AddMonths(-3);
            dtpReportFrom.Value = DateTime.Now.AddMonths(-6);

            LoadPatients();
            LoadVisits();
            LoadMedCardPatients();
            LoadDoctorsList();
            LoadDiagnosesList();
        }

        // ============================================================
        // ПАЦИЕНТЫ
        // ============================================================

        private void LoadPatients()
        {
            string sql = @"SELECT patient_id, last_name, first_name, middle_name,
                                  birth_date, gender, phone, address, policy_number
                           FROM patients ORDER BY last_name";

            dgvPatients.DataSource = DbHelper.ExecuteQuery(sql);
            dgvPatients.Columns["patient_id"].Visible = false;
            dgvPatients.Columns["last_name"].HeaderText = "Фамилия";
            dgvPatients.Columns["first_name"].HeaderText = "Имя";
            dgvPatients.Columns["middle_name"].HeaderText = "Отчество";
            dgvPatients.Columns["birth_date"].HeaderText = "Дата рождения";
            dgvPatients.Columns["gender"].HeaderText = "Пол";
            dgvPatients.Columns["phone"].HeaderText = "Телефон";
            dgvPatients.Columns["address"].HeaderText = "Адрес";
            dgvPatients.Columns["policy_number"].HeaderText = "Полис";
        }

        private void btnSearchPatient_Click(object sender, EventArgs e)
        {
            string search = txtSearchPatient.Text.Trim();
            if (string.IsNullOrEmpty(search))
            {
                LoadPatients();
                return;
            }

            string sql = @"SELECT patient_id, last_name, first_name, middle_name,
                                  birth_date, gender, phone, address, policy_number
                           FROM patients
                           WHERE last_name ILIKE @s OR first_name ILIKE @s OR policy_number ILIKE @s
                           ORDER BY last_name";

            dgvPatients.DataSource = DbHelper.ExecuteQuery(sql,
                new NpgsqlParameter("@s", "%" + search + "%"));
            dgvPatients.Columns["patient_id"].Visible = false;
        }

        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            var form = new PatientEditForm(null);
            if (form.ShowDialog() == DialogResult.OK)
                LoadPatients();
        }

        private void btnEditPatient_Click(object sender, EventArgs e)
        {
            if (dgvPatients.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvPatients.CurrentRow.Cells["patient_id"].Value);
            var form = new PatientEditForm(id);
            if (form.ShowDialog() == DialogResult.OK)
                LoadPatients();
        }

        private void btnDeletePatient_Click(object sender, EventArgs e)
        {
            if (dgvPatients.CurrentRow == null) return;

            if (MessageBox.Show("Удалить пациента?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dgvPatients.CurrentRow.Cells["patient_id"].Value);
                try
                {
                    DbHelper.ExecuteNonQuery("DELETE FROM patients WHERE patient_id = @id",
                        new NpgsqlParameter("@id", id));
                    LoadPatients();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Нельзя удалить: у пациента есть приёмы.\n" + ex.Message,
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ============================================================
        // ПРИЁМЫ
        // ============================================================

        private void LoadVisits()
        {
            string sql = @"SELECT v.visit_id, v.visit_date,
                                  p.last_name || ' ' || p.first_name AS patient_name,
                                  d.last_name || ' ' || d.first_name AS doctor_name,
                                  s.specialty_name,
                                  diag.icd_code,
                                  diag.diagnosis_name,
                                  v.complaints,
                                  v.treatment
                           FROM visits v
                           JOIN patients p ON v.patient_id = p.patient_id
                           JOIN doctors d ON v.doctor_id = d.doctor_id
                           JOIN specialties s ON d.specialty_id = s.specialty_id
                           LEFT JOIN diagnoses diag ON v.diagnosis_id = diag.diagnosis_id
                           WHERE v.visit_date BETWEEN @from AND @to
                           ORDER BY v.visit_date DESC";

            dgvVisits.DataSource = DbHelper.ExecuteQuery(sql,
                new NpgsqlParameter("@from", dtpVisitsFrom.Value.Date),
                new NpgsqlParameter("@to", dtpVisitsTo.Value.Date));

            dgvVisits.Columns["visit_id"].Visible = false;
            dgvVisits.Columns["visit_date"].HeaderText = "Дата";
            dgvVisits.Columns["patient_name"].HeaderText = "Пациент";
            dgvVisits.Columns["doctor_name"].HeaderText = "Врач";
            dgvVisits.Columns["specialty_name"].HeaderText = "Специальность";
            dgvVisits.Columns["icd_code"].HeaderText = "МКБ-10";
            dgvVisits.Columns["diagnosis_name"].HeaderText = "Диагноз";
            dgvVisits.Columns["complaints"].HeaderText = "Жалобы";
            dgvVisits.Columns["treatment"].HeaderText = "Лечение";
        }

        private void btnFilterVisits_Click(object sender, EventArgs e)
        {
            LoadVisits();
        }

        private void btnAddVisit_Click(object sender, EventArgs e)
        {
            var form = new VisitEditForm(null);
            if (form.ShowDialog() == DialogResult.OK)
                LoadVisits();
        }

        private void btnEditVisit_Click(object sender, EventArgs e)
        {
            if (dgvVisits.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvVisits.CurrentRow.Cells["visit_id"].Value);
            var form = new VisitEditForm(id);
            if (form.ShowDialog() == DialogResult.OK)
                LoadVisits();
        }

        private void btnDeleteVisit_Click(object sender, EventArgs e)
        {
            if (dgvVisits.CurrentRow == null) return;

            if (MessageBox.Show("Удалить приём?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dgvVisits.CurrentRow.Cells["visit_id"].Value);
                DbHelper.ExecuteNonQuery("DELETE FROM visits WHERE visit_id = @id",
                    new NpgsqlParameter("@id", id));
                LoadVisits();
            }
        }

        // ============================================================
        // МЕДИЦИНСКАЯ КАРТА
        // ============================================================

        private void LoadMedCardPatients()
        {
            var patients = DbHelper.ExecuteQuery(
                @"SELECT patient_id,
                         last_name || ' ' || first_name || ' ' || COALESCE(middle_name, '') AS full_name
                  FROM patients ORDER BY last_name");

            cmbMedCardPatient.DisplayMember = "full_name";
            cmbMedCardPatient.ValueMember = "patient_id";
            cmbMedCardPatient.DataSource = patients;
        }

        private void btnShowMedCard_Click(object sender, EventArgs e)
        {
            if (cmbMedCardPatient.SelectedValue == null) return;
            int patientId = Convert.ToInt32(cmbMedCardPatient.SelectedValue);

            var info = DbHelper.ExecuteQuery(
                @"SELECT last_name || ' ' || first_name || ' ' || COALESCE(middle_name,'') AS name,
                         birth_date, gender, phone, policy_number
                  FROM patients WHERE patient_id = @id",
                new NpgsqlParameter("@id", patientId));

            if (info.Rows.Count > 0)
            {
                var row = info.Rows[0];
                lblMedCardInfo.Text = row["name"] + " | Д.р.: " +
                    Convert.ToDateTime(row["birth_date"]).ToString("dd.MM.yyyy") +
                    " | Пол: " + row["gender"] +
                    " | Тел: " + row["phone"] +
                    " | Полис: " + row["policy_number"];
            }

            string sql = @"SELECT v.visit_id, v.visit_date,
                                  d.last_name || ' ' || d.first_name AS doctor_name,
                                  s.specialty_name,
                                  diag.icd_code, diag.diagnosis_name,
                                  v.complaints, v.treatment, v.notes
                           FROM visits v
                           JOIN doctors d ON v.doctor_id = d.doctor_id
                           JOIN specialties s ON d.specialty_id = s.specialty_id
                           LEFT JOIN diagnoses diag ON v.diagnosis_id = diag.diagnosis_id
                           WHERE v.patient_id = @pid
                           ORDER BY v.visit_date DESC";

            dgvMedCardHistory.DataSource = DbHelper.ExecuteQuery(sql,
                new NpgsqlParameter("@pid", patientId));

            dgvMedCardHistory.Columns["visit_id"].Visible = false;
            dgvMedCardHistory.Columns["visit_date"].HeaderText = "Дата";
            dgvMedCardHistory.Columns["doctor_name"].HeaderText = "Врач";
            dgvMedCardHistory.Columns["specialty_name"].HeaderText = "Специальность";
            dgvMedCardHistory.Columns["icd_code"].HeaderText = "МКБ-10";
            dgvMedCardHistory.Columns["diagnosis_name"].HeaderText = "Диагноз";
            dgvMedCardHistory.Columns["complaints"].HeaderText = "Жалобы";
            dgvMedCardHistory.Columns["treatment"].HeaderText = "Лечение";
            dgvMedCardHistory.Columns["notes"].HeaderText = "Примечание";

            dgvMedCardPrescriptions.DataSource = null;
        }

        private void dgvMedCardHistory_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMedCardHistory.CurrentRow == null) return;
            if (!dgvMedCardHistory.Columns.Contains("visit_id")) return;

            object val = dgvMedCardHistory.CurrentRow.Cells["visit_id"].Value;
            if (val == null || val == DBNull.Value) return;

            int visitId = Convert.ToInt32(val);

            string sql = @"SELECT medicine_name, dosage, duration_days, instructions
                           FROM prescriptions WHERE visit_id = @vid";

            dgvMedCardPrescriptions.DataSource = DbHelper.ExecuteQuery(sql,
                new NpgsqlParameter("@vid", visitId));

            if (dgvMedCardPrescriptions.Columns.Count > 0)
            {
                dgvMedCardPrescriptions.Columns["medicine_name"].HeaderText = "Препарат";
                dgvMedCardPrescriptions.Columns["dosage"].HeaderText = "Дозировка";
                dgvMedCardPrescriptions.Columns["duration_days"].HeaderText = "Дней";
                dgvMedCardPrescriptions.Columns["instructions"].HeaderText = "Указания";
            }
        }

        // ============================================================
        // СПРАВОЧНИКИ
        // ============================================================

        private void LoadDoctorsList()
        {
            string sql = @"SELECT d.doctor_id, d.last_name, d.first_name, d.middle_name,
                                  s.specialty_name, d.phone, d.cabinet
                           FROM doctors d
                           JOIN specialties s ON d.specialty_id = s.specialty_id
                           ORDER BY d.last_name";

            dgvDoctors.DataSource = DbHelper.ExecuteQuery(sql);
            dgvDoctors.Columns["doctor_id"].Visible = false;
            dgvDoctors.Columns["last_name"].HeaderText = "Фамилия";
            dgvDoctors.Columns["first_name"].HeaderText = "Имя";
            dgvDoctors.Columns["middle_name"].HeaderText = "Отчество";
            dgvDoctors.Columns["specialty_name"].HeaderText = "Специальность";
            dgvDoctors.Columns["phone"].HeaderText = "Телефон";
            dgvDoctors.Columns["cabinet"].HeaderText = "Кабинет";
        }

        private void LoadDiagnosesList()
        {
            string sql = @"SELECT diagnosis_id, icd_code, diagnosis_name
                           FROM diagnoses ORDER BY icd_code";

            dgvDiagnoses.DataSource = DbHelper.ExecuteQuery(sql);
            dgvDiagnoses.Columns["diagnosis_id"].Visible = false;
            dgvDiagnoses.Columns["icd_code"].HeaderText = "Код МКБ-10";
            dgvDiagnoses.Columns["diagnosis_name"].HeaderText = "Диагноз";
        }

        // ============================================================
        // ОТЧЁТЫ
        // ============================================================

        private void btnReportDisease_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT diag.icd_code, diag.diagnosis_name,
                                  COUNT(v.visit_id) AS case_count,
                                  COUNT(DISTINCT v.patient_id) AS patient_count
                           FROM visits v
                           JOIN diagnoses diag ON v.diagnosis_id = diag.diagnosis_id
                           WHERE v.visit_date BETWEEN @from AND @to
                           GROUP BY diag.icd_code, diag.diagnosis_name
                           ORDER BY COUNT(v.visit_id) DESC";

            var data = DbHelper.ExecuteQuery(sql,
                new NpgsqlParameter("@from", dtpReportFrom.Value.Date),
                new NpgsqlParameter("@to", dtpReportTo.Value.Date));

            dgvReport.DataSource = data;
            dgvReport.Columns["icd_code"].HeaderText = "Код МКБ-10";
            dgvReport.Columns["diagnosis_name"].HeaderText = "Диагноз";
            dgvReport.Columns["case_count"].HeaderText = "Случаев";
            dgvReport.Columns["patient_count"].HeaderText = "Пациентов";

            int totalCases = 0;
            foreach (DataRow row in data.Rows)
                totalCases += Convert.ToInt32(row["case_count"]);

            lblReportTotal.Text = "Итого случаев: " + totalCases +
                " | Период: " + dtpReportFrom.Value.ToString("dd.MM.yyyy") +
                " — " + dtpReportTo.Value.ToString("dd.MM.yyyy");
        }

        private void btnReportDoctors_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT d.last_name || ' ' || d.first_name AS doctor_name,
                                  s.specialty_name,
                                  COUNT(v.visit_id) AS visit_count,
                                  COUNT(DISTINCT v.patient_id) AS patient_count
                           FROM doctors d
                           JOIN specialties s ON d.specialty_id = s.specialty_id
                           LEFT JOIN visits v ON d.doctor_id = v.doctor_id
                           GROUP BY d.doctor_id, d.last_name, d.first_name, s.specialty_name
                           ORDER BY COUNT(v.visit_id) DESC";

            dgvReport.DataSource = DbHelper.ExecuteQuery(sql);
            dgvReport.Columns["doctor_name"].HeaderText = "Врач";
            dgvReport.Columns["specialty_name"].HeaderText = "Специальность";
            dgvReport.Columns["visit_count"].HeaderText = "Приёмов";
            dgvReport.Columns["patient_count"].HeaderText = "Пациентов";

            lblReportTotal.Text = "Статистика по врачам";
        }

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPatients = new System.Windows.Forms.TabPage();
            this.dgvPatients = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDeletePatient = new System.Windows.Forms.Button();
            this.btnEditPatient = new System.Windows.Forms.Button();
            this.btnAddPatient = new System.Windows.Forms.Button();
            this.btnSearchPatient = new System.Windows.Forms.Button();
            this.txtSearchPatient = new System.Windows.Forms.TextBox();
            this.tabVisits = new System.Windows.Forms.TabPage();
            this.dgvVisits = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDeleteVisit = new System.Windows.Forms.Button();
            this.btnEditVisit = new System.Windows.Forms.Button();
            this.btnAddVisit = new System.Windows.Forms.Button();
            this.btnFilterVisits = new System.Windows.Forms.Button();
            this.dtpVisitsTo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpVisitsFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tabMedCard = new System.Windows.Forms.TabPage();
            this.dgvMedCardHistory = new System.Windows.Forms.DataGridView();
            this.dgvMedCardPrescriptions = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblMedCardInfo = new System.Windows.Forms.Label();
            this.btnShowMedCard = new System.Windows.Forms.Button();
            this.cmbMedCardPatient = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabReferences = new System.Windows.Forms.TabPage();
            this.dgvDoctors = new System.Windows.Forms.DataGridView();
            this.dgvDiagnoses = new System.Windows.Forms.DataGridView();
            this.tabReports = new System.Windows.Forms.TabPage();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblReportTotal = new System.Windows.Forms.Label();
            this.btnReportDoctors = new System.Windows.Forms.Button();
            this.btnReportDisease = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpReportTo = new System.Windows.Forms.DateTimePicker();
            this.dtpReportFrom = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPatients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatients)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabVisits.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisits)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabMedCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedCardHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedCardPrescriptions)).BeginInit();
            this.panel3.SuspendLayout();
            this.tabReferences.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoctors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiagnoses)).BeginInit();
            this.tabReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPatients);
            this.tabControl.Controls.Add(this.tabVisits);
            this.tabControl.Controls.Add(this.tabMedCard);
            this.tabControl.Controls.Add(this.tabReferences);
            this.tabControl.Controls.Add(this.tabReports);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(836, 572);
            this.tabControl.TabIndex = 1;
            // 
            // tabPatients
            // 
            this.tabPatients.Controls.Add(this.dgvPatients);
            this.tabPatients.Controls.Add(this.panel1);
            this.tabPatients.Location = new System.Drawing.Point(4, 25);
            this.tabPatients.Name = "tabPatients";
            this.tabPatients.Padding = new System.Windows.Forms.Padding(3);
            this.tabPatients.Size = new System.Drawing.Size(828, 543);
            this.tabPatients.TabIndex = 5;
            this.tabPatients.Text = "Пациенты";
            this.tabPatients.UseVisualStyleBackColor = true;
            // 
            // dgvPatients
            // 
            this.dgvPatients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPatients.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvPatients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPatients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPatients.Location = new System.Drawing.Point(3, 48);
            this.dgvPatients.Name = "dgvPatients";
            this.dgvPatients.ReadOnly = true;
            this.dgvPatients.RowHeadersWidth = 51;
            this.dgvPatients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPatients.Size = new System.Drawing.Size(822, 492);
            this.dgvPatients.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDeletePatient);
            this.panel1.Controls.Add(this.btnEditPatient);
            this.panel1.Controls.Add(this.btnAddPatient);
            this.panel1.Controls.Add(this.btnSearchPatient);
            this.panel1.Controls.Add(this.txtSearchPatient);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(822, 45);
            this.panel1.TabIndex = 0;
            // 
            // btnDeletePatient
            // 
            this.btnDeletePatient.BackColor = System.Drawing.Color.Red;
            this.btnDeletePatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDeletePatient.ForeColor = System.Drawing.SystemColors.Control;
            this.btnDeletePatient.Location = new System.Drawing.Point(662, 4);
            this.btnDeletePatient.Name = "btnDeletePatient";
            this.btnDeletePatient.Size = new System.Drawing.Size(151, 38);
            this.btnDeletePatient.TabIndex = 4;
            this.btnDeletePatient.Text = "Удалить";
            this.btnDeletePatient.UseVisualStyleBackColor = false;
            this.btnDeletePatient.Click += new System.EventHandler(this.btnDeletePatient_Click);
            // 
            // btnEditPatient
            // 
            this.btnEditPatient.BackColor = System.Drawing.Color.Goldenrod;
            this.btnEditPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnEditPatient.ForeColor = System.Drawing.SystemColors.Control;
            this.btnEditPatient.Location = new System.Drawing.Point(490, 4);
            this.btnEditPatient.Name = "btnEditPatient";
            this.btnEditPatient.Size = new System.Drawing.Size(151, 38);
            this.btnEditPatient.TabIndex = 3;
            this.btnEditPatient.Text = "Редактировать";
            this.btnEditPatient.UseVisualStyleBackColor = false;
            this.btnEditPatient.Click += new System.EventHandler(this.btnEditPatient_Click);
            // 
            // btnAddPatient
            // 
            this.btnAddPatient.BackColor = System.Drawing.Color.DarkGreen;
            this.btnAddPatient.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddPatient.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAddPatient.Location = new System.Drawing.Point(333, 4);
            this.btnAddPatient.Name = "btnAddPatient";
            this.btnAddPatient.Size = new System.Drawing.Size(151, 38);
            this.btnAddPatient.TabIndex = 2;
            this.btnAddPatient.Text = "Добавить";
            this.btnAddPatient.UseVisualStyleBackColor = false;
            this.btnAddPatient.Click += new System.EventHandler(this.btnAddPatient_Click);
            // 
            // btnSearchPatient
            // 
            this.btnSearchPatient.Location = new System.Drawing.Point(183, 8);
            this.btnSearchPatient.Name = "btnSearchPatient";
            this.btnSearchPatient.Size = new System.Drawing.Size(97, 26);
            this.btnSearchPatient.TabIndex = 1;
            this.btnSearchPatient.Text = "Найти";
            this.btnSearchPatient.UseVisualStyleBackColor = true;
            this.btnSearchPatient.Click += new System.EventHandler(this.btnSearchPatient_Click);
            // 
            // txtSearchPatient
            // 
            this.txtSearchPatient.Location = new System.Drawing.Point(14, 10);
            this.txtSearchPatient.Name = "txtSearchPatient";
            this.txtSearchPatient.Size = new System.Drawing.Size(163, 22);
            this.txtSearchPatient.TabIndex = 0;
            // 
            // tabVisits
            // 
            this.tabVisits.Controls.Add(this.dgvVisits);
            this.tabVisits.Controls.Add(this.panel2);
            this.tabVisits.Location = new System.Drawing.Point(4, 25);
            this.tabVisits.Name = "tabVisits";
            this.tabVisits.Padding = new System.Windows.Forms.Padding(3);
            this.tabVisits.Size = new System.Drawing.Size(828, 543);
            this.tabVisits.TabIndex = 9;
            this.tabVisits.Text = "Приёмы";
            this.tabVisits.UseVisualStyleBackColor = true;
            // 
            // dgvVisits
            // 
            this.dgvVisits.AllowUserToAddRows = false;
            this.dgvVisits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVisits.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvVisits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVisits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVisits.Location = new System.Drawing.Point(3, 53);
            this.dgvVisits.Name = "dgvVisits";
            this.dgvVisits.ReadOnly = true;
            this.dgvVisits.RowHeadersWidth = 51;
            this.dgvVisits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVisits.Size = new System.Drawing.Size(822, 487);
            this.dgvVisits.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDeleteVisit);
            this.panel2.Controls.Add(this.btnEditVisit);
            this.panel2.Controls.Add(this.btnAddVisit);
            this.panel2.Controls.Add(this.btnFilterVisits);
            this.panel2.Controls.Add(this.dtpVisitsTo);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.dtpVisitsFrom);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(822, 50);
            this.panel2.TabIndex = 0;
            // 
            // btnDeleteVisit
            // 
            this.btnDeleteVisit.Location = new System.Drawing.Point(953, 12);
            this.btnDeleteVisit.Name = "btnDeleteVisit";
            this.btnDeleteVisit.Size = new System.Drawing.Size(90, 26);
            this.btnDeleteVisit.TabIndex = 0;
            this.btnDeleteVisit.Text = "Удалить";
            this.btnDeleteVisit.UseVisualStyleBackColor = true;
            this.btnDeleteVisit.Click += new System.EventHandler(this.btnDeleteVisit_Click);
            // 
            // btnEditVisit
            // 
            this.btnEditVisit.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnEditVisit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnEditVisit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnEditVisit.Location = new System.Drawing.Point(618, 12);
            this.btnEditVisit.Name = "btnEditVisit";
            this.btnEditVisit.Size = new System.Drawing.Size(143, 32);
            this.btnEditVisit.TabIndex = 1;
            this.btnEditVisit.Text = "Редактировать";
            this.btnEditVisit.UseVisualStyleBackColor = false;
            this.btnEditVisit.Click += new System.EventHandler(this.btnEditVisit_Click);
            // 
            // btnAddVisit
            // 
            this.btnAddVisit.BackColor = System.Drawing.Color.Green;
            this.btnAddVisit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddVisit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAddVisit.Location = new System.Drawing.Point(485, 12);
            this.btnAddVisit.Name = "btnAddVisit";
            this.btnAddVisit.Size = new System.Drawing.Size(127, 32);
            this.btnAddVisit.TabIndex = 2;
            this.btnAddVisit.Text = "Новый приём";
            this.btnAddVisit.UseVisualStyleBackColor = false;
            this.btnAddVisit.Click += new System.EventHandler(this.btnAddVisit_Click);
            // 
            // btnFilterVisits
            // 
            this.btnFilterVisits.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnFilterVisits.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnFilterVisits.ForeColor = System.Drawing.SystemColors.Control;
            this.btnFilterVisits.Location = new System.Drawing.Point(340, 12);
            this.btnFilterVisits.Name = "btnFilterVisits";
            this.btnFilterVisits.Size = new System.Drawing.Size(109, 32);
            this.btnFilterVisits.TabIndex = 3;
            this.btnFilterVisits.Text = "Фильтр";
            this.btnFilterVisits.UseVisualStyleBackColor = false;
            this.btnFilterVisits.Click += new System.EventHandler(this.btnFilterVisits_Click);
            // 
            // dtpVisitsTo
            // 
            this.dtpVisitsTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVisitsTo.Location = new System.Drawing.Point(200, 14);
            this.dtpVisitsTo.Name = "dtpVisitsTo";
            this.dtpVisitsTo.Size = new System.Drawing.Size(120, 22);
            this.dtpVisitsTo.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(170, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "По:";
            // 
            // dtpVisitsFrom
            // 
            this.dtpVisitsFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVisitsFrom.Location = new System.Drawing.Point(42, 14);
            this.dtpVisitsFrom.Name = "dtpVisitsFrom";
            this.dtpVisitsFrom.Size = new System.Drawing.Size(120, 22);
            this.dtpVisitsFrom.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "С:";
            // 
            // tabMedCard
            // 
            this.tabMedCard.Controls.Add(this.dgvMedCardHistory);
            this.tabMedCard.Controls.Add(this.dgvMedCardPrescriptions);
            this.tabMedCard.Controls.Add(this.panel3);
            this.tabMedCard.Location = new System.Drawing.Point(4, 25);
            this.tabMedCard.Name = "tabMedCard";
            this.tabMedCard.Padding = new System.Windows.Forms.Padding(3);
            this.tabMedCard.Size = new System.Drawing.Size(828, 543);
            this.tabMedCard.TabIndex = 6;
            this.tabMedCard.Text = "Мед. карта";
            this.tabMedCard.UseVisualStyleBackColor = true;
            // 
            // dgvMedCardHistory
            // 
            this.dgvMedCardHistory.AllowUserToAddRows = false;
            this.dgvMedCardHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMedCardHistory.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvMedCardHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedCardHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMedCardHistory.Location = new System.Drawing.Point(3, 73);
            this.dgvMedCardHistory.Name = "dgvMedCardHistory";
            this.dgvMedCardHistory.ReadOnly = true;
            this.dgvMedCardHistory.RowHeadersWidth = 51;
            this.dgvMedCardHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMedCardHistory.Size = new System.Drawing.Size(822, 367);
            this.dgvMedCardHistory.TabIndex = 2;
            this.dgvMedCardHistory.SelectionChanged += new System.EventHandler(this.dgvMedCardHistory_SelectionChanged);
            // 
            // dgvMedCardPrescriptions
            // 
            this.dgvMedCardPrescriptions.AllowUserToAddRows = false;
            this.dgvMedCardPrescriptions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMedCardPrescriptions.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvMedCardPrescriptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedCardPrescriptions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvMedCardPrescriptions.Location = new System.Drawing.Point(3, 440);
            this.dgvMedCardPrescriptions.Name = "dgvMedCardPrescriptions";
            this.dgvMedCardPrescriptions.ReadOnly = true;
            this.dgvMedCardPrescriptions.RowHeadersWidth = 51;
            this.dgvMedCardPrescriptions.Size = new System.Drawing.Size(822, 100);
            this.dgvMedCardPrescriptions.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblMedCardInfo);
            this.panel3.Controls.Add(this.btnShowMedCard);
            this.panel3.Controls.Add(this.cmbMedCardPatient);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(822, 70);
            this.panel3.TabIndex = 0;
            // 
            // lblMedCardInfo
            // 
            this.lblMedCardInfo.AutoSize = true;
            this.lblMedCardInfo.Location = new System.Drawing.Point(5, 48);
            this.lblMedCardInfo.Name = "lblMedCardInfo";
            this.lblMedCardInfo.Size = new System.Drawing.Size(0, 16);
            this.lblMedCardInfo.TabIndex = 0;
            // 
            // btnShowMedCard
            // 
            this.btnShowMedCard.BackColor = System.Drawing.Color.Green;
            this.btnShowMedCard.ForeColor = System.Drawing.SystemColors.Control;
            this.btnShowMedCard.Location = new System.Drawing.Point(498, 9);
            this.btnShowMedCard.Name = "btnShowMedCard";
            this.btnShowMedCard.Size = new System.Drawing.Size(167, 39);
            this.btnShowMedCard.TabIndex = 1;
            this.btnShowMedCard.Text = "Показать карту";
            this.btnShowMedCard.UseVisualStyleBackColor = false;
            this.btnShowMedCard.Click += new System.EventHandler(this.btnShowMedCard_Click);
            // 
            // cmbMedCardPatient
            // 
            this.cmbMedCardPatient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMedCardPatient.FormattingEnabled = true;
            this.cmbMedCardPatient.Location = new System.Drawing.Point(75, 17);
            this.cmbMedCardPatient.Name = "cmbMedCardPatient";
            this.cmbMedCardPatient.Size = new System.Drawing.Size(350, 24);
            this.cmbMedCardPatient.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Пациент:";
            // 
            // tabReferences
            // 
            this.tabReferences.Controls.Add(this.dgvDoctors);
            this.tabReferences.Controls.Add(this.dgvDiagnoses);
            this.tabReferences.Location = new System.Drawing.Point(4, 25);
            this.tabReferences.Name = "tabReferences";
            this.tabReferences.Padding = new System.Windows.Forms.Padding(3);
            this.tabReferences.Size = new System.Drawing.Size(828, 543);
            this.tabReferences.TabIndex = 7;
            this.tabReferences.Text = "Справочники";
            this.tabReferences.UseVisualStyleBackColor = true;
            // 
            // dgvDoctors
            // 
            this.dgvDoctors.AllowUserToAddRows = false;
            this.dgvDoctors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDoctors.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDoctors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoctors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDoctors.Location = new System.Drawing.Point(3, 3);
            this.dgvDoctors.Name = "dgvDoctors";
            this.dgvDoctors.ReadOnly = true;
            this.dgvDoctors.RowHeadersWidth = 51;
            this.dgvDoctors.Size = new System.Drawing.Size(822, 387);
            this.dgvDoctors.TabIndex = 0;
            // 
            // dgvDiagnoses
            // 
            this.dgvDiagnoses.AllowUserToAddRows = false;
            this.dgvDiagnoses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDiagnoses.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDiagnoses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDiagnoses.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvDiagnoses.Location = new System.Drawing.Point(3, 390);
            this.dgvDiagnoses.Name = "dgvDiagnoses";
            this.dgvDiagnoses.ReadOnly = true;
            this.dgvDiagnoses.RowHeadersWidth = 51;
            this.dgvDiagnoses.Size = new System.Drawing.Size(822, 150);
            this.dgvDiagnoses.TabIndex = 1;
            // 
            // tabReports
            // 
            this.tabReports.Controls.Add(this.dgvReport);
            this.tabReports.Controls.Add(this.panel4);
            this.tabReports.Location = new System.Drawing.Point(4, 25);
            this.tabReports.Name = "tabReports";
            this.tabReports.Padding = new System.Windows.Forms.Padding(3);
            this.tabReports.Size = new System.Drawing.Size(828, 543);
            this.tabReports.TabIndex = 8;
            this.tabReports.Text = "Отчёты";
            this.tabReports.UseVisualStyleBackColor = true;
            // 
            // dgvReport
            // 
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReport.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReport.Location = new System.Drawing.Point(3, 53);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersWidth = 51;
            this.dgvReport.Size = new System.Drawing.Size(822, 487);
            this.dgvReport.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblReportTotal);
            this.panel4.Controls.Add(this.btnReportDoctors);
            this.panel4.Controls.Add(this.btnReportDisease);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.dtpReportTo);
            this.panel4.Controls.Add(this.dtpReportFrom);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(822, 50);
            this.panel4.TabIndex = 2;
            // 
            // lblReportTotal
            // 
            this.lblReportTotal.AutoSize = true;
            this.lblReportTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblReportTotal.Location = new System.Drawing.Point(650, 15);
            this.lblReportTotal.Name = "lblReportTotal";
            this.lblReportTotal.Size = new System.Drawing.Size(0, 17);
            this.lblReportTotal.TabIndex = 0;
            // 
            // btnReportDoctors
            // 
            this.btnReportDoctors.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnReportDoctors.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnReportDoctors.ForeColor = System.Drawing.SystemColors.Control;
            this.btnReportDoctors.Location = new System.Drawing.Point(490, 10);
            this.btnReportDoctors.Name = "btnReportDoctors";
            this.btnReportDoctors.Size = new System.Drawing.Size(143, 34);
            this.btnReportDoctors.TabIndex = 1;
            this.btnReportDoctors.Text = "По врачам";
            this.btnReportDoctors.UseVisualStyleBackColor = false;
            this.btnReportDoctors.Click += new System.EventHandler(this.btnReportDoctors_Click);
            // 
            // btnReportDisease
            // 
            this.btnReportDisease.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnReportDisease.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnReportDisease.ForeColor = System.Drawing.SystemColors.Control;
            this.btnReportDisease.Location = new System.Drawing.Point(340, 10);
            this.btnReportDisease.Name = "btnReportDisease";
            this.btnReportDisease.Size = new System.Drawing.Size(144, 34);
            this.btnReportDisease.TabIndex = 2;
            this.btnReportDisease.Text = "Заболеваемость";
            this.btnReportDisease.UseVisualStyleBackColor = false;
            this.btnReportDisease.Click += new System.EventHandler(this.btnReportDisease_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(160, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "По:";
            // 
            // dtpReportTo
            // 
            this.dtpReportTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReportTo.Location = new System.Drawing.Point(190, 12);
            this.dtpReportTo.Name = "dtpReportTo";
            this.dtpReportTo.Size = new System.Drawing.Size(120, 22);
            this.dtpReportTo.TabIndex = 4;
            // 
            // dtpReportFrom
            // 
            this.dtpReportFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReportFrom.Location = new System.Drawing.Point(31, 12);
            this.dtpReportFrom.Name = "dtpReportFrom";
            this.dtpReportFrom.Size = new System.Drawing.Size(120, 22);
            this.dtpReportFrom.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "С:";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(836, 572);
            this.Controls.Add(this.tabControl);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Электронная мед. книжка";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPatients.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatients)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabVisits.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisits)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabMedCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedCardHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedCardPrescriptions)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabReferences.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoctors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiagnoses)).EndInit();
            this.tabReports.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}