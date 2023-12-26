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
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnDeleteBD = new Button();
            btnMakeBackUp = new Button();
            btnPruebaB64 = new Button();
            SuspendLayout();
            // 
            // btnVerificarApi
            // 
            btnVerificarApi.Location = new Point(195, 97);
            btnVerificarApi.Name = "btnVerificarApi";
            btnVerificarApi.Size = new Size(125, 23);
            btnVerificarApi.TabIndex = 0;
            btnVerificarApi.Text = "Verificar Conexión";
            btnVerificarApi.UseVisualStyleBackColor = true;
            btnVerificarApi.Click += btnVerificarApi_Click;
            // 
            // pgrVerificarApi
            // 
            pgrVerificarApi.Location = new Point(360, 97);
            pgrVerificarApi.Name = "pgrVerificarApi";
            pgrVerificarApi.Size = new Size(100, 23);
            pgrVerificarApi.TabIndex = 1;
            // 
            // pgrCargarDB
            // 
            pgrCargarDB.Location = new Point(360, 160);
            pgrCargarDB.Name = "pgrCargarDB";
            pgrCargarDB.Size = new Size(100, 23);
            pgrCargarDB.TabIndex = 3;
            // 
            // btnCargarDB
            // 
            btnCargarDB.Location = new Point(195, 160);
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
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Location = new Point(490, 46);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(186, 199);
            flowLayoutPanel1.TabIndex = 5;
            // 
            // btnDeleteBD
            // 
            btnDeleteBD.Location = new Point(215, 208);
            btnDeleteBD.Name = "btnDeleteBD";
            btnDeleteBD.Size = new Size(75, 23);
            btnDeleteBD.TabIndex = 6;
            btnDeleteBD.Text = "borrar bd";
            btnDeleteBD.UseVisualStyleBackColor = true;
            btnDeleteBD.Click += btnDeleteBD_Click;
            // 
            // btnMakeBackUp
            // 
            btnMakeBackUp.Location = new Point(223, 251);
            btnMakeBackUp.Name = "btnMakeBackUp";
            btnMakeBackUp.Size = new Size(75, 23);
            btnMakeBackUp.TabIndex = 7;
            btnMakeBackUp.Text = "make copy";
            btnMakeBackUp.UseVisualStyleBackColor = true;
            btnMakeBackUp.Click += btnMakeBackUp_Click;
            // 
            // btnPruebaB64
            // 
            btnPruebaB64.Location = new Point(390, 266);
            btnPruebaB64.Name = "btnPruebaB64";
            btnPruebaB64.Size = new Size(130, 23);
            btnPruebaB64.TabIndex = 8;
            btnPruebaB64.Text = "Convertir base 64";
            btnPruebaB64.UseVisualStyleBackColor = true;
            btnPruebaB64.Click += btnPruebaB64_Click;
            // 
            // frmTestApi
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(688, 295);
            Controls.Add(btnPruebaB64);
            Controls.Add(btnMakeBackUp);
            Controls.Add(btnDeleteBD);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(pgrCargarDB);
            Controls.Add(btnCargarDB);
            Controls.Add(pgrVerificarApi);
            Controls.Add(btnVerificarApi);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmTestApi";
            Text = "TestApi";
            ResumeLayout(false);
        }

        #endregion

        private Button btnVerificarApi;
        private ProgressBar pgrVerificarApi;
        private ProgressBar pgrCargarDB;
        private Button btnCargarDB;
        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btnDeleteBD;
        private Button btnMakeBackUp;
        private Button btnPruebaB64;
    }
}