namespace control_asistencia_savin.Notifications
{
    partial class frmNotification
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
            lblMessage = new Label();
            btnOk = new Button();
            pnlNotification = new Panel();
            pnlNotification.SuspendLayout();
            SuspendLayout();
            // 
            // lblMessage
            // 
            lblMessage.Anchor = AnchorStyles.Top;
            lblMessage.AutoSize = true;
            lblMessage.Font = new Font("Segoe UI", 9F);
            lblMessage.Location = new Point(52, 41);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(66, 15);
            lblMessage.TabIndex = 0;
            lblMessage.Text = "lblMessage";
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.Location = new Point(204, 91);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 23);
            btnOk.TabIndex = 1;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // pnlNotification
            // 
            pnlNotification.Controls.Add(lblMessage);
            pnlNotification.Controls.Add(btnOk);
            pnlNotification.Dock = DockStyle.Fill;
            pnlNotification.Location = new Point(0, 0);
            pnlNotification.Name = "pnlNotification";
            pnlNotification.Size = new Size(291, 126);
            pnlNotification.TabIndex = 2;
            // 
            // frmNotification
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(291, 126);
            Controls.Add(pnlNotification);
            Name = "frmNotification";
            Text = "Notification";
            pnlNotification.ResumeLayout(false);
            pnlNotification.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblMessage;
        private Button btnOk;
        private Panel pnlNotification;
    }
}