using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_asistencia_savin.Notifications
{
    public partial class frmHuellaRechazada : Form
    {
        public frmHuellaRechazada()
        {
            InitializeComponent();
            this.Text = "ALERTA";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //this.Size = new Size(600, 300);

            // Configuración del lblTitulo
            //lblTitulo.Text = "HUELLA RECHAZADA";
            //lblTitulo.ForeColor = Color.Red;
            //lblTitulo.Font = new Font("Arial", 24, FontStyle.Bold);
            lblTitulo.AutoSize = false;
            //lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            //lblTitulo.Dock = DockStyle.Top;
            //lblTitulo.Height = 100;

            // Configuración del lblSubTitulo
            //lblSubTitulo.Text = "SU ASISTENCIA NO FUE REGISTRADA";
            //lblSubTitulo.Font = new Font("Arial", 16, FontStyle.Regular);
            lblSubTitulo.AutoSize = false;
            //lblSubTitulo.TextAlign = ContentAlignment.MiddleCenter;
            //lblSubTitulo.Dock = DockStyle.Top;
            //lblSubTitulo.Height = 50;

            // Configuración del btnEntendido
            //btnEntendido.Text = "ENTENDIDO";
            //btnEntendido.Font = new Font("Arial", 14, FontStyle.Regular);
            btnEntendido.AutoSize = false;
            //btnEntendido.TextAlign = ContentAlignment.MiddleCenter;
            //btnEntendido.Dock = DockStyle.Bottom;
            //btnEntendido.Height = 50;
            //btnEntendido.Click += (s, e) => { this.Close(); };

            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblSubTitulo);
            this.Controls.Add(btnEntendido);
        }

        private void btnEntendido_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
