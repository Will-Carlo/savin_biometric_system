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
    public partial class frmAsistencia : VerifyForm
    {
        private MetodosAsistencia m = new MetodosAsistencia();
        private readonly ApiService.ApiService _apiService;

        public frmAsistencia()
        {
            InitializeComponent();
            btnVerificarHuellaCod.Text = "Leer Huella";
            _apiService = new ApiService.ApiService();
        }


        private void btnVerificarHuellaCod_Click_1(object sender, EventArgs e)
        {
            m.setCapturaHoraMarcado(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            frmVerificar verificar = new frmVerificar();
            verificar.ShowDialog();

            try
            {
                // si la verificación es correcta muestra en verde 'VERIFICADO'
                //if (verificar.statusProcess)
                if (verificar.statusProcess)
                {
                    if (!m.EsRegistroDoble(verificar.idEncontrado))
                    {
                        string tipoMov = m.capturaTipoMovimiento(verificar.idEncontrado) == 461 ? "ENTRADA" : "SALIDA";
                        lblStatusProcess.Text = tipoMov + " VERIFICADA";
                        lblStatusProcess.ForeColor = Color.Green;
                        lblStatusProcess.Visible = true;
                        // carga los datos del empleado en el label
                        lblHora.Text = m.getHora();
                        lblNombre.Text = verificar.personalName;
                        //Muestra en pantalla los datos y hora
                        lblNombre.Visible = true;
                        lblHora.Visible = true;

                        RrhhAsistencia regisAsis = new RrhhAsistencia()
                        {
                            IdTurno = m.getIdTurno(verificar.idEncontrado),
                            IdPersonal = verificar.idEncontrado,
                            HoraMarcado = m.getHoraMarcado(),
                            MinutosAtraso = m.getMinutosAtraso(verificar.idEncontrado),
                            IndTipoMovimiento = m.getIndTipoMovimiento(verificar.idEncontrado),
                            IdPuntoAsistencia = m.getIdPuntoAsistencia()
                        };
                    
                        // v1
                        // m.setAddAsistencia(regisAsis);
                        // Enviando datos al API REST
                        // var response = _apiService.RegistrarAsistenciaAsync(regisAsis);
                        // -----

                        m.setAddAsistencia(regisAsis);
                        // Enviando datos al API REST
                        var response = _apiService.RegistrarAsistenciaAsync(regisAsis);
                    }
                    else
                    {
                        string tipoMov2 = m.capturaTipoMovimiento(verificar.idEncontrado) != 461 ? "ENTRADA" : "SALIDA";
                        m.NotificationMessage("Cuidado estás volviendo a marcar tu: " + tipoMov2+"\nDebes esperar al menos 5 min. para volver a marcar.", "alert");
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
            catch (DbUpdateException ex)
            {
                // Si hay una excepción interna, muestra su mensaje; de lo contrario, muestra el mensaje de la excepción principal.
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "Error al guardar datos");
                CleanLabels();

            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Error: "+ex.Message);
                CleanLabels();

            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message, "Error");
                CleanLabels();

            }

        }

        private void CleanLabels()
        {
            lblStatusProcess.Text = "RECHAZADO...";
            lblStatusProcess.ForeColor = Color.Red;
            lblStatusProcess.Visible = true;
            // No muestra en pantalla los datos y hora por el rechazo
            lblNombre.Visible = false;
            lblHora.Visible = false;
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
