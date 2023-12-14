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
    public partial class frmAtrasos : Form
    {
        private Models2.StoreContext contexto;

        public frmAtrasos()
        {
            InitializeComponent();
            tmrTime.Start();

            this.FormBorderStyle = FormBorderStyle.None; // Remueve los bordes de la ventana
            this.WindowState = FormWindowState.Maximized; // Maximiza la ventana
            //this.TopMost = true;

        }

 

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            frmVerificar verificar = new frmVerificar();
            verificar.ShowDialog();



            //// Suponiendo que 'contexto' es tu instancia de DbContext
            //var mesActual = DateTime.Now.Month;
            //var sumaMinutosAtraso = contexto.RrhhAsistencia
            //.Where(a => a.IdPersonal == verificar.idEncontrado) // Asume que idClient es el ID del personal
            //                        .Where(a => a.HoraMarcado.Month == mesActual) // Asume que HoraMarcado es un DateTime
            //                        .Sum(a => a.MinutosAtraso ?? 0);

            //var empleado = contexto.RrhhPersonals.Find(verificar.idEncontrado);
            //var nombreCompleto = $"{empleado.Paterno} {empleado.Materno} {empleado.Nombre}";



            


            lblNombre.Text = verificar.personalName;
            lblMinAtraso.Text = SumarMinutosAtraso(verificar.idEncontrado).ToString();


            lblNombre.Visible = true;
            lblMinAtraso.Visible = true;

            Console.WriteLine("Nombre: " + verificar.personalName);
            Console.WriteLine("Hora: " + DateTime.Now.ToString("HH:mm:ss"));


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

