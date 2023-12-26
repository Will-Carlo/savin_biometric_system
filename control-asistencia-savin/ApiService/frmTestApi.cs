using control_asistencia_savin.ApiService;
using control_asistencia_savin.Models;
using DPFP;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_asistencia_savin
{
    public partial class frmTestApi : Form
    {
        private readonly ApiService.ApiService _apiService;
        public frmTestApi()
        {
            InitializeComponent();
            _apiService = new ApiService.ApiService();
        }

        private async void btnVerificarApi_Click(object sender, EventArgs e)
        {
            try
            {
                var data = await _apiService.GetDataAsync();

                if (data != null)
                {
                    MessageBox.Show("Conexión exitosa.", "Test de conexión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo conectar al servidor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnCargarDB_Click(object sender, EventArgs e)
        {
            try
            {
                var data = await _apiService.GetDataAsync();

                if (data != null)
                {
                    GuardarDatosEnBaseDeDatos(data);
                }
                MessageBox.Show("Datos guardados con éxito");

            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("error bd reg: " + ex.InnerException.Message);
            }
        }
        private void GuardarDatosEnBaseDeDatos(ModelJson data)
        {
            using (var context = new StoreContext())
            {
                // guardar datos de RrhhTurnoAsignado
                //foreach (var turno in data.RrhhTurnoAsignado)
                //{
                //    var existente = context.RrhhTurnoAsignados.Find(turno.Id);
                //    if (existente == null)
                //    {
                //        context.RrhhTurnoAsignados.Add(turno);
                //    }
                //}

                // Utilizar el método genérico para cada tipo de entidad
                GuardarEntidades(context, data.RrhhTurno);
                GuardarEntidades(context, data.GenCiudad);
                GuardarEntidades(context, data.InvAlmacen);


                foreach (var personal in data.RrhhPersonal)
                {

                DPFP.Template template = new DPFP.Template();
                Stream stream;


                    // Convertir las propiedades de tipo string base64 a byte[]
                    if (personal.IndiceDerecho != null)
                    {
                        //string representacionBase64 = personal.IndiceDerecho;
                        //string representacionBase64 = Convert.ToBase64String(personal.IndiceDerecho);
                        //byte[] blobData = Convert.FromBase64String(representacionBase64);

                        //MessageBox.Show("Json: "+ representacionBase64);
                        //personal.IndiceDerecho = blobData;

                        

                        stream = new MemoryStream(personal.IndiceDerecho);
                        template = new DPFP.Template(stream);

                        byte[] streamHuella = Template.Bytes;

                        personal.IndiceDerecho = streamHuella;
                       
                        //personal.IndiceDerecho = Convert.FromBase64String(personal.IndiceDerecho);
                    }

                    GuardarEntidades(context, data.RrhhPersonal);
                }




                GuardarEntidades(context, data.InvSucursal);
                GuardarEntidades(context, data.RrhhFeriado);

                GuardarEntidades(context, data.RrhhPuntoAsistencia);
                GuardarEntidades(context, data.RrhhTurnoAsignado);

                // NodeLabelEditEventArgs recibimos datos de la tabla rrhh_asistencia
                //GuardarEntidades(context, data.RrhhAsistencia);

                // Confirmar todos los cambios en la base de datos
                context.SaveChanges();
            }
        }


        public bool EsBase64Valido(string cadenaBase64)
        {
            // La cadena no puede ser nula o vacía y debe tener una longitud múltiplo de 4 para ser base64 válida.
            if (string.IsNullOrEmpty(cadenaBase64) || cadenaBase64.Length % 4 != 0
                || cadenaBase64.Contains(" ") || cadenaBase64.Contains("\t") || cadenaBase64.Contains("\r") || cadenaBase64.Contains("\n"))
                return false;

            try
            {
                // Intenta decodificar la cadena para verificar si es base64 válido.
                Convert.FromBase64String(cadenaBase64);
                return true;
            }
            catch (FormatException)
            {
                // La cadena no es base64 válido.
                return false;
            }
        }

        // Método genérico para guardar entidades
        private void GuardarEntidades<TEntity>(StoreContext context, List<TEntity> entidades) where TEntity : class
        {
            var dbSet = context.Set<TEntity>();
            try
            {
                foreach (var entidad in entidades)
                {
                    // Aquí asumo que tienes una propiedad 'Id' en todas tus entidades,
                    // y que es de tipo int. Esto puede necesitar ajustes si tu situación es diferente.
                    var idProperty = entidad.GetType().GetProperty("Id");
                    if (idProperty != null)
                    {
                        var idValue = (int)idProperty.GetValue(entidad);
                        var existente = dbSet.Find(idValue);
                        if (existente == null)
                        {
                            dbSet.Add(entidad);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar en la bd" + ex.Message, "Error");
            }
        }



        public void LimpiarDB()
        {
            try
            {
                using (var context = new StoreContext())
                {
                    // Borrar todas las tablas.
                    BorrarDatosDeTabla(context.GenCiudads);
                    BorrarDatosDeTabla(context.InvAlmacens);
                    BorrarDatosDeTabla(context.InvSucursals);
                    BorrarDatosDeTabla(context.RrhhAsistencia);
                    BorrarDatosDeTabla(context.RrhhFeriados);
                    BorrarDatosDeTabla(context.RrhhPersonals);
                    BorrarDatosDeTabla(context.RrhhPuntoAsistencia);
                    BorrarDatosDeTabla(context.RrhhTurnos);
                    BorrarDatosDeTabla(context.RrhhTurnoAsignados);

                    context.SaveChanges();
                }
                MessageBox.Show("Se ha borrado la base de datos con éxito.");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar base de daros" + ex.Message, "Error");
            }
        }

        private void BorrarDatosDeTabla<T>(DbSet<T> dbSet) where T : class
        {
            foreach (var entidad in dbSet)
            {
                dbSet.Remove(entidad);
            }
        }

        public void BackUpDB(string dateBackUp)
        {
            var rutaBaseDeDatos = "store.db";
            var backupFolder = "backup";
            var rutaCopiaDeSeguridad = Path.Combine(backupFolder, "store_backup_" + dateBackUp + ".db");

            // Asegurarse de que la base de datos no está siendo utilizada
            GC.Collect();
            GC.WaitForPendingFinalizers();

            try
            {
                // Copiar el archivo de la base de datos a la ruta de copia de seguridad
                File.Copy(rutaBaseDeDatos, rutaCopiaDeSeguridad, overwrite: true);

                MessageBox.Show("La copia de seguridad se ha creado con éxito.");
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error al crear la copia de seguridad: " + ex.Message);
            }
        }

        private void btnDeleteBD_Click(object sender, EventArgs e)
        {
            LimpiarDB();
        }

        private void btnMakeBackUp_Click(object sender, EventArgs e)
        {
            BackUpDB("20231222 18 50 00");
        }

        private void btnPruebaB64_Click(object sender, EventArgs e)
        {

        }
    }
}
