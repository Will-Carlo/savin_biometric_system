namespace control_asistencia_savin.Frm
{
    partial class frmAsistenciaSimulation
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
            btnRegistrarFake = new Button();
            txtIdPersonal = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtFecha = new TextBox();
            btnRegFaltas = new Button();
            SuspendLayout();
            // 
            // btnRegistrarFake
            // 
            btnRegistrarFake.Location = new Point(279, 249);
            btnRegistrarFake.Name = "btnRegistrarFake";
            btnRegistrarFake.Size = new Size(193, 23);
            btnRegistrarFake.TabIndex = 0;
            btnRegistrarFake.Text = "Registrar Fake";
            btnRegistrarFake.UseVisualStyleBackColor = true;
            btnRegistrarFake.Click += button1_Click;
            // 
            // txtIdPersonal
            // 
            txtIdPersonal.Location = new Point(372, 122);
            txtIdPersonal.Name = "txtIdPersonal";
            txtIdPersonal.Size = new Size(52, 23);
            txtIdPersonal.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(279, 125);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 2;
            label1.Text = "IdPersonal";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(279, 172);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 3;
            label2.Text = "Fecha";
            // 
            // txtFecha
            // 
            txtFecha.Location = new Point(374, 169);
            txtFecha.Name = "txtFecha";
            txtFecha.Size = new Size(154, 23);
            txtFecha.TabIndex = 4;
            // 
            // btnRegFaltas
            // 
            btnRegFaltas.Location = new Point(670, 122);
            btnRegFaltas.Name = "btnRegFaltas";
            btnRegFaltas.Size = new Size(235, 23);
            btnRegFaltas.TabIndex = 5;
            btnRegFaltas.Text = "Registrar Faltas";
            btnRegFaltas.UseVisualStyleBackColor = true;
            btnRegFaltas.Click += btnRegFaltas_Click;
            // 
            // frmAsistenciaSimulation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1109, 418);
            Controls.Add(btnRegFaltas);
            Controls.Add(txtFecha);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtIdPersonal);
            Controls.Add(btnRegistrarFake);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmAsistenciaSimulation";
            Text = "frmAsistenciaSimulation";
            Load += frmAsistenciaSimulation_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRegistrarFake;
        private TextBox txtIdPersonal;
        private Label label1;
        private Label label2;
        private TextBox txtFecha;
        private Button btnRegFaltas;
    }
}