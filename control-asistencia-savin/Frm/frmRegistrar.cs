using control_asistencia_savin;
using control_asistencia_savin.Models;
using Newtonsoft.Json;
using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
        private void frmRegistrar_Load(object sender, EventArgs e)
        {
            contexto = new Models.StoreContext();
            Listar();

            CargarCiudadesEnComboBox();
            if (cbxCiudad.Items.Count > 0)
            {
                cbxCiudad.SelectedIndex = 0;
            }
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
        private void Limpiar()
        {
            //txtId_ciudad.Text = "";
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

            dgvListar.RowHeadersVisible = false;

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
                    //IdCiudad = int.Parse(txtId_ciudad.Text),
                    IdCiudad = getIdCiudad(),
                    Paterno = txtPaterno.Text,
                    Materno = txtMaterno.Text,
                    Nombres = txtNombre.Text,
                    IndiceDerecho = streamHuella1,
                    IndiceIzquierdo = streamHuella2,
                    PulgarDerecho = streamHuella3,
                    PulgarIzquierdo = streamHuella4,
                };
                AddPersonal(personal);
                //MessageBox.Show("Registro agregado a la BD correctamente.");
                Limpiar();
                Listar();
                TemplateIndDer = null;
                TemplateIndIzq = null;
                TemplatePulDer = null;
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
        private void SaveFingerText()
        {
            try
            {
                string filePath = "fingers.js";
                string name = txtNombre.Text + " " + txtPaterno.Text + " " + txtMaterno.Text;


                byte[]? streamHuella1 = TemplateIndDer != null ? TemplateIndDer.Bytes : null;
                byte[]? streamHuella2 = TemplateIndIzq != null ? TemplateIndIzq.Bytes : null;
                byte[]? streamHuella3 = TemplatePulDer != null ? TemplatePulDer.Bytes : null;
                byte[]? streamHuella4 = TemplatePulIzq != null ? TemplatePulIzq.Bytes : null;

                string IndiceDerecho = "";
                string IndiceIzquierdo = "";
                string PulgarDerecho = "";
                string PulgarIzquierdo = "";


                string currentContent = File.Exists(filePath) ? File.ReadAllText(filePath) : "";

                if (streamHuella1 != null)
                {
                    byte[] datoPrueba = streamHuella1;
                    IndiceDerecho = Convert.ToBase64String(datoPrueba);
                }

                if (streamHuella2 != null)
                {
                    byte[] datoPrueba = streamHuella2;
                    IndiceIzquierdo = Convert.ToBase64String(datoPrueba);
                }

                if (streamHuella3 != null)
                {
                    byte[] datoPrueba = streamHuella3;
                    PulgarDerecho = Convert.ToBase64String(datoPrueba);
                }

                if (streamHuella4 != null)
                {
                    byte[] datoPrueba = streamHuella4;
                    PulgarIzquierdo = Convert.ToBase64String(datoPrueba);
                }


                var regFinger = new
                {
                    Nombre = name,
                    Indice_Derecho = IndiceDerecho,
                    Indice_Izquierdo = IndiceIzquierdo,
                    Pulgar_Derecho = PulgarDerecho,
                    Pulgar_Izquierdo = PulgarIzquierdo
                };

                string json = JsonConvert.SerializeObject(regFinger);

                // Agregar un salto de línea si el archivo ya contiene datos
                if (!string.IsNullOrEmpty(currentContent))
                {
                    json = Environment.NewLine + json;
                }

                // Concatenar el nuevo registro al contenido actual y escribir en el archivo
                File.AppendAllText(filePath, json);

                MessageBox.Show("Datos guardados en personal.js correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar en el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveFingerInArray(string name, byte[] f1, byte[] f2, byte[] f3, byte[] f4)
        {
            try
            {
                string fileName = "huellas.js";
                //string name = item.Nombres + " " + item.Paterno + " " + item.Materno;
                // Obtener la ruta del escritorio del usuario actual
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                // Combinar la ruta del escritorio con el nombre del archivo
                string filePath = Path.Combine(desktopPath, fileName);


                byte[]? streamHuella1 = f1 != null ? f1 : null;
                byte[]? streamHuella2 = f2 != null ? f2 : null;
                byte[]? streamHuella3 = f3 != null ? f3 : null;
                byte[]? streamHuella4 = f4 != null ? f4 : null;

                string IndiceDerecho = "";
                string IndiceIzquierdo = "";
                string PulgarDerecho = "";
                string PulgarIzquierdo = "";


                string currentContent = File.Exists(filePath) ? File.ReadAllText(filePath) : "";

                if (streamHuella1 != null)
                {
                    byte[] datoPrueba = streamHuella1;
                    IndiceDerecho = Convert.ToBase64String(datoPrueba);
                }

                if (streamHuella2 != null)
                {
                    byte[] datoPrueba = streamHuella2;
                    IndiceIzquierdo = Convert.ToBase64String(datoPrueba);
                }

                if (streamHuella3 != null)
                {
                    byte[] datoPrueba = streamHuella3;
                    PulgarDerecho = Convert.ToBase64String(datoPrueba);
                }

                if (streamHuella4 != null)
                {
                    byte[] datoPrueba = streamHuella4;
                    PulgarIzquierdo = Convert.ToBase64String(datoPrueba);
                }


                var regFinger = new
                {
                    Nombre = name,
                    Indice_Derecho = IndiceDerecho,
                    Indice_Izquierdo = IndiceIzquierdo,
                    Pulgar_Derecho = PulgarDerecho,
                    Pulgar_Izquierdo = PulgarIzquierdo
                };

                string json = JsonConvert.SerializeObject(regFinger);

                // Agregar un salto de línea si el archivo ya contiene datos
                if (!string.IsNullOrEmpty(currentContent))
                {
                    json = Environment.NewLine + json;
                }

                // Concatenar el nuevo registro al contenido actual y escribir en el archivo
                File.AppendAllText(filePath, json);

                //MessageBox.Show("Datos guardados en personal.js correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar en el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveReportJS()
        {
            try
            {
                var personal = from emp in contexto.RrhhPersonals
                               select new
                               {
                                   Name = emp.Nombres + " " + emp.Paterno + " " + emp.Materno,
                                   RightIndexFinger = emp.IndiceDerecho != null ? emp.IndiceDerecho : null,
                                   LeftIndexFinger = emp.IndiceIzquierdo != null ? emp.IndiceIzquierdo : null,
                                   RightThumb = emp.PulgarDerecho != null ? emp.PulgarDerecho : null,
                                   LeftThumb = emp.PulgarIzquierdo != null ? emp.PulgarIzquierdo : null,
                               };
                foreach (var item in personal)
                {
                    SaveFingerInArray(item.Name, item.RightIndexFinger, item.LeftIndexFinger, item.RightThumb, item.LeftThumb);
                }


                //if (personal != null)
                //{
                //    dgvListar.DataSource = personal.ToList();
                //}
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }
        private void AddPersonal(RrhhPersonal item)
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
        private void btnReportTxt_Click(object sender, EventArgs e)
        {
            SaveReportJS();
        }
        
        // PARA CARGAR DATOS EN COMBO BOX
        public bool ExisteDatos()
        {
            using (var context = new StoreContext())
            {
                return context.GenCiudads.Any();
            }
        }
        public void CargarCiudadesEnComboBox()
        {
            // Limpiamos el ComboBox antes de agregar los elementos
            cbxCiudad.Items.Clear();
            // Hacemos que no sea editable
            cbxCiudad.DropDownStyle = ComboBoxStyle.DropDownList;

            // Agregamos los departamentos uno por uno con valores del 1 al 9
            cbxCiudad.Items.Add(new { Value = 1, Text = "Zapata (La Paz)" });
            cbxCiudad.Items.Add(new { Value = 1, Text = "Loayza (La Paz)" });
            cbxCiudad.Items.Add(new { Value = 1, Text = "Oficina (La Paz)" });
            cbxCiudad.Items.Add(new { Value = 1, Text = "Ceibo (El Alto)" });
            cbxCiudad.Items.Add(new { Value = 1, Text = "Satélite (El Alto)" });
            cbxCiudad.Items.Add(new { Value = 1, Text = "Almacén (El Alto)" });
            cbxCiudad.Items.Add(new { Value = 1, Text = "Cochabamba" });
            cbxCiudad.Items.Add(new { Value = 1, Text = "Santa Cruz" });
            cbxCiudad.Items.Add(new { Value = 1, Text = "Oruro" });
            cbxCiudad.Items.Add(new { Value = 1, Text = "Potosí" });
            cbxCiudad.Items.Add(new { Value = 1, Text = "Chuquisaca" });
            cbxCiudad.Items.Add(new { Value = 1, Text = "Tarija" });
            



            // Establecemos DisplayMember en "Text" para mostrar solo el nombre del departamento
            cbxCiudad.DisplayMember = "Text";
        }
        public int getIdCiudad()
        {
            var selectedItem = (dynamic)cbxCiudad.SelectedItem;
            //string selectedText = selectedItem.Text;
            return selectedItem.Value;
        }
    }
}
