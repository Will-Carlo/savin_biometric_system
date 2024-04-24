using control_asistencia_savin.ApiService;
using control_asistencia_savin.Models;
using control_asistencia_savin.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_asistencia_savin.Frm
{
    public partial class frmAsistenciaSimulation : Form
    {
        private MetodosAsistencia m = new MetodosAsistencia();
        //private MetodosAsistenciaTemporalTable mtt = new MetodosAsistenciaTemporalTable();

        private readonly ApiService.ApiService _apiService;
        private ApiService.FunctionsDataBase _functionsDataBase = new FunctionsDataBase();
        private readonly Microsoft.Extensions.Logging.ILogger _logger = LoggingManager.GetLogger<frmAsistenciaSimulation>();

        public frmAsistenciaSimulation()
        {
            InitializeComponent();
            //btnVerificarHuellaCod.Text = "Leer Huella";
            _apiService = new ApiService.ApiService();
        }

        private void button1_Click(object sender, EventArgs e)
        {



            try
            {
                int IdPersonal = int.Parse(txtIdPersonal.Text);

                if (!_functionsDataBase.verifyConection() || _functionsDataBase.verifyAnteriorRegistroTT(IdPersonal))
                {
                    m.setExisteConexion(false);
                }

                this.RegistroAsistencia(IdPersonal);


                // el sistema detecta cuando hay conexión a internet, 404 o 500.
                //if (_functionsDataBase.verifyConection() || !_functionsDataBase.verifyAnteriorRegistroTT(IdPersonal))
                //{
                //    this.RegistroAsistencia(IdPersonal);
                //}
                //else
                //{
                //    m.setExisteConexion(false);
                //    this.RegistroAsistencia(IdPersonal);
                //}
            }
            catch (DbUpdateException ex)
            {
                // Si hay una excepción interna, muestra su mensaje; de lo contrario, muestra el mensaje de la excepción principal.
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "Error al guardar datos");
                _logger.LogError("Error al registrar asistencia: " + ex.Message);
                //CleanLabels();

            }
            catch (HttpRequestException ex)
            {
                m.NotificationMessage("Error: " + ex.Message, "alert");
                _logger.LogError("Error al registrar asistencia: " + ex.Message);
                //CleanLabels();
            }
            catch (Exception ex)
            {
                m.NotificationMessage("error: " + ex.Message, "alert");
                _logger.LogError("Error al registrar asistencia: " + ex.Message);
                //CleanLabels();
            }

        }

        private void RegistroAsistencia(int idPersonalVal)
        {
            m.setCapturaHoraMarcado(txtFecha.Text);

            if (!m.EsRegistroDoble(idPersonalVal))
            {
                   
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
                m.NotificationMessage("Cuidado estás volviendo a marcar tu: " + tipoMov2 + "\nDebes esperar al menos 1 min. para volver a marcar.", "alert");
                _logger.LogInformation($"El usuario {idPersonalVal} está marcando por segunda vez su asistencia.");

            }

        }

        private void RegistroAsistenciaTemporalTable(int idPersonalVal)
        {
            m.setCapturaHoraMarcado(txtFecha.Text);
            m.setExisteConexion(false);

            if (!m.EsRegistroDoble(idPersonalVal))
            {


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
                m.NotificationMessage("Cuidado estás volviendo a marcar tu: " + tipoMov2 + "\nDebes esperar al menos 1 min. para volver a marcar.", "alert");
            }
        }

        //private void RegistroAsistenciaTemporalTable(int idPersonalVal)
        //{
        //    mtt.setCapturaHoraMarcado(txtFecha.Text);


        //    if (!mtt.EsRegistroDoble(idPersonalVal))
        //    {


        //        RrhhAsistencia regisAsis = new RrhhAsistencia()
        //        {
        //            IdTurno = mtt.getIdTurno(idPersonalVal),
        //            IdPersonal = idPersonalVal,
        //            HoraMarcado = mtt.getHoraMarcado(),
        //            MinutosAtraso = mtt.getMinutosAtraso(idPersonalVal),
        //            IndTipoMovimiento = mtt.getIndTipoMovimiento(idPersonalVal),
        //            IdPuntoAsistencia = mtt.getIdPuntoAsistencia()
        //        };

        //        // Enviando asistencia al servidor o a la tabla temporal

        //        m.ValidarAsistencia(regisAsis);
        //    }
        //    else
        //    {
        //        string tipoMov2 = mtt.capturaTipoMovimiento(idPersonalVal) != 461 ? "ENTRADA" : "SALIDA";
        //        mtt.NotificationMessage("Cuidado estás volviendo a marcar tu: " + tipoMov2 + "\nDebes esperar al menos 1 min. para volver a marcar.", "alert");
        //    }
        //}


        private void btnRegFaltas_Click(object sender, EventArgs e)
        {
            m.RegistrarFaltasDelDia();
        }
    }
}
