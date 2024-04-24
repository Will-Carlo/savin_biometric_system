using control_asistencia_savin.ApiService;
using control_asistencia_savin.Frm;
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
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace control_asistencia_savin
{
    public partial class frmAtrasos : Form
    {

        private Models.StoreContext contexto;
        private string _fecha;
        private int _idPersonal;
        private MetodosAsistencia m = new MetodosAsistencia();
        private ApiService.FunctionsDataBase _functionsDataBase;

        //private int _xStatus;
        public frmAtrasos()
        {
            InitializeComponent();
            dgvListDelay.ReadOnly = true;
            limpiarCampos();

            //cbxPersonalMonth.Items.Insert(0, "Selecciona un mes.");
            //cbxPersonalMonth.SelectedIndex = 0;

            //_xStatus = lblStatusProcess.Left;
            btnVerificarHuella.Text = "Leer Huella";
            btnVerificarCode.Text = "Leer Código";

            lblTitHora.Text = "Gestión:";
        }
        private void btnVerificarHuellaCod_Click_1(object sender, EventArgs e)
        {
            limpiarCampos();

            frmVerificar verificar = new frmVerificar();
            verificar.ShowDialog();

            try
            {

                if (verificar.statusProcess)
                {
                    //lblStatusProcess.Left = _xStatus;
                    lblStatusProcess.Text = "Verificado.";
                    lblStatusProcess.ForeColor = Color.Green;
                    lblStatusProcess.Visible = true;
                    // iniciando el evento de listado
                    _idPersonal = verificar.idEncontrado;

                    lblNombre.Text = verificar.personalName;

                    _functionsDataBase = new ApiService.FunctionsDataBase();
                    _functionsDataBase.LimpiarAuxAsistencia();
                    _functionsDataBase.LoadDataBaseAsistencia(_idPersonal);

                    if (ExisteDatos())
                    {
                        CargarMesesEnComboBox(_idPersonal);
                        //mostrar valores por defecto
                        if (cbxPersonalMonth.Items.Count > 0)
                        {
                            cbxPersonalMonth.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        MessageBox.Show("El personal no tiene datos de asistencia registrados");
                    }

                }
                else
                {
                    //lblStatusProcess.Left = _xStatus;
                    lblStatusProcess.Text = "Rechazado.";
                    lblStatusProcess.ForeColor = Color.Red;
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
        private void btnVerificarCode_Click(object sender, EventArgs e)
        {
            try
            {
                // guardamos el código en una variaable
                string txtCod = txtCodigo.Text;

                limpiarCampos();

                // buscamos el id de la persona según su código
                _idPersonal = BuscarIdPersonal(txtCod);

                //txtCodigo.Text = txtCod;

                if (txtCod != "")
                {
                    if (PersonalName(_idPersonal) != null)
                    {
                        //lblStatusProcess.Left = _xStatus + 150;
                        lblStatusProcess.Text = "VERIFICADO.";
                        lblStatusProcess.ForeColor = Color.Green;
                        lblStatusProcess.Visible = true;
                        // carga los datos del empleado en el label
                        lblNombre.Text = PersonalName(_idPersonal);
                        //Muestra en pantalla los datos y hora
                        lblNombre.Visible = true;


                        _functionsDataBase = new ApiService.FunctionsDataBase();
                        _functionsDataBase.LimpiarAuxAsistencia();
                        _functionsDataBase.LoadDataBaseAsistencia(_idPersonal);

                        if (ExisteDatos())
                        {
                            CargarMesesEnComboBox(_idPersonal);
                            //mostrar valores por defecto
                            if (cbxPersonalMonth.Items.Count > 0)
                            {
                                cbxPersonalMonth.SelectedIndex = 0;
                            }
                        }
                        else
                        {
                            MessageBox.Show("El personal no tiene datos de asistencia registrados");
                        }

                    }
                    else
                    {
                        //lblStatusProcess.Left = _xStatus + 50;
                        lblStatusProcess.Text = "RECHAZADO.";
                        lblStatusProcess.ForeColor = Color.Red;
                        lblStatusProcess.Visible = true;

                        //txtCodigo.Text = "";
                        MessageBox.Show("Código incorrecto.", "Error");
                    }

                }
                else
                {
                    lblStatusProcess.Text = "";
                    lblStatusProcess.Visible = false;

                    txtCodigo.Text = "";
                    MessageBox.Show("Debes ingresar un código.", "Error");
                }

            }
            catch (DbUpdateException ex)
            {
                // Si hay una excepción interna, muestra su mensaje; de lo contrario, muestra el mensaje de la excepción principal.
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "Error al guardar datos");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                lblNombre.Visible = false;
                MessageBox.Show("error: " + ex.Message, "Error");

            }
        }
        public void CargarMesesEnComboBox(int idPersonal)
        {
            using (var context = new StoreContext())
            {
                var mesesQuery = context.AuxAsistencia
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

                //MessageBox.Show("Meses: " + ms.ToString());
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
                var asistencias = context.AuxAsistencia
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
                        var entradaManana = g.Where(a => a.IdTurno == 1 && a.IndTipoMovimiento == 461).Select(a => a.FechaMarcado.TimeOfDay).FirstOrDefault();
                        var salidaManana = g.Where(a => a.IdTurno == 1 && a.IndTipoMovimiento == 462).Select(a => a.FechaMarcado.TimeOfDay).FirstOrDefault();
                        var minutosAtrasadoManana = g.Where(a => a.IdTurno == 1).Sum(a => a.MinutosAtraso);

                        // Calcula los datos para el turno de sábado si existe
                        var entradaSabado = g.Where(a => a.IdTurno == 3 && a.IndTipoMovimiento == 461).Select(a => a.FechaMarcado.TimeOfDay).FirstOrDefault();
                        var salidaSabado = g.Where(a => a.IdTurno == 3 && a.IndTipoMovimiento == 462).Select(a => a.FechaMarcado.TimeOfDay).FirstOrDefault();
                        var minutosAtrasadoSabado = g.Where(a => a.IdTurno == 3).Sum(a => a.MinutosAtraso);

                        // Si es sábado, los datos de tarde estarán vacíos
                        TimeSpan? entradaTarde = null;
                        TimeSpan? salidaTarde = null;
                        int? minutosAtrasadoTarde = null;

                        // Si no es sábado, calcular turnos de tarde
                        if (!g.Any(a => a.IdTurno == 3))
                        {
                            entradaTarde = g.Where(a => a.IdTurno == 2 && a.IndTipoMovimiento == 461).Select(a => a.FechaMarcado.TimeOfDay).FirstOrDefault();
                            salidaTarde = g.Where(a => a.IdTurno == 2 && a.IndTipoMovimiento == 462).Select(a => a.FechaMarcado.TimeOfDay).FirstOrDefault();
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
                TotalAtrasos();
                filtrosDGV();
            }
        }
        private void txtCodigo_Click(object sender, EventArgs e)
        {
            btnVerificarCode.Visible = true;
            btnVerificarHuella.Visible = false;
            lblStatusProcess.Visible = false;
        }
        private void frmAtrasos_Click(object sender, EventArgs e)
        {
            GameOfClicks();
        }
        private void GameOfClicks()
        {
            if (txtCodigo.Text == "")
            {
                btnVerificarCode.Visible = false;
                lblStatusProcess.Visible = false;
                btnVerificarHuella.Visible = true;
            }
        }
        private int BuscarIdPersonal(string codigo)
        {
            using (var context = new StoreContext())
            {
                var asignacion = context.RrhhTurnoAsignados
                    .Where(ta => ta.Codigo == codigo)
                    .Select(ta => ta.IdPersonal)
                    .FirstOrDefault(); // Sincronizado

                return asignacion; // Esto será null si no se encuentra ninguna coincidencia
            }
        }
        // Devuelve el nombre del personal según su ID
        private string PersonalName(int id)
        {
            using (var context = new StoreContext())
            {
                var personal = context.RrhhPersonals
                    .Where(p => p.Id == id)
                    .Select(p => new { p.Paterno, p.Materno, p.Nombres })
                    .FirstOrDefault(); // Usamos FirstOrDefault para obtener un solo registro o null

                if (personal != null)
                {
                    return $"{personal.Nombres} {personal.Paterno} {personal.Materno}";
                }
                else
                {
                    return null;
                }
            }
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }
        private void limpiarCampos()
        {
            lblStatusProcess.Visible = false;

            dgvListDelay.DataSource = null;
            cbxPersonalMonth.Items.Clear();


            lblNombre.Text = "";

            lblAtrasosMin.Text = "";
            lblAtrasosHoras.Text = "";

            lblAtrasosMin.Visible = false;
            lblAtrasosHoras.Visible = false;

            cbxPersonalMonth.Text = "Selecciona el mes.";
            _idPersonal = 0;
            txtCodigo.Text = "";

            btnVerificarHuella.Visible = true;

        }
        private void TotalAtrasos()
        {
            int TotalAtrasos = 7;
            int suma = 0;

            foreach (DataGridViewRow fila in dgvListDelay.Rows)
            {
                if (fila.Cells[TotalAtrasos].Value != null)
                {
                    int valor = 0;
                    if (int.TryParse(fila.Cells[TotalAtrasos].Value.ToString(), out valor))
                    {
                        suma += valor;
                    }
                }
            }

            lblAtrasosMin.Text = suma.ToString();
            lblAtrasosHoras.Text = MinHoras(suma).ToString();
            lblAtrasosMin.Visible = true;
            lblAtrasosHoras.Visible = true;
        }
        public double MinHoras(int minutos)
        {
            double horas = minutos / 60.0;
            return Math.Round(horas, 2);
        }
        private void dgvListDelay_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Color colYellow = ColorTranslator.FromHtml("#f1ce00");
            Color colRose = ColorTranslator.FromHtml("#f4af85");
            Color colBlue = ColorTranslator.FromHtml("#2f75b5");
            Color colGreen = ColorTranslator.FromHtml("#92d155");


            dgvListDelay.Columns[0].HeaderCell.Style.BackColor = colYellow;

            Color colorGrupo1 = ColorTranslator.FromHtml("#f4af85"); // Ejemplo de color hexadecimal
            dgvListDelay.Columns[1].HeaderCell.Style.BackColor = colorGrupo1;
            dgvListDelay.Columns[2].HeaderCell.Style.BackColor = colorGrupo1;
            dgvListDelay.Columns[3].HeaderCell.Style.BackColor = colorGrupo1;

            Color colorGrupo2 = ColorTranslator.FromHtml("#2f75b5"); // Otro ejemplo de color hexadecimal
            dgvListDelay.Columns[4].HeaderCell.Style.BackColor = colorGrupo2;
            dgvListDelay.Columns[5].HeaderCell.Style.BackColor = colorGrupo2;
            dgvListDelay.Columns[6].HeaderCell.Style.BackColor = colorGrupo2;

            dgvListDelay.Columns[7].HeaderCell.Style.BackColor = colGreen;


            // Asegúrate de que el estilo se aplique
            dgvListDelay.EnableHeadersVisualStyles = false;
        }
        private void panel2_Click(object sender, EventArgs e)
        {
            GameOfClicks();

        }
        private void panel3_Click(object sender, EventArgs e)
        {
            GameOfClicks();

        }
        private void panel4_Click(object sender, EventArgs e)
        {
            GameOfClicks();

        }
        private void panel5_Click(object sender, EventArgs e)
        {
            GameOfClicks();

        }
        private void panel6_Click(object sender, EventArgs e)
        {
            GameOfClicks();

        }
        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnVerificarCode.PerformClick();
            }
        }
        public bool ExisteDatos()
        {
            using (var context = new StoreContext())
            {
                return context.AuxAsistencia.Any();
            }
        }

        private void btnShowRegisters_Click(object sender, EventArgs e)
        {
            try
            {
                // guardamos el código en una variaable
                limpiarCampos();

                // buscamos el id de la persona según su código
                _idPersonal = int.Parse(txtIdPersonalRegisters.Text);

                //txtCodigo.Text = txtCod;

                if (PersonalName(_idPersonal) != null)
                {
                    //lblStatusProcess.Left = _xStatus + 150;
                    lblStatusProcess.Text = "VERIFICADO.";
                    lblStatusProcess.ForeColor = Color.Green;
                    lblStatusProcess.Visible = true;
                    // carga los datos del empleado en el label
                    lblNombre.Text = PersonalName(_idPersonal);
                    //Muestra en pantalla los datos y hora
                    lblNombre.Visible = true;


                    _functionsDataBase = new ApiService.FunctionsDataBase();
                    _functionsDataBase.LimpiarAuxAsistencia();
                    _functionsDataBase.LoadDataBaseAsistencia(_idPersonal);

                    if (ExisteDatos())
                    {
                        CargarMesesEnComboBox(_idPersonal);
                        //mostrar valores por defecto
                        if (cbxPersonalMonth.Items.Count > 0)
                        {
                            cbxPersonalMonth.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        MessageBox.Show("El personal no tiene datos de asistencia registrados");
                    }

                }
                else
                {
                    //lblStatusProcess.Left = _xStatus + 50;
                    lblStatusProcess.Text = "RECHAZADO.";
                    lblStatusProcess.ForeColor = Color.Red;
                    lblStatusProcess.Visible = true;

                    //txtCodigo.Text = "";
                    MessageBox.Show("El personal no está cargado.", "Error");
                }
            }
            catch (DbUpdateException ex)
            {
                // Si hay una excepción interna, muestra su mensaje; de lo contrario, muestra el mensaje de la excepción principal.
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "Error al guardar datos");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                lblNombre.Visible = false;
                MessageBox.Show("error: " + ex.Message, "Error");

            }
        }

        private void filtrosDGV()
        {
            // Iterar sobre las filas y aplicar el formato deseado
            foreach (DataGridViewRow fila in dgvListDelay.Rows)
            {
                fila.DefaultCellStyle.Font = new Font(dgvListDelay.Font, FontStyle.Bold); // Texto en negrita
                fila.DefaultCellStyle.ForeColor = Color.Black; // Color del texto en negro

            }

            // Deshabilitar la capacidad del usuario para cambiar la altura de la fila
            dgvListDelay.AllowUserToResizeRows = false;

            // Ocultar la primera fila que muestra la flecha de ordenamiento
            dgvListDelay.RowHeadersVisible = false;



            dgvListDelay.MultiSelect = false;


            // Establecer la propiedad DataGridView.AutoGenerateColumns en true si no se ha hecho previamente
            dgvListDelay.AutoGenerateColumns = true;


        }
    }
}

