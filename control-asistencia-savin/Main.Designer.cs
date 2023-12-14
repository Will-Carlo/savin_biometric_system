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
            btnRegistrar = new Button();
            btnVerificar = new Button();
            label1 = new Label();
            label2 = new Label();
            lblTime = new Label();
            textBox1 = new TextBox();
            linkLabel1 = new LinkLabel();
            label4 = new Label();
            label5 = new Label();
            dsa = new Label();
            lblNombre = new Label();
            lblHora = new Label();
            tmrTime = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // btnRegistrar
            // 
            btnRegistrar.Location = new Point(784, 436);
            btnRegistrar.Margin = new Padding(4, 3, 4, 3);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(138, 46);
            btnRegistrar.TabIndex = 0;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // btnVerificar
            // 
            btnVerificar.Location = new Point(136, 202);
            btnVerificar.Margin = new Padding(4, 3, 4, 3);
            btnVerificar.Name = "btnVerificar";
            btnVerificar.Size = new Size(138, 46);
            btnVerificar.TabIndex = 1;
            btnVerificar.Text = "Leer Huella";
            btnVerificar.UseVisualStyleBackColor = true;
            btnVerificar.Click += btnVerificar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(362, 59);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(208, 15);
            label1.TabIndex = 2;
            label1.Text = "Sistema de Asistencia: PUNTO ZAPATA";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(421, 108);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(65, 15);
            label2.TabIndex = 3;
            label2.Text = "12/12/2023";
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Location = new Point(440, 136);
            lblTime.Margin = new Padding(4, 0, 4, 0);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(49, 15);
            lblTime.TabIndex = 4;
            lblTime.Text = "18:42:02";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(310, 215);
            textBox1.Margin = new Padding(4, 3, 4, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(439, 23);
            textBox1.TabIndex = 5;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(690, 268);
            linkLabel1.Margin = new Padding(4, 0, 4, 0);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(57, 15);
            linkLabel1.TabIndex = 6;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Opciones";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(166, 361);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(93, 15);
            label4.TabIndex = 7;
            label4.Text = "Último Marcado";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(240, 436);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(36, 15);
            label5.TabIndex = 9;
            label5.Text = "Hora:";
            // 
            // dsa
            // 
            dsa.AutoSize = true;
            dsa.Location = new Point(222, 408);
            dsa.Margin = new Padding(4, 0, 4, 0);
            dsa.Name = "dsa";
            dsa.Size = new Size(55, 15);
            dsa.TabIndex = 8;
            dsa.Text = "Personal:";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(288, 408);
            lblNombre.Margin = new Padding(4, 0, 4, 0);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(64, 15);
            lblNombre.TabIndex = 10;
            lblNombre.Text = "lblNombre";
            lblNombre.Visible = false;
            // 
            // lblHora
            // 
            lblHora.AutoSize = true;
            lblHora.Location = new Point(288, 436);
            lblHora.Margin = new Padding(4, 0, 4, 0);
            lblHora.Name = "lblHora";
            lblHora.Size = new Size(46, 15);
            lblHora.TabIndex = 11;
            lblHora.Text = "lblHora";
            lblHora.Visible = false;
            // 
            // tmrTime
            // 
            tmrTime.Enabled = true;
            tmrTime.Interval = 1000;
            tmrTime.Tick += tmrTime_Tick;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(936, 496);
            Controls.Add(lblHora);
            Controls.Add(lblNombre);
            Controls.Add(label5);
            Controls.Add(dsa);
            Controls.Add(label4);
            Controls.Add(linkLabel1);
            Controls.Add(textBox1);
            Controls.Add(lblTime);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnVerificar);
            Controls.Add(btnRegistrar);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Main";
            Text = "Main";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Button btnVerificar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label dsa;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Timer tmrTime;
    }
}

