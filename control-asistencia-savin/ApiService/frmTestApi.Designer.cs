namespace control_asistencia_savin
{
    partial class frmTestApi
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
            btnVerificarApi = new Button();
            pgrVerificarApi = new ProgressBar();
            pgrCargarDB = new ProgressBar();
            btnCargarDB = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnDeleteBD = new Button();
            btnMakeBackUp = new Button();
            btnDeleteBackups = new Button();
            txtDateBackup = new TextBox();
            cbxStore = new ComboBox();
            btnCargarRegistros = new Button();
            lblPuntoAsistencia = new Label();
            txtBackupMonth = new TextBox();
            SuspendLayout();
            // 
            // btnVerificarApi
            // 
            btnVerificarApi.Location = new Point(195, 56);
            btnVerificarApi.Name = "btnVerificarApi";
            btnVerificarApi.Size = new Size(125, 23);
            btnVerificarApi.TabIndex = 0;
            btnVerificarApi.Text = "Verificar Conexión";
            btnVerificarApi.UseVisualStyleBackColor = true;
            btnVerificarApi.Click += btnVerificarApi_Click;
            // 
            // pgrVerificarApi
            // 
            pgrVerificarApi.Location = new Point(360, 56);
            pgrVerificarApi.Name = "pgrVerificarApi";
            pgrVerificarApi.Size = new Size(100, 23);
            pgrVerificarApi.TabIndex = 1;
            // 
            // pgrCargarDB
            // 
            pgrCargarDB.Location = new Point(360, 119);
            pgrCargarDB.Name = "pgrCargarDB";
            pgrCargarDB.Size = new Size(100, 23);
            pgrCargarDB.TabIndex = 3;
            // 
            // btnCargarDB
            // 
            btnCargarDB.Location = new Point(195, 106);
            btnCargarDB.Name = "btnCargarDB";
            btnCargarDB.Size = new Size(125, 23);
            btnCargarDB.TabIndex = 2;
            btnCargarDB.Text = "Cargar Datos";
            btnCargarDB.UseVisualStyleBackColor = true;
            btnCargarDB.Click += btnCargarDB_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Location = new Point(34, 60);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(119, 151);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // btnDeleteBD
            // 
            btnDeleteBD.Location = new Point(195, 156);
            btnDeleteBD.Name = "btnDeleteBD";
            btnDeleteBD.Size = new Size(125, 23);
            btnDeleteBD.TabIndex = 6;
            btnDeleteBD.Text = "borrar bd";
            btnDeleteBD.UseVisualStyleBackColor = true;
            btnDeleteBD.Click += btnDeleteBD_Click;
            // 
            // btnMakeBackUp
            // 
            btnMakeBackUp.Location = new Point(195, 205);
            btnMakeBackUp.Name = "btnMakeBackUp";
            btnMakeBackUp.Size = new Size(125, 23);
            btnMakeBackUp.TabIndex = 7;
            btnMakeBackUp.Text = "make copy";
            btnMakeBackUp.UseVisualStyleBackColor = true;
            btnMakeBackUp.Click += btnMakeBackUp_Click;
            // 
            // btnDeleteBackups
            // 
            btnDeleteBackups.Location = new Point(195, 251);
            btnDeleteBackups.Name = "btnDeleteBackups";
            btnDeleteBackups.Size = new Size(125, 23);
            btnDeleteBackups.TabIndex = 8;
            btnDeleteBackups.Text = "delete backups";
            btnDeleteBackups.UseVisualStyleBackColor = true;
            btnDeleteBackups.Click += btnDeleteBackups_Click;
            // 
            // txtDateBackup
            // 
            txtDateBackup.Location = new Point(338, 206);
            txtDateBackup.Name = "txtDateBackup";
            txtDateBackup.Size = new Size(137, 23);
            txtDateBackup.TabIndex = 9;
            // 
            // cbxStore
            // 
            cbxStore.FormattingEnabled = true;
            cbxStore.Location = new Point(496, 72);
            cbxStore.Name = "cbxStore";
            cbxStore.Size = new Size(162, 23);
            cbxStore.TabIndex = 10;
            cbxStore.SelectedIndexChanged += cbxStore_SelectedIndexChanged;
            // 
            // btnCargarRegistros
            // 
            btnCargarRegistros.Location = new Point(496, 119);
            btnCargarRegistros.Name = "btnCargarRegistros";
            btnCargarRegistros.Size = new Size(162, 23);
            btnCargarRegistros.TabIndex = 11;
            btnCargarRegistros.Text = "Cargar Registros";
            btnCargarRegistros.UseVisualStyleBackColor = true;
            btnCargarRegistros.Click += btnCargarRegistros_Click;
            // 
            // lblPuntoAsistencia
            // 
            lblPuntoAsistencia.AutoSize = true;
            lblPuntoAsistencia.Location = new Point(496, 164);
            lblPuntoAsistencia.Name = "lblPuntoAsistencia";
            lblPuntoAsistencia.Size = new Size(99, 15);
            lblPuntoAsistencia.TabIndex = 12;
            lblPuntoAsistencia.Text = "Punto asistencia: ";
            // 
            // txtBackupMonth
            // 
            txtBackupMonth.Location = new Point(338, 252);
            txtBackupMonth.Name = "txtBackupMonth";
            txtBackupMonth.Size = new Size(137, 23);
            txtBackupMonth.TabIndex = 13;
            // 
            // frmTestApi
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(688, 295);
            Controls.Add(txtBackupMonth);
            Controls.Add(lblPuntoAsistencia);
            Controls.Add(btnCargarRegistros);
            Controls.Add(cbxStore);
            Controls.Add(txtDateBackup);
            Controls.Add(btnDeleteBackups);
            Controls.Add(btnMakeBackUp);
            Controls.Add(btnDeleteBD);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(pgrCargarDB);
            Controls.Add(btnCargarDB);
            Controls.Add(pgrVerificarApi);
            Controls.Add(btnVerificarApi);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmTestApi";
            Text = "TestApi";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar pgrVerificarApi;
        private ProgressBar pgrCargarDB;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnVerificarApi;
        private Button btnCargarDB;
        private Button btnDeleteBD;
        private Button btnMakeBackUp;
        private Button btnDeleteBackups;
        private TextBox txtDateBackup;
        private ComboBox cbxStore;
        private Button btnCargarRegistros;
        private Label lblPuntoAsistencia;
        private TextBox txtBackupMonth;
    }
}