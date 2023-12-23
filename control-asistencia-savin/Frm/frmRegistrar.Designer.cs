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
            btnAgregar = new Button();
            txtPaterno = new TextBox();
            txtHuella = new TextBox();
            btnRegistrarHuella = new Button();
            dgvListar = new DataGridView();
            txtId_ciudad = new TextBox();
            label3 = new Label();
            txtMaterno = new TextBox();
            label4 = new Label();
            txtNombre = new TextBox();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            textBox1 = new TextBox();
            label8 = new Label();
            textBox2 = new TextBox();
            label9 = new Label();
            textBox3 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
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
            // btnAgregar
            // 
            btnAgregar.Enabled = false;
            btnAgregar.Location = new Point(955, 418);
            btnAgregar.Margin = new Padding(4, 3, 4, 3);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(157, 27);
            btnAgregar.TabIndex = 2;
            btnAgregar.Text = "Guardar Usuario";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // txtPaterno
            // 
            txtPaterno.Location = new Point(114, 59);
            txtPaterno.Margin = new Padding(4, 3, 4, 3);
            txtPaterno.Name = "txtPaterno";
            txtPaterno.Size = new Size(224, 23);
            txtPaterno.TabIndex = 3;
            // 
            // txtHuella
            // 
            txtHuella.Enabled = false;
            txtHuella.Location = new Point(114, 238);
            txtHuella.Margin = new Padding(4, 3, 4, 3);
            txtHuella.Name = "txtHuella";
            txtHuella.Size = new Size(131, 23);
            txtHuella.TabIndex = 4;
            // 
            // btnRegistrarHuella
            // 
            btnRegistrarHuella.Location = new Point(259, 234);
            btnRegistrarHuella.Margin = new Padding(4, 3, 4, 3);
            btnRegistrarHuella.Name = "btnRegistrarHuella";
            btnRegistrarHuella.Size = new Size(79, 27);
            btnRegistrarHuella.TabIndex = 5;
            btnRegistrarHuella.Text = "Registrar Huella";
            btnRegistrarHuella.UseVisualStyleBackColor = true;
            btnRegistrarHuella.Click += btnRegistrarHuella_Click;
            // 
            // dgvListar
            // 
            dgvListar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvListar.Location = new Point(355, 12);
            dgvListar.Margin = new Padding(4, 3, 4, 3);
            dgvListar.Name = "dgvListar";
            dgvListar.Size = new Size(757, 400);
            dgvListar.TabIndex = 6;
            dgvListar.CellContentClick += dgvListar_CellContentClick;
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
            label3.Click += label3_Click;
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
            // textBox1
            // 
            textBox1.Enabled = false;
            textBox1.Location = new Point(114, 277);
            textBox1.Margin = new Padding(4, 3, 4, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(131, 23);
            textBox1.TabIndex = 14;
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
            // textBox2
            // 
            textBox2.Enabled = false;
            textBox2.Location = new Point(114, 319);
            textBox2.Margin = new Padding(4, 3, 4, 3);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(131, 23);
            textBox2.TabIndex = 16;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(51, 364);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(45, 15);
            label9.TabIndex = 19;
            label9.Text = "Pul Der";
            // 
            // textBox3
            // 
            textBox3.Enabled = false;
            textBox3.Location = new Point(114, 361);
            textBox3.Margin = new Padding(4, 3, 4, 3);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(131, 23);
            textBox3.TabIndex = 18;
            // 
            // button1
            // 
            button1.Location = new Point(259, 277);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(79, 27);
            button1.TabIndex = 20;
            button1.Text = "Registrar Huella";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(259, 316);
            button2.Margin = new Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new Size(79, 27);
            button2.TabIndex = 21;
            button2.Text = "Registrar Huella";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(259, 357);
            button3.Margin = new Padding(4, 3, 4, 3);
            button3.Name = "button3";
            button3.Size = new Size(79, 27);
            button3.TabIndex = 22;
            button3.Text = "Registrar Huella";
            button3.UseVisualStyleBackColor = true;
            // 
            // frmRegistrar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1125, 457);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label9);
            Controls.Add(textBox3);
            Controls.Add(label8);
            Controls.Add(textBox2);
            Controls.Add(label7);
            Controls.Add(textBox1);
            Controls.Add(label6);
            Controls.Add(txtNombre);
            Controls.Add(label5);
            Controls.Add(txtMaterno);
            Controls.Add(label4);
            Controls.Add(txtId_ciudad);
            Controls.Add(label3);
            Controls.Add(dgvListar);
            Controls.Add(btnRegistrarHuella);
            Controls.Add(txtHuella);
            Controls.Add(txtPaterno);
            Controls.Add(btnAgregar);
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
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.TextBox txtPaterno;
        private System.Windows.Forms.TextBox txtHuella;
        private System.Windows.Forms.Button btnRegistrarHuella;
        private System.Windows.Forms.DataGridView dgvListar;
        private System.Windows.Forms.TextBox txtId_ciudad;
        private System.Windows.Forms.Label label3;
        private TextBox txtMaterno;
        private Label label4;
        private TextBox txtNombre;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox textBox1;
        private Label label8;
        private TextBox textBox2;
        private Label label9;
        private TextBox textBox3;
        private Button button1;
        private Button button2;
        private Button button3;
    }
}