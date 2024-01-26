namespace control_asistencia_savin
{
    partial class frmRegistrar
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
            label1 = new Label();
            label2 = new Label();
            btnGuardarUsuario = new Button();
            txtPaterno = new TextBox();
            txtIndiceDerecho = new TextBox();
            btnRegIndDer = new Button();
            dgvListar = new DataGridView();
            txtId_ciudad = new TextBox();
            label3 = new Label();
            txtMaterno = new TextBox();
            label4 = new Label();
            txtNombre = new TextBox();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            txtIndiceIzquierdo = new TextBox();
            label8 = new Label();
            txtPulgarDerecho = new TextBox();
            label9 = new Label();
            txtPulgarIzquierdo = new TextBox();
            btnRegIndIzq = new Button();
            btnRegPulDer = new Button();
            btnRegPulIzq = new Button();
            btnReportTxt = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvListar).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(51, 59);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 0;
            label1.Text = "Paterno:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(55, 199);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(44, 15);
            label2.TabIndex = 1;
            label2.Text = "Huella:";
            // 
            // btnGuardarUsuario
            // 
            btnGuardarUsuario.Enabled = false;
            btnGuardarUsuario.Location = new Point(955, 418);
            btnGuardarUsuario.Margin = new Padding(4, 3, 4, 3);
            btnGuardarUsuario.Name = "btnGuardarUsuario";
            btnGuardarUsuario.Size = new Size(157, 27);
            btnGuardarUsuario.TabIndex = 2;
            btnGuardarUsuario.Text = "Guardar Usuario";
            btnGuardarUsuario.UseVisualStyleBackColor = true;
            btnGuardarUsuario.Click += btnAgregar_Click;
            // 
            // txtPaterno
            // 
            txtPaterno.Location = new Point(114, 59);
            txtPaterno.Margin = new Padding(4, 3, 4, 3);
            txtPaterno.Name = "txtPaterno";
            txtPaterno.Size = new Size(224, 23);
            txtPaterno.TabIndex = 3;
            // 
            // txtIndiceDerecho
            // 
            txtIndiceDerecho.Enabled = false;
            txtIndiceDerecho.Location = new Point(114, 238);
            txtIndiceDerecho.Margin = new Padding(4, 3, 4, 3);
            txtIndiceDerecho.Name = "txtIndiceDerecho";
            txtIndiceDerecho.Size = new Size(131, 23);
            txtIndiceDerecho.TabIndex = 4;
            // 
            // btnRegIndDer
            // 
            btnRegIndDer.Location = new Point(259, 234);
            btnRegIndDer.Margin = new Padding(4, 3, 4, 3);
            btnRegIndDer.Name = "btnRegIndDer";
            btnRegIndDer.Size = new Size(79, 27);
            btnRegIndDer.TabIndex = 5;
            btnRegIndDer.Text = "Registrar Huella";
            btnRegIndDer.UseVisualStyleBackColor = true;
            btnRegIndDer.Click += btnRegIndDer_Click;
            // 
            // dgvListar
            // 
            dgvListar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvListar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvListar.Location = new Point(355, 12);
            dgvListar.Margin = new Padding(4, 3, 4, 3);
            dgvListar.Name = "dgvListar";
            dgvListar.Size = new Size(757, 400);
            dgvListar.TabIndex = 6;
            // 
            // txtId_ciudad
            // 
            txtId_ciudad.Location = new Point(114, 14);
            txtId_ciudad.Margin = new Padding(4, 3, 4, 3);
            txtId_ciudad.Name = "txtId_ciudad";
            txtId_ciudad.Size = new Size(224, 23);
            txtId_ciudad.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(51, 14);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(61, 15);
            label3.TabIndex = 7;
            label3.Text = "Id_ciudad:";
            // 
            // txtMaterno
            // 
            txtMaterno.Location = new Point(114, 102);
            txtMaterno.Margin = new Padding(4, 3, 4, 3);
            txtMaterno.Name = "txtMaterno";
            txtMaterno.Size = new Size(224, 23);
            txtMaterno.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(51, 102);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 9;
            label4.Text = "Materno:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(114, 145);
            txtNombre.Margin = new Padding(4, 3, 4, 3);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(224, 23);
            txtNombre.TabIndex = 12;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(51, 145);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(54, 15);
            label5.TabIndex = 11;
            label5.Text = "Nombre:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(51, 241);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(45, 15);
            label6.TabIndex = 13;
            label6.Text = "Ind Der";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(51, 280);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(42, 15);
            label7.TabIndex = 15;
            label7.Text = "Ind Izq";
            // 
            // txtIndiceIzquierdo
            // 
            txtIndiceIzquierdo.Enabled = false;
            txtIndiceIzquierdo.Location = new Point(114, 277);
            txtIndiceIzquierdo.Margin = new Padding(4, 3, 4, 3);
            txtIndiceIzquierdo.Name = "txtIndiceIzquierdo";
            txtIndiceIzquierdo.Size = new Size(131, 23);
            txtIndiceIzquierdo.TabIndex = 14;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(51, 322);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(45, 15);
            label8.TabIndex = 17;
            label8.Text = "Pul Der";
            // 
            // txtPulgarDerecho
            // 
            txtPulgarDerecho.Enabled = false;
            txtPulgarDerecho.Location = new Point(114, 319);
            txtPulgarDerecho.Margin = new Padding(4, 3, 4, 3);
            txtPulgarDerecho.Name = "txtPulgarDerecho";
            txtPulgarDerecho.Size = new Size(131, 23);
            txtPulgarDerecho.TabIndex = 16;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(51, 364);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(42, 15);
            label9.TabIndex = 19;
            label9.Text = "Pul izq";
            // 
            // txtPulgarIzquierdo
            // 
            txtPulgarIzquierdo.Enabled = false;
            txtPulgarIzquierdo.Location = new Point(114, 361);
            txtPulgarIzquierdo.Margin = new Padding(4, 3, 4, 3);
            txtPulgarIzquierdo.Name = "txtPulgarIzquierdo";
            txtPulgarIzquierdo.Size = new Size(131, 23);
            txtPulgarIzquierdo.TabIndex = 18;
            // 
            // btnRegIndIzq
            // 
            btnRegIndIzq.Location = new Point(259, 277);
            btnRegIndIzq.Margin = new Padding(4, 3, 4, 3);
            btnRegIndIzq.Name = "btnRegIndIzq";
            btnRegIndIzq.Size = new Size(79, 27);
            btnRegIndIzq.TabIndex = 20;
            btnRegIndIzq.Text = "Registrar Huella";
            btnRegIndIzq.UseVisualStyleBackColor = true;
            btnRegIndIzq.Click += btnRegIndIzq_Click;
            // 
            // btnRegPulDer
            // 
            btnRegPulDer.Location = new Point(259, 316);
            btnRegPulDer.Margin = new Padding(4, 3, 4, 3);
            btnRegPulDer.Name = "btnRegPulDer";
            btnRegPulDer.Size = new Size(79, 27);
            btnRegPulDer.TabIndex = 21;
            btnRegPulDer.Text = "Registrar Huella";
            btnRegPulDer.UseVisualStyleBackColor = true;
            btnRegPulDer.Click += btnRegPulDer_Click;
            // 
            // btnRegPulIzq
            // 
            btnRegPulIzq.Location = new Point(259, 357);
            btnRegPulIzq.Margin = new Padding(4, 3, 4, 3);
            btnRegPulIzq.Name = "btnRegPulIzq";
            btnRegPulIzq.Size = new Size(79, 27);
            btnRegPulIzq.TabIndex = 22;
            btnRegPulIzq.Text = "Registrar Huella";
            btnRegPulIzq.UseVisualStyleBackColor = true;
            btnRegPulIzq.Click += btnRegPulIzq_Click;
            // 
            // btnReportTxt
            // 
            btnReportTxt.Location = new Point(355, 420);
            btnReportTxt.Name = "btnReportTxt";
            btnReportTxt.Size = new Size(138, 23);
            btnReportTxt.TabIndex = 23;
            btnReportTxt.Text = "Generar Reporte";
            btnReportTxt.UseVisualStyleBackColor = true;
            btnReportTxt.Click += btnReportTxt_Click;
            // 
            // frmRegistrar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1125, 457);
            Controls.Add(btnReportTxt);
            Controls.Add(btnRegPulIzq);
            Controls.Add(btnRegPulDer);
            Controls.Add(btnRegIndIzq);
            Controls.Add(label9);
            Controls.Add(txtPulgarIzquierdo);
            Controls.Add(label8);
            Controls.Add(txtPulgarDerecho);
            Controls.Add(label7);
            Controls.Add(txtIndiceIzquierdo);
            Controls.Add(label6);
            Controls.Add(txtNombre);
            Controls.Add(label5);
            Controls.Add(txtMaterno);
            Controls.Add(label4);
            Controls.Add(txtId_ciudad);
            Controls.Add(label3);
            Controls.Add(dgvListar);
            Controls.Add(btnRegIndDer);
            Controls.Add(txtIndiceDerecho);
            Controls.Add(txtPaterno);
            Controls.Add(btnGuardarUsuario);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmRegistrar";
            Text = "frmRegistrar";
            Load += frmRegistrar_Load;
            ((System.ComponentModel.ISupportInitialize)dgvListar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGuardarUsuario;
        private System.Windows.Forms.TextBox txtPaterno;
        private System.Windows.Forms.TextBox txtIndiceDerecho;
        private System.Windows.Forms.Button btnRegIndDer;
        private System.Windows.Forms.DataGridView dgvListar;
        private System.Windows.Forms.TextBox txtId_ciudad;
        private System.Windows.Forms.Label label3;
        private TextBox txtMaterno;
        private Label label4;
        private TextBox txtNombre;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox txtIndiceIzquierdo;
        private Label label8;
        private TextBox txtPulgarDerecho;
        private Label label9;
        private TextBox txtPulgarIzquierdo;
        private Button btnRegIndIzq;
        private Button btnRegPulDer;
        private Button btnRegPulIzq;
        private Button btnReportTxt;
    }
}