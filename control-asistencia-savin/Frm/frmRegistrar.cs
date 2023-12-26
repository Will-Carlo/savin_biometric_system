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
        private DPFP.Template Template;
        //private UsuariosDBEntities contexto;
        private Models.StoreContext contexto;
        public frmRegistrar()
        {
            InitializeComponent();
        }

        private void btnRegistrarHuella_Click(object sender, EventArgs e)
        {
            CapturarHuella capturar = new CapturarHuella();
            capturar.OnTemplate += this.OnTemplate;
            capturar.ShowDialog();
        }

        private void OnTemplate(DPFP.Template template)
        {
            this.Invoke(new Function(delegate ()
            {
                Template = template;
                btnAgregar.Enabled = (Template != null);
                if (Template != null)
                {
                    MessageBox.Show("The fingerprint template is ready for fingerprint verification.", "Fingerprint Enrollment");
                    txtHuella.Text = "Huella capturada correctamente";
                }
                else
                {
                    MessageBox.Show("The fingerprint template is not valid. Repeat fingerprint enrollment.", "Fingerprint Enrollment");
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
            txtHuella.Text = "";

        }

        private void Listar()
        {
            try
            {
                var personal = from emp in contexto.RrhhPersonals
                               select new
                               {
                                   ID = emp.Id,
                                   CIUDAD = emp.IdCiudad,
                                   EMPLEADO = emp.Nombres + " " + emp.Paterno + " " + emp.Materno,
                                   //HUELLA = emp.IndiceDerecho,
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
                byte[] streamHuella = Template.Bytes;
                RrhhPersonal personal = new RrhhPersonal()

                {
                    //Id = 1,
                    IdCiudad = int.Parse(txtId_ciudad.Text),
                    Paterno = txtPaterno.Text,
                    Materno = txtMaterno.Text,
                    Nombres = txtNombre.Text,
                    IndiceDerecho = streamHuella
                };
                AddPersonal(personal);
                //contexto.RrhhPersonals.Add(personal);
                //contexto.SaveChanges();
                MessageBox.Show("Registro agregado a la BD correctamente");
                Limpiar();
                Listar();
                Template = null;
                btnAgregar.Enabled = false;

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

    }
}
