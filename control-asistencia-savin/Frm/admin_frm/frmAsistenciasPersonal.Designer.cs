namespace control_asistencia_savin.Frm.admin_frm
{
    partial class frmAsistenciasPersonal
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
            dgvPersonalAsistencias = new DataGridView();
            lblPuntoAsistencia = new Label();
            btnCargarAsistencias = new Button();
            cbxStore = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvPersonalAsistencias).BeginInit();
            SuspendLayout();
            // 
            // dgvPersonalAsistencias
            // 
            dgvPersonalAsistencias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPersonalAsistencias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPersonalAsistencias.Location = new Point(181, 12);
            dgvPersonalAsistencias.Margin = new Padding(4, 3, 4, 3);
            dgvPersonalAsistencias.MultiSelect = false;
            dgvPersonalAsistencias.Name = "dgvPersonalAsistencias";
            dgvPersonalAsistencias.ReadOnly = true;
            dgvPersonalAsistencias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPersonalAsistencias.Size = new Size(931, 433);
            dgvPersonalAsistencias.TabIndex = 7;
            dgvPersonalAsistencias.CellFormatting += dgvPersonalAsistencias_CellFormatting;
            dgvPersonalAsistencias.RowPrePaint += dgvPersonalAsistencias_RowPrePaint;
            // 
            // lblPuntoAsistencia
            // 
            lblPuntoAsistencia.AutoSize = true;
            lblPuntoAsistencia.Location = new Point(12, 106);
            lblPuntoAsistencia.Name = "lblPuntoAsistencia";
            lblPuntoAsistencia.Size = new Size(99, 15);
            lblPuntoAsistencia.TabIndex = 15;
            lblPuntoAsistencia.Text = "Punto asistencia: ";
            // 
            // btnCargarAsistencias
            // 
            btnCargarAsistencias.Location = new Point(12, 67);
            btnCargarAsistencias.Name = "btnCargarAsistencias";
            btnCargarAsistencias.Size = new Size(162, 23);
            btnCargarAsistencias.TabIndex = 14;
            btnCargarAsistencias.Text = "Cargar Registros";
            btnCargarAsistencias.UseVisualStyleBackColor = true;
            btnCargarAsistencias.Click += btnCargarRegistros_Click;
            // 
            // cbxStore
            // 
            cbxStore.FormattingEnabled = true;
            cbxStore.Location = new Point(12, 24);
            cbxStore.Name = "cbxStore";
            cbxStore.Size = new Size(162, 23);
            cbxStore.TabIndex = 13;
            cbxStore.SelectedIndexChanged += cbxStore_SelectedIndexChanged;
            // 
            // frmAsistenciasPersonal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1125, 457);
            Controls.Add(lblPuntoAsistencia);
            Controls.Add(btnCargarAsistencias);
            Controls.Add(cbxStore);
            Controls.Add(dgvPersonalAsistencias);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmAsistenciasPersonal";
            Text = "frmAsistenciasPersonal";
            ((System.ComponentModel.ISupportInitialize)dgvPersonalAsistencias).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvPersonalAsistencias;
        private Label lblPuntoAsistencia;
        private Button btnCargarAsistencias;
        private ComboBox cbxStore;
    }
}