using control_asistencia_savin.Models2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_asistencia_savin
{
    public partial class frmAtrasos : VerifyForm
    {

        public frmAtrasos()
        {
            InitializeComponent();
            btnVerificarHuellaCod.Text = "Leer Huella";
            lblTitOption.Text = "Minutos acumulados:";
            lblTitHora.Text = "Min. :";

        }


        private void btnVerificarHuellaCod_Click(object sender, EventArgs e)
        {
            lblStatusProcess.Visible = false;
            //lblRechazado.Visible = false;

            frmVerificar verificar = new frmVerificar();
            verificar.ShowDialog();

            try
            {
                lblNombre.Text = verificar.personalName;
                lblHora.Text = SumarMinutosAtraso(verificar.idEncontrado).ToString();

                if (lblNombre.Text != "")
                {
                    lblStatusProcess.Text = "Verficado..";
                    lblStatusProcess.Visible = true;

                }
                else
                {
                    lblStatusProcess.Text = "Rechazado..";
                    lblStatusProcess.Visible = true;
                }

                lblNombre.Visible = true;
                lblHora.Visible = true;


                Console.WriteLine("Nombre: " + verificar.personalName);
                Console.WriteLine("Hora: " + DateTime.Now.ToString("HH:mm:ss"));
            }
            catch (Exception ex)
            {
                lblNombre.Visible = false;
                lblHora.Visible = false;

                MessageBox.Show("No se registraron huellas");
            }
        }

        public int SumarMinutosAtraso(int idPersonal)
        {
            var mesActual = DateTime.Now.Month;
            using (var context = new StoreContext())
            {
                var suma = context.RrhhAsistencia
                            .Where(a => a.IdPersonal == idPersonal)
                            .AsEnumerable() // Realiza la conversión en memoria para evitar errores de Linq-to-Entities
                            .Where(a => DateTime.Parse(a.HoraMarcado).Month == mesActual)
                            .Sum(a => a.MinutosAtraso ?? 0);

                return suma;
            }
        }


    }
}

