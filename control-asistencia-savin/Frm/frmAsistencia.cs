using control_asistencia_savin.ApiService;
using control_asistencia_savin.Frm;
using control_asistencia_savin.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_asistencia_savin
{
    public partial class frmAsistencia : VerifyForm
    {
        private MetodosAsistencia m = new MetodosAsistencia();
        //private MetodosAsistenciaTemporalTable mtt = new MetodosAsistenciaTemporalTable(); 
        private ApiService.FunctionsDataBase _functionsDataBase = new FunctionsDataBase();

        private readonly ApiService.ApiService _apiService;

        public frmAsistencia()
        {
            InitializeComponent();
            btnVerificarHuellaCod.Text = "Leer Huella";
            _apiService = new ApiService.ApiService();
        }


        private void btnVerificarHuellaCod_Click_1(object sender, EventArgs e)
        {
            //if (_functionsDataBase.verifyConection())
            //{
            //    m.registrarAsistenciasTemporales();
            //    _functionsDataBase.LimpiarDB();
            //    _functionsDataBase.loadDataBase();
            //}
            frmVerificar verificar = new frmVerificar();
            verificar.ShowDialog();

            


            try
            {
                // 1 1: !no hay conexión    existe registros
                // 1 0: !no hay conexión    no existe registro
                // 0 1: !hay conexión       existe registro
                // 0 0: !hay conexión       no existe registro


                //if (_functionsDataBase.verifyAnteriorRegistroTT(verificar.idEncontrado))
                //{
                //    if (_functionsDataBase.verifyConection())
                //    {
                //        m.registrarAsistenciasTemporales();
                //        _functionsDataBase.LimpiarDB();
                //        _functionsDataBase.loadDataBase();
                //    }
                //    else
                //    {
                //        m.setExisteConexion(false);
                //    }
                //}

                //this.RegistroAsistencia(verificar.statusProcess, verificar.idEncontrado, verificar.personalName);


                //1 1: hay conexión        !no existe registro
                // 1 0: hay conexión        !existe registro
                // 0 1: no hay conexión         !no existe registro
                // 0 0: no hay conexión         !existe registro

                // el sistema detecta cuando hay conexión a internet, 404 o 500.
                if (_functionsDataBase.verifyConection() || !_functionsDataBase.verifyAnteriorRegistroTT(verificar.idEncontrado))
                {
                    this.RegistroAsistencia(verificar.statusProcess, verificar.idEncontrado, verificar.personalName);
                }
                else
                {
                    m.setExisteConexion(false);
                    this.RegistroAsistencia(verificar.statusProcess, verificar.idEncontrado, verificar.personalName);
                }
            }
            catch (DbUpdateException ex)
            {
                // Si hay una excepción interna, muestra su mensaje; de lo contrario, muestra el mensaje de la excepción principal.
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "Error al guardar datos");
                CleanLabels();

            }
            catch (HttpRequestException ex)
            {
                m.NotificationMessage("Error: "+ex.Message, "alert");
                CleanLabels();
            }
            catch (Exception ex)
            {
                m.NotificationMessage("error: " + ex.Message, "alert");
                CleanLabels();
            }

        }


        private void RegistroAsistencia(bool statuProcessVal, int idPersonalVal, string NombrePersonalVal)
        {
            m.setCapturaHoraMarcado(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            if (statuProcessVal)
            {
                if (!m.EsRegistroDoble(idPersonalVal))
                {
                    string tipoMov = m.capturaTipoMovimiento(idPersonalVal) == 461 ? "ENTRADA" : "SALIDA";
                    ShowInOut(tipoMov);
                    lblStatusProcess.Text = "HUELLA VERIFICADA";
                    lblInOut.Text = tipoMov;
                    lblStatusProcess.ForeColor = Color.Green;
                    lblStatusProcess.Visible = true;
                    // carga los datos del empleado en el label
                    lblHora.Text = m.getHora();
                    lblNombre.Text = NombrePersonalVal;
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
                    m.NotificationMessage("Cuidado estás volviendo a marcar tu: " + tipoMov2 + "\nDebes esperar al menos 1 min. para volver a marcar.", "alert");
                    CleanLabels();
                }


                //if (response != null)
                //{
                //    MessageBox.Show("Asistencia enviada al servidor con éxito: " + response.Status);
                //}
            }
            else
            {
                CleanLabels();
            }
        }

        //private void RegistroAsistenciaTemporalTable2(bool statuProcessVal, int idPersonalVal, string NombrePersonalVal)
        //{
        //    m.setCapturaHoraMarcado(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //    m.setExisteConexion(false);

        //    if (statuProcessVal)
        //    {
        //        if (!m.EsRegistroDoble(idPersonalVal))
        //        {
        //            string tipoMov = m.capturaTipoMovimiento(idPersonalVal) == 461 ? "ENTRADA" : "SALIDA";
        //            ShowInOut(tipoMov);
        //            lblStatusProcess.Text = "HUELLA VERIFICADA";
        //            lblInOut.Text = tipoMov;
        //            lblStatusProcess.ForeColor = Color.Green;
        //            lblStatusProcess.Visible = true;
        //            // carga los datos del empleado en el label
        //            lblHora.Text = m.getHora();
        //            lblNombre.Text = NombrePersonalVal;
        //            //Muestra en pantalla los datos y hora
        //            lblNombre.Visible = true;
        //            lblHora.Visible = true;

        //            RrhhAsistencia regisAsis = new RrhhAsistencia()
        //            {
        //                IdTurno = m.getIdTurno(idPersonalVal),
        //                IdPersonal = idPersonalVal,
        //                HoraMarcado = m.getHoraMarcado(),
        //                MinutosAtraso = m.getMinutosAtraso(idPersonalVal),
        //                IndTipoMovimiento = m.getIndTipoMovimiento(idPersonalVal),
        //                IdPuntoAsistencia = m.getIdPuntoAsistencia()
        //            };

        //            // Enviando asistencia al servidor o a la tabla temporal

        //            m.ValidarAsistencia(regisAsis);
        //        }
        //        else
        //        {
        //            string tipoMov2 = m.capturaTipoMovimiento(idPersonalVal) != 461 ? "ENTRADA" : "SALIDA";
        //            m.NotificationMessage("Cuidado estás volviendo a marcar tu: " + tipoMov2 + "\nDebes esperar al menos 1 min. para volver a marcar.", "alert");
        //            CleanLabels();
        //        }


        //        //if (response != null)
        //        //{
        //        //    MessageBox.Show("Asistencia enviada al servidor con éxito: " + response.Status);
        //        //}
        //    }
        //    else
        //    {
        //        CleanLabels();
        //    }
        //}

        //private void RegistroAsistenciaTemporalTable(bool statuProcessVal, int idPersonalVal, string NombrePersonalVal)
        //{
        //    mtt.setCapturaHoraMarcado(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        //    if (statuProcessVal)
        //    {
        //        if (!mtt.EsRegistroDoble(idPersonalVal))
        //        {
        //            string tipoMov = mtt.capturaTipoMovimiento(idPersonalVal) == 461 ? "ENTRADA" : "SALIDA";
        //            ShowInOut(tipoMov);
        //            lblStatusProcess.Text = "HUELLA VERIFICADA";
        //            lblInOut.Text = tipoMov;
        //            lblStatusProcess.ForeColor = Color.Green;
        //            lblStatusProcess.Visible = true;
        //            // carga los datos del empleado en el label
        //            lblHora.Text = mtt.getHora();
        //            lblNombre.Text = NombrePersonalVal;
        //            //Muestra en pantalla los datos y hora
        //            lblNombre.Visible = true;
        //            lblHora.Visible = true;

        //            RrhhAsistencia regisAsis = new RrhhAsistencia()
        //            {
        //                IdTurno = mtt.getIdTurno(idPersonalVal),
        //                IdPersonal = idPersonalVal,
        //                HoraMarcado = mtt.getHoraMarcado(),
        //                MinutosAtraso = mtt.getMinutosAtraso(idPersonalVal),
        //                IndTipoMovimiento = mtt.getIndTipoMovimiento(idPersonalVal),
        //                IdPuntoAsistencia = mtt.getIdPuntoAsistencia()
        //            };

        //            // Enviando asistencia al servidor o a la tabla temporal

        //            m.ValidarAsistencia(regisAsis);
        //        }
        //        else
        //        {
        //            string tipoMov2 = mtt.capturaTipoMovimiento(idPersonalVal) != 461 ? "ENTRADA" : "SALIDA";
        //            mtt.NotificationMessage("Cuidado estás volviendo a marcar tu: " + tipoMov2 + "\nDebes esperar al menos 1 min. para volver a marcar.", "alert");
        //            CleanLabels();
        //        }


        //        //if (response != null)
        //        //{
        //        //    MessageBox.Show("Asistencia enviada al servidor con éxito: " + response.Status);
        //        //}
        //    }
        //    else
        //    {
        //        CleanLabels();
        //    }
        //}

        private void CleanLabels()
        {
            lblStatusProcess.Text = "HUELLA RECHAZADA";
            lblStatusProcess.ForeColor = Color.Red;
            lblStatusProcess.Visible = true;
            // No muestra en pantalla los datos y hora por el rechazo
            lblNombre.Visible = false;
            lblHora.Visible = false;
            ShowInOut("none");
        }

        //private void btnVerificar_Click_1(object sender, EventArgs e)
        //{
        //    m.setCapturaHoraMarcado(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        //    frmVerificar verificar = new frmVerificar();
        //    verificar.ShowDialog();

        //    try
        //    {
        //        // si la verificación es correcta muestra en verde 'VERIFICADO'
        //        if (verificar.statusProcess)
        //        {
        //            lblStatusProcess.Text = "VERIFICADO...";
        //            lblStatusProcess.ForeColor = Color.Green;
        //            lblStatusProcess.Visible = true;
        //            // carga los datos del empleado en el label
        //            lblNombre.Text = verificar.personalName;
        //            lblHora.Text = m.getHora();
        //            //Muestra en pantalla los datos y hora
        //            lblNombre.Visible = true;
        //            lblHora.Visible = true;

        //            RrhhAsistencia regisAsis = new RrhhAsistencia()
        //            {
        //                IdTurno = m.capturaIdTurno(),
        //                IdPersonal = verificar.idEncontrado,
        //                HoraMarcado = m.getCapturaHoraMarcado(),
        //                MinutosAtraso = m.capturaMinAtraso(verificar.idEncontrado),
        //                IndTipoMovimiento = m.capturaIndMov(verificar.idEncontrado)
        //            };
        //            m.AddAsistencia(regisAsis);
        //        }
        //        else
        //        {
        //            lblStatusProcess.Text = "RECHAZADO...";
        //            lblStatusProcess.ForeColor = Color.Red;
        //            lblStatusProcess.Visible = true;
        //            // No muestra en pantalla los datos y hora por el rechazo
        //            lblNombre.Visible = false;
        //            lblHora.Visible = false;
        //        }
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        // Si hay una excepción interna, muestra su mensaje; de lo contrario, muestra el mensaje de la excepción principal.
        //        MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "Error al guardar datos");

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("error: " + ex.Message, "Error");
        //    }
        //}


    }
}
