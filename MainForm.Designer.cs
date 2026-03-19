namespace PolyclinicApp
{
    partial class MainForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPatients = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
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
            this.dgvMedCardPrescriptions = new System.Windows.Forms.DataGridView();
            this.dgvMedCardHistory = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblMedCardInfo = new System.Windows.Forms.Label();
            this.btnShowMedCard = new System.Windows.Forms.Button();
            this.cmbMedCardPatient = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabReferences = new System.Windows.Forms.TabPage();
            this.dgvDiagnoses = new System.Windows.Forms.DataGridView();
            this.dgvDoctors = new System.Windows.Forms.DataGridView();
            this.tabReports = new System.Windows.Forms.TabPage();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblReportTotal = new System.Windows.Forms.Label();
            this.btnReportDoctors = new System.Windows.Forms.Button();
            this.btnReportDisease = new System.Windows.Forms.Button();
            this.dtpReportTo = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpReportFrom = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            // ── НОВАЯ ВКЛАДКА: Пользователи ──
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.panelUsers = new System.Windows.Forms.Panel();
            this.btnToggleActive = new System.Windows.Forms.Button();
            this.btnChangeRole = new System.Windows.Forms.Button();
            this.btnRefreshUsers = new System.Windows.Forms.Button();

            this.tabControl.SuspendLayout();
            this.tabPatients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabVisits.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisits)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabMedCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedCardPrescriptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedCardHistory)).BeginInit();
            this.panel3.SuspendLayout();
            this.tabReferences.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiagnoses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoctors)).BeginInit();
            this.tabReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.panel4.SuspendLayout();
            this.tabUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.panelUsers.SuspendLayout();
            this.SuspendLayout();

            // ── tabControl ────────────────────────────────────────────
            this.tabControl.Controls.Add(this.tabPatients);
            this.tabControl.Controls.Add(this.tabVisits);
            this.tabControl.Controls.Add(this.tabMedCard);
            this.tabControl.Controls.Add(this.tabReferences);
            this.tabControl.Controls.Add(this.tabReports);
            this.tabControl.Controls.Add(this.tabUsers);   // <-- новая вкладка
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(975, 465);
            this.tabControl.TabIndex = 0;

            // ── tabPatients ───────────────────────────────────────────
            this.tabPatients.Controls.Add(this.dataGridView1);
            this.tabPatients.Controls.Add(this.panel1);
            this.tabPatients.Location = new System.Drawing.Point(4, 25);
            this.tabPatients.Name = "tabPatients";
            this.tabPatients.Padding = new System.Windows.Forms.Padding(3);
            this.tabPatients.Size = new System.Drawing.Size(967, 436);
            this.tabPatients.TabIndex = 0;
            this.tabPatients.Text = "Пациенты";
            this.tabPatients.UseVisualStyleBackColor = true;

            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 60);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(961, 373);
            this.dataGridView1.TabIndex = 1;

            this.panel1.Controls.Add(this.btnDeletePatient);
            this.panel1.Controls.Add(this.btnEditPatient);
            this.panel1.Controls.Add(this.btnAddPatient);
            this.panel1.Controls.Add(this.btnSearchPatient);
            this.panel1.Controls.Add(this.txtSearchPatient);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(961, 57);
            this.panel1.TabIndex = 0;

            this.btnDeletePatient.BackColor = System.Drawing.Color.Red;
            this.btnDeletePatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnDeletePatient.ForeColor = System.Drawing.SystemColors.Control;
            this.btnDeletePatient.Location = new System.Drawing.Point(674, 4);
            this.btnDeletePatient.Name = "btnDeletePatient";
            this.btnDeletePatient.Size = new System.Drawing.Size(103, 45);
            this.btnDeletePatient.TabIndex = 4;
            this.btnDeletePatient.Text = "Удалить";
            this.btnDeletePatient.UseVisualStyleBackColor = false;
            this.btnDeletePatient.Click += new System.EventHandler(this.btnDeletePatient_Click);

            this.btnEditPatient.BackColor = System.Drawing.Color.Gold;
            this.btnEditPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnEditPatient.ForeColor = System.Drawing.SystemColors.Control;
            this.btnEditPatient.Location = new System.Drawing.Point(513, 4);
            this.btnEditPatient.Name = "btnEditPatient";
            this.btnEditPatient.Size = new System.Drawing.Size(155, 46);
            this.btnEditPatient.TabIndex = 3;
            this.btnEditPatient.Text = "Редактировать";
            this.btnEditPatient.UseVisualStyleBackColor = false;
            this.btnEditPatient.Click += new System.EventHandler(this.btnEditPatient_Click);

            this.btnAddPatient.BackColor = System.Drawing.Color.Green;
            this.btnAddPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnAddPatient.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAddPatient.Location = new System.Drawing.Point(360, 3);
            this.btnAddPatient.Name = "btnAddPatient";
            this.btnAddPatient.Size = new System.Drawing.Size(147, 46);
            this.btnAddPatient.TabIndex = 2;
            this.btnAddPatient.Text = "Добавить";
            this.btnAddPatient.UseVisualStyleBackColor = false;
            this.btnAddPatient.Click += new System.EventHandler(this.btnAddPatient_Click);

            this.btnSearchPatient.Location = new System.Drawing.Point(175, 16);
            this.btnSearchPatient.Name = "btnSearchPatient";
            this.btnSearchPatient.Size = new System.Drawing.Size(75, 23);
            this.btnSearchPatient.TabIndex = 1;
            this.btnSearchPatient.Text = "Найти";
            this.btnSearchPatient.Click += new System.EventHandler(this.btnSearchPatient_Click);

            this.txtSearchPatient.Location = new System.Drawing.Point(5, 16);
            this.txtSearchPatient.Name = "txtSearchPatient";
            this.txtSearchPatient.Size = new System.Drawing.Size(164, 22);
            this.txtSearchPatient.TabIndex = 0;

            // ── tabVisits ─────────────────────────────────────────────
            this.tabVisits.Controls.Add(this.dgvVisits);
            this.tabVisits.Controls.Add(this.panel2);
            this.tabVisits.Location = new System.Drawing.Point(4, 25);
            this.tabVisits.Name = "tabVisits";
            this.tabVisits.Padding = new System.Windows.Forms.Padding(3);
            this.tabVisits.Size = new System.Drawing.Size(967, 436);
            this.tabVisits.TabIndex = 1;
            this.tabVisits.Text = "Приёмы";
            this.tabVisits.UseVisualStyleBackColor = true;

            this.dgvVisits.AllowUserToAddRows = false;
            this.dgvVisits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVisits.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvVisits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVisits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVisits.Location = new System.Drawing.Point(3, 61);
            this.dgvVisits.Name = "dgvVisits";
            this.dgvVisits.ReadOnly = true;
            this.dgvVisits.RowHeadersWidth = 51;
            this.dgvVisits.RowTemplate.Height = 24;
            this.dgvVisits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVisits.Size = new System.Drawing.Size(961, 372);
            this.dgvVisits.TabIndex = 1;

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
            this.panel2.Size = new System.Drawing.Size(961, 58);
            this.panel2.TabIndex = 0;

            this.btnDeleteVisit.BackColor = System.Drawing.Color.Red;
            this.btnDeleteVisit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnDeleteVisit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnDeleteVisit.Location = new System.Drawing.Point(861, 15);
            this.btnDeleteVisit.Name = "btnDeleteVisit";
            this.btnDeleteVisit.Size = new System.Drawing.Size(95, 35);
            this.btnDeleteVisit.TabIndex = 7;
            this.btnDeleteVisit.Text = "Удалить";
            this.btnDeleteVisit.UseVisualStyleBackColor = false;
            this.btnDeleteVisit.Click += new System.EventHandler(this.btnDeleteVisit_Click);

            this.btnEditVisit.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnEditVisit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnEditVisit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnEditVisit.Location = new System.Drawing.Point(713, 15);
            this.btnEditVisit.Name = "btnEditVisit";
            this.btnEditVisit.Size = new System.Drawing.Size(142, 35);
            this.btnEditVisit.TabIndex = 6;
            this.btnEditVisit.Text = "Редактировать";
            this.btnEditVisit.UseVisualStyleBackColor = false;
            this.btnEditVisit.Click += new System.EventHandler(this.btnEditVisit_Click);

            this.btnAddVisit.BackColor = System.Drawing.Color.Green;
            this.btnAddVisit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnAddVisit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAddVisit.Location = new System.Drawing.Point(565, 13);
            this.btnAddVisit.Name = "btnAddVisit";
            this.btnAddVisit.Size = new System.Drawing.Size(142, 35);
            this.btnAddVisit.TabIndex = 5;
            this.btnAddVisit.Text = "Добавить";
            this.btnAddVisit.UseVisualStyleBackColor = false;
            this.btnAddVisit.Click += new System.EventHandler(this.btnAddVisit_Click);

            this.btnFilterVisits.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnFilterVisits.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnFilterVisits.ForeColor = System.Drawing.SystemColors.Control;
            this.btnFilterVisits.Location = new System.Drawing.Point(434, 15);
            this.btnFilterVisits.Name = "btnFilterVisits";
            this.btnFilterVisits.Size = new System.Drawing.Size(125, 35);
            this.btnFilterVisits.TabIndex = 4;
            this.btnFilterVisits.Text = "Применить";
            this.btnFilterVisits.UseVisualStyleBackColor = false;
            this.btnFilterVisits.Click += new System.EventHandler(this.btnFilterVisits_Click);

            this.dtpVisitsTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVisitsTo.Location = new System.Drawing.Point(290, 18);
            this.dtpVisitsTo.Name = "dtpVisitsTo";
            this.dtpVisitsTo.Size = new System.Drawing.Size(111, 22);
            this.dtpVisitsTo.TabIndex = 3;

            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(258, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "По:";

            this.dtpVisitsFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVisitsFrom.Location = new System.Drawing.Point(136, 18);
            this.dtpVisitsFrom.Name = "dtpVisitsFrom";
            this.dtpVisitsFrom.Size = new System.Drawing.Size(111, 22);
            this.dtpVisitsFrom.TabIndex = 1;

            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(111, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "С:";

            // ── tabMedCard ────────────────────────────────────────────
            this.tabMedCard.Controls.Add(this.dgvMedCardPrescriptions);
            this.tabMedCard.Controls.Add(this.dgvMedCardHistory);
            this.tabMedCard.Controls.Add(this.panel3);
            this.tabMedCard.Location = new System.Drawing.Point(4, 25);
            this.tabMedCard.Name = "tabMedCard";
            this.tabMedCard.Padding = new System.Windows.Forms.Padding(3);
            this.tabMedCard.Size = new System.Drawing.Size(967, 436);
            this.tabMedCard.TabIndex = 2;
            this.tabMedCard.Text = "Мед. карта";
            this.tabMedCard.UseVisualStyleBackColor = true;

            this.dgvMedCardPrescriptions.AllowUserToAddRows = false;
            this.dgvMedCardPrescriptions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMedCardPrescriptions.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvMedCardPrescriptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedCardPrescriptions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvMedCardPrescriptions.Location = new System.Drawing.Point(3, 323);
            this.dgvMedCardPrescriptions.Name = "dgvMedCardPrescriptions";
            this.dgvMedCardPrescriptions.ReadOnly = true;
            this.dgvMedCardPrescriptions.RowHeadersWidth = 51;
            this.dgvMedCardPrescriptions.RowTemplate.Height = 24;
            this.dgvMedCardPrescriptions.Size = new System.Drawing.Size(961, 110);
            this.dgvMedCardPrescriptions.TabIndex = 2;

            this.dgvMedCardHistory.AllowUserToAddRows = false;
            this.dgvMedCardHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMedCardHistory.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvMedCardHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedCardHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMedCardHistory.Location = new System.Drawing.Point(3, 70);
            this.dgvMedCardHistory.Name = "dgvMedCardHistory";
            this.dgvMedCardHistory.ReadOnly = true;
            this.dgvMedCardHistory.RowHeadersWidth = 51;
            this.dgvMedCardHistory.RowTemplate.Height = 24;
            this.dgvMedCardHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMedCardHistory.Size = new System.Drawing.Size(961, 253);
            this.dgvMedCardHistory.TabIndex = 1;
            this.dgvMedCardHistory.SelectionChanged += new System.EventHandler(this.dgvMedCardHistory_SelectionChanged);

            this.panel3.Controls.Add(this.lblMedCardInfo);
            this.panel3.Controls.Add(this.btnShowMedCard);
            this.panel3.Controls.Add(this.cmbMedCardPatient);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(961, 67);
            this.panel3.TabIndex = 0;

            this.lblMedCardInfo.AutoSize = true;
            this.lblMedCardInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.lblMedCardInfo.Location = new System.Drawing.Point(459, 21);
            this.lblMedCardInfo.Name = "lblMedCardInfo";
            this.lblMedCardInfo.Size = new System.Drawing.Size(0, 16);
            this.lblMedCardInfo.TabIndex = 3;

            this.btnShowMedCard.BackColor = System.Drawing.Color.Green;
            this.btnShowMedCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnShowMedCard.ForeColor = System.Drawing.SystemColors.Control;
            this.btnShowMedCard.Location = new System.Drawing.Point(239, 11);
            this.btnShowMedCard.Name = "btnShowMedCard";
            this.btnShowMedCard.Size = new System.Drawing.Size(203, 43);
            this.btnShowMedCard.TabIndex = 2;
            this.btnShowMedCard.Text = "Показать карту";
            this.btnShowMedCard.UseVisualStyleBackColor = false;
            this.btnShowMedCard.Click += new System.EventHandler(this.btnShowMedCard_Click);

            this.cmbMedCardPatient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMedCardPatient.FormattingEnabled = true;
            this.cmbMedCardPatient.Location = new System.Drawing.Point(87, 21);
            this.cmbMedCardPatient.Name = "cmbMedCardPatient";
            this.cmbMedCardPatient.Size = new System.Drawing.Size(121, 24);
            this.cmbMedCardPatient.TabIndex = 1;

            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(5, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Пациент:";

            // ── tabReferences ─────────────────────────────────────────
            this.tabReferences.Controls.Add(this.dgvDiagnoses);
            this.tabReferences.Controls.Add(this.dgvDoctors);
            this.tabReferences.Location = new System.Drawing.Point(4, 25);
            this.tabReferences.Name = "tabReferences";
            this.tabReferences.Padding = new System.Windows.Forms.Padding(3);
            this.tabReferences.Size = new System.Drawing.Size(967, 436);
            this.tabReferences.TabIndex = 3;
            this.tabReferences.Text = "Справочники";
            this.tabReferences.UseVisualStyleBackColor = true;

            this.dgvDiagnoses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDiagnoses.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDiagnoses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDiagnoses.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvDiagnoses.Location = new System.Drawing.Point(3, 313);
            this.dgvDiagnoses.Name = "dgvDiagnoses";
            this.dgvDiagnoses.RowHeadersWidth = 51;
            this.dgvDiagnoses.RowTemplate.Height = 24;
            this.dgvDiagnoses.Size = new System.Drawing.Size(961, 120);
            this.dgvDiagnoses.TabIndex = 1;

            this.dgvDoctors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDoctors.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDoctors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoctors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDoctors.Location = new System.Drawing.Point(3, 3);
            this.dgvDoctors.Name = "dgvDoctors";
            this.dgvDoctors.RowHeadersWidth = 51;
            this.dgvDoctors.RowTemplate.Height = 24;
            this.dgvDoctors.Size = new System.Drawing.Size(961, 430);
            this.dgvDoctors.TabIndex = 0;

            // ── tabReports ────────────────────────────────────────────
            this.tabReports.Controls.Add(this.dgvReport);
            this.tabReports.Controls.Add(this.panel4);
            this.tabReports.Location = new System.Drawing.Point(4, 25);
            this.tabReports.Name = "tabReports";
            this.tabReports.Padding = new System.Windows.Forms.Padding(3);
            this.tabReports.Size = new System.Drawing.Size(967, 436);
            this.tabReports.TabIndex = 4;
            this.tabReports.Text = "Отчёты";
            this.tabReports.UseVisualStyleBackColor = true;

            this.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReport.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReport.Location = new System.Drawing.Point(3, 58);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersWidth = 51;
            this.dgvReport.RowTemplate.Height = 24;
            this.dgvReport.Size = new System.Drawing.Size(961, 375);
            this.dgvReport.TabIndex = 1;

            this.panel4.Controls.Add(this.lblReportTotal);
            this.panel4.Controls.Add(this.btnReportDoctors);
            this.panel4.Controls.Add(this.btnReportDisease);
            this.panel4.Controls.Add(this.dtpReportTo);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.dtpReportFrom);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(961, 55);
            this.panel4.TabIndex = 0;

            this.lblReportTotal.AutoSize = true;
            this.lblReportTotal.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
            this.lblReportTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblReportTotal.Location = new System.Drawing.Point(646, 20);
            this.lblReportTotal.Name = "lblReportTotal";
            this.lblReportTotal.Size = new System.Drawing.Size(0, 16);
            this.lblReportTotal.TabIndex = 6;

            this.btnReportDoctors.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnReportDoctors.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnReportDoctors.ForeColor = System.Drawing.SystemColors.Control;
            this.btnReportDoctors.Location = new System.Drawing.Point(492, 8);
            this.btnReportDoctors.Name = "btnReportDoctors";
            this.btnReportDoctors.Size = new System.Drawing.Size(148, 40);
            this.btnReportDoctors.TabIndex = 5;
            this.btnReportDoctors.Text = "По врачам";
            this.btnReportDoctors.UseVisualStyleBackColor = false;
            this.btnReportDoctors.Click += new System.EventHandler(this.btnReportDoctors_Click);

            this.btnReportDisease.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnReportDisease.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnReportDisease.ForeColor = System.Drawing.SystemColors.Control;
            this.btnReportDisease.Location = new System.Drawing.Point(331, 8);
            this.btnReportDisease.Name = "btnReportDisease";
            this.btnReportDisease.Size = new System.Drawing.Size(155, 41);
            this.btnReportDisease.TabIndex = 4;
            this.btnReportDisease.Text = "Заболеваемость";
            this.btnReportDisease.UseVisualStyleBackColor = false;
            this.btnReportDisease.Click += new System.EventHandler(this.btnReportDisease_Click);

            this.dtpReportTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReportTo.Location = new System.Drawing.Point(195, 12);
            this.dtpReportTo.Name = "dtpReportTo";
            this.dtpReportTo.Size = new System.Drawing.Size(111, 22);
            this.dtpReportTo.TabIndex = 3;

            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(158, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 16);
            this.label5.TabIndex = 2;
            this.label5.Text = "По:";

            this.dtpReportFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReportFrom.Location = new System.Drawing.Point(41, 12);
            this.dtpReportFrom.Name = "dtpReportFrom";
            this.dtpReportFrom.Size = new System.Drawing.Size(111, 22);
            this.dtpReportFrom.TabIndex = 1;

            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(16, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "С:";

            // ── tabUsers (НОВАЯ ВКЛАДКА) ──────────────────────────────
            this.tabUsers.Controls.Add(this.dgvUsers);
            this.tabUsers.Controls.Add(this.panelUsers);
            this.tabUsers.Location = new System.Drawing.Point(4, 25);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabUsers.Size = new System.Drawing.Size(967, 436);
            this.tabUsers.TabIndex = 5;
            this.tabUsers.Text = "Пользователи";
            this.tabUsers.UseVisualStyleBackColor = true;

            // Таблица пользователей
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsers.Location = new System.Drawing.Point(3, 58);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersWidth = 51;
            this.dgvUsers.RowTemplate.Height = 24;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.Size = new System.Drawing.Size(961, 375);
            this.dgvUsers.TabIndex = 1;

            // Панель кнопок управления пользователями
            this.panelUsers.Controls.Add(this.btnRefreshUsers);
            this.panelUsers.Controls.Add(this.btnToggleActive);
            this.panelUsers.Controls.Add(this.btnChangeRole);
            this.panelUsers.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUsers.Location = new System.Drawing.Point(3, 3);
            this.panelUsers.Name = "panelUsers";
            this.panelUsers.Size = new System.Drawing.Size(961, 55);
            this.panelUsers.TabIndex = 0;

            // Кнопка "Сменить роль"
            this.btnChangeRole.BackColor = System.Drawing.Color.SteelBlue;
            this.btnChangeRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnChangeRole.ForeColor = System.Drawing.SystemColors.Control;
            this.btnChangeRole.Location = new System.Drawing.Point(5, 8);
            this.btnChangeRole.Name = "btnChangeRole";
            this.btnChangeRole.Size = new System.Drawing.Size(160, 38);
            this.btnChangeRole.TabIndex = 0;
            this.btnChangeRole.Text = "Сменить роль";
            this.btnChangeRole.UseVisualStyleBackColor = false;
            this.btnChangeRole.Click += new System.EventHandler(this.btnChangeRole_Click);

            // Кнопка "Активировать / Заблокировать"
            this.btnToggleActive.BackColor = System.Drawing.Color.DarkOrange;
            this.btnToggleActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnToggleActive.ForeColor = System.Drawing.SystemColors.Control;
            this.btnToggleActive.Location = new System.Drawing.Point(175, 8);
            this.btnToggleActive.Name = "btnToggleActive";
            this.btnToggleActive.Size = new System.Drawing.Size(200, 38);
            this.btnToggleActive.TabIndex = 1;
            this.btnToggleActive.Text = "Активировать / Заблокировать";
            this.btnToggleActive.UseVisualStyleBackColor = false;
            this.btnToggleActive.Click += new System.EventHandler(this.btnToggleActive_Click);

            // Кнопка "Обновить"
            this.btnRefreshUsers.Location = new System.Drawing.Point(385, 12);
            this.btnRefreshUsers.Name = "btnRefreshUsers";
            this.btnRefreshUsers.Size = new System.Drawing.Size(90, 28);
            this.btnRefreshUsers.TabIndex = 2;
            this.btnRefreshUsers.Text = "Обновить";
            this.btnRefreshUsers.Click += new System.EventHandler(this.btnRefreshUsers_Click);

            // ── MainForm ──────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 465);
            this.Controls.Add(this.tabControl);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Электронная мед. книжка";
            this.Load += new System.EventHandler(this.MainForm_Load);

            this.tabControl.ResumeLayout(false);
            this.tabPatients.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabVisits.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisits)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabMedCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedCardPrescriptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedCardHistory)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabReferences.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiagnoses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoctors)).EndInit();
            this.tabReports.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.panelUsers.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        // Существующие контролы
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPatients;
        private System.Windows.Forms.TabPage tabVisits;
        private System.Windows.Forms.TabPage tabMedCard;
        private System.Windows.Forms.TabPage tabReferences;
        private System.Windows.Forms.TabPage tabReports;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEditPatient;
        private System.Windows.Forms.Button btnAddPatient;
        private System.Windows.Forms.Button btnSearchPatient;
        private System.Windows.Forms.TextBox txtSearchPatient;
        private System.Windows.Forms.Button btnDeletePatient;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFilterVisits;
        private System.Windows.Forms.DateTimePicker dtpVisitsTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpVisitsFrom;
        private System.Windows.Forms.Button btnEditVisit;
        private System.Windows.Forms.Button btnAddVisit;
        private System.Windows.Forms.DataGridView dgvVisits;
        private System.Windows.Forms.Button btnDeleteVisit;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvMedCardPrescriptions;
        private System.Windows.Forms.DataGridView dgvMedCardHistory;
        private System.Windows.Forms.Label lblMedCardInfo;
        private System.Windows.Forms.Button btnShowMedCard;
        private System.Windows.Forms.ComboBox cmbMedCardPatient;
        private System.Windows.Forms.DataGridView dgvDiagnoses;
        private System.Windows.Forms.DataGridView dgvDoctors;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DateTimePicker dtpReportFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblReportTotal;
        private System.Windows.Forms.Button btnReportDoctors;
        private System.Windows.Forms.Button btnReportDisease;
        private System.Windows.Forms.DateTimePicker dtpReportTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvReport;
        // Новые контролы вкладки Пользователи
        private System.Windows.Forms.TabPage tabUsers;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.Panel panelUsers;
        private System.Windows.Forms.Button btnChangeRole;
        private System.Windows.Forms.Button btnToggleActive;
        private System.Windows.Forms.Button btnRefreshUsers;
    }
}