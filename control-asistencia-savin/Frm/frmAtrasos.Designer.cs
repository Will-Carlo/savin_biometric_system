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
            tmrTime = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // btnVerificarHuellaCod
            // 
            btnVerificarHuellaCod.Click += btnVerificarHuellaCod_Click;
            // 
            // tmrTime
            // 
            tmrTime.Enabled = true;
            tmrTime.Interval = 1000;
            // 
            // frmAtrasos
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(1125, 457);
            Name = "frmAtrasos";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer tmrTime;
    }
}