namespace control_asistencia_savin
{
    partial class frmAtrasos
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
            components = new System.ComponentModel.Container();
            tmrTime = new System.Windows.Forms.Timer(components);
            lblTitOption = new Label();
            lblTitPersonal = new Label();
            lblNombre = new Label();
            btnVerificarHuella = new Button();
            lblStatusProcess = new Label();
            txtCodigo = new TextBox();
            lblTitHora = new Label();
            dgvListDelay = new DataGridView();
            cbxPersonalMonth = new ComboBox();
            btnVerificarCode = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvListDelay).BeginInit();
            SuspendLayout();
            // 
            // tmrTime
            // 
            tmrTime.Enabled = true;
            tmrTime.Interval = 1000;
            // 
            // lblTitOption
            // 
            lblTitOption.Anchor = AnchorStyles.Top;
            lblTitOption.AutoSize = true;
            lblTitOption.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitOption.ForeColor = Color.FromArgb(10, 38, 102);
            lblTitOption.Location = new Point(303, 9);
            lblTitOption.Margin = new Padding(4, 0, 4, 0);
            lblTitOption.Name = "lblTitOption";
            lblTitOption.Size = new Size(601, 65);
            lblTitOption.TabIndex = 34;
            lblTitOption.Text = "MINUTOS ACUMULADOS";
            // 
            // lblTitPersonal
            // 
            lblTitPersonal.Anchor = AnchorStyles.Top;
            lblTitPersonal.AutoSize = true;
            lblTitPersonal.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold);
            lblTitPersonal.ForeColor = Color.FromArgb(10, 38, 102);
            lblTitPersonal.Location = new Point(126, 165);
            lblTitPersonal.Margin = new Padding(4, 0, 4, 0);
            lblTitPersonal.Name = "lblTitPersonal";
            lblTitPersonal.Size = new Size(176, 50);
            lblTitPersonal.TabIndex = 35;
            lblTitPersonal.Text = "Nombre:";
            // 
            // lblNombre
            // 
            lblNombre.Anchor = AnchorStyles.Top;
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNombre.ForeColor = Color.FromArgb(10, 38, 102);
            lblNombre.Location = new Point(324, 165);
            lblNombre.Margin = new Padding(4, 0, 4, 0);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(198, 50);
            lblNombre.TabIndex = 37;
            lblNombre.Text = "lblNombre";
            lblNombre.Visible = false;
            // 
            // btnVerificarHuella
            // 
            btnVerificarHuella.Anchor = AnchorStyles.Top;
            btnVerificarHuella.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVerificarHuella.ForeColor = Color.FromArgb(10, 38, 102);
            btnVerificarHuella.Location = new Point(126, 94);
            btnVerificarHuella.Margin = new Padding(4, 3, 4, 3);
            btnVerificarHuella.Name = "btnVerificarHuella";
            btnVerificarHuella.Size = new Size(138, 34);
            btnVerificarHuella.TabIndex = 32;
            btnVerificarHuella.Text = "btnHuella";
            btnVerificarHuella.UseVisualStyleBackColor = true;
            btnVerificarHuella.Click += btnVerificarHuellaCod_Click_1;
            // 
            // lblStatusProcess
            // 
            lblStatusProcess.Anchor = AnchorStyles.Top;
            lblStatusProcess.AutoSize = true;
            lblStatusProcess.BackColor = Color.Transparent;
            lblStatusProcess.FlatStyle = FlatStyle.System;
            lblStatusProcess.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStatusProcess.ForeColor = Color.FromArgb(10, 38, 102);
            lblStatusProcess.Location = new Point(411, 102);
            lblStatusProcess.Name = "lblStatusProcess";
            lblStatusProcess.Size = new Size(134, 21);
            lblStatusProcess.TabIndex = 38;
            lblStatusProcess.Text = "lblStatusProcess";
            lblStatusProcess.Visible = false;
            // 
            // txtCodigo
            // 
            txtCodigo.Anchor = AnchorStyles.Top;
            txtCodigo.BackColor = Color.Ivory;
            txtCodigo.BorderStyle = BorderStyle.FixedSingle;
            txtCodigo.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCodigo.Location = new Point(272, 94);
            txtCodigo.Margin = new Padding(4, 3, 4, 3);
            txtCodigo.Multiline = true;
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(666, 34);
            txtCodigo.TabIndex = 33;
            txtCodigo.Click += txtCodigo_Click;
            // 
            // lblTitHora
            // 
            lblTitHora.Anchor = AnchorStyles.Top;
            lblTitHora.AutoSize = true;
            lblTitHora.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitHora.ForeColor = Color.FromArgb(10, 38, 102);
            lblTitHora.Location = new Point(138, 211);
            lblTitHora.Margin = new Padding(4, 0, 4, 0);
            lblTitHora.Name = "lblTitHora";
            lblTitHora.Size = new Size(164, 50);
            lblTitHora.TabIndex = 36;
            lblTitHora.Text = "Gestión:";
            // 
            // dgvListDelay
            // 
            dgvListDelay.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvListDelay.Location = new Point(126, 273);
            dgvListDelay.Name = "dgvListDelay";
            dgvListDelay.Size = new Size(881, 135);
            dgvListDelay.TabIndex = 40;
            // 
            // cbxPersonalMonth
            // 
            cbxPersonalMonth.FormattingEnabled = true;
            cbxPersonalMonth.Location = new Point(324, 233);
            cbxPersonalMonth.Name = "cbxPersonalMonth";
            cbxPersonalMonth.Size = new Size(211, 23);
            cbxPersonalMonth.TabIndex = 41;
            cbxPersonalMonth.SelectedIndexChanged += cbxPersonalMonth_SelectedIndexChanged;
            // 
            // btnVerificarCode
            // 
            btnVerificarCode.Anchor = AnchorStyles.Top;
            btnVerificarCode.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVerificarCode.ForeColor = Color.FromArgb(10, 38, 102);
            btnVerificarCode.Location = new Point(126, 94);
            btnVerificarCode.Margin = new Padding(4, 3, 4, 3);
            btnVerificarCode.Name = "btnVerificarCode";
            btnVerificarCode.Size = new Size(138, 34);
            btnVerificarCode.TabIndex = 42;
            btnVerificarCode.Text = "btnCode";
            btnVerificarCode.UseVisualStyleBackColor = true;
            btnVerificarCode.Visible = false;
            // 
            // frmAtrasos
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(1125, 457);
            Controls.Add(btnVerificarCode);
            Controls.Add(cbxPersonalMonth);
            Controls.Add(dgvListDelay);
            Controls.Add(lblTitOption);
            Controls.Add(lblTitPersonal);
            Controls.Add(lblNombre);
            Controls.Add(btnVerificarHuella);
            Controls.Add(lblStatusProcess);
            Controls.Add(txtCodigo);
            Controls.Add(lblTitHora);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmAtrasos";
            Text = "Form1";
            Load += frmAtrasos_Load;
            Click += frmAtrasos_Click;
            ((System.ComponentModel.ISupportInitialize)dgvListDelay).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer tmrTime;
        public Label lblTitOption;
        public Label lblTitPersonal;
        public Label lblNombre;
        public Button btnVerificarHuella;
        public Label lblStatusProcess;
        public TextBox txtCodigo;
        public Label lblTitHora;
        private DateTimePicker dateTimer;
        private DataGridView dgvListDelay;
        private ComboBox cbxPersonalMonth;
        public Button btnVerificarCode;
    }
}