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
using System.Windows.Forms.VisualStyles;

namespace control_asistencia_savin
{
    public partial class frmTestApi : Form
    {
        private readonly ApiService.ApiService _apiService;
        private DPFP.Template Template;

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


                //foreach (var personal in data.RrhhPersonal)
                //{
                //    // Convertir las propiedades de tipo string base64 a byte[]
                //    if (personal.IndiceDerecho != null)
                //    {
                //        //string representacionBase64 = personal.IndiceDerecho;
                //        //string representacionBase64 = Convert.ToBase64String(personal.IndiceDerecho);
                //        //byte[] blobData = Convert.FromBase64String(representacionBase64);

                //        //MessageBox.Show("Json: "+ representacionBase64);
                //        //personal.IndiceDerecho = blobData;


                //        MessageBox.Show("Finger: " + personal.IndiceDerecho);

                //        byte[] streamHuella = Template.Bytes;

                //        personal.IndiceDerecho = streamHuella;

                //        //personal.IndiceDerecho = Convert.FromBase64String(personal.IndiceDerecho);
                //    }

                //}

                GuardarPersonal(context, data.RrhhPersonal);
                //GuardarEntidades(context, data.RrhhPersonal);



                GuardarEntidades(context, data.InvSucursal);
                GuardarEntidades(context, data.RrhhFeriado);

                GuardarEntidades(context, data.RrhhPuntoAsistencia);
                GuardarEntidades(context, data.RrhhTurnoAsignado);

                //recibimos datos de la tabla rrhh_asistencia
                //GuardarEntidades(context, data.RrhhAsistencia);

                // Confirmar todos los cambios en la base de datos
                context.SaveChanges();
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
                    // Asumiendo que se tiene una propiedad 'Id' en todas las entidades.
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


        private void GuardarPersonal(StoreContext context, List<RrhhPersonal> personalList)
        {
            try
            {
                foreach (var personal in personalList)
                {
                    // Encuentra la entidad existente por ID o crea una nueva si no existe.
                    var existente = context.RrhhPersonals.Find(personal.Id);
                    if (existente == null)
                    {
                        // Si la entidad no existe, simplemente la añade al contexto.

                        string finger = Encoding.UTF8.GetString(personal.IndiceDerecho);
                        byte[] datoPrueba = Convert.FromBase64String(finger);
                        personal.IndiceDerecho = datoPrueba;


                        context.RrhhPersonals.Add(personal);
                    }
                    else
                    {
                        // Si la entidad ya existe, podría ser necesario actualizar los datos.
                        // Copia los datos de las propiedades que quieres actualizar.
                        existente.IdCiudad = personal.IdCiudad;
                        existente.Paterno = personal.Paterno;
                        existente.Materno = personal.Materno;
                        existente.Nombres = personal.Nombres;
                        existente.IndiceDerecho = personal.IndiceDerecho;
                        existente.IndiceIzquierdo = personal.IndiceIzquierdo;
                        existente.PulgarDerecho = personal.PulgarDerecho;
                        existente.PulgarIzquierdo = personal.PulgarIzquierdo;
                        // No es necesario llamar a Update ya que el objeto ya está siendo rastreado por el contexto.
                    }
                }

                // Guarda todos los cambios en la base de datos.
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar en la bd: " + ex.Message, "Error");
            }
        }


        private void GuardarPersonal2(List<RrhhPersonal> personal)
        {
            //var dbSet = context.Set<TEntity>();

            //DPFP.Template template = new DPFP.Template();
            //Stream stream;
            try
            {
                foreach (var entidad in personal)
                {
                    // Asumiendo que se tiene una propiedad 'Id' en todas las entidades.
                    //var idProperty = entidad.GetType().GetProperty("Id");
                    //if (idProperty != null || entidad.IndiceDerecho != null)
                    if (entidad.IndiceDerecho != null)
                        {
                        //var idValue = (int)idProperty.GetValue(entidad);
                        //var existente = dbSet.Find(idValue);
                        //if (existente == null)
                        //{
                        //string prueba = "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";
                        //byte[] datoPrueba = Convert.FromBase64String(prueba);


                        string finger = Encoding.UTF8.GetString(entidad.IndiceDerecho);
                        byte[] datoPrueba = Convert.FromBase64String(finger);


                        //stream = new MemoryStream(entidad.IndiceDerecho);
                        //template = new DPFP.Template(stream);


                        //MessageBox.Show("IndDer: " + entidad.IndiceDerecho);
                        //MessageBox.Show("Template: " + template);

                        //byte[] streamHuella = Template.Bytes;


                        RrhhPersonal p = new RrhhPersonal()
                        {
                            Id = entidad.Id,
                            IdCiudad = entidad.IdCiudad,
                            Paterno = entidad.Paterno,
                            Materno = entidad.Materno,
                            Nombres = entidad.Nombres,
                            IndiceDerecho = datoPrueba
                        };
                        AddPersonal(p);


                        //dbSet.Add(entidad);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar personal en la bd: " + ex.Message, "Error");
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
            string finger = "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";
            RrhhPersonal p = new RrhhPersonal()
            {
                IdCiudad = 1,
                Paterno = "  ",
                Materno = "CARLO",
                Nombres = "Will"
            };
            pruebasDeBlob(p, finger);
        }



        private void pruebasDeBlob(RrhhPersonal entidad,string finger)
        {
            //var dbSet = context.Set<TEntity>();

            DPFP.Template template = new DPFP.Template();
            Stream stream;

            try
            {
                //foreach (var entidad in personal)
                //{
                    // Asumiendo que se tiene una propiedad 'Id' en todas las entidades.
                    //var idProperty = entidad.GetType().GetProperty("Id");
                    //if (entidad.IndiceDerecho != null)
                    //{
                        //var idValue = (int)idProperty.GetValue(entidad);
                        //var existente = dbSet.Find(idValue);
                        //if (existente == null)
                        //{

                            byte[] datoPrueba = Convert.FromBase64String(finger);



                        //    stream = new MemoryStream(datoPrueba);
                        //template = new DPFP.Template(stream);
                        //byte[] streamHuella = Template.Bytes;


                        RrhhPersonal p = new RrhhPersonal()
                        {
                            IdCiudad = entidad.IdCiudad,
                            Paterno = entidad.Paterno,
                            Materno = entidad.Materno,
                            Nombres = entidad.Nombres,
                            IndiceDerecho = datoPrueba
                        };
                        AddPersonal(p);


                        //dbSet.Add(entidad);
                        //}
                    //}
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar en la bd" + ex.Message, "Error");
            }
        }
    }
}
