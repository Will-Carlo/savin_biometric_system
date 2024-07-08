using control_asistencia_savin.Frm;
using control_asistencia_savin.Models;
using control_asistencia_savin.Notifications;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_asistencia_savin.ApiService
{           
    public class ApiService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private Credenciales _credenciales;
        private readonly Microsoft.Extensions.Logging.ILogger _logger = LoggingManager.GetLogger<ApiService>();
        //private readonly MetodosAsistencia _m = new MetodosAsistencia();

        public string _dirMac { get; set; }
        public bool _esProduction = false;
        // ----------------------------------
        // NO MODIFICAR
        //public bool _serverConexion = false;
        public ApiService()
        {
            _credenciales = new Credenciales(_esProduction);
            _dirMac = _credenciales._PssdMac;
            this.CleanHeaders();
        }
        public ApiService(bool _prod, string _mac)
        {
            _credenciales = new Credenciales(_prod);
            _dirMac = _mac;
            this.CleanHeaders();
        }
        public bool IsInternetAvailable()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (client.OpenRead("http://clients3.google.com/generate_204"))
                    {
                        _logger.LogInformation("-> La conexión a internet es correcta.");
                        return true;
                    }
                }
            }
            catch
            {
                _logger.LogCritical("-> Se ha perdido la conexión a internet.");
                return false;
            }
        }
        //public async Task<ModelJson?> GetDataAsyncBU()
        //{
        //    try
        //    {
        //        //MessageBox.Show("m: " + IsInternetAvailable().ToString());
        //        if (!IsInternetAvailable())
        //        {
        //            MessageBox.Show("No hay conexión a Internet: verifique su conexión a internet." +
        //                "\nPuede marcar su asistencia, pero restablezca la conexión a internet lo antes posible.", "Error de conexión");
        //            // Environment.Exit(0);
        //            return null;
        //        }

        //        var response = await _httpClient.GetAsync(_credenciales._getApiLink);

        //        if (response.StatusCode == HttpStatusCode.BadRequest)
        //        {
        //            MessageBox.Show("Tu dirección MAC no está registrada.\nDir mac: " + _credenciales._PssdMac + "\nCerrando la aplicación.");
        //            Environment.Exit(0);
        //            return null;
        //        } else if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.InternalServerError)
        //        {
        //            // MessageBox.Show("Error de conexión en el servidor: "+ response.StatusCode.ToString()); // mensaje principal de error de conexión
        //            return null;
        //        }
        //        else if (response.IsSuccessStatusCode)
        //        {
        //            var json = await response.Content.ReadAsStringAsync();

        //            if (!string.IsNullOrEmpty(json))
        //            {
        //                var data = JsonConvert.DeserializeObject<ModelJson>(json);
        //                _serverConexion = true;
        //                return data;
        //            }
        //            else
        //            {
        //                return null;
        //            }
        //        }
        //        else
        //        {
        //            return null;
        //            //throw new HttpRequestException($"No se pudo conectar al servidor: {response.StatusCode}");
        //        }
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        // Captura la excepción y maneja el error aquí
        //        //MessageBox.Show("Error al conectar al servidor: " + ex.Message+ "\nContacte con el administrador.");
        //        //if (this._esProduction)
        //        //{
        //        //    Environment.Exit(0);
        //        //}
        //        return null;
        //    }
        //}




        public ModelJson GetDataAsync()
        {
            try
            {

                var response = _httpClient.GetAsync(_credenciales._getApiLink).Result;

 
                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;

                    if (!string.IsNullOrEmpty(json))
                    {
                        var data = JsonConvert.DeserializeObject<ModelJson>(json);

                        _logger.LogInformation("Datos recibidos desde la API correctamente.");
                        return data;
                    }
                    else
                    {
                        _logger.LogError("Error al recibir datos de la API.");
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión en el servidor.");
                return null;
            }
        }


        public bool GetDataConexion()
        {
            try
            {
                _logger.LogInformation("-> Verificando conexión a internet y servidor...");

                // Verificar la conexión a Internet
                if (!IsInternetAvailable())
                {
                    _logger.LogCritical("-> No hay conexión a Internet.");
                    return false;
                }

                // Realizar la solicitud HTTP de forma síncrona
                var response = _httpClient.GetAsync(_credenciales._getApiLink).Result;

                switch (response.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        MessageBox.Show("Tu dirección MAC no está registrada.\nDir mac: " + _credenciales._PssdMac + "\nCerrando la aplicación.");
                        _logger.LogError($"-> La dirección MAC no está registrada. Mac: {_credenciales._PssdMac}.");
                        _logger.LogCritical("\n------------------ CERRANDO LA APLICACIÓN POR MAC NO REGISTRADA ------------------");
                        LoggingManager.CloseAndFlush();
                        Environment.Exit(0);
                        return false;

                    case HttpStatusCode.NotFound:
                    case HttpStatusCode.InternalServerError:
                        _logger.LogCritical($"-> No hay conexión con el servidor: {response.StatusCode}."); // mensaje principal de error de conexión
                        return false;

                    case HttpStatusCode.ServiceUnavailable:
                        _logger.LogCritical($"-> El servidor no está disponible: {response.StatusCode}.");
                        //MessageBox.Show("El servidor no está disponible. Por favor, intente nuevamente más tarde.");
                        return false;

                    case HttpStatusCode.TooManyRequests:
                        _logger.LogCritical($"-> Límite de solicitudes alcanzado: {response.StatusCode}.");
                        //MessageBox.Show("Has alcanzado el límite de solicitudes. Por favor, intente nuevamente más tarde.");
                        return false;

                    default:
                        if (response.IsSuccessStatusCode)
                        {
                            var json = response.Content.ReadAsStringAsync().Result;
                            _logger.LogInformation("-> La conexión al servidor es correcta.");
                            return true;
                        }
                        _logger.LogError($"-> Error desconocido al conectar con el servidor: {response.StatusCode}.");
                        return false;
                }
            }
            catch (AggregateException agEx)
            {
                foreach (var ex in agEx.InnerExceptions)
                {
                    HandleHttpRequestException(ex);
                }
                return false;
            }
            catch (HttpRequestException ex)
            {
                HandleHttpRequestException(ex);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se produjo un error inesperado.");
                //MessageBox.Show("Se produjo un error inesperado. Por favor, intente nuevamente más tarde.");
                return false;
            }
        }


        private void HandleHttpRequestException(Exception ex)
        {
            if (ex is HttpRequestException httpRequestEx && httpRequestEx.InnerException is SocketException socketEx)
            {
                switch (socketEx.SocketErrorCode)
                {
                    case SocketError.TimedOut:
                        _logger.LogError(httpRequestEx, "Error de conexión: tiempo de espera agotado.");
                        //MessageBox.Show("Error de conexión: el tiempo de espera se ha agotado.");
                        break;

                    case SocketError.HostNotFound:
                        _logger.LogError(httpRequestEx, "Error de conexión: host no encontrado.");
                        //MessageBox.Show("Error de conexión: host no encontrado.");
                        break;

                    case SocketError.ConnectionRefused:
                        _logger.LogError(httpRequestEx, "Error de conexión: conexión rechazada.");
                        //MessageBox.Show("Error de conexión: la conexión fue rechazada por el servidor.");
                        break;

                    case SocketError.NetworkUnreachable:
                        _logger.LogError(httpRequestEx, "Error de conexión: red no accesible.");
                        //MessageBox.Show("Error de conexión: la red no es accesible.");
                        break;

                    default:
                        _logger.LogError(httpRequestEx, "Error de conexión desconocido.");
                        //MessageBox.Show("Error de conexión desconocido. Por favor, intente nuevamente más tarde.");
                        break;
                }
            }
            else if (ex is HttpRequestException httpRequestEx2 && httpRequestEx2.InnerException is AuthenticationException)
            {
                _logger.LogError(httpRequestEx2, "Error de conexión: problema con SSL/TLS.");
                //MessageBox.Show("Error de conexión: problema con la seguridad de la conexión (SSL/TLS).");
            }
            else
            {
                _logger.LogError(ex, "Error de conexión en el servidor.");
                //MessageBox.Show("Error de conexión en el servidor. Por favor, intente nuevamente más tarde.");
            }
        }


        //public bool GetDataConexion()
        //{
        //    try
        //    {
        //        _logger.LogInformation("-> Verificando conexión a internet y servidor...");
        //        // Verificar la conexión a Internet
        //        if (!IsInternetAvailable())
        //        {
        //            _logger.LogCritical("-> No hay conexión a Internet.");
        //            return false;
        //        }

        //        // Realizar la solicitud HTTP de forma síncrona
        //        var response = _httpClient.GetAsync(_credenciales._getApiLink).Result;

        //        if (response.StatusCode == HttpStatusCode.BadRequest)
        //        {
        //            MessageBox.Show("Tu dirección MAC no está registrada.\nDir mac: " + _credenciales._PssdMac + "\nCerrando la aplicación.");
        //            _logger.LogError($"-> La dirección MAC no está registrada. Mac: {_credenciales._PssdMac}.");
        //            _logger.LogCritical("\n------------------ CERRANDO LA APLICACIÓN POR MAC NO REGISTRADA ------------------");
        //            LoggingManager.CloseAndFlush();

        //            Environment.Exit(0);
        //            return false;
        //        }
        //        else if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.InternalServerError)
        //        {
        //            _logger.LogCritical($"-> No hay conexión con el servidor: {response.StatusCode}."); // mensaje principal de error de conexión
        //            return false;
        //        }
        //        else if (response.IsSuccessStatusCode)
        //        {
        //            var json = response.Content.ReadAsStringAsync().Result;

        //            //if (!string.IsNullOrEmpty(json))
        //            //{
        //            //    //var data = JsonConvert.DeserializeObject<ModelJson>(json);
        //            // //   _serverConexion = true;
        //            _logger.LogInformation("-> La conexión al servidor es correcta.");
        //            return true;
        //            //}
        //            //else
        //            //{
        //            //    return false;
        //            //}
        //        }
        //        //else
        //        //{
        //            return false;
        //        //}
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        _logger.LogError(ex, "Error de conexión en el servidor.");
        //        return false;
        //    }
        //}


        public ModelJson GetDataForMac()
        {
            try
            {
                _logger.LogInformation("-> Verificando conexión a internet y servidor...");
                // Verificar la conexión a Internet
                if (!IsInternetAvailable())
                {
                    _logger.LogCritical("-> No hay conexión a Internet.");
                    return null;
                }

                // Realizar la solicitud HTTP de forma síncrona
                var response = _httpClient.GetAsync(_credenciales._getApiLink).Result;

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("Tu dirección MAC no está registrada.\nDir mac: " + _credenciales._PssdMac + "\nCerrando la aplicación.");
                    _logger.LogError($"-> La dirección MAC no está registrada. Mac: {_credenciales._PssdMac}.");
                    _logger.LogCritical("\n------------------ CERRANDO LA APLICACIÓN POR MAC NO REGISTRADA ------------------");
                    LoggingManager.CloseAndFlush();

                    Environment.Exit(0);
                    return null;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    _logger.LogCritical($"-> No hay conexión con el servidor: {response.StatusCode}."); // mensaje principal de error de conexión
                    return null;
                }
                else if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;

                    if (!string.IsNullOrEmpty(json))
                    {
                        var data = JsonConvert.DeserializeObject<ModelJson>(json);
                        //_serverConexion = true;
                        _logger.LogInformation("-> La conexión al servidor es correcta.");
                        return data;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión en el servidor.");
                return null;
            }
        }

        public List<AuxAsistencia> GetDataAsistencia(int idPersonal)
        {
            try
            {
                _logger.LogInformation("------ VER ASISTENCIAS POR PERSONAL API ------");
                this.CleanHeaders();
                _httpClient.DefaultRequestHeaders.Add("IdPersonal", idPersonal.ToString());
                var response = _httpClient.GetAsync(_credenciales._getAsistenciaLink).Result;
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Respuesta: {response.StatusCode}");
                    //_logger.LogInformation($"Extrayendo asistencias del personal: {_m.NombrePersonal(idPersonal)}");
                    _logger.LogInformation($"Extrayendo asistencias del personal: {idPersonal.ToString()}");

                    var json = response.Content.ReadAsStringAsync().Result;
                    var asistencias = JsonConvert.DeserializeObject<List<AuxAsistencia>>(json);
                    if (asistencias != null && asistencias.Any())
                    {
                        _logger.LogInformation("Mostrando asistencias registradas...");
                        return asistencias;
                    }
                    else
                    {
                        _logger.LogInformation("El personal no tiene asistencias registradas.");
                        return new List<AuxAsistencia>();
                    }
                }
                else
                {
                    _logger.LogError($"Error al obtener los datos de asistencia por personal: {response.StatusCode}");
                }
                _logger.LogInformation("------ FIN VER ASISTENCIAS POR PERSONAL API ------");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al obtener los datos de asistencia por personal.");
            }

            return new List<AuxAsistencia>();

        }
        public async Task<HttpResponseMessage> RegistrarAsistenciaAsync(RrhhAsistencia asistencia)
        {

            try
            {

                //if (!this.IsInternetAvailable())
                //{
                //    MessageBox.Show("No hay conexión a internet, se registrará en la tabla de TemporalAsistencia", "Estado de la respuesta del servidor");
                //    //_m.setAddAsistenciaTemporal(asistencia);
                //}


                this.CleanHeaders();

                var regAsistencia = new
                {
                    idTurno = asistencia.IdTurno,
                    idPersonal = asistencia.IdPersonal,
                    horaMarcado = asistencia.HoraMarcado,
                    minutosAtraso = asistencia.MinutosAtraso,
                    indTipoMovimiento = asistencia.IndTipoMovimiento,
                    idPuntoAsistencia = asistencia.IdPuntoAsistencia,
                    observaciones = asistencia.Observaciones
                };

                string jsonContent = JsonConvert.SerializeObject(regAsistencia);


                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                _logger.LogInformation("------ REGISTRO DE ASISTENCIA API ------");
                _logger.LogDebug("Asistencia: " + regAsistencia);

                // Realiza la solicitud POST a la API
                var response = await _httpClient.PostAsync(_credenciales._postApiLink, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                _logger.LogDebug($"Estado de la respuesta: {response.StatusCode}, Contenido de la respuesta: {responseBody}.");
                _logger.LogInformation("------ FIN REGISTRO DE ASISTENCIA API ------");

                //_m.setAddAsistencia(asistencia);


                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error al registrar la asistencia: " + response.StatusCode + "\nDetalles: " + responseBody + "\nContactar con el administrador.");
                    _logger.LogCritical($"Error al registrar la asistencia: {response.StatusCode} \nDetalles: {responseBody}.");

                    //throw new HttpRequestException($"Error al registrar a la asistencia: {response.StatusCode}" + "\nDetalles: " + responseBody);
                }

                return response;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al registrar asistencia.");
                return null;
            }
        }


        public async Task<HttpResponseMessage> ModificarAsistenciaAsync(int IdPersonal, string HoraMarcado, int IdPuntoAsistencia)
        {
            try
            {
                _logger.LogInformation("------ REGISTRO DE SALIDA EXTRA API ------");
                this.CleanHeaders();

                // Asignar los datos al encabezado de la solicitud
                _httpClient.DefaultRequestHeaders.Add("IdPersonal", IdPersonal.ToString());
                _httpClient.DefaultRequestHeaders.Add("HoraMarcado", HoraMarcado);
                // _httpClient.DefaultRequestHeaders.Add("IdPuntoAsistencia", IdPuntoAsistencia.ToString());

                // Realizar la solicitud PUT a la API
                var response = await _httpClient.PutAsync(_credenciales._putSalidasLink, null);

                // Verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Se ha hecho el registro de salida extra correctamente.");
                } else
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error al modificar el registro: {response.StatusCode} \nDetalles: {responseBody}.");
                }
                _logger.LogInformation("------ FIN REGISTRO DE SALIDA EXTRA API ------");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Error al realizar la solicitud PUT para el registro de salida extra.");
                return null;
            }
        }
        private void CleanHeaders()
        {
            // Limpiar los encabezados antes de agregarlos nuevamente
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add(_credenciales._token, _credenciales._PssdToken);
            _httpClient.DefaultRequestHeaders.Add(_credenciales._mac, this._dirMac);
        }
  
    }


}

        //public ModelJson? GetData()
        //{
        //    this.CleanHeaders();
        //    var response = _httpClient.GetAsync(_credenciales._getApiLink).GetAwaiter().GetResult();

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        //        var data = JsonConvert.DeserializeObject<ModelJson>(json);
        //        return data;
        //    }
        //    else
        //    {
        //        throw new HttpRequestException($"No se pudo conectar al servidor: {response.StatusCode}");
        //    }
        //}

        //public async Task<ModelJson> GetDataAsync()
        //{
        //    var response = await _httpClient.GetAsync(_getApiLink);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var json = await response.Content.ReadAsStringAsync();
        //        var data = JsonConvert.DeserializeObject<ModelJson>(json);

        //        return data;
        //    }
        //    else
        //    {
        //        throw new HttpRequestException($"No se pudo conectar al servidor: {response.StatusCode}");
        //    }
        //}

        //public List<RrhhAsistencia> GetDataAsistencia()
        //{
        //    var response = _httpClient.GetAsync(_getAsistenciaLink).GetAwaiter().GetResult();

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        //        var data = JsonConvert.DeserializeObject<List<RrhhAsistencia>>(json);
        //        return data;
        //    }
        //    else
        //    {
        //        throw new HttpRequestException($"No se pudo conectar al servidor: {response.StatusCode}");
        //    }
        //}
        //public async Task<List<AuxAsistencia>> GetDataAsistenciaAsync(int idPersonal)
        //{
        //    var response = await _httpClient.GetAsync(_getAsistenciaLink);
        //    _httpClient.DefaultRequestHeaders.Add("IdPersonal", idPersonal.ToString());

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var json = await response.Content.ReadAsStringAsync();
        //        var asistencias = JsonConvert.DeserializeObject<List<AuxAsistencia>>(json);
        //        return asistencias;
        //    }
        //    else
        //    {
        //        throw new HttpRequestException($"Error al obtener los datos de asistencia: {response.StatusCode}");
        //    }
        //}
