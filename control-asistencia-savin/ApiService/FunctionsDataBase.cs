using control_asistencia_savin.Models;
using control_asistencia_savin.Notifications;
using DPFP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

//Comando para actualiza los modelos desde la base de datos
//Scaffold-DbContext "Data Source=store.db" Microsoft.EntityFrameworkCore.Sqlite -OutputDir Models
//Scaffold-DbContext "Data Source=store.db" Microsoft.EntityFrameworkCore.Sqlite -OutputDir Models


namespace control_asistencia_savin.ApiService
{

    internal class FunctionsDataBase
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        private readonly ApiService _apiService = new ApiService();
        //public bool correctConection = false;
        public FunctionsDataBase()
        {
            _logger = LoggingManager.GetLogger<FunctionsDataBase>();
        }
        public bool verifyConection()
        {
            try
            {
                //var data = _apiService.GetDataConexion();

                if (_apiService.GetDataConexion())
                //if (data != null)

                    {

                        //                    _logger.LogInformation($"Test de conexión: Conexión al servidor exitosa, Respuesta: {@data}");
                        //this.correctConection = true;
                        return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al verificar conexión: {ex.Message}");
            }
            return false;
        }

        public void loadDataBase()
        {
            try
            {
                var data =  _apiService.GetDataAsync();

                if (data != null)
                {
                    GuardarDatosEnBaseDeDatos(data);
                }

                _logger.LogDebug("Datos guardados en la base de datos con éxito.");

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error al cargar los datos en la base de datos: " + ex.InnerException.Message);
                MessageBox.Show("Ha ocurrido un error al cargar los datos. \nContactar con el administrador. \n");
                _logger.LogCritical("\n------------------ CERRANDO LA APLICACIÓN POR ERROR AL CARGAR LA BASE DE DATOS ------------------");

                LoggingManager.CloseAndFlush();

                Environment.Exit(0);

            }
        }

        public void loadDataBaseForMac(ApiService _apiService)
        {
            try
            {
                var data = _apiService.GetDataForMac();

                if (data != null)
                {
                    GuardarDatosEnBaseDeDatos(data);
                }

                _logger.LogDebug("Los datos se han cargado con éxito.");

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error al cargar los datos en la base de datos: " + ex.InnerException.Message);
                MessageBox.Show("Ha ocurrido un error al cargar los datos: " + ex.InnerException.Message);
                // Environment.Exit(0);

            }
        }

        //public void loadDataBase()
        //{
        //    try
        //    {
        //        var data = _apiService.GetData();

        //        if (data != null)
        //        {
        //            GuardarDatosEnBaseDeDatos(data);
        //        }
        //        //MessageBox.Show("Datos guardados con éxito.");

        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        MessageBox.Show("error bd reg: " + ex.InnerException.Message);
        //    }
        //}
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

                //////recibimos datos de la tabla rrhh_asistencia
                GuardarEntidades(context, data.RrhhAsistencia);

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
                _logger.LogError("Error al guardar tablas en la base de datos: " + ex.Message);
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
                _logger.LogError("Error al guardar personal en la base de datos: " + ex.Message);
            }
        }
        public void LimpiarDB()
        {
            try
            {
                using (var context = new StoreContext())
                {
                    // Borrar todas las tablas.
                    BorrarDatosDeTabla(context.RrhhTurnos);
                    BorrarDatosDeTabla(context.GenCiudads);
                    BorrarDatosDeTabla(context.InvAlmacens);

                    BorrarDatosDeTabla(context.RrhhPersonals);

                    BorrarDatosDeTabla(context.InvSucursals);
                    BorrarDatosDeTabla(context.RrhhFeriados);
                    
                    BorrarDatosDeTabla(context.RrhhPuntoAsistencia);
                    BorrarDatosDeTabla(context.RrhhTurnoAsignados);
                    
                    BorrarDatosDeTabla(context.RrhhAsistencia);
                    BorrarDatosDeTabla(context.RrhhAsistenciaTemporals);
                    BorrarDatosDeTabla(context.AuxAsistencia);


                    context.SaveChanges();
                }
                _logger.LogDebug("La base de datos se ha limpiado con éxito.");

            }
            catch (Exception ex)
            {
                _logger.LogError("Error al limpiar base de datos: " + ex.Message);
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
            //string dateBackUp = "2023 12 27 15 56 12";
            var rutaBaseDeDatos = "store.db";
            var backupFolder = "backup";
            string nameStore = this.GetNombreTienda() ?? "store";
            string nameBackup = nameStore.Replace(' ', '_').ToLower() + "_backup_" + dateBackUp + ".db";
            var rutaCopiaDeSeguridad = Path.Combine(backupFolder, nameBackup);
            _logger.LogDebug($"ruta copia de seguridad: {rutaCopiaDeSeguridad}");
            _logger.LogInformation("-> Creando copia de seguridad...");
            _logger.LogInformation($"Backup: {nameBackup}");

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

                _logger.LogDebug("La copia de seguridad se ha creado con éxito.");
            }
            catch (IOException ex)
            {
                _logger.LogError("Error al crear la copia de seguridad: " + ex.Message);
                // MessageBox.Show("Error al crear la copia de seguridad.");
            }
        }
        //public async Task LoadDataBaseAsistenciaAsync(int idPersonal)
        //{
        //    LimpiarAuxAsistencia();
        //    try
        //    {
        //        var asistencias = await _apiService.GetDataAsistenciaAsync(idPersonal);

        //        using (var context = new StoreContext())
        //        {
        //            foreach (var asistencia in asistencias)
        //            {
        //                var existente = context.AuxAsistencia.Find(asistencia.Id);
        //                if (existente == null)
        //                {
        //                    context.AuxAsistencia.Add(asistencia);
        //                }
        //                else
        //                {
        //                    // Actualiza los datos si es necesario.
        //                    context.Entry(existente).CurrentValues.SetValues(asistencia);
        //                }
        //            }
        //            await context.SaveChangesAsync();
        //            //MessageBox.Show("Asistencias guardadas con éxito.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error al cargar asistencias: " + ex.Message, "Error");
        //    }
        //}
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
                    _logger.LogDebug("Asistencias cargadas con éxito.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al cargar asistencias: {ex.Message}.");
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
                _logger.LogDebug("La tabla auxiliar se ha borrado con éxito.");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al limpiar la tabla auxiliar {ex.Message}.");
            }
        }
        public void DeleteBackupFiles(int monthBackups)
        {

            string backupDirectory = "backup";
            string numberMonth = monthBackups < 10 ? "0" + monthBackups : monthBackups.ToString();
            string nameStore = this.GetNombreTienda() ?? "store";
            int currentYear = DateTime.Now.Year;
            string nameBackups = nameStore.Replace(' ','_').ToLower()+"_backup_*_" + numberMonth + "_" + currentYear + "_*";
            // _logger.LogDebug($"Eliminando old backups. Name directory: {backupDirectory}, Name backup: {nameBackups}.");
            _logger.LogDebug($"Name format db backup: {nameBackups}.db");

            // Asegúrate de que el directorio existe
            if (Directory.Exists(backupDirectory))
            {
                // Crea una instancia de DirectoryInfo para el directorio 'backup'
                DirectoryInfo directoryInfo = new DirectoryInfo(backupDirectory);

                // Encuentra todos los archivos que comienzan con 'store_backup_30_12_'
                //_logger.LogDebug("test: " + nameBackups);
                FileInfo[] files = directoryInfo.GetFiles(nameBackups);

                // Itera sobre cada archivo y elimínalos
                foreach (FileInfo file in files)
                {

                    try
                    {
                        file.Delete();
                        _logger.LogDebug($"El archivo {file.Name} ha sido eliminado.");
                    }
                    catch (Exception ex)
                    {
                        // Puedes manejar errores aquí, si no puedes borrar un archivo por alguna razón
                        _logger.LogError($"Error al eliminar el archivo {file.Name}: {ex.Message}");
                    }
                }
            }
            else
            {
                _logger.LogError($"El directorio {backupDirectory} no existe.");
            }
        }

        public void ModificarHoraMarcado(int IdPersonal, string HoraAnterior, int IdPuntoAsistencia)
        {
            using (var dbContext = new StoreContext())
            {
                // Buscar el registro que cumple con los criterios de búsqueda
                var registro = dbContext.RrhhAsistencia
                    .FirstOrDefault(a => a.IdPersonal == IdPersonal &&
                                         a.HoraMarcado == HoraAnterior &&
                                         a.IdPuntoAsistencia == IdPuntoAsistencia);

                // Verificar si se encontró el registro
                if (registro != null)
                {
                    _logger.LogDebug($"Seteando a cero 'min de atraso' en asistencias del personal: {IdPersonal}.");

                    // Modificar los minutos_atraso a 0
                    registro.MinutosAtraso = 0;

                    // Guardar los cambios en la base de datos
                    dbContext.SaveChanges();
                }
                else
                {
                    _logger.LogError($"No se encontró registros para setear a cero del personal: {IdPersonal}.");
                }
            }
        }

        //public void ModificarHoraMarcadoTT(int IdPersonal, string HoraAnterior, int IdPuntoAsistencia)
        //{
        //    using (var dbContext = new StoreContext())
        //    {
        //        // Buscar el registro que cumple con los criterios de búsqueda
        //        var registro = dbContext.RrhhAsistenciaTemporals
        //            .FirstOrDefault(a => a.IdPersonal == IdPersonal &&
        //                                 a.HoraMarcado == HoraAnterior &&
        //                                 a.IdPuntoAsistencia == IdPuntoAsistencia);

        //        // Verificar si se encontró el registro
        //        if (registro != null)
        //        {
        //            _logger.LogDebug($"Seteando a cero 'min de atraso' en backup del personal: {IdPersonal}.");

        //            // Modificar los minutos_atraso a 0
        //            registro.MinutosAtraso = 0;

        //            // Guardar los cambios en la base de datos
        //            dbContext.SaveChanges();
        //        }
        //        else
        //        {
        //            _logger.LogError($"Error al setear a cero en backup del personal: {IdPersonal}.");
        //        }
        //    }
        //}


        public bool verifyAnteriorRegistroTT(int idPersonal)
        {
            using (var context = new StoreContext())
            {
                // Verifica si existe al menos un registro en la tabla rrhh_asistencia_temporal que coincida con el id_personal

                bool existeRegistro = context.RrhhAsistenciaTemporals.Any(a => a.IdPersonal == idPersonal);
                _logger.LogDebug($"Verificando registros anteriores en el backup de asistencias: {existeRegistro}.");


                return existeRegistro;
            }
        }


        public int MinutosDeTolerancia()
        {
            using (var dbContext = new StoreContext())
            {
                // Consulta para obtener el primer registro de rrhh_punto_asistencia y extraer minutos_tolerancia
                var minutosTolerancia = dbContext.RrhhPuntoAsistencia
                    .Select(pa => pa.MinutosTolerancia)
                    .FirstOrDefault();

                int minTol = minutosTolerancia != null ? (int)minutosTolerancia : 0;
                _logger.LogDebug($"Los minutos de tolerancia de la tienda es: {minTol}");
                return minTol;
            }
        }

        public string? GetNombreTienda()
        {
            using (var context = new StoreContext())
            {
                var puntoAsistencia = context.RrhhPuntoAsistencia
                                            .FirstOrDefault(pa => pa.DireccionMac == _apiService._dirMac);
                return puntoAsistencia?.Nombre;
            }
        }
        // ---------------------------- FUNCIONES PARA VER REGISTROS DE TIENDAS ----------------------------
        public string LoadRegisters(int n, string messageMac)
        {
            this.LimpiarDB();
            Credenciales cd = new Credenciales(n);
            //MessageBox.Show("MAC: "+cd._mac);
            ApiService ap = new ApiService(true, cd._mac);
            this.loadDataBaseForMac(ap);

            _logger.LogDebug($"Cargando datos de la tienda: {messageMac}");
            MessageBox.Show(messageMac);

            return "Punto Asistencia: " + this.GetPuntoAsistencia(cd._mac) +
                "\nNombre de la tienda: \n" + this.GetNombreTienda(cd._mac);
        }

        public int? LoadRegistersNew(int n)
        {
            this.LimpiarDB();
            Credenciales cd = new Credenciales(n);
            //MessageBox.Show("MAC: "+cd._mac);
            ApiService ap = new ApiService(true, cd._mac);
            this.loadDataBaseForMac(ap);


            return this.GetPuntoAsistenciaNew(cd._mac);
        }
        public int? GetPuntoAsistenciaNew(string _dirMac)
        {
            using (var context = new StoreContext())
            {
                var puntoAsistencia = context.RrhhPuntoAsistencia
                                            .FirstOrDefault(pa => pa.DireccionMac == _dirMac);
                return puntoAsistencia?.Id;
            }
        }
        public string? GetPuntoAsistencia(string _dirMac)
        {
            using (var context = new StoreContext())
            {
                var puntoAsistencia = context.RrhhPuntoAsistencia
                                            .FirstOrDefault(pa => pa.DireccionMac == _dirMac);
                return puntoAsistencia?.Id.ToString();
            }
        }
        public string? GetNombreTienda(string _dirMac)
        {
            using (var context = new StoreContext())
            {
                var puntoAsistencia = context.RrhhPuntoAsistencia
                                            .FirstOrDefault(pa => pa.DireccionMac == _dirMac);
                return puntoAsistencia?.Nombre;
            }
        }
        // ---------------------------- FIN FUNCIONES PARA VER REGISTROS DE TIENDAS ----------------------------

        public bool esConObservacion()
        {

            using (var context = new StoreContext())
            {
                bool permiteObservacion = false;
                var puntoAsistencia = context.RrhhPuntoAsistencia
                                            .FirstOrDefault(pa => pa.DireccionMac == _apiService._dirMac);
                // Verificar si se encontró el punto de asistencia y devolver el valor de permite_observacion
                if (puntoAsistencia != null)
                {
                    permiteObservacion = puntoAsistencia.PermiteObservacion != null ? (puntoAsistencia.PermiteObservacion != false ? true : false) : false;
                    //return puntoAsistencia.PermiteObservacion == 1; // Devuelve true si permite_observacion es 1, de lo contrario false
                    _logger.LogDebug($"Observaciones por tienda: {permiteObservacion}");
                    return permiteObservacion;
                }
                return false;
            }
         

        }

        // ---------------------------- FIN FUNCIONES PARA VALIDAR BACKUP ----------------------------
        public List<RrhhAsistencia> ObtenerAsistenciasNoEnviadas()
        {
            using (var context = new StoreContext())
            {
                // Filtrar los registros donde RegistroApi es específicamente 'false'
                var asistenciasNoEnviadas = context.RrhhAsistencia
                    .Where(a => a.RegistroApi.HasValue && a.RegistroApi.Value == false)
                    .ToList();
           
                return asistenciasNoEnviadas;
            }
        }

        public bool existenRegistrosSinSincronizar()
        {
            List<RrhhAsistencia> registros = ObtenerAsistenciasNoEnviadas();
            if (registros.Any())
            {
                _logger.LogDebug($"Hay {registros.Count} registros en el backup.");
            }
            else
            {
                _logger.LogDebug("No hay datos en backup.");
            }


            return registros.Any();
        }
    }
}
