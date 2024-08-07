﻿using control_asistencia_savin.ApiService;
using control_asistencia_savin.Models;
using control_asistencia_savin.Notifications;
using DPFP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_asistencia_savin.Frm
{
    internal class MetodosAsistencia
    {
        private ApiService.FunctionsDataBase _functionsDataBase = new FunctionsDataBase();
        private readonly ApiService.ApiService _apiService = new ApiService.ApiService();
        private readonly Microsoft.Extensions.Logging.ILogger _logger = LoggingManager.GetLogger<MetodosAsistencia>();
        //private MetodosAsistencia _m = new MetodosAsistencia();
        private string _capturaHoraMarcado = "";
        private bool existeConexion = true;
        private string guardarObservacionPersonal = "";
        // -------------------------------------------------------------------
        // PROCEDIMIENTOS PARA EL REGISTRO DE ASISTENCIA
        // -------------------------------------------------------------------
        public int getIdTurno(int idPersonal)
        {
            _logger.LogDebug("Nombre Personal capturado: " + idPersonal + ". " + this.NombrePersonal(idPersonal));
            int idTurno = this.capturaIdTurno(idPersonal);
            _logger.LogDebug("IdTurno capturado: " + idTurno);
            if (validarTurno(idPersonal, idTurno))
            {
                return idTurno;
            }
            else
            {
                _logger.LogDebug("No tienes asignado el turno: " + idTurno);
                throw new HttpRequestException($"No tienes asignado el turno: " + turnoNombre2(idTurno) + "\nContactar con el administrador.");
            }
        }
        public string getHoraMarcado()
        {
            return _capturaHoraMarcado;
        }
        public int getMinutosAtraso(int IdPersonal)
        {
            // capturamos el tipo de turno para sumar si se atrasa o si sale antes

            //MessageBox.Show("IndMov: " + indMov +
            //    "\nEsSalidaExtra?: " + EsSalidaExtra(IdPersonal).ToString());
            if (!EsSalidaExtra(IdPersonal))
            {
                //if (indMov == 461)
                //{
                //    return this.capturaMinutosAtrasoEntrada(IdPersonal);
                //}
                //else if (indMov == 462)
                //{
                //    return this.capturaMinutosAtrasoSalida(IdPersonal);
                //}
                return this.capturaMinutosAtraso(IdPersonal);
            }
            else
            {
                return this.capturaMinutosSalidaExtra(IdPersonal);
            }
        }
        public int getIndTipoMovimiento(int IdPersonal)
        {
            return this.capturaTipoMovimiento(IdPersonal);
        }
        public int getIdPuntoAsistencia()
        {
            return this.capturaIdPuntoAsistencia();
        }
        // -------------------------------------------------------------------
        // PROCEDIMIENTOS PARA VALIDAR EN CLASES DE CÓDIGO Y HUELLA
        // -------------------------------------------------------------------
        public int capturaTipoMovimiento(int IdPersonal)
        {
            using (var context = new StoreContext())
            {
                // Capturamos la fecha de hoy y la convertimos a su representación en cadena
                DateTime getDateToString = DateTime.Parse(_capturaHoraMarcado);
                string fechaHoyStr = getDateToString.ToString("yyyy-MM-dd");

                //if (existeConexion)
                //{
                    var ultimoRegistro = context.RrhhAsistencia
                        .Where(a => a.IdPersonal == IdPersonal &&
                                     a.HoraMarcado.StartsWith(fechaHoyStr) && a.IndTipoMovimiento != 469 && a.IndTipoMovimiento != 499 && a.IndTipoMovimiento != 506)  // no usamos los registros de faltas
                        .OrderByDescending(a => a.HoraMarcado) // Asumiendo que 'Id' es un autoincremento y representa el más reciente; ordenamos por Hora de marcado por si se agregó por postman
                        .Select(a => a.IndTipoMovimiento)
                        .FirstOrDefault(); // Devuelve el primer elemento o 0 si la secuencia está vacía,

                    // Si el valor es 1, devolver 2. Si el valor es 2 o no hay registros, devolver 1.
                    return ultimoRegistro == 461 ? 462 : 461;
                //}
                //else
                //{
                //    var ultimoRegistro = context.RrhhAsistenciaTemporals
                //   .Where(a => a.IdPersonal == IdPersonal &&
                //                a.HoraMarcado.StartsWith(fechaHoyStr) && a.IndTipoMovimiento != 469)  // no usamos los registros de faltas
                //   .OrderByDescending(a => a.HoraMarcado) // Asumiendo que 'Id' es un autoincremento y representa el más reciente; ordenamos por Hora de marcado por si se agregó por postman
                //   .Select(a => a.IndTipoMovimiento)
                //   .FirstOrDefault(); // Devuelve el primer elemento o 0 si la secuencia está vacía.

                //    // Si el valor es 1, devolver 2. Si el valor es 2 o no hay registros, devolver 1.
                //    return ultimoRegistro == 461 ? 462 : 461;
                //}

            }
        }
        public void setCapturaHoraMarcado(string capturaHoraMarcado)
        {
            _capturaHoraMarcado = capturaHoraMarcado;
        }
        public string getCapturaHoraMarcado()
        {
            return this._capturaHoraMarcado;
        }
        public string getHora()
        {
            DateTime getDateToString = DateTime.Parse(_capturaHoraMarcado);
            return getDateToString.ToString("HH:mm:ss");
        }
        // -------------------------------------------------------------------
        // PROCEDIMIENTOS PARA VALIDAR ASISTENCIAS EN APIREST O LOCAL CON ASISTENCIAS TEMPORALES
        // -------------------------------------------------------------------        
        public void setAddAsistencia(RrhhAsistencia item)
        {
            _logger.LogInformation("-------------- REGISTRO DE ASISTENCIA BD --------------");


            var regAsistenciaForLog = new
            {
                idTurno = item.IdTurno,
                idPersonal = item.IdPersonal,
                horaMarcado = item.HoraMarcado,
                minutosAtraso = item.MinutosAtraso,
                indTipoMovimiento = item.IndTipoMovimiento,
                idPuntoAsistencia = item.IdPuntoAsistencia,
                observaciones = item.Observaciones
            };

            using (var db = new StoreContext())
            {
                db.RrhhAsistencia.Add(item);
                db.SaveChanges();
            }
            _logger.LogDebug("Asistencia: " + regAsistenciaForLog);
            _logger.LogInformation("-------------- FIN REGISTRO DE ASISTENCIA BD --------------");

        }
        //public void setAddAsistenciaTemporal(RrhhAsistencia item)
        //{
        //    _logger.LogInformation("-------------- REGISTRO DE ASISTENCIA BD BACKUP --------------");

        //    var regAsistenciaForLog = new
        //    {
        //        idTurno = item.IdTurno,
        //        idPersonal = item.IdPersonal,
        //        horaMarcado = item.HoraMarcado,
        //        minutosAtraso = item.MinutosAtraso,
        //        indTipoMovimiento = item.IndTipoMovimiento,
        //        idPuntoAsistencia = item.IdPuntoAsistencia,
        //        observaciones = item.Observaciones
        //    };

        //    RrhhAsistenciaTemporal regAsistencia = new RrhhAsistenciaTemporal()
        //    {
        //        IdTurno = item.IdTurno,
        //        IdPersonal = item.IdPersonal,
        //        HoraMarcado = item.HoraMarcado,
        //        MinutosAtraso = item.MinutosAtraso,
        //        IndTipoMovimiento = item.IndTipoMovimiento,
        //        IdPuntoAsistencia = item.IdPuntoAsistencia,
        //        Observaciones = item.Observaciones
        //    };

        //    using (var db = new StoreContext())
        //    {
        //        db.RrhhAsistenciaTemporals.Add(regAsistencia);
        //        db.SaveChanges();
        //    }
        //    _logger.LogDebug("Asistencia: " + regAsistenciaForLog);
        //    _logger.LogInformation("-------------- FIN REGISTRO DE ASISTENCIA BD BACKUP --------------");
        //}
        public void ValidarAsistencia(RrhhAsistencia item)
        {
            //if (_apiService.IsInternetAvailable())
            item.Observaciones = this.guardarObservacionPersonal;
            this.guardarObservacionPersonal = "";

            if (_functionsDataBase.verifyConection())
            {
                var response = _apiService.RegistrarAsistenciaAsync(item);
                this.setAddAsistencia(item);
            }
            else
            {
                _logger.LogDebug("La asistencia se registrará en el backup del programa.");
                
                item.RegistroApi = false;
                this.setAddAsistencia(item);
                //this.setAddAsistenciaTemporal(item);
            }
        }


        public void registrarAsistenciasTemporales()
        {
            try
            {
                using (var context = new StoreContext())
                {
                    _logger.LogInformation("-> Verificando si existe asistencias almacenadas en el backup...");
                    // Obtener todos los registros de rrhh_asistencia_temporal
                    List<RrhhAsistencia> listaRegistros = _functionsDataBase.ObtenerAsistenciasNoEnviadas();
                    if (listaRegistros.Count == 0)
                    {
                        _logger.LogInformation("No existen asistencias guardadas en el backup.");
                    }
                    else
                    {
                        _logger.LogInformation($"Registrando {listaRegistros.Count} asistencias.");
                    }

                    foreach (var registroTemporal in listaRegistros)
                    {
                        // Convertir el registro temporal a un objeto RrhhAsistencia
                        var item = new RrhhAsistencia
                        {
                            IdTurno = registroTemporal.IdTurno,
                            IdPersonal = registroTemporal.IdPersonal,
                            HoraMarcado = registroTemporal.HoraMarcado,
                            MinutosAtraso = registroTemporal.MinutosAtraso,
                            IndTipoMovimiento = registroTemporal.IndTipoMovimiento,
                            IdPuntoAsistencia = registroTemporal.IdPuntoAsistencia,
                            Observaciones = registroTemporal.Observaciones
                        };

                        // Enviar el objeto a través del servicio API
                        _logger.LogInformation($"Envio de asistencia del backup: {item}.");
                        var response = _apiService.RegistrarAsistenciaAsync(item);

                        // Si el registro se envió con éxito, elimínalo de la tabla temporal
                        //if (response.IsSuccessStatusCode)
                        //{
                        _logger.LogInformation($"Eliminando registro del backup.");
                        item.RegistroApi = true;
                        //}
                    }

                    // Guardar los cambios en la base de datos
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al verificar asistencias guardadas en el backup: " + ex.Message);
            }

        }

        


        //public void registrarAsistenciasTemporales2()
        //{
        //    try
        //    {
        //        using (var context = new StoreContext())
        //        {
        //            _logger.LogInformation("-> Verificando si existe asistencias almacenadas en el backup...");
        //            // Obtener todos los registros de rrhh_asistencia_temporal
        //            var registrosTemporales = context.RrhhAsistenciaTemporals.ToList();
        //            if (registrosTemporales.Count == 0)
        //            {
        //                _logger.LogInformation("No existen asistencias guardadas en el backup.");
        //            } else
        //            {
        //                _logger.LogInformation($"Registrando {registrosTemporales.Count} asistencias.");
        //            }

        //            foreach (var registroTemporal in registrosTemporales)
        //            {
        //                // Convertir el registro temporal a un objeto RrhhAsistencia
        //                var item = new RrhhAsistencia
        //                {
        //                    IdTurno = registroTemporal.IdTurno,
        //                    IdPersonal = registroTemporal.IdPersonal,
        //                    HoraMarcado = registroTemporal.HoraMarcado,
        //                    MinutosAtraso = registroTemporal.MinutosAtraso,
        //                    IndTipoMovimiento = registroTemporal.IndTipoMovimiento,
        //                    IdPuntoAsistencia = registroTemporal.IdPuntoAsistencia,
        //                    Observaciones = registroTemporal.Observaciones
        //                };

        //                // Enviar el objeto a través del servicio API
        //                _logger.LogInformation($"Envio de asistencia del backup: {item}.");
        //                var response = _apiService.RegistrarAsistenciaAsync(item);

        //                // Si el registro se envió con éxito, elimínalo de la tabla temporal
        //                //if (response.IsSuccessStatusCode)
        //                //{
        //                _logger.LogInformation($"Eliminando registro del backup.");
        //                context.RrhhAsistenciaTemporals.Remove(registroTemporal);
        //                //}
        //            }

        //            // Guardar los cambios en la base de datos
        //            context.SaveChanges();
        //        }
        //    } catch (Exception ex) 
        //    {
        //        _logger.LogError("Error al verificar asistencias guardadas en el backup: " + ex.Message);
        //    }
           
        //}
        public bool getExisteConexion()
        {
            return this.existeConexion;
        }
        public void setExisteConexion(bool existeConexion)
        {
            this.existeConexion = existeConexion;
        }
        // -------------------------------------------------------------------
        // 
        // -------------------------------------------------------------------
        // no se necesita validar la fecha de hoy
        private int capturaIdPuntoAsistencia()
        {
            //_logger.LogDebug("VALIDANDO PUNTO DE ASISTENCIA");
            //_logger.LogDebug("_dirMac: " + _apiService._dirMac);
            using (var context = new StoreContext())
            {
                string dirMac = _apiService._dirMac;
                var ultimoRegistro = context.RrhhPuntoAsistencia
                    .Where(a => a.DireccionMac == dirMac)
                    .Select(a => a.Id)
                    .FirstOrDefault(); // Devuelve el primer elemento o 0 si la secuencia está vacía.
                //_logger.LogDebug("ultimoRegistro: " + ultimoRegistro);
                return ultimoRegistro;
            }
        }
        // no se necesita validar la fecha de hoy sólo necesitamos capturar las horas del día
        private int capturaIdTurno(int IdPersonal)
        {
            var horaActual = DateTime.Parse(this._capturaHoraMarcado);
            //_logger.LogDebug("hora capturada: " + this._capturaHoraMarcado + " idpersonal: " + IdPersonal);
            var AntesDeLas_14_29 = new DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 14, 29, 59);
            var EsDespuesDe_12_30 = new DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 12, 30, 0);
            //_logger.LogDebug("\nHora Actual: " + horaActual + "\nAntes de las 14:29: " + AntesDeLas_14_29 + "\nDespués de 12:30: " + EsDespuesDe_12_30);
            //var horaActual;
            if (EsSabado())
            {
                return 3;
            }
            else if (validarTurno(IdPersonal, 4))
            {
                // si el personal es de tipo "horario continuo"
                return 4;
            }
            else if (getAnteriorIndMov(IdPersonal) == 461)
            {
                // si sólo marcó la 'entrada' por obligación se marcará su 'salida' con el mismo id Turno.
                return getAnteriorIdTurno(IdPersonal);
            }
            else
            {
                //_logger.LogDebug("Validando turno 1 2 3");
                //_logger.LogDebug("existeAnteriorIdTurno " + this.existeAnteriorIdTurno(IdPersonal));

                if (this.existeAnteriorIdTurno(IdPersonal))
                {
                    //_logger.LogDebug("getAnteriorIdTurno " + this.getAnteriorIdTurno(IdPersonal));
                    if (this.getAnteriorIdTurno(IdPersonal) == 1)
                    {
                        if (horaActual < AntesDeLas_14_29)
                        {
                            //if (horaActual > EsDespuesDe_12_30 && this.getIndTipoMovimiento(IdPersonal) == 462)
                            if (horaActual > EsDespuesDe_12_30)
                            {
                                // Si el personal está saliendo en este rango entonces se le asigna la salida con turno 1
                                // Pero si esta entrando es decir 461, entonces se le asigna la entrada de la tarde
                                //_logger.LogDebug("capturaTipoMovimiento " + this.capturaTipoMovimiento(IdPersonal));
                                if (this.capturaTipoMovimiento(IdPersonal) == 462)
                                {
                                    return 1;
                                }
                                else
                                {
                                    return 2;
                                }
                                // Aquí es clave para el cambio de turno al medio día
                                //return 2;
                            }
                            else
                            {
                                return 1;
                            }

                        }
                        else
                        {
                            //MessageBox.Show("Aquí 2");
                            return 2;
                        }

                    }
                    else if (this.getAnteriorIdTurno(IdPersonal) == 2)
                    {
                        return 2;
                    }
                    else
                    {
                        _logger.LogError("ERROR DE VALIDACIÓN DE TURNO: " + this.getAnteriorIdTurno(IdPersonal));
                    }
                }
                else
                {
                    if (horaActual > EsDespuesDe_12_30)
                    {
                        //MessageBox.Show("Aquí 3");
                        return 2;
                    }
                    else
                    {
                        return 1;
                    }
                }
                //var horaActual = DateTime.Now;
                //var limiteHora = new DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 13, 20, 0);
                //return horaActual < limiteHora ? 1 : 2;
            }
            return 0;
        }
        // no se necesita validar la fecha de hoy, sólo validamos si el personal tiene asignado el turno 
        private bool validarTurno(int idPersonal, int idTurno)
        {
            using (var context = new StoreContext())
            {
                // Busca si existe un turno asignado que coincida con el idPersonal y idTurno
                var asignacion = context.RrhhTurnoAsignados
                                        .Any(ta => ta.IdPersonal == idPersonal && ta.IdTurno == idTurno);

                return asignacion; // True si existe la asignación, False de lo contrario
            }
        }
        private string turnoNombre2(int idTurno)
        {
            switch (idTurno)
            {
                case 0: return "error";
                case 1: return "MAÑANA";
                case 2: return "TARDE";
                case 3: return "SÁBADO";
                case 4: return "HORARIO CONTINUO LV";
                default : return "error def";
            }
        }
        public bool EsSabado()
        {
            DateTime fechaCapturada = DateTime.Parse(this._capturaHoraMarcado);
            // return DateTime.Today.DayOfWeek == DayOfWeek.Saturday;
            return fechaCapturada.DayOfWeek == DayOfWeek.Saturday;
        }
        // validamos si hoy es un día feriado para evitar que el sistema les marque falatas
        private bool EsFeriado()
        {

            DateTime fechaHora = DateTime.Now;
            DateOnly fechaActual = new DateOnly(fechaHora.Year, fechaHora.Month, fechaHora.Day);


            try
            {
                using (var context = new StoreContext())
                {
                    // Obtener la lista de feriados
                    List<RrhhFeriado> feriados = context.RrhhFeriados.ToList();

                    // Verificar si alguna fecha de feriado coincide con la fecha actual
                    foreach (var feriado in feriados)
                    {
                        if (feriado.Fecha == fechaActual) // Error CS0019 aquí
                        {
                            return true; // Hay un feriado en la fecha actual
                        }
                    }
                }

                return false; // No hay feriado en la fecha actual
            }
            catch (Exception ex)
            {
                // Capturar cualquier excepción que ocurra durante la consulta
                Console.WriteLine($"Error al comprobar feriados: {ex.Message}");
                return false;
            }
        }
        // -------------------------------------------------------------------
        // SALIDAS EXTRAS
        // -------------------------------------------------------------------
        private bool EsSalidaExtra(int IdPersonal)
        {
            if (this.getAnteriorIdTurno(IdPersonal) == this.capturaIdTurno(IdPersonal) && this.getAnteriorIndMov(IdPersonal) == 462)
            {
                //MessageBox.Show("ant. ind mov: " + this.getAnteriorIndMov(IdPersonal));
                // también validamos si es entrada antes de su horario de entraada, para que no le marque como minutos acumulados de salida
                //DateTime fechaMarcada = DateTime.Parse(this._capturaHoraMarcado);
                //TimeSpan horaMarcada = fechaMarcada.TimeOfDay;
                //var tiempoEntrada = capturaHoraEntrada(capturaIdTurno(IdPersonal));

                //if (horaMarcada > tiempoEntrada)
                //{
                return true;
                //}
                //return false;

            }
            return false;
        }
        // validamos la fecha de hoy
        private int getAnteriorIdTurno(int IdPersonal)
        {
            int ultimoIdTurno = 0; // Valor por defecto si no se encuentra ningún registro

            using (var dbContext = new StoreContext())
            {
                // Obtener la fecha de hoy en el formato de texto correcto para comparar con la base de datos
                DateTime getDateToString = DateTime.Parse(_capturaHoraMarcado);
                string fechaHoy = getDateToString.ToString("yyyy-MM-dd");
                //string fechaHoy = DateTime.Today.ToString("yyyy-MM-dd");

                // Consulta para obtener el ID del último registro para el IdPersonal dado y la fecha de hoy

                //if (existeConexion)
                //{
                var ultimoRegistro = dbContext.RrhhAsistencia
                    .Where(a => a.IdPersonal == IdPersonal && a.HoraMarcado.StartsWith(fechaHoy) && a.IndTipoMovimiento != 469 && a.IndTipoMovimiento != 499 && a.IndTipoMovimiento != 506) // Filtrar por fecha de hoy
                    .OrderByDescending(a => a.HoraMarcado) // Ordenar por el ID en orden descendente para obtener el último registro
                    .FirstOrDefault();

                if (ultimoRegistro != null)
                {
                    ultimoIdTurno = ultimoRegistro.IdTurno;
                }
                //}
                //else
                //{
                //    var ultimoRegistro = dbContext.RrhhAsistenciaTemporals
                //    .Where(a => a.IdPersonal == IdPersonal && a.HoraMarcado.StartsWith(fechaHoy) && a.IndTipoMovimiento != 469) // Filtrar por fecha de hoy
                //    .OrderByDescending(a => a.HoraMarcado) // Ordenar por el ID en orden descendente para obtener el último registro
                //    .FirstOrDefault();

                //    if (ultimoRegistro != null)
                //    {
                //        ultimoIdTurno = ultimoRegistro.IdTurno;
                //    }
                //}


            }

            return ultimoIdTurno;
        }
        // validamos la fecha de hoy
        private int getAnteriorIndMov(int IdPersonal)
        {
            int indTipoMovimiento = 0; // Valor predeterminado en caso de no encontrar registro

            using (var context = new StoreContext())
            {
                // Obtener la fecha de hoy
                DateTime getDateToString = DateTime.Parse(_capturaHoraMarcado);
                string fechaHoy = getDateToString.ToString("yyyy-MM-dd");
                //string fechaHoy = DateTime.Today.ToString("yyyy-MM-dd");

                // Consulta LINQ para obtener el ind_tipo_movimiento del último registro para el IdPersonal dado
                //if (existeConexion)
                //{
                var ultimoRegistro = context.RrhhAsistencia
                    .Where(a => a.IdPersonal == IdPersonal && a.HoraMarcado.StartsWith(fechaHoy) && a.IndTipoMovimiento != 469 && a.IndTipoMovimiento != 499 && a.IndTipoMovimiento != 506)
                    .OrderByDescending(a => a.HoraMarcado)
                    .FirstOrDefault(); // Obtiene el primer elemento o null si no hay registros

                if (ultimoRegistro != null)
                {
                    indTipoMovimiento = (int)ultimoRegistro.IndTipoMovimiento;
                }
                //}
                //else
                //{
                //    var ultimoRegistro = context.RrhhAsistenciaTemporals
                //    .Where(a => a.IdPersonal == IdPersonal && a.HoraMarcado.StartsWith(fechaHoy) && a.IndTipoMovimiento != 469)
                //    .OrderByDescending(a => a.HoraMarcado)
                //    .FirstOrDefault(); // Obtiene el primer elemento o null si no hay registros

                //    if (ultimoRegistro != null)
                //    {
                //        indTipoMovimiento = (int)ultimoRegistro.IndTipoMovimiento;
                //    }
                //}
            }

            return indTipoMovimiento;
        }
        // pedimos el anterior horario y comparamos con la nueva entrada
        private int capturaMinutosSalidaExtra(int IdPersonal)
        {
            // reconvertirmos la hora capturada del personal
            DateTime horaMarcada = DateTime.Parse(this._capturaHoraMarcado); // 16:00:00
            DateTime horaAnterior = DateTime.Parse(this.getAnteriorHora(IdPersonal)); // 15:00:00

            // Hacemos las modificaciones en la base de datos local y en la API
            //if (existeConexion)
            //{
            _functionsDataBase.ModificarHoraMarcado(IdPersonal, this.getAnteriorHora(IdPersonal), this.capturaIdPuntoAsistencia());
            if (_functionsDataBase.verifyConection())
            {
                _apiService.ModificarAsistenciaAsync(IdPersonal, this.getAnteriorHora(IdPersonal), this.capturaIdPuntoAsistencia());
            }
            //}
            //else
            //{
            //    _functionsDataBase.ModificarHoraMarcadoTT(IdPersonal, this.getAnteriorHora(IdPersonal), this.capturaIdPuntoAsistencia());
            //}


            // comparamos y hacemos la sumatoria de minutos
            return (int)(horaMarcada - horaAnterior).TotalMinutes;

            //return 69;
        }
        private string getAnteriorHora(int IdPersonal)
        {
            string horaMarcado = "";

            using (var dbContext = new StoreContext())
            {
                //if (existeConexion)
                //{
                    // Consulta para obtener el último registro de asistencia para el idPersonal dado
                var ultimoRegistro = dbContext.RrhhAsistencia
                    .Where(a => a.IdPersonal == IdPersonal && a.IndTipoMovimiento != 469 && a.IndTipoMovimiento != 499 && a.IndTipoMovimiento != 506)
                    .OrderByDescending(a => a.HoraMarcado)
                    .FirstOrDefault();

                // Verificar si se encontró un registro
                if (ultimoRegistro != null)
                {
                    horaMarcado = ultimoRegistro.HoraMarcado;
                }
                //}
                //else
                //{
                //    // Consulta para obtener el último registro de asistencia para el idPersonal dado
                //    var ultimoRegistro = dbContext.RrhhAsistenciaTemporals
                //        .Where(a => a.IdPersonal == IdPersonal && a.IndTipoMovimiento != 469)
                //        .OrderByDescending(a => a.HoraMarcado)
                //        .FirstOrDefault();

                //    // Verificar si se encontró un registro
                //    if (ultimoRegistro != null)
                //    {
                //        horaMarcado = ultimoRegistro.HoraMarcado;
                //    }
                //}


            }

            return horaMarcado;
        }
        // -------------------------------------------------------------------
        // MINUTOS DE ATRASOS DE ENTRADA Y SALIDA
        // -------------------------------------------------------------------
        private int capturaMinutosAtrasoEntrada(int IdPersonal)
        {
            // reconvertirmos la hora capturada del personal
            DateTime horaMarcada = DateTime.Parse(this._capturaHoraMarcado);
            int idTurno = capturaIdTurno(IdPersonal);
            int MinTol = _functionsDataBase.MinutosDeTolerancia();
            //int MinTol = 0;


            // definimos los hoarios de entrada
            var tiempoInicioTurno1 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 8, 30, 0);
            tiempoInicioTurno1 = tiempoInicioTurno1.AddMinutes(MinTol);
            var tiempoInicioTurno2 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 14, 30, 0);
            tiempoInicioTurno2 = tiempoInicioTurno2.AddMinutes(MinTol);
            var tiempoInicioTurno3 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 9, 00, 0);
            tiempoInicioTurno3 = tiempoInicioTurno3.AddMinutes(MinTol);

            //MessageBox.Show("horaMarcada: " + this._capturaHoraMarcado+
            //                "\nTurno: "+ idTurno+
            //                "\ntiempoInicioTurno2: "+ tiempoInicioTurno2);


            // comparamos y hacemos la sumatoria de minutos
            if (idTurno == 1 || idTurno == 4)
            {
                // 08:30:00
                return horaMarcada > tiempoInicioTurno1 ? this.alertPersonal(horaMarcada, tiempoInicioTurno1, IdPersonal, "Estás llegando tarde") : 0;
            }
            else if (idTurno == 2)
            {
                // 14:30:00
                //return horaMarcada > tiempoInicioTurno2 ? (int)(horaMarcada - tiempoInicioTurno2).TotalMinutes : 0;
                return horaMarcada > tiempoInicioTurno2 ? this.alertPersonal(horaMarcada, tiempoInicioTurno2, IdPersonal, "Estás llegando tarde") : 0;
            }
            else if (idTurno == 3)
            {
                // 09:00:00
                //return horaMarcada > tiempoInicioTurno3 ? (int)(horaMarcada - tiempoInicioTurno3).TotalMinutes : 0;
                return horaMarcada > tiempoInicioTurno3 ? this.alertPersonal(horaMarcada, tiempoInicioTurno3, IdPersonal, "Estás llegando tarde") : 0;
            }

            // por default
            return 0;
        }
        private int capturaMinutosAtraso(int IdPersonal)
        {
            // reconvertirmos la hora capturada del personal
            DateTime fechaMarcada = DateTime.Parse(this._capturaHoraMarcado);
            TimeSpan horaMarcada = fechaMarcada.TimeOfDay;
            int idTurno = capturaIdTurno(IdPersonal);
            int indMov = capturaTipoMovimiento(IdPersonal);

            int MinTol = _functionsDataBase.MinutosDeTolerancia();
            var tiempoEntrada = capturaHoraEntrada(idTurno);
            tiempoEntrada = tiempoEntrada.Add(TimeSpan.FromMinutes(MinTol));

            var tiempoSalida = capturaHoraSalida(idTurno);

            if (indMov == 461)
            {
                // 08:30:00
                return horaMarcada > tiempoEntrada ? this.alertPersonalLate(horaMarcada, tiempoEntrada, IdPersonal) : 0;
            }
            else if (indMov == 462 )
            {
                // caso mañana
                // si lel personal ya tiene un registro de salida antes de las 8:30 y está entrando 8:27 no debería generar min atrasos
                // si el personal tiene asistencia está saliendo antes de las 8:30 que es hora de entrada, no debería generar minutos de deuda

                // caso tardes
                // si el personal ya tiene su salida despues de las 7:00 pm entonces cualquier entrada no debería ser verificada con min atraasos
                // si el personsal tiene su salida de la tarde no afecta si es después de las 7pm

                // 12:30:00
                //if (horaMarcada < tiempoSalida && horaMarcada > tiempoEntrada)
                //{
                //    return this.alertPersonalEarly(horaMarcada, tiempoSalida, IdPersonal);
                //}
                //else
                //{
                //    return 0;
                //}

                return horaMarcada < tiempoSalida ? this.alertPersonalEarly(horaMarcada, tiempoSalida, IdPersonal): 0;
            }

            // por default
            return 0;
        }
        private TimeSpan capturaHoraEntrada(int idTurno)
        {
            using (var context = new StoreContext())
            {
                var horaEntrada = context.RrhhTurnos
                    .Where(a => a.Id == idTurno)
                    .Select(a => a.HoraIngreso)
                    .FirstOrDefault(); // Devuelve el primer elemento o 0 si la secuencia está vacía.

                return horaEntrada??TimeSpan.Zero;
            }
        }
        private TimeSpan capturaHoraSalida(int idTurno)
        {
            using (var context = new StoreContext())
            {
                var horaEntrada = context.RrhhTurnos
                    .Where(a => a.Id == idTurno)
                    .Select(a => a.HoraSalida)
                    .FirstOrDefault(); // Devuelve el primer elemento o 0 si la secuencia está vacía.

                return horaEntrada ?? TimeSpan.Zero;
            }
        }
        private int capturaMinutosAtrasoSalida(int IdPersonal)
        {
            // reconvertirmos la hora capturada del personal
            DateTime horaMarcada = DateTime.Parse(this._capturaHoraMarcado);
            int idTurno = capturaIdTurno(IdPersonal);

            // definimos los hoarios de salida
            var tiempoInicioTurno1 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 12, 30, 0);
            var tiempoInicioTurno2 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 19, 00, 0);
            var tiempoInicioTurno3 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 13, 00, 0);


            // comparamos y hacemos la sumatoria de minutos
            if (idTurno == 1)
            {
                // 12:30:00
                return horaMarcada < tiempoInicioTurno1 ? (int)(tiempoInicioTurno1 - horaMarcada).TotalMinutes : 0;
            }
            else if (idTurno == 2 || idTurno == 4)
            {
                // 19:00:00
                return horaMarcada < tiempoInicioTurno2 ? (int)(tiempoInicioTurno2 - horaMarcada).TotalMinutes : 0;
            }
            else if (idTurno == 3)
            {
                // 13:00:00
                return horaMarcada < tiempoInicioTurno3 ? (int)(tiempoInicioTurno3 - horaMarcada).TotalMinutes : 0;
            }

            // por default
            return 0;
        }
        // -------------------------------------------------------------------
        // NOTIFICACIONES
        // -------------------------------------------------------------------
        private int alertPersonal(DateTime horaMarcada, DateTime tiempoInicioTurno1, int IdPersonal, String MessageAlert)
        {

            int min = (int)(horaMarcada - tiempoInicioTurno1).TotalMinutes;
            //MessageBox.Show(this.NombrePersonal(IdPersonal) + " estás llegando tarde por " + min + " minutos.");
            this.NotificationMessage(this.NombrePersonal(IdPersonal) + "\nEstás llegando tarde por " + min + " minutos.", "alert");
            return min;
        }
        private int alertPersonal2(DateTime horaMarcada, DateTime tiempoInicioTurno1, int IdPersonal, String MessageAlert)
        {

            int min = (int)(horaMarcada - tiempoInicioTurno1).TotalMinutes;
            //MessageBox.Show(this.NombrePersonal(IdPersonal) + " estás llegando tarde por " + min + " minutos.");
            this.NotificationMessage(this.NombrePersonal(IdPersonal) + "\nEstás saliendo antes por " + min + " minutos.", "alert");
            return min;
        }
        public string NombrePersonal(int idPersonal)
        {
            string nombreCompleto = "";

            // Suponiendo que dbContext es tu instancia de DbContext
            using (var dbContext = new StoreContext())
            {
                // Buscar el personal por su id
                var personal = dbContext.RrhhPersonals
                                        .FirstOrDefault(p => p.Id == idPersonal);

                // Verificar si se encontró el personal
                if (personal != null)
                {
                    // Construir el nombre completo
                    nombreCompleto = $"{personal.Nombres} {personal.Paterno} {personal.Materno}";
                }
                else
                {
                    // Si no se encuentra el personal, puedes manejar esta situación
                    nombreCompleto = "Empleado no encontrado";
                }
            }

            return nombreCompleto;
        }
        public void NotificationMessage(string MessageUser, string TypeMessage)
        {
            frmNotification customMessage = new frmNotification(MessageUser, TypeMessage);
            customMessage.ShowDialog();
            this.guardarObservacionPersonal = customMessage.observacionPersonal;
            //customMessage.ShowWarningNotification(MessageUser);
        }
        private int alertPersonalLate(TimeSpan horaMarcada, TimeSpan tiempoEntrada, int IdPersonal)
        {
            int min = (int)(horaMarcada - tiempoEntrada).TotalMinutes;
            if (min > 0)
            {
                this.NotificationMessage(this.NombrePersonal(IdPersonal) + "\nEstás llegando tarde por " + min + " minutos.", "alertLateEarly");
            }
            return min;
        }
        private int alertPersonalEarly(TimeSpan horaMarcada, TimeSpan tiempoSalida, int IdPersonal)
        {
            int min = (int)(tiempoSalida - horaMarcada).TotalMinutes;
            if (min > 0)
            {
                //this.NotificationMessage(this.NombrePersonal(IdPersonal) + "\nEstás saliendo antes por " + min + " minutos.", "alertLateEarly");
                // se está quitando la alerta de que se está saliendo antes del horario de trabajo
                //this.NotificationMessage(this.NombrePersonal(IdPersonal) + "\nEstás saliendo antes.", "alertLateEarly");
            }
            return min;
        }
        // -------------------------------------------------------------------
        // FUNCIONES PARA CAPTURAR EL IDTURNO
        // -------------------------------------------------------------------
        // validamos la fecha de hoy para saber si el personal tiene algún IdTurno marcado hoy
        private bool existeAnteriorIdTurno(int IdPersonal)
        {
            using (var context = new StoreContext())
            {
                // Obtener la fecha de hoy
                DateTime fechaHoy = DateTime.Parse(_capturaHoraMarcado);

                // Convertir la fecha de hoy a su representación en cadena
                string fechaHoyStr = fechaHoy.ToString("yyyy-MM-dd");

                //if (existeConexion)
                //{
                    // Verificar si hay algún registro para la fecha de hoy
                bool existeRegistroHoy = context.RrhhAsistencia
                    // CORRIGIENDO ERROR DE VALIDACIÓN DE TURNO
                    //.Any(a => a.IdPersonal == IdPersonal && a.HoraMarcado.StartsWith(fechaHoyStr));
                    .Any(a => a.IdPersonal == IdPersonal && a.HoraMarcado.StartsWith(fechaHoyStr) && a.IndTipoMovimiento != 469 && a.IndTipoMovimiento != 499 && a.IndTipoMovimiento != 506);


                return existeRegistroHoy;
                //}
                //else
                //{
                //    // Verificar si hay algún registro para la fecha de hoy
                //    bool existeRegistroHoy = context.RrhhAsistenciaTemporals
                //        .Any(a => a.IdPersonal == IdPersonal && a.HoraMarcado.StartsWith(fechaHoyStr));

                //    return existeRegistroHoy;
                //}

            }
        }
        // -------------------------------------------------------------------
        // REGISTRAR FALTAS
        // -------------------------------------------------------------------
        public void RegistrarFaltasDelDiaAfterClose()
        {
            if (!SeRegistroFaltas())
            {
                this.RegistrarFaltasDelDia();
            }
            //else
            //{
            //    MessageBox.Show("Ya se registraron las faltas hoy");
            //}
        }
        private void RegistrarFalta(int IdPersonal, int MinFalta, int IdTurno)
        {
            // MessageBox.Show("Id recibido: " + IdPersonal);

            // Obtener la fecha de hoy
            DateTime dateAux = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 19, 00, 01);
            // MessageBox.Show("DateAux: " + dateAux.ToString());

            string fechaAsistencia = dateAux.ToString("yyyy-MM-dd HH:mm:ss");

            //MessageBox.Show("Registrando falta: " + fechaAsistencia);

            RrhhAsistencia auxAsis = new RrhhAsistencia
            {
                IdTurno = IdTurno,
                IdPersonal = IdPersonal,
                HoraMarcado = fechaAsistencia,
                MinutosAtraso = MinFalta,
                // por convención el parámetro 469 es para registrar un falta
                IndTipoMovimiento = 469,
                IdPuntoAsistencia = capturaIdPuntoAsistencia()
            };
            // No es necesario registrar en la base de datos puesto que se enviará al final deía directo al sistema central
            this.setAddAsistencia(auxAsis);
            _apiService.RegistrarAsistenciaAsync(auxAsis);
        }
        public void RegistrarFaltasDelDia()
        {
            using (var context = new StoreContext())
            {
                try
                {
                    // Extraer todos los IdPersonal de la tabla rrhh_personal
                    var idPersonales = context.RrhhPersonals.Select(p => p.Id).ToList();

                    // Imprimir los IdPersonal
                    foreach (var IdPersonal in idPersonales)
                    {
                        //MessageBox.Show("Personal list: " + IdPersonal);
                        int falta = this.FaltasPorPersonal(IdPersonal); 
                    
                        if (falta == 510)
                        {
                            this.RegistrarFalta(IdPersonal, 510, 1);
                        }
                        else if (falta == 240)
                        {
                            if (this.EsSabado())
                            {
                                this.RegistrarFalta(IdPersonal, 240, 3);
                            }
                            else
                            {
                                this.RegistrarFalta(IdPersonal, 240, 1);
                            }
                        }
                        else if(falta == 270)
                        {
                            this.RegistrarFalta(IdPersonal, 270, 2);
                        }
                        //else
                        //{
                        //    MessageBox.Show("El personal no tiene faltas hoy");
                        //}
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al registrar faltas del Personal: {ex.Message}");
                }
            }
        }
        private int FaltasPorPersonal(int IdPersonal)
        {
            int cont = 0;
            if (!EsFeriado())
            {
                if (!EsSabado())
                {
                    for (global::System.Int32 i = 1; i < 3; i++)
                    {
                        //MessageBox.Show("id: " + IdPersonal +   "\ntiene turno? :" + this.TieneTurnoAsignado(IdPersonal, i).ToString() + 
                        //                                        "\nmarco hoy? :" + this.MarcoTurnoHoy(IdPersonal, i).ToString());
                        if (this.TieneTurnoAsignado(IdPersonal, i))
                        {
                            if (!this.MarcoTurnoHoy(IdPersonal, i))
                            {
                                cont += NivelFalta(i);
                                //MessageBox.Show("cont :" + cont);

                            }
                        }
                    }
                }
                else
                {
                    //MessageBox.Show("id: " + IdPersonal +   "\ntiene turno? sab:" + this.TieneTurnoAsignado(IdPersonal, 3).ToString() +
                    //                                        "\nmarco hoy? sab:" + this.MarcoTurnoHoy(IdPersonal, 3).ToString());
                    if (this.TieneTurnoAsignado(IdPersonal, 3))
                    {
                        if (!this.MarcoTurnoHoy(IdPersonal, 3))
                        {
                            cont = NivelFalta(3);
                            //MessageBox.Show("cont sab:" + cont);
                        }
                    }
                }
            }
            return cont;
        }
        private bool TieneTurnoAsignado(int IdPersonal, int IdTurno)
        {
            using (var context = new StoreContext())
            {
                // Verifica si existe un registro con el IdPersonal y el IdTurno dados
                bool existeRegistro = context.RrhhTurnoAsignados
                    .Any(t => t.IdPersonal == IdPersonal && t.IdTurno == IdTurno);
                return existeRegistro;
            }
        }
        private bool MarcoTurnoHoy(int IdPersonal, int IdTurno)
        {
            using (var context = new StoreContext())
            {
                // Obtener la fecha de hoy en formato de cadena yyyy-MM-dd
                string fechaHoyStr = DateTime.Today.ToString("yyyy-MM-dd");

                // Verificar si existe al menos un registro con el IdPersonal y el IdTurno dados para la fecha de hoy
                bool marcoHoy = context.RrhhAsistencia
                    .Any(a => a.IdPersonal == IdPersonal &&
                              a.IdTurno == IdTurno &&
                              a.HoraMarcado.StartsWith(fechaHoyStr));

                return marcoHoy;
            }
        }
        private int NivelFalta(int Nivel)
        {
            if (Nivel == 1 || Nivel == 3)
            {
                return 240;
            }
            else
            {
                return 270;
            }
        }
        private bool SeRegistroFaltas()
        {
            using (var context = new StoreContext())
            {
                // Obtener la fecha de hoy en formato de cadena yyyy-MM-dd
                string fechaHoyStr = DateTime.Today.ToString("yyyy-MM-dd");

                // Verifica si existe un registro con el IdPersonal y el IdTurno dados
                bool existeRegistro = context.RrhhAsistencia
                    .Any(t => t.IndTipoMovimiento == 469 &&
                              t.HoraMarcado.StartsWith(fechaHoyStr));
                return existeRegistro;
            }
        }

        // -------------------------------------------------------------------
        // REGISTROS DOBLES
        // -------------------------------------------------------------------
        public bool EsRegistroDoble(int IdPersonal)
        {
            if (this.MarcoAsistenciaHoy(IdPersonal))
            {
                if (this.MarcoHacePoco(IdPersonal))
                {
                    // MessageBox.Show("1: " + this.MarcoAsistenciaHoy(IdPersonal).ToString() + "\n2: " + this.MarcoHacePoco(IdPersonal).ToString());
                    return true;
                }
            }
            return false;

        }
        private bool MarcoAsistenciaHoy(int IdPersonal)
        {
            using (var context = new StoreContext())
            {
                // Obtener la fecha de hoy en formato de cadena yyyy-MM-dd
                DateTime horaMarcado = DateTime.ParseExact(this._capturaHoraMarcado, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                string fechaHoyStr = horaMarcado.ToString("yyyy-MM-dd");

                //if (existeConexion)
                //{
                // Verificar si existe al menos un registro con el IdPersonal y el IdTurno dados para la fecha de hoy
                bool marcoHoy = context.RrhhAsistencia
                    .Any(a => a.IdPersonal == IdPersonal &&
                                a.HoraMarcado.StartsWith(fechaHoyStr));

                return marcoHoy;
                //}
                //else
                //{
                //    // Verificar si existe al menos un registro con el IdPersonal y el IdTurno dados para la fecha de hoy
                //    bool marcoHoy = context.RrhhAsistenciaTemporals
                //        .Any(a => a.IdPersonal == IdPersonal &&
                //                  a.HoraMarcado.StartsWith(fechaHoyStr));

                //    return marcoHoy;
                //}
            }
        }
        private bool MarcoHacePoco(int IdPersonal)
        {
            using (var context = new StoreContext())
            {
                DateTime horaMarcadoHoy = DateTime.ParseExact(this._capturaHoraMarcado, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                string fechaHoyStr = horaMarcadoHoy.ToString("yyyy-MM-dd");


                // Obtener el último registro de hora_marcado para el IdPersonal dado
                //if (existeConexion)
                //{
                    var ultimoRegistro = context.RrhhAsistencia
                                            .Where(a => a.IdPersonal == IdPersonal && a.IndTipoMovimiento != 469 && a.IndTipoMovimiento != 499 && a.IndTipoMovimiento != 506)
                                            // && a.HoraMarcado.StartsWith(fechaHoyStr))
                                            .OrderByDescending(a => a.HoraMarcado) // Ordena los registros en orden descendente para obtener el más reciente; ordenamos por hora de marcado para validar datos integrados por postman
                                            .Select(a => a.HoraMarcado)
                                            .FirstOrDefault();
                    if (!string.IsNullOrEmpty(ultimoRegistro))
                    {
                        // Convertir el último registro de hora_marcado a un formato de fecha y hora
                        DateTime horaAnterior = DateTime.ParseExact(ultimoRegistro, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                        DateTime horaMarcado = DateTime.ParseExact(this._capturaHoraMarcado, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                        // Calcular la diferencia de tiempo entre la hora de marcado y la hora actual
                        TimeSpan diferenciaTiempo = horaMarcado - horaAnterior;
                        // MessageBox.Show("Actual: "+ horaMarcado + "\nAnterior: "+ horaAnterior + "\nDiferencia: "+diferenciaTiempo.ToString());
                        // Si la diferencia de tiempo es menor o igual a 15 minutos, devuelve true
                        if (diferenciaTiempo.TotalMinutes <= 1)
                        {
                            return true;
                        }
                    }

                    // Si no se encuentra ningún registro o la diferencia de tiempo es mayor a 15 minutos, devuelve false
                    return false;
                //}
                //else
                //{
                //    var ultimoRegistro = context.RrhhAsistenciaTemporals
                //                            .Where(a => a.IdPersonal == IdPersonal && a.IndTipoMovimiento != 469)
                //                            // && a.HoraMarcado.StartsWith(fechaHoyStr))
                //                            .OrderByDescending(a => a.HoraMarcado) // Ordena los registros en orden descendente para obtener el más reciente; ordenamos por hora de marcado para validar datos integrados por postman
                //                            .Select(a => a.HoraMarcado)
                //                            .FirstOrDefault();

                //    if (!string.IsNullOrEmpty(ultimoRegistro))
                //    {
                //        // Convertir el último registro de hora_marcado a un formato de fecha y hora
                //        DateTime horaAnterior = DateTime.ParseExact(ultimoRegistro, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                //        DateTime horaMarcado = DateTime.ParseExact(this._capturaHoraMarcado, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                //        // Calcular la diferencia de tiempo entre la hora de marcado y la hora actual
                //        TimeSpan diferenciaTiempo = horaMarcado - horaAnterior;
                //        // MessageBox.Show("Actual: "+ horaMarcado + "\nAnterior: "+ horaAnterior + "\nDiferencia: "+diferenciaTiempo.ToString());
                //        // Si la diferencia de tiempo es menor o igual a 15 minutos, devuelve true
                //        if (diferenciaTiempo.TotalMinutes <= 1)
                //        {
                //            return true;
                //        }
                //    }

                //    // Si no se encuentra ningún registro o la diferencia de tiempo es mayor a 15 minutos, devuelve false
                //    return false;
                //}

            }
        }

    }
}

//public int capturaMinAtraso()
//{
//    // Parse the captured time string to a DateTime object
//    DateTime horaMarcada = DateTime.Parse(_capturaHoraMarcado);

//    // Define the comparison times
//    var tiempoInicioTurno1 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 8, 30, 0);
//    var tiempoInicioTurno2 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 14, 30, 0);
//    var tiempoInicioTurno3 = new DateTime(horaMarcada.Year, horaMarcada.Month, horaMarcada.Day, 9, 00, 0);


//    // Get the values from the other functions
//    int idTurno = capturaIdTurno();
//    int indMov = capturaIndMov();

//    // Calculate the exceeded minutes according to the conditions
//    if (idTurno == 1 && indMov == 461)
//    {
//        return horaMarcada > tiempoInicioTurno1 ? (int)(horaMarcada - tiempoInicioTurno1).TotalMinutes : 0;
//    }
//    else if (idTurno == 2 && indMov == 461)
//    {
//        return horaMarcada > tiempoInicioTurno2 ? (int)(horaMarcada - tiempoInicioTurno2).TotalMinutes : 0;
//    }
//    else if (idTurno == 3 && indMov == 461)
//    {
//        return horaMarcada > tiempoInicioTurno3 ? (int)(horaMarcada - tiempoInicioTurno3).TotalMinutes : 0;
//    }

//    return 0;
//}

//public int capturaIndMov()
//{
//    var horaActual = DateTime.Now;
//    var medioDia = new DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 12, 0, 0);
//    var tarde = new DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 13, 20, 0);
//    var noche = new DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 18, 0, 0);

//    if (horaActual < medioDia)
//    {
//        return 461;
//    }
//    else if (horaActual >= medioDia && horaActual < tarde)
//    {
//        return 462;
//    }
//    else if (horaActual >= tarde && horaActual < noche)
//    {
//        return 461;
//    }
//    else
//    {
//        return 462;
//    }
//}

// capturamos su tipo de movimiento, salida o entrada
// aquí capturamos si es 1, 2 o 3, los feriados no está validados
//public string turnoNombre(int idTurno)
//{
//    using (var context = new StoreContext())
//    {
//        var turno = context.RrhhTurnos.Find(idTurno);
//        return turno?.Nombre; // Devuelve el nombre si se encuentra el turno, o null si no se encuentra
//    }
//}

//Automatiza el turno pendiente
