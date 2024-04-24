using control_asistencia_savin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using control_asistencia_savin.Notifications;
using Microsoft.Extensions.Logging;

namespace control_asistencia_savin
{
    public partial class frmVerificar : CaptureForm
    {

        private readonly Microsoft.Extensions.Logging.ILogger _logger = LoggingManager.GetLogger<frmVerificar>();

        private System.Timers.Timer timer;



        private DPFP.Template Template;
        private DPFP.Verification.Verification Verificator;
        private StoreContext contexto;

        public String personalName;
        public int idEncontrado;
        public bool statusProcess;


        public event EventHandler DatosCapturados;

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
            if (InvokeRequired)
            {
                Invoke(new Action(() => Process(Sample)));
                return;
            }
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
                                _logger.LogDebug("Huella verificada.");
                                personalName = emp.Nombres + " " + emp.Paterno + " " + emp.Materno;
                                idEncontrado = emp.Id;
                                statusProcess = true;
                                //MessageBox.Show("stt: " + statusProcess);
                                //this.Close();
                                Close();
                                Stop();
                                
                                //break;
                            }
                            else
                            {
                                //MessageBox.Show("stt: " + statusProcess);

                                statusProcess = false;
                                SetPrompt("Vuleve a colocar tu huella.");
                            }
                        }
                    }
                }

            }
        }

        public frmVerificar()
        {
            contexto = new StoreContext();
            InitializeComponent();
        }
    }
}
