namespace control_asistencia_savin
{
    partial class CaptureForm
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
            PromptLabel = new Label();
            Picture = new PictureBox();
            Prompt = new TextBox();
            StatusText = new TextBox();
            CloseButton = new Button();
            ((System.ComponentModel.ISupportInitialize)Picture).BeginInit();
            SuspendLayout();
            // 
            // PromptLabel
            // 
            PromptLabel.AutoSize = true;
            PromptLabel.Location = new Point(-25, 254);
            PromptLabel.Margin = new Padding(4, 0, 4, 0);
            PromptLabel.Name = "PromptLabel";
            PromptLabel.Size = new Size(93, 15);
            PromptLabel.TabIndex = 1;
            PromptLabel.Text = "Coloca tu huella";
            PromptLabel.Visible = false;
            // 
            // Picture
            // 
            Picture.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Picture.BackColor = SystemColors.Window;
            Picture.Location = new Point(3, 4);
            Picture.Margin = new Padding(4, 3, 4, 3);
            Picture.Name = "Picture";
            Picture.Size = new Size(203, 170);
            Picture.TabIndex = 0;
            Picture.TabStop = false;
            // 
            // Prompt
            // 
            Prompt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Prompt.BackColor = SystemColors.Control;
            Prompt.Enabled = false;
            Prompt.ForeColor = Color.Crimson;
            Prompt.Location = new Point(3, 180);
            Prompt.Margin = new Padding(4, 3, 4, 3);
            Prompt.Name = "Prompt";
            Prompt.ReadOnly = true;
            Prompt.Size = new Size(203, 23);
            Prompt.TabIndex = 2;
            // 
            // StatusText
            // 
            StatusText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            StatusText.BackColor = SystemColors.Window;
            StatusText.Location = new Point(-54, 209);
            StatusText.Margin = new Padding(4, 3, 4, 3);
            StatusText.Multiline = true;
            StatusText.Name = "StatusText";
            StatusText.ReadOnly = true;
            StatusText.ScrollBars = ScrollBars.Both;
            StatusText.Size = new Size(136, 56);
            StatusText.TabIndex = 4;
            StatusText.Visible = false;
            // 
            // CloseButton
            // 
            CloseButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            CloseButton.DialogResult = DialogResult.Cancel;
            CloseButton.Location = new Point(117, 209);
            CloseButton.Margin = new Padding(4, 3, 4, 3);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(89, 27);
            CloseButton.TabIndex = 6;
            CloseButton.Text = "Cerrar";
            CloseButton.UseVisualStyleBackColor = true;
            // 
            // CaptureForm
            // 
            AcceptButton = CloseButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = CloseButton;
            ClientSize = new Size(211, 241);
            Controls.Add(CloseButton);
            Controls.Add(StatusText);
            Controls.Add(Prompt);
            Controls.Add(PromptLabel);
            Controls.Add(Picture);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(100, 100);
            Name = "CaptureForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Capture Enrollment";
            FormClosed += CaptureForm_FormClosed;
            Load += CaptureForm_Load;
            ((System.ComponentModel.ISupportInitialize)Picture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox Picture;
        private System.Windows.Forms.TextBox Prompt;
        private System.Windows.Forms.TextBox StatusText;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label PromptLabel;
    }
}