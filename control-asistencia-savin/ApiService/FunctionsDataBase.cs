using control_asistencia_savin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//Comando para actualiza los modelos desde la base de datos
//Scaffold - DbContext "Data Source=store.db" Microsoft.EntityFrameworkCore.Sqlite - OutputDir Models
//Scaffold - DbContext "Data Source=store.db" Microsoft.EntityFrameworkCore.Sqlite - OutputDir Models


namespace control_asistencia_savin.ApiService
{

    internal class FunctionsDataBase
    {
        private readonly ApiService _apiService = new ApiService();
        public bool correctConection = false;

        public void verifyConection()
        {
            try
            {
                var data = _apiService.GetData();

                if (data != null)
                {
                    MessageBox.Show("Conexión al servidor exitosa.", "Test de conexión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.correctConection = true;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"No se pudo conectar al servidor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //public async Task loadDataBase()
        //{
        //    try
        //    {
        //        var data = await _apiService.GetDataAsync();

        //        if (data != null)
        //        {
        //            GuardarDatosEnBaseDeDatos(data);
        //        }
        //        MessageBox.Show("Datos guardados con éxito");

        //    }   
        //    catch (DbUpdateException ex)
        //    {
        //        MessageBox.Show("error bd reg: " + ex.InnerException.Message);
        //    }
        //}

        public void loadDataBase()
        {
            try
            {
                var data = _apiService.GetData();

                if (data != null)
                {
                    GuardarDatosEnBaseDeDatos(data);
                }
                MessageBox.Show("Datos guardados con éxito.");

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
                GuardarEntidades(context, data.RrhhTurno);
                GuardarEntidades(context, data.GenCiudad);
                GuardarEntidades(context, data.InvAlmacen);

                GuardarPersonal(context, data.RrhhPersonal);

                GuardarEntidades(context, data.InvSucursal);
                GuardarEntidades(context, data.RrhhFeriado);

                GuardarEntidades(context, data.RrhhPuntoAsistencia);
                GuardarEntidades(context, data.RrhhTurnoAsignado);

                //recibimos datos de la tabla rrhh_asistencia
                //GuardarEntidades(context, data.RrhhAsistencia);

                context.SaveChanges();
            }
        }
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
                        //List<byte[]> FingerList = new List<byte[]>();
                        //List<byte[]> FingerListPersonal = new List<byte[]>();

                        //FingerList.Add(personal.IndiceDerecho);
                        //FingerList.Add(personal.IndiceIzquierdo);
                        //FingerList.Add(personal.PulgarDerecho);
                        //FingerList.Add(personal.PulgarIzquierdo);

                        //foreach (byte[] finger in FingerList)
                        //{
                            if (personal.IndiceDerecho != null)
                            {
                                string fingerAux = Encoding.UTF8.GetString(personal.IndiceDerecho);
                                byte[] datoPrueba = Convert.FromBase64String(fingerAux);
                                personal.IndiceDerecho = datoPrueba;
                            }

                            if (personal.IndiceIzquierdo != null)
                            {
                                string fingerAux = Encoding.UTF8.GetString(personal.IndiceIzquierdo);
                                byte[] datoPrueba = Convert.FromBase64String(fingerAux);
                                personal.IndiceIzquierdo = datoPrueba;
                            }

                            if (personal.PulgarDerecho != null)
                            {
                                string fingerAux = Encoding.UTF8.GetString(personal.PulgarDerecho);
                                byte[] datoPrueba = Convert.FromBase64String(fingerAux);
                                personal.PulgarDerecho = datoPrueba;
                            }

                            if (personal.PulgarIzquierdo != null)
                            {
                                string fingerAux = Encoding.UTF8.GetString(personal.PulgarIzquierdo);
                                byte[] datoPrueba = Convert.FromBase64String(fingerAux);
                                personal.PulgarIzquierdo = datoPrueba;
                            }

                        //}
                        context.RrhhPersonals.Add(personal);
                    }
                    else
                    {
                        // Si la entidad ya existe, podría ser necesario actualizar los datos.
                        // Copia los datos de las propiedades que quieres actualizar.
                            //existente.IdCiudad = personal.IdCiudad;
                            //existente.Paterno = personal.Paterno;
                            //existente.Materno = personal.Materno;
                            //existente.Nombres = personal.Nombres;
                            //existente.IndiceDerecho = personal.IndiceDerecho;
                            //existente.IndiceIzquierdo = personal.IndiceIzquierdo;
                            //existente.PulgarDerecho = personal.PulgarDerecho;
                            //existente.PulgarIzquierdo = personal.PulgarIzquierdo;
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
        public void LimpiarDB()
        {
            try
            {
                using (var context = new StoreContext())
                {
                    // Borrar todas las tablas.
                    BorrarDatosDeTabla(context.RrhhAsistencia);

                    BorrarDatosDeTabla(context.RrhhTurnoAsignados);
                    BorrarDatosDeTabla(context.RrhhPuntoAsistencia);

                    BorrarDatosDeTabla(context.RrhhFeriados);
                    BorrarDatosDeTabla(context.InvSucursals);

                    BorrarDatosDeTabla(context.RrhhPersonals);

                    BorrarDatosDeTabla(context.InvAlmacens);
                    BorrarDatosDeTabla(context.GenCiudads);
                    BorrarDatosDeTabla(context.RrhhTurnos);


                    context.SaveChanges();
                }
                MessageBox.Show("La base de datos se ha limpiado con éxito.");

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
        public void BackUpDB(String dateBackUp)
        {
            //string dateBackUp = "2023 12 27 15 56 12";
            var rutaBaseDeDatos = "store.db";
            var backupFolder = "backup";
            var rutaCopiaDeSeguridad = Path.Combine(backupFolder, "store_backup_" + dateBackUp + ".db");

            // Asegurarse de que la base de datos no está siendo utilizada
            GC.Collect();
            GC.WaitForPendingFinalizers();

            try
            {
                if (!Directory.Exists(backupFolder))
                {
                    Directory.CreateDirectory(backupFolder);
                }
                // Copiar el archivo de la base de datos a la ruta de copia de seguridad
                File.Copy(rutaBaseDeDatos, rutaCopiaDeSeguridad, overwrite: true);

                MessageBox.Show("La copia de seguridad se ha creado con éxito.");
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error al crear la copia de seguridad: " + ex.Message);
            }
        }

        public async Task LoadDataBaseAsistenciaAsync(int idPersonal)
        {
            LimpiarAuxAsistencia();
            try
            {
                var asistencias = await _apiService.GetDataAsistenciaAsync(idPersonal);

                using (var context = new StoreContext())
                {
                    foreach (var asistencia in asistencias)
                    {
                        var existente = context.AuxAsistencia.Find(asistencia.Id);
                        if (existente == null)
                        {
                            context.AuxAsistencia.Add(asistencia);
                        }
                        else
                        {
                            // Actualiza los datos si es necesario.
                            context.Entry(existente).CurrentValues.SetValues(asistencia);
                        }
                    }
                    await context.SaveChangesAsync();
                    //MessageBox.Show("Asistencias guardadas con éxito.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar asistencias: " + ex.Message, "Error");
            }
        }
        public void LoadDataBaseAsistencia(int idPersonal)
        {
            LimpiarAuxAsistencia();
            try
            {
                var asistencias = _apiService.GetDataAsistencia(idPersonal);

                using (var context = new StoreContext())
                {
                    foreach (var asistencia in asistencias)
                    {
                        var existente = context.AuxAsistencia.Find(asistencia.Id);
                        if (existente == null)
                        {
                            context.AuxAsistencia.Add(asistencia);
                        }
                        else
                        {
                            // Actualiza los datos si es necesario.
                            context.Entry(existente).CurrentValues.SetValues(asistencia);
                        }
                    }
                    context.SaveChanges();
                    //MessageBox.Show("Asistencias guardadas con éxito.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar asistencias: " + ex.Message, "Error");
            }
        }
        public void LimpiarAuxAsistencia()
        {
            try
            {
                using (var context = new StoreContext())
                {
                    // Borrar todas las tablas.
                    BorrarDatosDeTabla(context.AuxAsistencia);

                    context.SaveChanges();
                }
                //MessageBox.Show("La tabla auxiliar se ha borrado con éxito.");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar la tabla auxiliar"+ ex.Message, "Error");
            }
        }
    }
}
