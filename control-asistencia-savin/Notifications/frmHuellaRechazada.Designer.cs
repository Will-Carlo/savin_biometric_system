namespace control_asistencia_savin.Notifications
{
    partial class frmHuellaRechazada
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
            lblTitulo = new Label();
            btnEntendido = new Button();
            lblSubTitulo = new Label();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Tahoma", 54.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.Red;
            lblTitulo.Location = new Point(-8, 52);
            lblTitulo.Margin = new Padding(4, 0, 4, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(800, 88);
            lblTitulo.TabIndex = 4;
            lblTitulo.Text = "HUELLA RECHAZADA";
            // 
            // btnEntendido
            // 
            btnEntendido.Dock = DockStyle.Bottom;
            btnEntendido.Font = new Font("Tahoma", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEntendido.ForeColor = Color.FromArgb(10, 38, 102);
            btnEntendido.Location = new Point(0, 238);
            btnEntendido.Name = "btnEntendido";
            btnEntendido.Size = new Size(784, 73);
            btnEntendido.TabIndex = 5;
            btnEntendido.Text = "ENTENDIDO";
            btnEntendido.UseVisualStyleBackColor = true;
            btnEntendido.Click += btnEntendido_Click;
            // 
            // lblSubTitulo
            // 
            lblSubTitulo.Anchor = AnchorStyles.Top;
            lblSubTitulo.AutoSize = true;
            lblSubTitulo.Font = new Font("Tahoma", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSubTitulo.ForeColor = Color.FromArgb(10, 38, 102);
            lblSubTitulo.Location = new Point(25, 141);
            lblSubTitulo.Margin = new Padding(4, 0, 4, 0);
            lblSubTitulo.Name = "lblSubTitulo";
            lblSubTitulo.Size = new Size(722, 45);
            lblSubTitulo.TabIndex = 6;
            lblSubTitulo.Text = "SU ASISTENCIA NO FUE REGISTRADA";
            // 
            // frmHuellaRechazada
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 255, 128);
            ClientSize = new Size(784, 311);
            Controls.Add(lblSubTitulo);
            Controls.Add(btnEntendido);
            Controls.Add(lblTitulo);
            Name = "frmHuellaRechazada";
            Text = "frmHuellaRechazada";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Button btnEntendido;
        private Label lblSubTitulo;
    }
}