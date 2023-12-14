using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace control_asistencia_savin
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            tmrTime.Start();
            StoreContext s = new StoreContext();
            s.TestConnection();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            frmRegistrar registrar = new frmRegistrar();
            registrar.ShowDialog();
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            frmVerificar verificar = new frmVerificar();
            verificar.ShowDialog();
            lblNombre.Text = verificar.personalName;
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");


            lblNombre.Visible = true;
            lblHora.Visible = true;

            Console.WriteLine("Nombre: " + verificar.personalName);
            Console.WriteLine("Hora: " + DateTime.Now.ToString("HH:mm:ss"));


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tmrTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
