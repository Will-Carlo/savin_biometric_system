using control_asistencia_savin.ApiService;
using control_asistencia_savin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_asistencia_savin
{
    public partial class frmAtrasos : Form
    {

        private Models.StoreContext contexto;
        private string _fecha;
        private int _idPersonal;

        public frmAtrasos()
        {
            InitializeComponent();
            btnVerificarHuella.Text = "Leer Huella";
            lblTitOption.Text = "Minutos acumulados:";
            lblTitHora.Text = "Min. :";
        }
        private void btnVerificarHuellaCod_Click_1(object sender, EventArgs e)
        {
            lblStatusProcess.Visible = false;

            frmVerificar verificar = new frmVerificar();
            verificar.ShowDialog();

            try
            {
                lblNombre.Text = verificar.personalName;

                if (lblNombre.Text != "")
                {
                    lblStatusProcess.Text = "Verficado..";
                    lblStatusProcess.Visible = true;
                    _fecha = DateTime.Now.ToString("MM");
                    _idPersonal = verificar.idEncontrado;
                    // valores por defecto, es necesario la huella
                    CargarMesesEnComboBoxAsync(_idPersonal);
                    //mostrar valores por defecto
                    //CargarDatosEnDataGridView(_idPersonal);
                }
                else
                {
                    lblStatusProcess.Text = "Rechazado..";
                    lblStatusProcess.Visible = true;
                }

                lblNombre.Visible = true;


                Console.WriteLine("Nombre: " + verificar.personalName);
                Console.WriteLine("Hora: " + DateTime.Now.ToString("HH:mm:ss"));
            }
            catch (Exception ex)
            {
                lblNombre.Visible = false;
                lblStatusProcess.Visible = false;


                MessageBox.Show("No se registraron huellas." + ex.Message);
            }
        }

        private void frmAtrasos_Load(object sender, EventArgs e)
        {
            contexto = new Models.StoreContext();
        }


        public void CargarMesesEnComboBoxAsync(int idPersonal)
        {
            using (var context = new StoreContext())
            {
                var mesesQuery = context.RrhhAsistencia
                    .Where(a => a.IdPersonal == idPersonal)
                    .ToList() // Traer los datos a la memoria
                    .Select(a => DateTime.Parse(a.HoraMarcado)) // Parsear cada entrada como DateTime
                    .Select(date => date.ToString("MMMM/yyyy", new CultureInfo("es-ES"))) // Reformatear fecha como 'Mes/Año'
                    .Distinct();
                //.OrderBy(x => x); // Ordenar si se requiere
                var meses = mesesQuery.ToList();
                cbxPersonalMonth.Items.Clear();

                var ms = new StringBuilder();
                foreach (var mes in meses)
                {
                    cbxPersonalMonth.Items.Add(mes);
                    ms.AppendLine(mes);

                }

                MessageBox.Show("Meses: " + ms.ToString());
            }
        }

        //public void CargarDatosEnDataGridView(int idPersonal)
        //{
        //    using (var context = new StoreContext())
        //    {
        //        var asistencias = context.RrhhAsistencia
        //            .Where(a => a.IdPersonal == idPersonal)
        //            .ToList() // Traemos los datos a memoria para procesarlos
        //            .Select(a => new
        //            {
        //                FechaMarcado = DateTime.Parse(a.HoraMarcado),
        //                a.IdTurno,
        //                a.MinutosAtraso,
        //                a.IndTipoMovimiento
        //            })
        //            .GroupBy(a => a.FechaMarcado.Date) // Agrupamos por fecha
        //            .Select(g => new RegistroAsistencia
        //            {
        //                Fecha = g.Key.ToString("dd/MM/yyyy"),
        //                EntradaManana = g.Where(a => a.IdTurno == 1 && a.IndTipoMovimiento == 1).Select(a => a.FechaMarcado.TimeOfDay).FirstOrDefault(),
        //                SalidaManana = g.Where(a => a.IdTurno == 1 && a.IndTipoMovimiento == 2).Select(a => a.FechaMarcado.TimeOfDay).FirstOrDefault(),
        //                MinutosAtrasadoManana = g.Where(a => a.IdTurno == 1).Sum(a => a.MinutosAtraso),
        //                EntradaTarde = g.Where(a => a.IdTurno == 2 && a.IndTipoMovimiento == 1).Select(a => a.FechaMarcado.TimeOfDay).FirstOrDefault(),
        //                SalidaTarde = g.Where(a => a.IdTurno == 2 && a.IndTipoMovimiento == 2).Select(a => a.FechaMarcado.TimeOfDay).FirstOrDefault(),
        //                MinutosAtrasadoTarde = g.Where(a => a.IdTurno == 2).Sum(a => a.MinutosAtraso),
        //            })
        //            .ToList();

        //        dgvListDelay.DataSource = asistencias;
        //    }
        //}

        public static DateTime ConvertirFecha(string fechaPersonal)
        {
            // Diccionario con los nombres de los meses en español y su respectivo número de mes.
            var meses = new Dictionary<string, int>
                {
                    {"enero", 1}, {"febrero", 2}, {"marzo", 3}, {"abril", 4},
                    {"mayo", 5}, {"junio", 6}, {"julio", 7}, {"agosto", 8},
                    {"septiembre", 9}, {"octubre", 10}, {"noviembre", 11}, {"diciembre", 12}
                };

            // Dividir el string en mes y año.
            var partes = fechaPersonal.Split('/');
            var mes = partes[0].ToLower();
            var año = int.Parse(partes[1]);

            // Obtener el número del mes.
            if (meses.TryGetValue(mes, out var numeroMes))
            {
                // Crear un nuevo objeto DateTime con el primer día del mes especificado.
                return new DateTime(año, numeroMes, 1);
            }
            else
            {
                throw new InvalidOperationException("El mes proporcionado no es válido.");
            }
        }
        public void CargarDatosEnDataGridView(int idPersonal, string fechaPersonal)
        {
            DateTime dt = ConvertirFecha(fechaPersonal);

            var yearUser = dt.Year.ToString();
            var monthUser = dt.Month.ToString("00");


            //MessageBox.Show("year: " + dt.Year + "\nMonth: " + dt.Month);
            using (var context = new StoreContext())
            {
                var asistencias = context.RrhhAsistencia
                    .Where(a => a.IdPersonal == idPersonal &&
                                    a.HoraMarcado.Substring(0, 4) == yearUser &&
                                    a.HoraMarcado.Substring(5, 2) == monthUser)
                    .ToList() // Traemos los datos a memoria para procesarlos
                    .Select(a => new
                    {
                        FechaMarcado = DateTime.Parse(a.HoraMarcado),
                        a.IdTurno,
                        a.MinutosAtraso,
                        a.IndTipoMovimiento
                    })
                    .GroupBy(a => a.FechaMarcado.Date) // Agrupamos por fecha
                    .Select(g =>
                    {
                        // Calcula los datos para los turnos mañana y tarde
                        var entradaManana = g.Where(a => a.IdTurno == 1 && a.IndTipoMovimiento == 1).Select(a => a.FechaMarcado.TimeOfDay).FirstOrDefault();
                        var salidaManana = g.Where(a => a.IdTurno == 1 && a.IndTipoMovimiento == 2).Select(a => a.FechaMarcado.TimeOfDay).FirstOrDefault();
                        var minutosAtrasadoManana = g.Where(a => a.IdTurno == 1).Sum(a => a.MinutosAtraso);

                        // Calcula los datos para el turno de sábado si existe
                        var entradaSabado = g.Where(a => a.IdTurno == 3 && a.IndTipoMovimiento == 1).Select(a => a.FechaMarcado.TimeOfDay).FirstOrDefault();
                        var salidaSabado = g.Where(a => a.IdTurno == 3 && a.IndTipoMovimiento == 2).Select(a => a.FechaMarcado.TimeOfDay).FirstOrDefault();
                        var minutosAtrasadoSabado = g.Where(a => a.IdTurno == 3).Sum(a => a.MinutosAtraso);

                        // Si es sábado, los datos de tarde estarán vacíos
                        TimeSpan? entradaTarde = null;
                        TimeSpan? salidaTarde = null;
                        int? minutosAtrasadoTarde = null;

                        // Si no es sábado, calcular turnos de tarde
                        if (!g.Any(a => a.IdTurno == 3))
                        {
                            entradaTarde = g.Where(a => a.IdTurno == 2 && a.IndTipoMovimiento == 1).Select(a => a.FechaMarcado.TimeOfDay).FirstOrDefault();
                            salidaTarde = g.Where(a => a.IdTurno == 2 && a.IndTipoMovimiento == 2).Select(a => a.FechaMarcado.TimeOfDay).FirstOrDefault();
                            minutosAtrasadoTarde = g.Where(a => a.IdTurno == 2).Sum(a => a.MinutosAtraso);
                        }

                        return new RegistroAsistencia
                        {
                            Fecha = g.Key.ToString("dd/MM/yyyy"),
                            EntradaManana = entradaSabado != TimeSpan.Zero ? entradaSabado : entradaManana,
                            SalidaManana = salidaSabado != TimeSpan.Zero ? salidaSabado : salidaManana,
                            MinutosAtrasadoManana = minutosAtrasadoSabado > 0 ? minutosAtrasadoSabado : minutosAtrasadoManana,
                            EntradaTarde = entradaTarde,
                            SalidaTarde = salidaTarde,
                            MinutosAtrasadoTarde = minutosAtrasadoTarde,
                            // Calcula el total de atrasos sumando los de mañana y tarde, si es sábado solo cuenta el de sábado
                            //TotalAtrasos = (minutosAtrasadoSabado > 0 ? minutosAtrasadoSabado : 0) + (minutosAtrasadoManana ?? 0) + (minutosAtrasadoTarde ?? 0)
                        };
                    })
                    .ToList();

                dgvListDelay.DataSource = asistencias;
            }
        }


        private void cbxPersonalMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxPersonalMonth.SelectedItem is string mesAnio)
            {
                //CargarDatosEnDataGridView(mesAnio, _idPersonal);
                CargarDatosEnDataGridView(_idPersonal, mesAnio);
            }
        }

        private void txtCodigo_Click(object sender, EventArgs e)
        {
            btnVerificarCode.Visible = true;
        }

        private void frmAtrasos_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                btnVerificarCode.Visible = false;
            }
        }
    }
}

