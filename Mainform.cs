using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace PolyclinicApp
{
    public partial class MainForm : Form
    {
        private readonly string currentUser;
        private readonly string currentRole;
        private int? currentPatientId = null;

        public MainForm(string username, string role)
        {
            InitializeComponent();
            currentUser = username;
            currentRole = role;
            this.Text = $"Электронная мед. книжка — {username} ({GetRoleName(role)})";
            ApplyRoleRestrictions();
        }

        private static string GetRoleName(string role)
        {
            switch (role)
            {
                case "admin": return "Администратор";
                case "doctor": return "Врач";
                case "registrar": return "Пациент";
                default: return role;
            }
        }

        private void ApplyRoleRestrictions()
        {
            switch (currentRole)
            {
                case "admin":
                    break;

                case "doctor":
                    tabControl.TabPages.Remove(tabReferences);
                    tabControl.TabPages.Remove(tabReports);
                    tabControl.TabPages.Remove(tabUsers);
                    break;

                case "registrar":
                    tabControl.TabPages.Remove(tabPatients);
                    tabControl.TabPages.Remove(tabVisits);
                    tabControl.TabPages.Remove(tabReferences);
                    tabControl.TabPages.Remove(tabReports);
                    tabControl.TabPages.Remove(tabUsers);
                    label3.Visible = false;
                    cmbMedCardPatient.Visible = false;
                    btnShowMedCard.Visible = false;
                    break;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dtpVisitsFrom.Value = DateTime.Now.AddMonths(-3);
            dtpReportFrom.Value = DateTime.Now.AddMonths(-6);

            if (tabControl.TabPages.Contains(tabPatients))
                LoadPatients();

            if (tabControl.TabPages.Contains(tabVisits))
                LoadVisits();

            if (tabControl.TabPages.Contains(tabMedCard))
            {
                if (currentRole == "registrar")
                    LoadOwnMedCard();
                else
                    LoadMedCardPatients();
            }

            if (tabControl.TabPages.Contains(tabReferences))
            {
                LoadDoctorsList();
                LoadDiagnosesList();
            }

            if (tabControl.TabPages.Contains(tabUsers))
                LoadUsers();
        }

        // ============================================================
        // ПАЦИЕНТЫ
        // ============================================================

        private void LoadPatients()
        {
            dataGridView1.DataSource = DbHelper.ExecuteQuery(
                @"SELECT patient_id, last_name, first_name, middle_name,
                         birth_date, gender, phone, address, policy_number
                  FROM patients ORDER BY last_name");
            dataGridView1.Columns["patient_id"].Visible = false;
            dataGridView1.Columns["last_name"].HeaderText = "Фамилия";
            dataGridView1.Columns["first_name"].HeaderText = "Имя";
            dataGridView1.Columns["middle_name"].HeaderText = "Отчество";
            dataGridView1.Columns["birth_date"].HeaderText = "Дата рождения";
            dataGridView1.Columns["gender"].HeaderText = "Пол";
            dataGridView1.Columns["phone"].HeaderText = "Телефон";
            dataGridView1.Columns["address"].HeaderText = "Адрес";
            dataGridView1.Columns["policy_number"].HeaderText = "Полис";
        }

        private void btnSearchPatient_Click(object sender, EventArgs e)
        {
            string s = txtSearchPatient.Text.Trim();
            if (string.IsNullOrEmpty(s)) { LoadPatients(); return; }
            dataGridView1.DataSource = DbHelper.ExecuteQuery(
                @"SELECT patient_id, last_name, first_name, middle_name,
                         birth_date, gender, phone, address, policy_number
                  FROM patients
                  WHERE last_name ILIKE @s OR first_name ILIKE @s OR policy_number ILIKE @s
                  ORDER BY last_name",
                new NpgsqlParameter("@s", "%" + s + "%"));
            dataGridView1.Columns["patient_id"].Visible = false;
        }

        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            var form = new PatientEditForm(null);
            if (form.ShowDialog() == DialogResult.OK) LoadPatients();
        }

        private void btnEditPatient_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["patient_id"].Value);
            var form = new PatientEditForm(id);
            if (form.ShowDialog() == DialogResult.OK) LoadPatients();
        }

        private void btnDeletePatient_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            if (MessageBox.Show("Удалить пациента?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["patient_id"].Value);
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

        // ============================================================
        // ПРИЁМЫ
        // ============================================================

        private void LoadVisits()
        {
            dgvVisits.DataSource = DbHelper.ExecuteQuery(
                @"SELECT v.visit_id, v.visit_date,
                         p.last_name || ' ' || p.first_name AS patient_name,
                         d.last_name || ' ' || d.first_name AS doctor_name,
                         s.specialty_name, diag.icd_code, diag.diagnosis_name,
                         v.complaints, v.treatment
                  FROM visits v
                  JOIN patients p    ON v.patient_id   = p.patient_id
                  JOIN doctors d     ON v.doctor_id    = d.doctor_id
                  JOIN specialties s ON d.specialty_id = s.specialty_id
                  LEFT JOIN diagnoses diag ON v.diagnosis_id = diag.diagnosis_id
                  WHERE v.visit_date BETWEEN @from AND @to
                  ORDER BY v.visit_date DESC",
                new NpgsqlParameter("@from", dtpVisitsFrom.Value.Date),
                new NpgsqlParameter("@to", dtpVisitsTo.Value.Date));

            if (dgvVisits.Columns.Count == 0) return;
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

        private void btnFilterVisits_Click(object sender, EventArgs e) => LoadVisits();

        private void btnAddVisit_Click(object sender, EventArgs e)
        {
            var form = new VisitEditForm(null);
            if (form.ShowDialog() == DialogResult.OK) LoadVisits();
        }

        private void btnEditVisit_Click(object sender, EventArgs e)
        {
            if (dgvVisits.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvVisits.CurrentRow.Cells["visit_id"].Value);
            var form = new VisitEditForm(id);
            if (form.ShowDialog() == DialogResult.OK) LoadVisits();
        }

        private void btnDeleteVisit_Click(object sender, EventArgs e)
        {
            if (dgvVisits.CurrentRow == null) return;
            if (MessageBox.Show("Удалить приём?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            int id = Convert.ToInt32(dgvVisits.CurrentRow.Cells["visit_id"].Value);
            DbHelper.ExecuteNonQuery("DELETE FROM visits WHERE visit_id = @id",
                new NpgsqlParameter("@id", id));
            LoadVisits();
        }

        // ============================================================
        // МЕД. КАРТА
        // ============================================================

        private void LoadMedCardPatients()
        {
            // ВАЖНО: DisplayMember и ValueMember всегда до DataSource,
            // иначе WinForms привязывает первый столбец по умолчанию
            cmbMedCardPatient.DisplayMember = "full_name";
            cmbMedCardPatient.ValueMember = "patient_id";
            cmbMedCardPatient.DataSource = DbHelper.ExecuteQuery(
                @"SELECT patient_id,
                         last_name || ' ' || first_name || ' ' || COALESCE(middle_name,'') AS full_name
                  FROM patients ORDER BY last_name");
        }

        private void LoadOwnMedCard()
        {
            var result = DbHelper.ExecuteQuery(
                "SELECT patient_id FROM users WHERE username = @u AND patient_id IS NOT NULL",
                new NpgsqlParameter("@u", currentUser));

            if (result.Rows.Count == 0 || result.Rows[0]["patient_id"] == DBNull.Value)
            {
                lblMedCardInfo.Text = "Медицинская карта не найдена. Обратитесь к администратору.";
                return;
            }

            currentPatientId = Convert.ToInt32(result.Rows[0]["patient_id"]);
            ShowMedCardForPatient(currentPatientId.Value);
        }

        private void btnShowMedCard_Click(object sender, EventArgs e)
        {
            if (cmbMedCardPatient.SelectedValue == null) return;
            ShowMedCardForPatient(Convert.ToInt32(cmbMedCardPatient.SelectedValue));
        }

        private void ShowMedCardForPatient(int patientId)
        {
            var info = DbHelper.ExecuteQuery(
                @"SELECT last_name || ' ' || first_name || ' ' || COALESCE(middle_name,'') AS name,
                         birth_date, gender, phone, policy_number
                  FROM patients WHERE patient_id = @id",
                new NpgsqlParameter("@id", patientId));

            if (info.Rows.Count > 0)
            {
                var row = info.Rows[0];
                lblMedCardInfo.Text =
                    $"{row["name"]} | Д.р.: {Convert.ToDateTime(row["birth_date"]):dd.MM.yyyy} | " +
                    $"Пол: {row["gender"]} | Тел: {row["phone"]} | Полис: {row["policy_number"]}";
            }

            dgvMedCardHistory.DataSource = DbHelper.ExecuteQuery(
                @"SELECT v.visit_id, v.visit_date,
                         d.last_name || ' ' || d.first_name AS doctor_name,
                         s.specialty_name, diag.icd_code, diag.diagnosis_name,
                         v.complaints, v.treatment, v.notes
                  FROM visits v
                  JOIN doctors d     ON v.doctor_id    = d.doctor_id
                  JOIN specialties s ON d.specialty_id = s.specialty_id
                  LEFT JOIN diagnoses diag ON v.diagnosis_id = diag.diagnosis_id
                  WHERE v.patient_id = @pid
                  ORDER BY v.visit_date DESC",
                new NpgsqlParameter("@pid", patientId));

            if (dgvMedCardHistory.Columns.Count == 0) return;
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

            dgvMedCardPrescriptions.DataSource = DbHelper.ExecuteQuery(
                "SELECT medicine_name, dosage, duration_days, instructions FROM prescriptions WHERE visit_id = @vid",
                new NpgsqlParameter("@vid", Convert.ToInt32(val)));

            if (dgvMedCardPrescriptions.Columns.Count > 0)
            {
                dgvMedCardPrescriptions.Columns["medicine_name"].HeaderText = "Препарат";
                dgvMedCardPrescriptions.Columns["dosage"].HeaderText = "Дозировка";
                dgvMedCardPrescriptions.Columns["duration_days"].HeaderText = "Дней";
                dgvMedCardPrescriptions.Columns["instructions"].HeaderText = "Указания";
            }
        }

        // Справочники

        private void LoadDoctorsList()
        {
            dgvDoctors.DataSource = DbHelper.ExecuteQuery(
                @"SELECT d.doctor_id, d.last_name, d.first_name, d.middle_name,
                         s.specialty_name, d.phone, d.cabinet
                  FROM doctors d
                  JOIN specialties s ON d.specialty_id = s.specialty_id
                  ORDER BY d.last_name");
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
            dgvDiagnoses.DataSource = DbHelper.ExecuteQuery(
                "SELECT diagnosis_id, icd_code, diagnosis_name FROM diagnoses ORDER BY icd_code");
            dgvDiagnoses.Columns["diagnosis_id"].Visible = false;
            dgvDiagnoses.Columns["icd_code"].HeaderText = "Код МКБ-10";
            dgvDiagnoses.Columns["diagnosis_name"].HeaderText = "Диагноз";
        }

        // Отчеты

        private void btnReportDisease_Click(object sender, EventArgs e)
        {
            var data = DbHelper.ExecuteQuery(
                @"SELECT diag.icd_code, diag.diagnosis_name,
                         COUNT(v.visit_id)            AS case_count,
                         COUNT(DISTINCT v.patient_id) AS patient_count
                  FROM visits v
                  JOIN diagnoses diag ON v.diagnosis_id = diag.diagnosis_id
                  WHERE v.visit_date BETWEEN @from AND @to
                  GROUP BY diag.icd_code, diag.diagnosis_name
                  ORDER BY COUNT(v.visit_id) DESC",
                new NpgsqlParameter("@from", dtpReportFrom.Value.Date),
                new NpgsqlParameter("@to", dtpReportTo.Value.Date));

            dgvReport.DataSource = data;
            dgvReport.Columns["icd_code"].HeaderText = "Код МКБ-10";
            dgvReport.Columns["diagnosis_name"].HeaderText = "Диагноз";
            dgvReport.Columns["case_count"].HeaderText = "Случаев";
            dgvReport.Columns["patient_count"].HeaderText = "Пациентов";

            int total = 0;
            foreach (DataRow r in data.Rows) total += Convert.ToInt32(r["case_count"]);
            lblReportTotal.Text =
                $"Итого случаев: {total} | Период: {dtpReportFrom.Value:dd.MM.yyyy} — {dtpReportTo.Value:dd.MM.yyyy}";
        }

        private void btnReportDoctors_Click(object sender, EventArgs e)
        {
            dgvReport.DataSource = DbHelper.ExecuteQuery(
                @"SELECT d.last_name || ' ' || d.first_name AS doctor_name,
                         s.specialty_name,
                         COUNT(v.visit_id)            AS visit_count,
                         COUNT(DISTINCT v.patient_id) AS patient_count
                  FROM doctors d
                  JOIN specialties s ON d.specialty_id = s.specialty_id
                  LEFT JOIN visits v ON d.doctor_id = v.doctor_id
                  GROUP BY d.doctor_id, d.last_name, d.first_name, s.specialty_name
                  ORDER BY COUNT(v.visit_id) DESC");
            dgvReport.Columns["doctor_name"].HeaderText = "Врач";
            dgvReport.Columns["specialty_name"].HeaderText = "Специальность";
            dgvReport.Columns["visit_count"].HeaderText = "Приёмов";
            dgvReport.Columns["patient_count"].HeaderText = "Пациентов";
            lblReportTotal.Text = "Статистика по врачам";
        }

        // admin

        private void LoadUsers()
        {
            dgvUsers.DataSource = DbHelper.ExecuteQuery(
                @"SELECT u.user_id,
                         u.username                                          AS ""Логин"",
                         u.role                                              AS ""Роль"",
                         CASE WHEN u.is_active THEN 'Да' ELSE 'Нет' END     AS ""Активен"",
                         COALESCE(p.last_name || ' ' || p.first_name, '—')  AS ""Пациент""
                  FROM users u
                  LEFT JOIN patients p ON u.patient_id = p.patient_id
                  ORDER BY u.role, u.username");

            if (dgvUsers.Columns.Count == 0) return;
            dgvUsers.Columns["user_id"].Visible = false;
        }

        private void btnChangeRole_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null) return;

            int userId = Convert.ToInt32(dgvUsers.CurrentRow.Cells["user_id"].Value);
            string login = dgvUsers.CurrentRow.Cells["Логин"].Value?.ToString();
            string userRole = dgvUsers.CurrentRow.Cells["Роль"].Value?.ToString();

            if (login == currentUser)
            {
                MessageBox.Show("Нельзя изменить роль своей учётной записи.",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var form = new UserRoleForm(login, userRole);
            if (form.ShowDialog() != DialogResult.OK) return;

            try
            {
                DbHelper.ExecuteNonQuery(
                    "UPDATE users SET role = @r WHERE user_id = @id",
                    new NpgsqlParameter("@r", form.SelectedRole),
                    new NpgsqlParameter("@id", userId));
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка смены роли:\n" + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Кнопка "Активировать / Заблокировать"
        private void btnToggleActive_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null) return;

            int userId = Convert.ToInt32(dgvUsers.CurrentRow.Cells["user_id"].Value);
            string login = dgvUsers.CurrentRow.Cells["Логин"].Value?.ToString();
            bool active = dgvUsers.CurrentRow.Cells["Активен"].Value?.ToString() == "Да";

            if (login == currentUser)
            {
                MessageBox.Show("Нельзя заблокировать свою учётную запись.",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string action = active ? "заблокировать" : "активировать";
            if (MessageBox.Show($"Вы хотите {action} пользователя «{login}»?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                DbHelper.ExecuteNonQuery(
                    "UPDATE users SET is_active = @a WHERE user_id = @id",
                    new NpgsqlParameter("@a", !active),
                    new NpgsqlParameter("@id", userId));
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:\n" + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefreshUsers_Click(object sender, EventArgs e) => LoadUsers();
    }
}