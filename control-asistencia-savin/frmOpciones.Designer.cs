namespace control_asistencia_savin
{
    partial class frmOpciones
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
            btnVerAtrasos = new Button();
            btnVolver = new Button();
            SuspendLayout();
            // 
            // btnVerAtrasos
            // 
            btnVerAtrasos.Location = new Point(252, 216);
            btnVerAtrasos.Name = "btnVerAtrasos";
            btnVerAtrasos.Size = new Size(75, 23);
            btnVerAtrasos.TabIndex = 0;
            btnVerAtrasos.Text = "Ver atrasos acumulados";
            btnVerAtrasos.UseVisualStyleBackColor = true;
            btnVerAtrasos.Click += btnVerAtrasos_Click;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(12, 415);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(75, 23);
            btnVolver.TabIndex = 1;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            // 
            // frmOpciones
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnVolver);
            Controls.Add(btnVerAtrasos);
            Name = "frmOpciones";
            Text = "frmOpciones";
            ResumeLayout(false);
        }

        #endregion

        private Button btnVerAtrasos;
        private Button btnVolver;
    }
}