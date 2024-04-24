namespace control_asistencia_savin
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            lblSisAsis = new Label();
            lblFecha = new Label();
            lblTime = new Label();
            tmrTime = new System.Windows.Forms.Timer(components);
            label5 = new Label();
            label4 = new Label();
            lblPunto = new Label();
            lnkMarcarCodigo = new LinkLabel();
            lnkApiTest = new LinkLabel();
            lnkRegistrar = new LinkLabel();
            lnkVerAtrasos = new LinkLabel();
            lnkInicio = new LinkLabel();
            lnkOpciones = new LinkLabel();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            lnkVerAsistencias = new LinkLabel();
            lnkMarcar2 = new LinkLabel();
            lnkFakeRegister = new LinkLabel();
            panel2 = new Panel();
            lnkSincronizarRegistros = new LinkLabel();
            pctWarning = new PictureBox();
            lblLogOut = new Label();
            pictureBox2 = new PictureBox();
            pnlInfoStore = new Panel();
            pnlHora = new Panel();
            pnlBase = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pctWarning).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            pnlInfoStore.SuspendLayout();
            pnlHora.SuspendLayout();
            SuspendLayout();
            // 
            // lblSisAsis
            // 
            lblSisAsis.Anchor = AnchorStyles.Top;
            lblSisAsis.AutoSize = true;
            lblSisAsis.Font = new Font("Tahoma", 48F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSisAsis.ForeColor = Color.FromArgb(10, 38, 102);
            lblSisAsis.Location = new Point(142, 9);
            lblSisAsis.Margin = new Padding(4, 0, 4, 0);
            lblSisAsis.Name = "lblSisAsis";
            lblSisAsis.Size = new Size(732, 77);
            lblSisAsis.TabIndex = 2;
            lblSisAsis.Text = "Sistema de Asistencia";
            // 
            // lblFecha
            // 
            lblFecha.Anchor = AnchorStyles.Top;
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Tahoma", 25F, FontStyle.Bold);
            lblFecha.ForeColor = Color.FromArgb(10, 38, 102);
            lblFecha.Location = new Point(437, 14);
            lblFecha.Margin = new Padding(4, 0, 4, 0);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(234, 41);
            lblFecha.TabIndex = 3;
            lblFecha.Text = "12/12/2023";
            // 
            // lblTime
            // 
            lblTime.Anchor = AnchorStyles.Top;
            lblTime.AutoSize = true;
            lblTime.Font = new Font("Tahoma", 25F, FontStyle.Bold);
            lblTime.ForeColor = Color.FromArgb(10, 38, 102);
            lblTime.Location = new Point(463, 55);
            lblTime.Margin = new Padding(4, 0, 4, 0);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(174, 41);
            lblTime.TabIndex = 4;
            lblTime.Text = "18:42:02";
            // 
            // tmrTime
            // 
            tmrTime.Enabled = true;
            tmrTime.Interval = 1000;
            tmrTime.Tick += tmrTime_Tick;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.Font = new Font("Tahoma", 24.75F);
            label5.ForeColor = Color.FromArgb(10, 38, 102);
            label5.Location = new Point(358, 57);
            label5.Name = "label5";
            label5.Size = new Size(98, 40);
            label5.TabIndex = 22;
            label5.Text = "Hora:";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new Font("Tahoma", 24.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(10, 38, 102);
            label4.Location = new Point(317, 14);
            label4.Name = "label4";
            label4.Size = new Size(113, 40);
            label4.TabIndex = 21;
            label4.Text = "Fecha:";
            // 
            // lblPunto
            // 
            lblPunto.Anchor = AnchorStyles.Top;
            lblPunto.AutoSize = true;
            lblPunto.Cursor = Cursors.No;
            lblPunto.Font = new Font("Tahoma", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPunto.ForeColor = Color.FromArgb(10, 38, 102);
            lblPunto.Location = new Point(344, 86);
            lblPunto.Margin = new Padding(4, 0, 4, 0);
            lblPunto.Name = "lblPunto";
            lblPunto.Size = new Size(316, 58);
            lblPunto.TabIndex = 19;
            lblPunto.Text = "Punto: label";
            lblPunto.TextAlign = ContentAlignment.TopCenter;
            // 
            // lnkMarcarCodigo
            // 
            lnkMarcarCodigo.Anchor = AnchorStyles.Left;
            lnkMarcarCodigo.AutoSize = true;
            lnkMarcarCodigo.BackColor = Color.FromArgb(10, 38, 102);
            lnkMarcarCodigo.Font = new Font("Segoe UI", 9F);
            lnkMarcarCodigo.LinkColor = Color.FromArgb(241, 206, 0);
            lnkMarcarCodigo.Location = new Point(66, 345);
            lnkMarcarCodigo.Margin = new Padding(4, 0, 4, 0);
            lnkMarcarCodigo.Name = "lnkMarcarCodigo";
            lnkMarcarCodigo.Size = new Size(105, 15);
            lnkMarcarCodigo.TabIndex = 24;
            lnkMarcarCodigo.TabStop = true;
            lnkMarcarCodigo.Text = "Marcar por código";
            lnkMarcarCodigo.Visible = false;
            lnkMarcarCodigo.LinkClicked += lnkMarcarCodigo_LinkClicked;
            // 
            // lnkApiTest
            // 
            lnkApiTest.Anchor = AnchorStyles.Left;
            lnkApiTest.AutoSize = true;
            lnkApiTest.BackColor = Color.FromArgb(10, 38, 102);
            lnkApiTest.Font = new Font("Segoe UI", 9F);
            lnkApiTest.LinkColor = Color.FromArgb(241, 206, 0);
            lnkApiTest.Location = new Point(66, 426);
            lnkApiTest.Margin = new Padding(4, 0, 4, 0);
            lnkApiTest.Name = "lnkApiTest";
            lnkApiTest.Size = new Size(98, 15);
            lnkApiTest.TabIndex = 25;
            lnkApiTest.TabStop = true;
            lnkApiTest.Text = "Verificar API REST";
            lnkApiTest.Visible = false;
            lnkApiTest.LinkClicked += lnkApiTest_LinkClicked;
            // 
            // lnkRegistrar
            // 
            lnkRegistrar.Anchor = AnchorStyles.Left;
            lnkRegistrar.AutoSize = true;
            lnkRegistrar.BackColor = Color.FromArgb(10, 38, 102);
            lnkRegistrar.Font = new Font("Segoe UI", 9F);
            lnkRegistrar.LinkColor = Color.FromArgb(241, 206, 0);
            lnkRegistrar.Location = new Point(66, 399);
            lnkRegistrar.Margin = new Padding(4, 0, 4, 0);
            lnkRegistrar.Name = "lnkRegistrar";
            lnkRegistrar.Size = new Size(88, 15);
            lnkRegistrar.TabIndex = 17;
            lnkRegistrar.TabStop = true;
            lnkRegistrar.Text = "Registrar huella";
            lnkRegistrar.Visible = false;
            lnkRegistrar.LinkClicked += lnkRegistrar_LinkClicked;
            // 
            // lnkVerAtrasos
            // 
            lnkVerAtrasos.Anchor = AnchorStyles.Left;
            lnkVerAtrasos.AutoSize = true;
            lnkVerAtrasos.BackColor = Color.FromArgb(10, 38, 102);
            lnkVerAtrasos.Font = new Font("Segoe UI", 9F);
            lnkVerAtrasos.LinkColor = Color.FromArgb(241, 206, 0);
            lnkVerAtrasos.Location = new Point(66, 372);
            lnkVerAtrasos.Margin = new Padding(4, 0, 4, 0);
            lnkVerAtrasos.Name = "lnkVerAtrasos";
            lnkVerAtrasos.Size = new Size(138, 15);
            lnkVerAtrasos.TabIndex = 16;
            lnkVerAtrasos.TabStop = true;
            lnkVerAtrasos.Text = "Ver minutos acumulados";
            lnkVerAtrasos.Visible = false;
            lnkVerAtrasos.LinkClicked += linkLabel2_LinkClicked;
            // 
            // lnkInicio
            // 
            lnkInicio.Anchor = AnchorStyles.Left;
            lnkInicio.AutoSize = true;
            lnkInicio.BackColor = Color.FromArgb(10, 38, 102);
            lnkInicio.Font = new Font("Segoe UI", 9F);
            lnkInicio.LinkColor = Color.FromArgb(241, 206, 0);
            lnkInicio.Location = new Point(66, 320);
            lnkInicio.Margin = new Padding(4, 0, 4, 0);
            lnkInicio.Name = "lnkInicio";
            lnkInicio.Size = new Size(100, 15);
            lnkInicio.TabIndex = 18;
            lnkInicio.TabStop = true;
            lnkInicio.Text = "Marcar por huella";
            lnkInicio.Visible = false;
            lnkInicio.LinkClicked += lnkInicio_LinkClicked;
            // 
            // lnkOpciones
            // 
            lnkOpciones.Anchor = AnchorStyles.Left;
            lnkOpciones.AutoSize = true;
            lnkOpciones.BackColor = Color.FromArgb(10, 38, 102);
            lnkOpciones.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lnkOpciones.LinkColor = Color.FromArgb(241, 206, 0);
            lnkOpciones.Location = new Point(39, 286);
            lnkOpciones.Margin = new Padding(4, 0, 4, 0);
            lnkOpciones.Name = "lnkOpciones";
            lnkOpciones.Size = new Size(71, 20);
            lnkOpciones.TabIndex = 15;
            lnkOpciones.TabStop = true;
            lnkOpciones.Text = "Opciones";
            lnkOpciones.LinkClicked += linkLabel1_LinkClicked;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Top;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(225, 172);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 20;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 37, 105);
            panel1.Controls.Add(lnkVerAsistencias);
            panel1.Controls.Add(lnkMarcar2);
            panel1.Controls.Add(lnkFakeRegister);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(lnkMarcarCodigo);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(lnkApiTest);
            panel1.Controls.Add(lnkOpciones);
            panel1.Controls.Add(lnkRegistrar);
            panel1.Controls.Add(lnkInicio);
            panel1.Controls.Add(lnkVerAtrasos);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(225, 729);
            panel1.TabIndex = 26;
            // 
            // lnkVerAsistencias
            // 
            lnkVerAsistencias.Anchor = AnchorStyles.Left;
            lnkVerAsistencias.AutoSize = true;
            lnkVerAsistencias.BackColor = Color.FromArgb(10, 38, 102);
            lnkVerAsistencias.Font = new Font("Segoe UI", 9F);
            lnkVerAsistencias.LinkColor = Color.FromArgb(241, 206, 0);
            lnkVerAsistencias.Location = new Point(66, 510);
            lnkVerAsistencias.Margin = new Padding(4, 0, 4, 0);
            lnkVerAsistencias.Name = "lnkVerAsistencias";
            lnkVerAsistencias.Size = new Size(82, 15);
            lnkVerAsistencias.TabIndex = 31;
            lnkVerAsistencias.TabStop = true;
            lnkVerAsistencias.Text = "Ver asistencias";
            lnkVerAsistencias.Visible = false;
            lnkVerAsistencias.LinkClicked += lnkVerAsistencias_LinkClicked;
            // 
            // lnkMarcar2
            // 
            lnkMarcar2.Anchor = AnchorStyles.Left;
            lnkMarcar2.AutoSize = true;
            lnkMarcar2.BackColor = Color.FromArgb(10, 38, 102);
            lnkMarcar2.Font = new Font("Segoe UI", 9F);
            lnkMarcar2.LinkColor = Color.FromArgb(241, 206, 0);
            lnkMarcar2.Location = new Point(66, 482);
            lnkMarcar2.Margin = new Padding(4, 0, 4, 0);
            lnkMarcar2.Name = "lnkMarcar2";
            lnkMarcar2.Size = new Size(118, 15);
            lnkMarcar2.TabIndex = 30;
            lnkMarcar2.TabStop = true;
            lnkMarcar2.Text = "Marcar por huella 2.0";
            lnkMarcar2.Visible = false;
            lnkMarcar2.LinkClicked += lnkMarcar2_LinkClicked;
            // 
            // lnkFakeRegister
            // 
            lnkFakeRegister.Anchor = AnchorStyles.Left;
            lnkFakeRegister.AutoSize = true;
            lnkFakeRegister.BackColor = Color.FromArgb(10, 38, 102);
            lnkFakeRegister.Font = new Font("Segoe UI", 9F);
            lnkFakeRegister.LinkColor = Color.FromArgb(241, 206, 0);
            lnkFakeRegister.Location = new Point(66, 454);
            lnkFakeRegister.Margin = new Padding(4, 0, 4, 0);
            lnkFakeRegister.Name = "lnkFakeRegister";
            lnkFakeRegister.Size = new Size(88, 15);
            lnkFakeRegister.TabIndex = 29;
            lnkFakeRegister.TabStop = true;
            lnkFakeRegister.Text = "Registros falsos";
            lnkFakeRegister.Visible = false;
            lnkFakeRegister.LinkClicked += lnkFakeRegister_LinkClicked;
            // 
            // panel2
            // 
            panel2.Controls.Add(lnkSincronizarRegistros);
            panel2.Controls.Add(pctWarning);
            panel2.Controls.Add(lblLogOut);
            panel2.Controls.Add(pictureBox2);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 602);
            panel2.Name = "panel2";
            panel2.Size = new Size(225, 127);
            panel2.TabIndex = 28;
            // 
            // lnkSincronizarRegistros
            // 
            lnkSincronizarRegistros.Anchor = AnchorStyles.Left;
            lnkSincronizarRegistros.AutoSize = true;
            lnkSincronizarRegistros.BackColor = Color.FromArgb(10, 38, 102);
            lnkSincronizarRegistros.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lnkSincronizarRegistros.LinkColor = Color.FromArgb(241, 206, 0);
            lnkSincronizarRegistros.Location = new Point(67, 34);
            lnkSincronizarRegistros.Margin = new Padding(4, 0, 4, 0);
            lnkSincronizarRegistros.Name = "lnkSincronizarRegistros";
            lnkSincronizarRegistros.Size = new Size(112, 13);
            lnkSincronizarRegistros.TabIndex = 32;
            lnkSincronizarRegistros.TabStop = true;
            lnkSincronizarRegistros.Text = "Sincronizar registros";
            lnkSincronizarRegistros.Visible = false;
            lnkSincronizarRegistros.LinkClicked += lnkSincronizarRegistros_LinkClicked;
            // 
            // pctWarning
            // 
            pctWarning.Image = (Image)resources.GetObject("pctWarning.Image");
            pctWarning.Location = new Point(34, 27);
            pctWarning.Name = "pctWarning";
            pctWarning.Size = new Size(29, 29);
            pctWarning.SizeMode = PictureBoxSizeMode.Zoom;
            pctWarning.TabIndex = 23;
            pctWarning.TabStop = false;
            pctWarning.Visible = false;
            // 
            // lblLogOut
            // 
            lblLogOut.AutoSize = true;
            lblLogOut.Cursor = Cursors.Hand;
            lblLogOut.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLogOut.ForeColor = Color.FromArgb(241, 206, 0);
            lblLogOut.Location = new Point(67, 73);
            lblLogOut.Name = "lblLogOut";
            lblLogOut.Size = new Size(103, 21);
            lblLogOut.TabIndex = 27;
            lblLogOut.Text = "Cerrar seeión";
            lblLogOut.Click += lblLogOut_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Cursor = Cursors.Hand;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(30, 66);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(34, 34);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 26;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pnlInfoStore
            // 
            pnlInfoStore.BackColor = Color.White;
            pnlInfoStore.Controls.Add(lblPunto);
            pnlInfoStore.Controls.Add(lblSisAsis);
            pnlInfoStore.Dock = DockStyle.Top;
            pnlInfoStore.Location = new Point(225, 0);
            pnlInfoStore.Name = "pnlInfoStore";
            pnlInfoStore.Size = new Size(1125, 172);
            pnlInfoStore.TabIndex = 27;
            // 
            // pnlHora
            // 
            pnlHora.BackColor = Color.White;
            pnlHora.Controls.Add(label4);
            pnlHora.Controls.Add(lblFecha);
            pnlHora.Controls.Add(lblTime);
            pnlHora.Controls.Add(label5);
            pnlHora.Dock = DockStyle.Top;
            pnlHora.Location = new Point(225, 172);
            pnlHora.Name = "pnlHora";
            pnlHora.Size = new Size(1125, 100);
            pnlHora.TabIndex = 28;
            // 
            // pnlBase
            // 
            pnlBase.BackColor = Color.IndianRed;
            pnlBase.Dock = DockStyle.Fill;
            pnlBase.Location = new Point(225, 272);
            pnlBase.Name = "pnlBase";
            pnlBase.Size = new Size(1125, 457);
            pnlBase.TabIndex = 29;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            BackColor = Color.White;
            ClientSize = new Size(1350, 729);
            Controls.Add(pnlBase);
            Controls.Add(pnlHora);
            Controls.Add(pnlInfoStore);
            Controls.Add(panel1);
            ForeColor = Color.DimGray;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "Main";
            Text = "Savin";
            FormClosing += Main_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pctWarning).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            pnlInfoStore.ResumeLayout(false);
            pnlInfoStore.PerformLayout();
            pnlHora.ResumeLayout(false);
            pnlHora.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Label lblSisAsis;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer tmrTime;
        private LinkLabel lnkOpciones;
        private LinkLabel lnkVerAtrasos;
        private LinkLabel lnkRegistrar;
        private LinkLabel lnkInicio;
        private Label lblPunto;
        private PictureBox pictureBox1;
        private Label label4;
        private Label label5;
        private LinkLabel linkLabel2;
        private LinkLabel lnkVerAtrasosMes;
        private LinkLabel lnkMarcarCodigo;
        private LinkLabel lnkApiTest;
        private Panel panel1;
        private Panel pnlInfoStore;
        private Panel pnlHora;
        private Panel pnlBase;
        private PictureBox pictureBox2;
        private Label lblLogOut;
        private Panel panel2;
        private LinkLabel lnkFakeRegister;
        private LinkLabel lnkMarcar2;
        private LinkLabel lnkVerAsistencias;
        private Label label1;
        private PictureBox picWarning;
        private LinkLabel lnkSincronizarRegistros;
        private PictureBox pctWarning;
    }
}

