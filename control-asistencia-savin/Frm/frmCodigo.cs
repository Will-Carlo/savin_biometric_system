using control_asistencia_savin.ApiService;
using control_asistencia_savin.Frm;
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

namespace control_asistencia_savin
{
    public partial class frmCodigo : VerifyForm
    {
        private MetodosAsistencia m = new MetodosAsistencia();
        private MetodosAsistenciaTemporalTable mtt = new MetodosAsistenciaTemporalTable();
        private readonly ApiService.ApiService _apiService;
        private ApiService.FunctionsDataBase _functionsDataBase = new FunctionsDataBase();


        public frmCodigo()
        {
            InitializeComponent();
            btnVerificarHuellaCod.Text = "Leer Código";
            lblStatusProcess.Left = 620;
            //lblStatusProcess.Top = 200;
            txtCodigo.Enabled = true;
            //lblStatusProcess.Enabled = false;
            _apiService = new ApiService.ApiService();
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

        private void btnVerificarHuellaCod_Click(object sender, EventArgs e)
        {
            //if (_functionsDataBase.verifyConection())
            //{
            //    m.registrarAsistenciasTemporales();
            //    _functionsDataBase.LimpiarDB();
            //    _functionsDataBase.loadDataBase();
            //}

            try
            {
                // buscamos el id de la persona según su código
                int IdPersonal = BuscarIdPersonal(txtCodigo.Text);
                // guardamos el código en una variaable
                String txtCod = txtCodigo.Text;
                txtCodigo.Text = "";
                if (txtCod != "")
                {
                    // el sistema detecta cuando hay conexión a internet, 404 o 500.
                    if (_functionsDataBase.verifyConection() || !_functionsDataBase.verifyAnteriorRegistroTT(IdPersonal))
                    {
                        this.RegistroAsistencia(IdPersonal);
                    }
                    else
                    {
                        this.RegistroAsistenciaTemporalTable(IdPersonal);
                    }

                }
                else
                {
                    lblStatusProcess.Text = "";
                    lblStatusProcess.Visible = false;
                    MessageBox.Show("Debes ingresar un código.", "Error");
                    this.CleanLabels();
                }

            }
            catch (DbUpdateException ex)
            {
                // Si hay una excepción interna, muestra su mensaje; de lo contrario, muestra el mensaje de la excepción principal.
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "Error al guardar datos");
            }

            catch (HttpRequestException ex)
            {
                m.NotificationMessage("Error: " + ex.Message, "alert");
            }
            catch (Exception ex)
            {
                lblNombre.Visible = false;
                lblHora.Visible = false;
                m.NotificationMessage("error: " + ex.Message, "alert");
            }
        }


        private void RegistroAsistencia(int idPersonalVal)
        {

            m.setCapturaHoraMarcado(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            if (PersonalName(idPersonalVal) != null)
            {
                if (!m.EsRegistroDoble(idPersonalVal))
                {
                    string tipoMov = m.capturaTipoMovimiento(idPersonalVal) == 461 ? "ENTRADA" : "SALIDA";
                    ShowInOut(tipoMov);
                    lblStatusProcess.Text = "CÓDIGO VERIFICADO";
                    lblInOut.Text = tipoMov;
                    lblStatusProcess.ForeColor = Color.Green;
                    lblStatusProcess.Visible = true;
                    // carga los datos del empleado en el label
                    lblHora.Text = m.getHora();
                    lblNombre.Text = PersonalName(idPersonalVal);
                    //Muestra en pantalla los datos y hora
                    lblNombre.Visible = true;
                    lblHora.Visible = true;

                    RrhhAsistencia regisAsis = new RrhhAsistencia()
                    {
                        IdTurno = m.getIdTurno(idPersonalVal),
                        IdPersonal = idPersonalVal,
                        HoraMarcado = m.getHoraMarcado(),
                        MinutosAtraso = m.getMinutosAtraso(idPersonalVal),
                        IndTipoMovimiento = m.getIndTipoMovimiento(idPersonalVal),
                        IdPuntoAsistencia = m.getIdPuntoAsistencia()
                    };

                    // Enviando asistencia al servidor o a la tabla temporal

                    m.ValidarAsistencia(regisAsis);
                }
                else
                {
                    string tipoMov2 = m.capturaTipoMovimiento(idPersonalVal) != 461 ? "ENTRADA" : "SALIDA";
                    m.NotificationMessage("Cuidado estás volviendo a marcar tu: " + tipoMov2 + "\nDebes esperar al menos 5 min. para volver a marcar.", "alert");
                    this.CleanLabels();
                }
            }
            else
            {
                this.CleanLabels();
                MessageBox.Show("Código incorrecto.", "Error");
            }
        }

        private void RegistroAsistenciaTemporalTable(int idPersonalVal)
        {

            mtt.setCapturaHoraMarcado(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            if (PersonalName(idPersonalVal) != null)
            {
                if (!mtt.EsRegistroDoble(idPersonalVal))
                {
                    string tipoMov = mtt.capturaTipoMovimiento(idPersonalVal) == 461 ? "ENTRADA" : "SALIDA";
                    ShowInOut(tipoMov);
                    lblStatusProcess.Text = "CÓDIGO VERIFICADO";
                    lblInOut.Text = tipoMov;
                    lblStatusProcess.ForeColor = Color.Green;
                    lblStatusProcess.Visible = true;
                    // carga los datos del empleado en el label
                    lblHora.Text = mtt.getHora();
                    lblNombre.Text = PersonalName(idPersonalVal);
                    //Muestra en pantalla los datos y hora
                    lblNombre.Visible = true;
                    lblHora.Visible = true;

                    RrhhAsistencia regisAsis = new RrhhAsistencia()
                    {
                        IdTurno = mtt.getIdTurno(idPersonalVal),
                        IdPersonal = idPersonalVal,
                        HoraMarcado = mtt.getHoraMarcado(),
                        MinutosAtraso = mtt.getMinutosAtraso(idPersonalVal),
                        IndTipoMovimiento = mtt.getIndTipoMovimiento(idPersonalVal),
                        IdPuntoAsistencia = mtt.getIdPuntoAsistencia()
                    };

                    // Enviando asistencia al servidor o a la tabla temporal

                    m.ValidarAsistencia(regisAsis);
                }
                else
                {
                    string tipoMov2 = mtt.capturaTipoMovimiento(idPersonalVal) != 461 ? "ENTRADA" : "SALIDA";
                    mtt.NotificationMessage("Cuidado estás volviendo a marcar tu: " + tipoMov2 + "\nDebes esperar al menos 5 min. para volver a marcar.", "alert");
                    this.CleanLabels();
                }
            }
            else
            {
                this.CleanLabels();
                MessageBox.Show("Código incorrecto.", "Error");
            }
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnVerificarHuellaCod.PerformClick();
            }
        }

        private void CleanLabels()
        {
            lblStatusProcess.Text = "CÓDIGO RECHAZADO";
            lblStatusProcess.ForeColor = Color.Red;
            lblStatusProcess.Visible = true;
            // No muestra en pantalla los datos y hora por el rechazo
            lblNombre.Visible = false;
            lblHora.Visible = false;
            ShowInOut("none");
        }
    }
}
