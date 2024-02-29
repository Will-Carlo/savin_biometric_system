namespace control_asistencia_savin.Frm
{
    partial class CaptureFormForVerification
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CaptureFormForVerification));
            CloseButton = new Button();
            StatusText = new TextBox();
            Prompt = new TextBox();
            PromptLabel = new Label();
            Picture = new PictureBox();
            btnVerificarHuellaCod = new Button();
            lblStatusProcess = new Label();
            lblTitOption = new Label();
            lblHora = new Label();
            lblTitPersonal = new Label();
            lblTitHora = new Label();
            lblNombre = new Label();
            txtCodigo = new TextBox();
            panel2 = new Panel();
            lblVersion = new Label();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)Picture).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // CloseButton
            // 
            CloseButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            CloseButton.DialogResult = DialogResult.Cancel;
            CloseButton.Location = new Point(982, 214);
            CloseButton.Margin = new Padding(4, 3, 4, 3);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(89, 27);
            CloseButton.TabIndex = 11;
            CloseButton.Text = "Cerrar";
            CloseButton.UseVisualStyleBackColor = true;
            // 
            // StatusText
            // 
            StatusText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            StatusText.BackColor = SystemColors.Window;
            StatusText.Location = new Point(763, 246);
            StatusText.Margin = new Padding(4, 3, 4, 3);
            StatusText.Multiline = true;
            StatusText.Name = "StatusText";
            StatusText.ReadOnly = true;
            StatusText.ScrollBars = ScrollBars.Both;
            StatusText.Size = new Size(135, 59);
            StatusText.TabIndex = 10;
            StatusText.Visible = false;
            // 
            // Prompt
            // 
            Prompt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Prompt.Enabled = false;
            Prompt.Location = new Point(938, 185);
            Prompt.Margin = new Padding(4, 3, 4, 3);
            Prompt.Name = "Prompt";
            Prompt.ReadOnly = true;
            Prompt.Size = new Size(133, 23);
            Prompt.TabIndex = 9;
            // 
            // PromptLabel
            // 
            PromptLabel.AutoSize = true;
            PromptLabel.Location = new Point(918, 278);
            PromptLabel.Margin = new Padding(4, 0, 4, 0);
            PromptLabel.Name = "PromptLabel";
            PromptLabel.Size = new Size(93, 15);
            PromptLabel.TabIndex = 8;
            PromptLabel.Text = "Coloca tu huella";
            PromptLabel.Visible = false;
            // 
            // Picture
            // 
            Picture.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Picture.BackColor = SystemColors.Window;
            Picture.Location = new Point(938, 42);
            Picture.Margin = new Padding(4, 3, 4, 3);
            Picture.Name = "Picture";
            Picture.Size = new Size(133, 137);
            Picture.TabIndex = 7;
            Picture.TabStop = false;
            // 
            // btnVerificarHuellaCod
            // 
            btnVerificarHuellaCod.Anchor = AnchorStyles.Top;
            btnVerificarHuellaCod.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVerificarHuellaCod.ForeColor = Color.FromArgb(10, 38, 102);
            btnVerificarHuellaCod.Location = new Point(108, 51);
            btnVerificarHuellaCod.Margin = new Padding(4, 3, 4, 3);
            btnVerificarHuellaCod.Name = "btnVerificarHuellaCod";
            btnVerificarHuellaCod.Size = new Size(138, 46);
            btnVerificarHuellaCod.TabIndex = 24;
            btnVerificarHuellaCod.Text = "btnVerificarHuellaCod";
            btnVerificarHuellaCod.UseVisualStyleBackColor = true;
            // 
            // lblStatusProcess
            // 
            lblStatusProcess.Anchor = AnchorStyles.Top;
            lblStatusProcess.AutoSize = true;
            lblStatusProcess.BackColor = Color.Transparent;
            lblStatusProcess.FlatStyle = FlatStyle.System;
            lblStatusProcess.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStatusProcess.ForeColor = Color.FromArgb(10, 38, 102);
            lblStatusProcess.Location = new Point(271, 58);
            lblStatusProcess.Name = "lblStatusProcess";
            lblStatusProcess.Size = new Size(171, 30);
            lblStatusProcess.TabIndex = 31;
            lblStatusProcess.Text = "lblStatusProcess";
            lblStatusProcess.Visible = false;
            // 
            // lblTitOption
            // 
            lblTitOption.Anchor = AnchorStyles.Top;
            lblTitOption.AutoSize = true;
            lblTitOption.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitOption.ForeColor = Color.FromArgb(10, 38, 102);
            lblTitOption.Location = new Point(73, 51);
            lblTitOption.Margin = new Padding(4, 0, 4, 0);
            lblTitOption.Name = "lblTitOption";
            lblTitOption.Size = new Size(395, 65);
            lblTitOption.TabIndex = 26;
            lblTitOption.Text = "Último Marcado";
            // 
            // lblHora
            // 
            lblHora.Anchor = AnchorStyles.Top;
            lblHora.AutoSize = true;
            lblHora.Font = new Font("Segoe UI", 27.75F);
            lblHora.ForeColor = Color.FromArgb(10, 38, 102);
            lblHora.Location = new Point(301, 212);
            lblHora.Margin = new Padding(4, 0, 4, 0);
            lblHora.Name = "lblHora";
            lblHora.Size = new Size(142, 50);
            lblHora.TabIndex = 30;
            lblHora.Text = "lblHora";
            lblHora.Visible = false;
            // 
            // lblTitPersonal
            // 
            lblTitPersonal.Anchor = AnchorStyles.Top;
            lblTitPersonal.AutoSize = true;
            lblTitPersonal.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold);
            lblTitPersonal.ForeColor = Color.FromArgb(10, 38, 102);
            lblTitPersonal.Location = new Point(112, 151);
            lblTitPersonal.Margin = new Padding(4, 0, 4, 0);
            lblTitPersonal.Name = "lblTitPersonal";
            lblTitPersonal.Size = new Size(181, 50);
            lblTitPersonal.TabIndex = 27;
            lblTitPersonal.Text = "Personal:";
            // 
            // lblTitHora
            // 
            lblTitHora.Anchor = AnchorStyles.Top;
            lblTitHora.AutoSize = true;
            lblTitHora.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitHora.ForeColor = Color.FromArgb(10, 38, 102);
            lblTitHora.Location = new Point(175, 212);
            lblTitHora.Margin = new Padding(4, 0, 4, 0);
            lblTitHora.Name = "lblTitHora";
            lblTitHora.Size = new Size(118, 50);
            lblTitHora.TabIndex = 28;
            lblTitHora.Text = "Hora:";
            // 
            // lblNombre
            // 
            lblNombre.Anchor = AnchorStyles.Top;
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNombre.ForeColor = Color.FromArgb(10, 38, 102);
            lblNombre.Location = new Point(301, 151);
            lblNombre.Margin = new Padding(4, 0, 4, 0);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(198, 50);
            lblNombre.TabIndex = 29;
            lblNombre.Text = "lblNombre";
            lblNombre.Visible = false;
            // 
            // txtCodigo
            // 
            txtCodigo.Anchor = AnchorStyles.Top;
            txtCodigo.BackColor = Color.Ivory;
            txtCodigo.BorderStyle = BorderStyle.FixedSingle;
            txtCodigo.Enabled = false;
            txtCodigo.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCodigo.Location = new Point(264, 49);
            txtCodigo.Margin = new Padding(4, 3, 4, 3);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(666, 46);
            txtCodigo.TabIndex = 25;
            // 
            // panel2
            // 
            panel2.Controls.Add(lblVersion);
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(Prompt);
            panel2.Controls.Add(CloseButton);
            panel2.Controls.Add(PromptLabel);
            panel2.Controls.Add(lblTitOption);
            panel2.Controls.Add(StatusText);
            panel2.Controls.Add(lblHora);
            panel2.Controls.Add(lblTitPersonal);
            panel2.Controls.Add(lblTitHora);
            panel2.Controls.Add(Picture);
            panel2.Controls.Add(lblNombre);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(10, 134);
            panel2.Name = "panel2";
            panel2.Size = new Size(1105, 310);
            panel2.TabIndex = 36;
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblVersion.Location = new Point(1009, 293);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(51, 21);
            lblVersion.TabIndex = 33;
            lblVersion.Text = "vX.X.X";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(512, 51);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(275, 192);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 32;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnVerificarHuellaCod);
            panel1.Controls.Add(lblStatusProcess);
            panel1.Controls.Add(txtCodigo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(10, 10);
            panel1.Name = "panel1";
            panel1.Size = new Size(1105, 124);
            panel1.TabIndex = 35;
            // 
            // CaptureFormForVerification
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1125, 457);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CaptureFormForVerification";
            Padding = new Padding(10);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "CaptureFormForVerification";
            ((System.ComponentModel.ISupportInitialize)Picture).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button CloseButton;
        private TextBox StatusText;
        private TextBox Prompt;
        private Label PromptLabel;
        private PictureBox Picture;
        public Button btnVerificarHuellaCod;
        public Label lblStatusProcess;
        public Label lblTitOption;
        public Label lblHora;
        public Label lblTitPersonal;
        public Label lblTitHora;
        public Label lblNombre;
        public TextBox txtCodigo;
        private Panel panel2;
        private Label lblVersion;
        private PictureBox pictureBox1;
        private Panel panel1;
    }
}
