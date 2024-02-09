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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_asistencia_savin.Frm
{
    public partial class frmAsistenciaSimulation : Form
    {
        private MetodosAsistencia m = new MetodosAsistencia();
        private readonly ApiService.ApiService _apiService;

        public frmAsistenciaSimulation()
        {
            InitializeComponent();
            //btnVerificarHuellaCod.Text = "Leer Huella";
            _apiService = new ApiService.ApiService();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            m.setCapturaHoraMarcado(txtFecha.Text);

            //frmVerificar verificar = new frmVerificar();
            //verificar.ShowDialog();

            try
            {
                // si la verificación es correcta muestra en verde 'VERIFICADO'
                //if (verificar.statusProcess)
                //if (verificar.statusProcess)
                //{
                int IdPersonal = int.Parse(txtIdPersonal.Text);
                string tipoMov = m.capturaTipoMovimiento(IdPersonal) == 461 ? "ENTRADA" : "SALIDA";
                //lblStatusProcess.Text = tipoMov + " VERIFICADA";
                //lblStatusProcess.ForeColor = Color.Green;
                //lblStatusProcess.Visible = true;
                // carga los datos del empleado en el label
                //lblHora.Text = m.getHora();
                //lblNombre.Text = verificar.personalName;
                //Muestra en pantalla los datos y hora
                //lblNombre.Visible = true;
                //lblHora.Visible = true;
                RrhhAsistencia regisAsis = new RrhhAsistencia()
                {
                    IdTurno = m.getIdTurno(IdPersonal),
                    IdPersonal = IdPersonal,
                    HoraMarcado = m.getHoraMarcado(),
                    MinutosAtraso = m.getMinutosAtraso(IdPersonal),
                    IndTipoMovimiento = m.getIndTipoMovimiento(IdPersonal),
                    IdPuntoAsistencia = m.getIdPuntoAsistencia()
                };
                if (!m.EsRegistroDoble(IdPersonal))
                {
                    m.setAddAsistencia(regisAsis);
                    // Enviando datos al API REST
                    //var response = _apiService.RegistrarAsistenciaAsync(regisAsis);
                }
                else
                {
                    string tipoMov2 = m.capturaTipoMovimiento(IdPersonal) != 461 ? "ENTRADA" : "SALIDA";

                    MessageBox.Show("Cuidado estás volviendo a marcar tu: "+ tipoMov2);
                }

                //if (response != null)
                //{
                //    MessageBox.Show("Asistencia enviada al servidor con éxito: " + response.Status);
                //}
                //}
                //else
                //{
                //    CleanLabels();
                //}
            }
            catch (DbUpdateException ex)
            {
                // Si hay una excepción interna, muestra su mensaje; de lo contrario, muestra el mensaje de la excepción principal.
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "Error al guardar datos");
                CleanLabels();

            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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
            //lblStatusProcess.Text = "RECHAZADO...";
            //lblStatusProcess.ForeColor = Color.Red;
            //lblStatusProcess.Visible = true;
            // No muestra en pantalla los datos y hora por el rechazo
            //lblNombre.Visible = false;
            //lblHora.Visible = false;
        }

        private void btnRegFaltas_Click(object sender, EventArgs e)
        {
            m.RegistrarFaltasDelDia();
        }
    }
}
