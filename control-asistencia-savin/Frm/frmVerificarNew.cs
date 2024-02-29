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

namespace control_asistencia_savin.Frm
{
    public partial class frmVerificarNew : CaptureFormForVerification
    {
        private DPFP.Template Template;
        private DPFP.Verification.Verification Verificator;
        private StoreContext contexto;

        public String personalName;
        public int idEncontrado;
        public bool statusProcess;


        public event EventHandler DatosCapturados;
        //´atributos para el regsitro

        private MetodosAsistencia m = new MetodosAsistencia();
        private readonly ApiService.ApiService _apiService;



        // Método que se llama cuando se han capturado los datos
        protected virtual void OnDatosCapturados()
        {
            DatosCapturados?.Invoke(this, EventArgs.Empty);
        }
        public void Verify(DPFP.Template template)
        {
            Template = template;
            ShowDialog();
        }
        protected override void Init()
        {
            base.Init();
            base.Text = "Coloca tu huella";
            Verificator = new DPFP.Verification.Verification();     // Create a fingerprint template verificator
            //UpdateStatus(0);
        }
        //private void UpdateStatus(int FAR)
        //{
        //    // Show "False accept rate" value
        //    SetStatus(String.Format("False Accept Rate (FAR) = {0}", FAR));
        //}
        protected override void Process(DPFP.Sample Sample)
        {
            base.Process(Sample);

            // Process the sample and create a feature set for the enrollment purpose.
            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);

            // Check quality of the sample and start verification if it's good
            // TODO: move to a separate task
            if (features != null)
            {
                //Compare the feature set with our template
                DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();

                DPFP.Template template = new DPFP.Template();
                Stream stream;

                foreach (var emp in contexto.RrhhPersonals)
                {
                    List<byte[]> FingerList = new List<byte[]>();
                    FingerList.Add(emp.IndiceDerecho);
                    FingerList.Add(emp.IndiceIzquierdo);
                    FingerList.Add(emp.PulgarDerecho);
                    FingerList.Add(emp.PulgarIzquierdo);


                    //MessageBox.Show("1. " + emp.IndiceDerecho);
                    //MessageBox.Show("2. " + emp.IndiceIzquierdo);
                    //MessageBox.Show("3. " + emp.PulgarDerecho);
                    //MessageBox.Show("4. " + emp.PulgarIzquierdo);


                    foreach (byte[] finger in FingerList)
                    {
                        if (finger != null && !statusProcess)
                        {
                            //MessageBox.Show("ind: " + finger);
                            stream = new MemoryStream(finger);
                            template = new DPFP.Template(stream);
                            Verificator.Verify(features, template, ref result);

                            // Cuando el usuario es encontrado
                            if (result.Verified)
                            {
                                SetPrompt("VERIFICADO");

                                personalName = emp.Nombres + " " + emp.Paterno + " " + emp.Materno;
                                idEncontrado = emp.Id;
                                statusProcess = true;
                                //MessageBox.Show("stt: " + statusProcess);
                                Stop();
                                //this.Close();

                                //break;
                            }
                            else
                            {
                                //MessageBox.Show("stt: " + statusProcess);

                                statusProcess = false;
                                SetPrompt("RECHAZADO");
                            }
                        }
                    }
                }

            }
        }

        public frmVerificarNew()
        {
            contexto = new StoreContext();
            InitializeComponent();

            btnVerificarHuellaCod.Text = "Leer Huella";
            _apiService = new ApiService.ApiService();
        }

        private void btnVerificarHuellaCod_Click(object sender, EventArgs e)
        {
            //frmVerificar verificar = new frmVerificar();
            //verificar.ShowDialog();

            this.Init();



            m.setCapturaHoraMarcado(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            try
            {
                // si la verificación es correcta muestra en verde 'VERIFICADO'
                //if (verificar.statusProcess)
                if (this.statusProcess)
                {
                    if (!m.EsRegistroDoble(this.idEncontrado))
                    {
                        string tipoMov = m.capturaTipoMovimiento(this.idEncontrado) == 461 ? "ENTRADA" : "SALIDA";
                        lblStatusProcess.Text = tipoMov + " VERIFICADA";
                        lblStatusProcess.ForeColor = Color.Green;
                        lblStatusProcess.Visible = true;
                        // carga los datos del empleado en el label
                        lblHora.Text = m.getHora();
                        lblNombre.Text = this.personalName;
                        //Muestra en pantalla los datos y hora
                        lblNombre.Visible = true;
                        lblHora.Visible = true;

                        RrhhAsistencia regisAsis = new RrhhAsistencia()
                        {
                            IdTurno = m.getIdTurno(this.idEncontrado),
                            IdPersonal = this.idEncontrado,
                            HoraMarcado = m.getHoraMarcado(),
                            MinutosAtraso = m.getMinutosAtraso(this.idEncontrado),
                            IndTipoMovimiento = m.getIndTipoMovimiento(this.idEncontrado),
                            IdPuntoAsistencia = m.getIdPuntoAsistencia()
                        };

                        // Enviando datos al API REST

                        m.ValidarAsistencia(regisAsis);
                    }
                    else
                    {
                        string tipoMov2 = m.capturaTipoMovimiento(this.idEncontrado) != 461 ? "ENTRADA" : "SALIDA";
                        m.NotificationMessage("Cuidado estás volviendo a marcar tu: " + tipoMov2 + "\nDebes esperar al menos 5 min. para volver a marcar.", "alert");
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
                m.NotificationMessage("Error: " + ex.Message, "alert");
                CleanLabels();
            }
            catch (Exception ex)
            {
                m.NotificationMessage("error: " + ex.Message, "alert");
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
    }
}
