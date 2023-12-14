using control_asistencia_savin.Models2;
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
        private Models2.StoreContext contexto;

        public Main()
        {
            InitializeComponent();
            tmrTime.Start();

            //this.FormBorderStyle = FormBorderStyle.None; // Remueve los bordes de la ventana
            this.WindowState = FormWindowState.Maximized; // Maximiza la ventana
            //this.TopMost = true;

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

            //Muestra en pantalla los datos y hora
            lblNombre.Visible = true;
            lblHora.Visible = true;

            Console.WriteLine("Nombre: " + verificar.personalName);
            Console.WriteLine("Hora: " + DateTime.Now.ToString("HH:mm:ss"));

            String capturaHoraMarcado = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            RrhhAsistencia regisAsis = new RrhhAsistencia()
            {
                IdTurno = capturaIdTurno(),
                IdPersonal = verificar.idEncontrado,
                HoraMarcado = capturaHoraMarcado,
                MinutosAtraso = capturaMinAtraso(capturaHoraMarcado),
                IndTipoMovimiento = capturaIndMov()
            };
            AddAsistencia(regisAsis);

        }


        void AddAsistencia(RrhhAsistencia item)
        {
            using (var db = new StoreContext())
            {
                db.RrhhAsistencia.Add(item);
                db.SaveChanges();
            }
        }

        public int capturaMinAtraso(string capturaHoraMarcado)
        {
            // Parse the captured time string to a DateTime object
            DateTime horaMarcada = DateTime.Parse(capturaHoraMarcado);

            // Define the comparison times
            var tiempoInicioTurno1 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 8, 30, 0);
            var tiempoInicioTurno2 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 14, 30, 0);

            // Get the values from the other functions
            int idTurno = capturaIdTurno();
            int indMov = capturaIndMov();

            // Calculate the exceeded minutes according to the conditions
            if (idTurno == 1 && indMov == 1)
            {
                // For the first condition, compare with 08:30:00
                return horaMarcada > tiempoInicioTurno1 ? (int)(horaMarcada - tiempoInicioTurno1).TotalMinutes : 0;
            }
            else if (idTurno == 2 && indMov == 1)
            {
                // For the second condition, compare with 14:30:00
                return horaMarcada > tiempoInicioTurno2 ? (int)(horaMarcada - tiempoInicioTurno2).TotalMinutes : 0;
            }

            // If none of the conditions are met, return 0
            return 0;
        }


        public int capturaIndMov()
        {
            var horaActual = DateTime.Now;
            var medioDia = new DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 12, 0, 0);
            var tarde = new DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 13, 20, 0);
            var noche = new DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 18, 0, 0);

            if (horaActual < medioDia)
            {
                return 1;
            }
            else if (horaActual >= medioDia && horaActual < tarde)
            {
                return 2;
            }
            else if (horaActual >= tarde && horaActual < noche)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public int capturaIdTurno()
        {
            if (!EsSabado())
            {
                var horaActual = DateTime.Now;
                var limiteHora = new DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 13, 20, 0);

                return horaActual < limiteHora ? 1 : 2;
            }
            return 3;
        }


        public bool EsSabado()
        {
            return DateTime.Today.DayOfWeek == DayOfWeek.Saturday;
        }




        private void tmrTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmOpciones opciones = new frmOpciones();   
            opciones.Show();
        }
    }
}
