using control_asistencia_savin;
using control_asistencia_savin.Models;
using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace control_asistencia_savin
{
    public partial class frmRegistrar : Form
    {
        private DPFP.Template? TemplateIndDer = null;
        private DPFP.Template? TemplateIndIzq = null;
        private DPFP.Template? TemplatePulDer = null;
        private DPFP.Template? TemplatePulIzq = null;
        private Models.StoreContext? contexto;
        public frmRegistrar()
        {
            InitializeComponent();
        }
        private void OnTemplateIndDer(DPFP.Template template)
        {
            this.Invoke(new Function(delegate ()
            {
                TemplateIndDer = template;
                btnGuardarUsuario.Enabled = (TemplateIndDer != null);
                if (TemplateIndDer != null)
                {
                    MessageBox.Show("Registro de huella exitoso.", "Registro de huellas dactilares");
                    txtIndiceDerecho.Text = "Huella capturada correctamente";
                }
                else
                {
                    MessageBox.Show("La huella dactilar no es válida. Repita el registro de huella.", "Registro de huellas dactilares");
                }
            }));
        }
        private void OnTemplateIndIzq(DPFP.Template template)
        {
            this.Invoke(new Function(delegate ()
            {
                TemplateIndIzq = template;
                btnGuardarUsuario.Enabled = (TemplateIndIzq != null);
                if (TemplateIndIzq != null)
                {
                    MessageBox.Show("Registro de huella exitoso.", "Registro de huellas dactilares");
                    txtIndiceIzquierdo.Text = "Huella capturada correctamente";
                }
                else
                {
                    MessageBox.Show("La huella dactilar no es válida. Repita el registro de huella.", "Registro de huellas dactilares");
                }
            }));
        }
        private void OnTemplatePulDer(DPFP.Template template)
        {
            this.Invoke(new Function(delegate ()
            {
                TemplatePulDer = template;
                btnGuardarUsuario.Enabled = (TemplatePulDer != null);
                if (TemplatePulDer != null)
                {
                    MessageBox.Show("Registro de huella exitoso.", "Registro de huellas dactilares");
                    txtPulgarDerecho.Text = "Huella capturada correctamente";
                }
                else
                {
                    MessageBox.Show("La huella dactilar no es válida. Repita el registro de huella.", "Registro de huellas dactilares");
                }
            }));
        }
        private void OnTemplatePulIzq(DPFP.Template template)
        {
            this.Invoke(new Function(delegate ()
            {
                TemplatePulIzq = template;
                btnGuardarUsuario.Enabled = (TemplatePulIzq != null);
                if (TemplatePulIzq != null)
                {
                    MessageBox.Show("Registro de huella exitoso.", "Registro de huellas dactilares");
                    txtPulgarIzquierdo.Text = "Huella capturada correctamente";
                }
                else
                {
                    MessageBox.Show("La huella dactilar no es válida. Repita el registro de huella.", "Registro de huellas dactilares");
                }
            }));
        }
        private void frmRegistrar_Load(object sender, EventArgs e)
        {
            contexto = new Models.StoreContext();
            Listar();
        }
        private void Limpiar()
        {
            txtId_ciudad.Text = "";
            txtPaterno.Text = "";
            txtMaterno.Text = "";
            txtNombre.Text = "";
            txtIndiceDerecho.Text = "";
            txtIndiceIzquierdo.Text = "";
            txtPulgarDerecho.Text = "";
            txtPulgarIzquierdo.Text = "";
        }
        private void Listar()
        {
            try
            {
                //string fingerAux = Encoding.UTF8.GetString(personal.IndiceIzquierdo);
                //byte[] datoPrueba = Convert.FromBase64String(fingerAux);
                //personal.IndiceIzquierdo = datoPrueba;
                var personal = from emp in contexto.RrhhPersonals
                               select new
                               {
                                   ID = emp.Id,
                                   CIUDAD = emp.IdCiudad,
                                   EMPLEADO = emp.Nombres + " " + emp.Paterno + " " + emp.Materno,
                                   IndiceDerecho = emp.IndiceDerecho != null ? Encoding.UTF8.GetString(emp.IndiceDerecho) : "empty",
                                   IndiceIzquierdo = emp.IndiceIzquierdo != null ? Encoding.UTF8.GetString(emp.IndiceIzquierdo) : "empty",
                                   PulgarDerecho = emp.PulgarDerecho != null ? Encoding.UTF8.GetString(emp.PulgarDerecho) : "empty",
                                   PulgarIzquierdo = emp.PulgarIzquierdo != null ? Encoding.UTF8.GetString(emp.PulgarIzquierdo) : "empty",
                               };
                if (personal != null)
                {
                    dgvListar.DataSource = personal.ToList();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                byte[]? streamHuella1 = TemplateIndDer != null ? TemplateIndDer.Bytes : null;
                byte[]? streamHuella2 = TemplateIndIzq != null ? TemplateIndIzq.Bytes : null;
                byte[]? streamHuella3 = TemplatePulDer != null ? TemplatePulDer.Bytes : null;
                byte[]? streamHuella4 = TemplatePulIzq != null ? TemplatePulIzq.Bytes : null;
                RrhhPersonal personal = new RrhhPersonal()
                {
                    //Id = 1,
                    IdCiudad = int.Parse(txtId_ciudad.Text),
                    Paterno = txtPaterno.Text,
                    Materno = txtMaterno.Text,
                    Nombres = txtNombre.Text,
                    IndiceDerecho = streamHuella1,
                    IndiceIzquierdo = streamHuella2,
                    PulgarDerecho = streamHuella3,
                    PulgarIzquierdo = streamHuella4,
                };
                AddPersonal(personal);
                MessageBox.Show("Registro agregado a la BD correctamente.");
                Limpiar();
                Listar();
                TemplateIndDer = null;
                TemplateIndIzq = null;
                TemplatePulDer  = null;
                TemplatePulIzq = null;
                btnGuardarUsuario.Enabled = false;
            }
            catch (Exception ex)
            {
                // Obtener detalles completos del error, incluyendo la InnerException
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void AddPersonal(RrhhPersonal item)
        {
            using (var db = new StoreContext())
            {
                db.RrhhPersonals.Add(item);
                db.SaveChanges();
            }
        }
        private void btnRegIndDer_Click(object sender, EventArgs e)
        {
            CapturarHuella capturar = new CapturarHuella();
            capturar.OnTemplate += this.OnTemplateIndDer;
            capturar.ShowDialog();
        }
        private void btnRegIndIzq_Click(object sender, EventArgs e)
        {
            CapturarHuella capturar = new CapturarHuella();
            capturar.OnTemplate += this.OnTemplateIndIzq;
            capturar.ShowDialog();
        }
        private void btnRegPulDer_Click(object sender, EventArgs e)
        {
            CapturarHuella capturar = new CapturarHuella();
            capturar.OnTemplate += this.OnTemplatePulDer;
            capturar.ShowDialog();
        }
        private void btnRegPulIzq_Click(object sender, EventArgs e)
        {
            CapturarHuella capturar = new CapturarHuella();
            capturar.OnTemplate += this.OnTemplatePulIzq;
            capturar.ShowDialog(); 
        }
    }
}
