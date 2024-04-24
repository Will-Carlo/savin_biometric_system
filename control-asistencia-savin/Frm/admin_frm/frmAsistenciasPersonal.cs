using control_asistencia_savin.ApiService;
using control_asistencia_savin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_asistencia_savin.Frm.admin_frm
{
    public partial class frmAsistenciasPersonal : Form
    {
        private FunctionsDataBase _functionsDataBase;
        private int _IdTienda;
        private string _NombreTienda;
        private Dictionary<string, int> nombresNumeros = new Dictionary<string, int>
            {
                { "ALMACÉN CENTRAL", 1 },
                { "GOITIA", 2 },
                { "OFICINA LOAYZA", 3 },
                { "TIENDA LOAYZA", 4 },
                { "TIENDA SATÉLITE", 5 },
                { "TIENDA CEIBO", 6 },
                { "TIENDA COCHA", 7 },
                { "SANTA CRUZ", 8 },
                { "TARIJA", 9 },
                { "ORURO", 10 },
                { "POTOSÍ", 11 },
                { "SUCRE", 12 },
                { "VARIABLE", 13 }
            };

        public frmAsistenciasPersonal()
        {

            InitializeComponent();
            _functionsDataBase = new FunctionsDataBase();


            cbxStore.DropDownStyle = ComboBoxStyle.DropDownList;

            foreach (string nombre in nombresNumeros.Keys)
            {
                cbxStore.Items.Add(nombre);
            }
            cbxStore.SelectedIndex = 0;

            // dgvPersonalAsistencias.CellFormatting += dgvPersonalAsistencias_CellFormatting;
        }

        private void btnCargarRegistros_Click(object sender, EventArgs e)
        {
            int? idPuntoAsistencia = _functionsDataBase.LoadRegistersNew(_IdTienda);
            //MessageBox.Show("Has seleccionado: " + this._NombreTienda);
            this.lblPuntoAsistencia.Text = idPuntoAsistencia.ToString();
            //_functionsDataBase.LimpiarDB();
            //_functionsDataBase.loadDataBase();
            if(_IdTienda == 13)
            {
                CargarDatosEnDataGridViewPersonalVariable();
            }
            else
            {
                CargarDatosEnDataGridView(idPuntoAsistencia);
            }
            filtrosDGV();
        }

        private void cbxStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nombreSeleccionado = cbxStore.SelectedItem.ToString();
            this._NombreTienda = nombreSeleccionado;

            // Verificar si el nombre seleccionado está en el diccionario
            if (nombresNumeros.ContainsKey(nombreSeleccionado))
            {
                this._IdTienda = nombresNumeros[nombreSeleccionado];
            }
        }


        public void CargarDatosEnDataGridView(int? idPuntoAsistencia)
        {


            using (var context = new StoreContext())
            {
                var asistencias = (from a in context.RrhhAsistencia
                                   join p in context.RrhhPersonals on a.IdPersonal equals p.Id
                                   where a.HoraMarcado != null && a.IdPuntoAsistencia == idPuntoAsistencia
                                   orderby a.IdPuntoAsistencia, p.Id
                                   select new
                                   {
                                       IdPersonal = p.Id,
                                       NombreCompleto = p.Paterno + " " + p.Materno + ", " + p.Nombres,
                                       HoraMarcado = DateTime.Parse(a.HoraMarcado).TimeOfDay, // Extraer solo la hora
                                       MinutosAtraso = a.MinutosAtraso,
                                       PuntoAsistencia = context.RrhhPuntoAsistencia.FirstOrDefault(pa => pa.Id == a.IdPuntoAsistencia).Nombre,
                                       TipoMovimiento = a.IndTipoMovimiento == 461 ? "entrada" :
                                                        a.IndTipoMovimiento == 462 ? "salida" :
                                                        a.IndTipoMovimiento == 469 ? "falta" : "",
                                       Turno = a.IdTurno == 1 ? "mañana" :
                                                        a.IdTurno == 2 ? "tarde" :
                                                        a.IdTurno == 3 ? "sábado" :
                                                        a.IdTurno == 4 ? "completo" : ""
                                   }).ToList();

                //return asistencias;
                dgvPersonalAsistencias.DataSource = asistencias;
            }

        }

        public void CargarDatosEnDataGridViewPersonalVariable()
        {
            using (var context = new StoreContext())
            {
                var asistencias = (from a in context.RrhhAsistencia
                                   join p in context.RrhhPersonals on a.IdPersonal equals p.Id
                                   join ta in context.RrhhTurnoAsignados on p.Id equals ta.IdPersonal
                                   where a.HoraMarcado != null
                                         && ta.IndMarcadoFijoVariable == 448
                                   orderby a.IdPuntoAsistencia, p.Id
                                   select new
                                   {
                                       IdPersonal = p.Id,
                                       NombreCompleto = p.Paterno + " " + p.Materno + ", " + p.Nombres,
                                       HoraMarcado = DateTime.Parse(a.HoraMarcado).TimeOfDay, // Extraer solo la hora
                                       MinutosAtraso = a.MinutosAtraso,
                                       PuntoAsistencia = context.RrhhPuntoAsistencia.FirstOrDefault(pa => pa.Id == a.IdPuntoAsistencia).Nombre,
                                       TipoMovimiento = a.IndTipoMovimiento == 461 ? "entrada" :
                                                        a.IndTipoMovimiento == 462 ? "salida" :
                                                        a.IndTipoMovimiento == 469 ? "falta" : "",
                                       Turno = a.IdTurno == 1 ? "mañana" :
                                                        a.IdTurno == 2 ? "tarde" :
                                                        a.IdTurno == 3 ? "sábado" :
                                                        a.IdTurno == 4 ? "completo" : ""
                                   }).Distinct().ToList(); // Aplicar Distinct() para obtener resultados únicos

                //return asistencias;
                dgvPersonalAsistencias.DataSource = asistencias;
            }
        }



        private void filtrosDGV()
        {
            // Iterar sobre las filas y aplicar el formato deseado
            foreach (DataGridViewRow fila in dgvPersonalAsistencias.Rows)
            {
                fila.DefaultCellStyle.Font = new Font(dgvPersonalAsistencias.Font, FontStyle.Bold); // Texto en negrita
                fila.DefaultCellStyle.ForeColor = Color.Black; // Color del texto en negro

            }

            // Deshabilitar la capacidad del usuario para cambiar la altura de la fila
            dgvPersonalAsistencias.AllowUserToResizeRows = false;

            // Ocultar la primera fila que muestra la flecha de ordenamiento
            dgvPersonalAsistencias.RowHeadersVisible = false;



            dgvPersonalAsistencias.MultiSelect = false;


            // Establecer la propiedad DataGridView.AutoGenerateColumns en true si no se ha hecho previamente
            dgvPersonalAsistencias.AutoGenerateColumns = true;


        }


        private void dgvPersonalAsistencias_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvPersonalAsistencias.Rows.Count)
            {
                // Obtener el valor del IdPersonal en la fila actual
                int idPersonalActual = (int)dgvPersonalAsistencias.Rows[e.RowIndex].Cells["IdPersonal"].Value;

                // Definir los colores que deseas utilizar
                Color[] colores = { Color.LightBlue, Color.LightGreen, Color.LightPink, Color.LightYellow, Color.LightCoral, Color.LightSalmon }; // Agrega más colores según necesites

                // Asignar un color diferente para cada IdPersonal
                dgvPersonalAsistencias.Rows[e.RowIndex].DefaultCellStyle.BackColor = colores[idPersonalActual % colores.Length];
            }
        }

        private void dgvPersonalAsistencias_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvPersonalAsistencias.Columns["MinutosAtraso"].Index && e.Value != null)
            {
                int minutosAtraso = (int)e.Value;
                if (minutosAtraso > 0)
                {
                    e.CellStyle.ForeColor = Color.Red; // Si el minuto de atraso es mayor a cero, pintar en rojo
                }
            }
        }
    }
}
