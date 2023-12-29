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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAtrasos));
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
            lblAtrasosMin = new Label();
            lblAtrasos = new Label();
            label3 = new Label();
            lblAtrasosHoras = new Label();
            btnCerrar = new Button();
            sqliteCommand1 = new Microsoft.Data.Sqlite.SqliteCommand();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            pictureBox1 = new PictureBox();
            panel5 = new Panel();
            panel6 = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvListDelay).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
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
            lblTitOption.Location = new Point(272, 9);
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
            lblTitPersonal.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Bold);
            lblTitPersonal.ForeColor = Color.FromArgb(10, 38, 102);
            lblTitPersonal.Location = new Point(171, 16);
            lblTitPersonal.Margin = new Padding(4, 0, 4, 0);
            lblTitPersonal.Name = "lblTitPersonal";
            lblTitPersonal.Size = new Size(148, 37);
            lblTitPersonal.TabIndex = 35;
            lblTitPersonal.Text = "Nombre:";
            // 
            // lblNombre
            // 
            lblNombre.Anchor = AnchorStyles.Top;
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNombre.ForeColor = Color.FromArgb(10, 38, 102);
            lblNombre.Location = new Point(327, 16);
            lblNombre.Margin = new Padding(4, 0, 4, 0);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(154, 40);
            lblNombre.TabIndex = 37;
            lblNombre.Text = "lblNombre";
            lblNombre.Visible = false;
            // 
            // btnVerificarHuella
            // 
            btnVerificarHuella.Anchor = AnchorStyles.Top;
            btnVerificarHuella.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVerificarHuella.ForeColor = Color.FromArgb(10, 38, 102);
            btnVerificarHuella.Location = new Point(126, 15);
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
            lblStatusProcess.Location = new Point(278, 21);
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
            txtCodigo.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCodigo.Location = new Point(272, 15);
            txtCodigo.Margin = new Padding(4, 3, 4, 3);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(735, 35);
            txtCodigo.TabIndex = 33;
            txtCodigo.Tag = "";
            txtCodigo.Click += txtCodigo_Click;
            // 
            // lblTitHora
            // 
            lblTitHora.Anchor = AnchorStyles.Top;
            lblTitHora.AutoSize = true;
            lblTitHora.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Bold);
            lblTitHora.ForeColor = Color.FromArgb(10, 38, 102);
            lblTitHora.Location = new Point(175, 54);
            lblTitHora.Margin = new Padding(4, 0, 4, 0);
            lblTitHora.Name = "lblTitHora";
            lblTitHora.Size = new Size(144, 37);
            lblTitHora.TabIndex = 36;
            lblTitHora.Text = "Gestión:";
            // 
            // dgvListDelay
            // 
            dgvListDelay.Anchor = AnchorStyles.Top;
            dgvListDelay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvListDelay.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvListDelay.Location = new Point(135, 3);
            dgvListDelay.Name = "dgvListDelay";
            dgvListDelay.Size = new Size(881, 140);
            dgvListDelay.TabIndex = 40;
            dgvListDelay.CellFormatting += dgvListDelay_CellFormatting;
            // 
            // cbxPersonalMonth
            // 
            cbxPersonalMonth.Anchor = AnchorStyles.Top;
            cbxPersonalMonth.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxPersonalMonth.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbxPersonalMonth.FormattingEnabled = true;
            cbxPersonalMonth.Location = new Point(327, 59);
            cbxPersonalMonth.Name = "cbxPersonalMonth";
            cbxPersonalMonth.Size = new Size(225, 29);
            cbxPersonalMonth.TabIndex = 41;
            cbxPersonalMonth.SelectedIndexChanged += cbxPersonalMonth_SelectedIndexChanged;
            // 
            // btnVerificarCode
            // 
            btnVerificarCode.Anchor = AnchorStyles.Top;
            btnVerificarCode.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVerificarCode.ForeColor = Color.FromArgb(10, 38, 102);
            btnVerificarCode.Location = new Point(126, 16);
            btnVerificarCode.Margin = new Padding(4, 3, 4, 3);
            btnVerificarCode.Name = "btnVerificarCode";
            btnVerificarCode.Size = new Size(138, 34);
            btnVerificarCode.TabIndex = 42;
            btnVerificarCode.Text = "btnCode";
            btnVerificarCode.UseVisualStyleBackColor = true;
            btnVerificarCode.Visible = false;
            btnVerificarCode.Click += btnVerificarCode_Click;
            // 
            // lblAtrasosMin
            // 
            lblAtrasosMin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblAtrasosMin.AutoSize = true;
            lblAtrasosMin.Location = new Point(973, 8);
            lblAtrasosMin.Name = "lblAtrasosMin";
            lblAtrasosMin.Size = new Size(80, 15);
            lblAtrasosMin.TabIndex = 43;
            lblAtrasosMin.Text = "lblAtrasosMin";
            lblAtrasosMin.Visible = false;
            // 
            // lblAtrasos
            // 
            lblAtrasos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblAtrasos.AutoSize = true;
            lblAtrasos.Location = new Point(847, 8);
            lblAtrasos.Name = "lblAtrasos";
            lblAtrasos.Size = new Size(120, 15);
            lblAtrasos.TabIndex = 44;
            lblAtrasos.Text = "TOTAL ATRASOS MIN:";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(831, 33);
            label3.Name = "label3";
            label3.Size = new Size(136, 15);
            label3.TabIndex = 45;
            label3.Text = "TOTAL ATRASOS HORAS:";
            // 
            // lblAtrasosHoras
            // 
            lblAtrasosHoras.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblAtrasosHoras.AutoSize = true;
            lblAtrasosHoras.Location = new Point(973, 33);
            lblAtrasosHoras.Name = "lblAtrasosHoras";
            lblAtrasosHoras.Size = new Size(90, 15);
            lblAtrasosHoras.TabIndex = 46;
            lblAtrasosHoras.Text = "lblAtrasosHoras";
            lblAtrasosHoras.Visible = false;
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = AnchorStyles.Top;
            btnCerrar.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCerrar.Location = new Point(510, 11);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(99, 37);
            btnCerrar.TabIndex = 47;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // sqliteCommand1
            // 
            sqliteCommand1.CommandTimeout = 30;
            sqliteCommand1.Connection = null;
            sqliteCommand1.Transaction = null;
            sqliteCommand1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top;
            panel1.BackColor = Color.Black;
            panel1.Location = new Point(63, 149);
            panel1.Name = "panel1";
            panel1.Size = new Size(1000, 2);
            panel1.TabIndex = 48;
            // 
            // panel2
            // 
            panel2.Controls.Add(lblTitOption);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1125, 81);
            panel2.TabIndex = 49;
            panel2.Click += panel2_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(lblStatusProcess);
            panel3.Controls.Add(txtCodigo);
            panel3.Controls.Add(btnVerificarCode);
            panel3.Controls.Add(btnVerificarHuella);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 81);
            panel3.Name = "panel3";
            panel3.Size = new Size(1125, 61);
            panel3.TabIndex = 50;
            panel3.Click += panel3_Click;
            // 
            // panel4
            // 
            panel4.Controls.Add(pictureBox1);
            panel4.Controls.Add(cbxPersonalMonth);
            panel4.Controls.Add(lblTitHora);
            panel4.Controls.Add(lblNombre);
            panel4.Controls.Add(lblTitPersonal);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 142);
            panel4.Name = "panel4";
            panel4.Size = new Size(1125, 100);
            panel4.TabIndex = 51;
            panel4.Click += panel4_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(862, 16);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(145, 72);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 42;
            pictureBox1.TabStop = false;
            // 
            // panel5
            // 
            panel5.Controls.Add(dgvListDelay);
            panel5.Controls.Add(panel1);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 242);
            panel5.Name = "panel5";
            panel5.Size = new Size(1125, 160);
            panel5.TabIndex = 52;
            panel5.Click += panel5_Click;
            // 
            // panel6
            // 
            panel6.Controls.Add(btnCerrar);
            panel6.Controls.Add(lblAtrasosHoras);
            panel6.Controls.Add(lblAtrasos);
            panel6.Controls.Add(label3);
            panel6.Controls.Add(lblAtrasosMin);
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(0, 402);
            panel6.Name = "panel6";
            panel6.Size = new Size(1125, 56);
            panel6.TabIndex = 41;
            panel6.Click += panel6_Click;
            // 
            // frmAtrasos
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(1125, 457);
            Controls.Add(panel6);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmAtrasos";
            Text = "Form1";
            Click += frmAtrasos_Click;
            ((System.ComponentModel.ISupportInitialize)dgvListDelay).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel5.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ResumeLayout(false);
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
        private Label lblAtrasosMin;
        private Label lblAtrasos;
        private Label label3;
        private Label lblAtrasosHoras;
        private Button btnCerrar;
        private Microsoft.Data.Sqlite.SqliteCommand sqliteCommand1;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private PictureBox pictureBox1;
        private Panel panel5;
        private Panel panel6;
    }
}