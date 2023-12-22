using control_asistencia_savin.Models2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace control_asistencia_savin
{
    public partial class frmVerificar : CaptureForm
    {
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
                    stream = new MemoryStream(emp.HuellaIndDer);
                    template = new DPFP.Template(stream);

                    Verificator.Verify(features, template, ref result);
                    //UpdateStatus(result.FARAchieved);
                    if (result.Verified)
                    {
                        SetPrompt("VERIFICADO");
                        //MakeReport("CIERRE LA VENTANA PARA CONTINUAR.");

                        personalName = emp.Nombre +" "+ emp.Paterno + " " + emp.Materno;
                        idEncontrado = emp.Id;
                        statusProcess = true;
                        Stop();
                        break;
                    }
                    else
                    {
                        statusProcess = false;
                        SetPrompt("RECHAZADO");
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
