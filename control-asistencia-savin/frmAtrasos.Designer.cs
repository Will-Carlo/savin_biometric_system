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
            lblMinAtraso = new Label();
            lblNombre = new Label();
            label5 = new Label();
            dsa = new Label();
            label4 = new Label();
            btnVerificar = new Button();
            tmrTime = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // lblMinAtraso
            // 
            lblMinAtraso.AutoSize = true;
            lblMinAtraso.Location = new Point(437, 223);
            lblMinAtraso.Margin = new Padding(4, 0, 4, 0);
            lblMinAtraso.Name = "lblMinAtraso";
            lblMinAtraso.Size = new Size(75, 15);
            lblMinAtraso.TabIndex = 23;
            lblMinAtraso.Text = "lblMinAtraso";
            lblMinAtraso.Visible = false;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(437, 195);
            lblNombre.Margin = new Padding(4, 0, 4, 0);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(64, 15);
            lblNombre.TabIndex = 22;
            lblNombre.Text = "lblNombre";
            lblNombre.Visible = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(282, 223);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(147, 15);
            label5.TabIndex = 21;
            label5.Text = "Minutos acumulados mes:";
            // 
            // dsa
            // 
            dsa.AutoSize = true;
            dsa.Location = new Point(371, 195);
            dsa.Margin = new Padding(4, 0, 4, 0);
            dsa.Name = "dsa";
            dsa.Size = new Size(55, 15);
            dsa.TabIndex = 20;
            dsa.Text = "Personal:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(349, 157);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(152, 15);
            label4.TabIndex = 19;
            label4.Text = "Horas de atraso acumulado";
            // 
            // btnVerificar
            // 
            btnVerificar.Location = new Point(133, 147);
            btnVerificar.Margin = new Padding(4, 3, 4, 3);
            btnVerificar.Name = "btnVerificar";
            btnVerificar.Size = new Size(119, 110);
            btnVerificar.TabIndex = 13;
            btnVerificar.Text = "Leer Huella";
            btnVerificar.UseVisualStyleBackColor = true;
            btnVerificar.Click += btnVerificar_Click;
            // 
            // tmrTime
            // 
            tmrTime.Enabled = true;
            tmrTime.Interval = 1000;
            // 
            // frmAtrasos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblMinAtraso);
            Controls.Add(lblNombre);
            Controls.Add(label5);
            Controls.Add(dsa);
            Controls.Add(label4);
            Controls.Add(btnVerificar);
            Name = "frmAtrasos";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMinAtraso;
        private Label lblNombre;
        private Label label5;
        private Label dsa;
        private Label label4;
        private Button btnVerificar;
        private System.Windows.Forms.Timer tmrTime;
    }
}