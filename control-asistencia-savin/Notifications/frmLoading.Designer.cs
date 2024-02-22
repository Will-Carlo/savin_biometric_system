namespace control_asistencia_savin.Notifications
{
    partial class frmLoading
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoading));
            pgbSavinLoad = new CustomControls.RJControls.RJProgressBar();
            pictureBox1 = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pgbSavinLoad
            // 
            pgbSavinLoad.ChannelColor = Color.LightSteelBlue;
            pgbSavinLoad.ChannelHeight = 16;
            pgbSavinLoad.ForeBackColor = Color.FromArgb(10, 38, 102);
            pgbSavinLoad.ForeColor = Color.White;
            pgbSavinLoad.Location = new Point(49, 209);
            pgbSavinLoad.Name = "pgbSavinLoad";
            pgbSavinLoad.ShowMaximun = false;
            pgbSavinLoad.ShowValue = CustomControls.RJControls.TextPosition.Right;
            pgbSavinLoad.Size = new Size(306, 44);
            pgbSavinLoad.SliderColor = Color.FromArgb(10, 38, 102);
            pgbSavinLoad.SliderHeight = 6;
            pgbSavinLoad.SymbolAfter = "";
            pgbSavinLoad.SymbolBefore = "";
            pgbSavinLoad.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Top;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.InitialImage = (Image)resources.GetObject("pictureBox1.InitialImage");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(408, 266);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 21;
            pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // frmLoading
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 206, 0);
            ClientSize = new Size(408, 278);
            Controls.Add(pgbSavinLoad);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmLoading";
            Text = "frmLoading";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CustomControls.RJControls.RJProgressBar pgbSavinLoad;
        private PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
    }
}