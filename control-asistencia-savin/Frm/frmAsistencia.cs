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
                if (verificar.statusProcess)
                {
                    lblStatusProcess.Text = "VERIFICADO...";
                    lblStatusProcess.ForeColor = Color.Green;
                    lblStatusProcess.Visible = true;
                    // carga los datos del empleado en el label
                    lblNombre.Text = verificar.personalName;
                    lblHora.Text = m.getHora();
                    //Muestra en pantalla los datos y hora
                    lblNombre.Visible = true;
                    lblHora.Visible = true;

                    RrhhAsistencia regisAsis = new RrhhAsistencia()
                    {
                        IdTurno = m.getIdTurno(verificar.idEncontrado),
                        IdPersonal = verificar.idEncontrado,
                        HoraMarcado = m.getCapturaHoraMarcado(),
                        MinutosAtraso = m.capturaMinAtraso(verificar.idEncontrado),
                        IndTipoMovimiento = m.capturaIndMov(verificar.idEncontrado)
                    };
                    m.AddAsistencia(regisAsis);

                    // Enviando datos al API REST
                    var response = _apiService.RegistrarAsistenciaAsync(regisAsis);
                    //if (response != null)
                    //{
                    //    MessageBox.Show("Asistencia enviada al servidor con éxito: " + response.Status);
                    //}
                }
                else
                {
                    lblStatusProcess.Text = "RECHAZADO...";
                    lblStatusProcess.ForeColor = Color.Red;
                    lblStatusProcess.Visible = true;
                    // No muestra en pantalla los datos y hora por el rechazo
                    lblNombre.Visible = false;
                    lblHora.Visible = false;
                }
            }
            catch (DbUpdateException ex)
            {
                // Si hay una excepción interna, muestra su mensaje; de lo contrario, muestra el mensaje de la excepción principal.
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "Error al guardar datos");
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Error: "+ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message, "Error");
            }

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
