namespace PolyclinicApp
{
    partial class VisitEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.colInstructions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDosage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMedicine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvPrescriptions = new System.Windows.Forms.DataGridView();
            this.btnRemovePrescription = new System.Windows.Forms.Button();
            this.btnAddPrescription = new System.Windows.Forms.Button();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.txtTreatment = new System.Windows.Forms.TextBox();
            this.txtComplaints = new System.Windows.Forms.TextBox();
            this.cmbDiagnosis = new System.Windows.Forms.ComboBox();
            this.dtpVisitDate = new System.Windows.Forms.DateTimePicker();
            this.cmbDoctor = new System.Windows.Forms.ComboBox();
            this.cmbPatient = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrescriptions)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Red;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Location = new System.Drawing.Point(87, 406);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 28);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // colInstructions
            // 
            this.colInstructions.HeaderText = "Указания";
            this.colInstructions.MinimumWidth = 6;
            this.colInstructions.Name = "colInstructions";
            this.colInstructions.Width = 140;
            // 
            // colDays
            // 
            this.colDays.HeaderText = "Дней";
            this.colDays.MinimumWidth = 6;
            this.colDays.Name = "colDays";
            this.colDays.Width = 60;
            // 
            // colDosage
            // 
            this.colDosage.HeaderText = "Дозировка";
            this.colDosage.MinimumWidth = 6;
            this.colDosage.Name = "colDosage";
            this.colDosage.Width = 120;
            // 
            // colMedicine
            // 
            this.colMedicine.HeaderText = "Препарат";
            this.colMedicine.MinimumWidth = 6;
            this.colMedicine.Name = "colMedicine";
            this.colMedicine.Width = 140;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSave.Location = new System.Drawing.Point(87, 372);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 28);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = false;
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
            this.dgvPrescriptions.Location = new System.Drawing.Point(220, 294);
            this.dgvPrescriptions.Name = "dgvPrescriptions";
            this.dgvPrescriptions.RowHeadersWidth = 30;
            this.dgvPrescriptions.Size = new System.Drawing.Size(493, 140);
            this.dgvPrescriptions.TabIndex = 22;
            // 
            // btnRemovePrescription
            // 
            this.btnRemovePrescription.BackColor = System.Drawing.Color.Red;
            this.btnRemovePrescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.8F, System.Drawing.FontStyle.Bold);
            this.btnRemovePrescription.Location = new System.Drawing.Point(147, 309);
            this.btnRemovePrescription.Name = "btnRemovePrescription";
            this.btnRemovePrescription.Size = new System.Drawing.Size(37, 25);
            this.btnRemovePrescription.TabIndex = 23;
            this.btnRemovePrescription.Text = "-";
            this.btnRemovePrescription.UseVisualStyleBackColor = false;
            // 
            // btnAddPrescription
            // 
            this.btnAddPrescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAddPrescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnAddPrescription.Location = new System.Drawing.Point(87, 309);
            this.btnAddPrescription.Name = "btnAddPrescription";
            this.btnAddPrescription.Size = new System.Drawing.Size(37, 25);
            this.btnAddPrescription.TabIndex = 24;
            this.btnAddPrescription.Text = "+";
            this.btnAddPrescription.UseVisualStyleBackColor = false;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(270, 251);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(384, 22);
            this.txtNotes.TabIndex = 25;
            // 
            // txtTreatment
            // 
            this.txtTreatment.Location = new System.Drawing.Point(254, 207);
            this.txtTreatment.Multiline = true;
            this.txtTreatment.Name = "txtTreatment";
            this.txtTreatment.Size = new System.Drawing.Size(400, 38);
            this.txtTreatment.TabIndex = 26;
            // 
            // txtComplaints
            // 
            this.txtComplaints.Location = new System.Drawing.Point(254, 163);
            this.txtComplaints.Multiline = true;
            this.txtComplaints.Name = "txtComplaints";
            this.txtComplaints.Size = new System.Drawing.Size(400, 38);
            this.txtComplaints.TabIndex = 27;
            // 
            // cmbDiagnosis
            // 
            this.cmbDiagnosis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiagnosis.FormattingEnabled = true;
            this.cmbDiagnosis.Location = new System.Drawing.Point(254, 128);
            this.cmbDiagnosis.Name = "cmbDiagnosis";
            this.cmbDiagnosis.Size = new System.Drawing.Size(400, 24);
            this.cmbDiagnosis.TabIndex = 28;
            // 
            // dtpVisitDate
            // 
            this.dtpVisitDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVisitDate.Location = new System.Drawing.Point(254, 93);
            this.dtpVisitDate.Name = "dtpVisitDate";
            this.dtpVisitDate.Size = new System.Drawing.Size(200, 22);
            this.dtpVisitDate.TabIndex = 29;
            // 
            // cmbDoctor
            // 
            this.cmbDoctor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDoctor.FormattingEnabled = true;
            this.cmbDoctor.Location = new System.Drawing.Point(254, 55);
            this.cmbDoctor.Name = "cmbDoctor";
            this.cmbDoctor.Size = new System.Drawing.Size(400, 24);
            this.cmbDoctor.TabIndex = 30;
            // 
            // cmbPatient
            // 
            this.cmbPatient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatient.FormattingEnabled = true;
            this.cmbPatient.Location = new System.Drawing.Point(254, 17);
            this.cmbPatient.Name = "cmbPatient";
            this.cmbPatient.Size = new System.Drawing.Size(400, 24);
            this.cmbPatient.TabIndex = 31;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(97, 280);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 17);
            this.label8.TabIndex = 32;
            this.label8.Text = "Рецепты:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(171, 254);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 16);
            this.label7.TabIndex = 33;
            this.label7.Text = "Примечание:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(171, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 16);
            this.label6.TabIndex = 34;
            this.label6.Text = "Лечение:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(171, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 35;
            this.label5.Text = "Жалобы:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(171, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 16);
            this.label4.TabIndex = 36;
            this.label4.Text = "Диагноз:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(171, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 16);
            this.label3.TabIndex = 37;
            this.label3.Text = "Дата:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(171, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 16);
            this.label2.TabIndex = 38;
            this.label2.Text = "Врач:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 16);
            this.label1.TabIndex = 39;
            this.label1.Text = "Пациент:";
            // 
            // VisitEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
            this.Name = "VisitEditForm";
            this.Text = "Медецинское назначение";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrescriptions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInstructions;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDays;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDosage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMedicine;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvPrescriptions;
        private System.Windows.Forms.Button btnRemovePrescription;
        private System.Windows.Forms.Button btnAddPrescription;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.TextBox txtTreatment;
        private System.Windows.Forms.TextBox txtComplaints;
        private System.Windows.Forms.ComboBox cmbDiagnosis;
        private System.Windows.Forms.DateTimePicker dtpVisitDate;
        private System.Windows.Forms.ComboBox cmbDoctor;
        private System.Windows.Forms.ComboBox cmbPatient;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}