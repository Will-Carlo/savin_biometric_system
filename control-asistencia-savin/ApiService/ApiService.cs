using control_asistencia_savin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace control_asistencia_savin.ApiService
{
    public class ApiService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string _getApiLink;
        private string _postApiLink;
        private string _getAsistenciaLink;
        private string _putSalidasLink;

        private String dirMac = "";
        public String nomTienda = "";
        public bool esProduction = false;
        public ApiService()
        {
            //this.Development();
            this.Production();

            nomTienda = GetNombreTienda();
            this.CleanHeaders();
        }

        public String getDirMac()
        {
            return this.dirMac;
        }
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
        public ModelJson? GetData()
        {
            this.CleanHeaders();
            var response = _httpClient.GetAsync(_getApiLink).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var data = JsonConvert.DeserializeObject<ModelJson>(json);
                return data;
            }
            else
            {
                throw new HttpRequestException($"No se pudo conectar al servidor: {response.StatusCode}");
            }
        }
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
        public List<AuxAsistencia> GetDataAsistencia(int idPersonal)
        {
            this.CleanHeaders();
            _httpClient.DefaultRequestHeaders.Add("IdPersonal", idPersonal.ToString());
            var response = _httpClient.GetAsync(_getAsistenciaLink).Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var asistencias = JsonConvert.DeserializeObject<List<AuxAsistencia>>(json);
                if (asistencias != null && asistencias.Any())
                {
                    return asistencias;
                }
                else
                {
                    // Devuelve una lista vacía si no hay asistencias
                    return new List<AuxAsistencia>();
                }
            }
            else
            {
                throw new HttpRequestException($"Error al obtener los datos de asistencia: {response.StatusCode}");
                //MessageBox.Show($"Error al obtener los datos de asistencia: {response.StatusCode}","error");
            }
        }
        public string macAddress()
        {
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

                foreach (NetworkInterface nic in nics)
                {
                    if (nic.OperationalStatus == OperationalStatus.Up && !nic.Description.ToLower().Contains("virtual"))
                    {
                        PhysicalAddress address = nic.GetPhysicalAddress();
                        byte[] bytes = address.GetAddressBytes();
                        return BitConverter.ToString(bytes);
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error al obtener la dirección MAC: {ex.Message}";
            }

            return "Dirección MAC no encontrada";
        }
        public string? GetNombreTienda()
        {
            using (var context = new StoreContext())
            {
                var puntoAsistencia = context.RrhhPuntoAsistencia
                                            .FirstOrDefault(pa => pa.DireccionMac == this.dirMac);
                return puntoAsistencia?.Nombre;
            }
        }
        public async Task<HttpResponseMessage> RegistrarAsistenciaAsync(RrhhAsistencia asistencia)
        {
            this.CleanHeaders();

            var regAsistencia = new
            {
                idTurno = asistencia.IdTurno,
                idPersonal = asistencia.IdPersonal,
                horaMarcado = asistencia.HoraMarcado,
                minutosAtraso = asistencia.MinutosAtraso,
                indTipoMovimiento = asistencia.IndTipoMovimiento,
                idPuntoAsistencia = asistencia.IdPuntoAsistencia
            };

            string jsonContent = JsonConvert.SerializeObject(regAsistencia);

            //MessageBox.Show("json" + asistencia 
            //                    + "\nIdTurno: "+ asistencia.IdTurno
            //                    + "\nIdPersonal: " + asistencia.IdPersonal
            //                    + "\nHoraMarcado: " + asistencia.HoraMarcado
            //                    + "\nMinutosAtraso: " + asistencia.MinutosAtraso
            //                    + "\nIndTipoMovimiento: " + asistencia.IndTipoMovimiento
            //                );

            //MessageBox.Show("json" + regAsistencia);

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Cabeceras para la autenticación
            //_httpClient.DefaultRequestHeaders.Add("Tkn", "SavinBio-23%");

            // Realiza la solicitud POST a la API
            var response = await _httpClient.PostAsync(_postApiLink, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Error al registrar a la asistencia: " + response.StatusCode + "\nDetalles: " + responseBody + "\nContactar con el administrador.");

                //throw new HttpRequestException($"Error al registrar a la asistencia: {response.StatusCode}" + "\nDetalles: " + responseBody);

            }

            return response;
        }
        public async Task<HttpResponseMessage> ModificarAsistenciaAsync(int IdPersonal, string HoraMarcado, int IdPuntoAsistencia)
        {
            try
            {
                this.CleanHeaders();

                // Asignar los datos al encabezado de la solicitud
                _httpClient.DefaultRequestHeaders.Add("IdPersonal", IdPersonal.ToString());
                _httpClient.DefaultRequestHeaders.Add("HoraMarcado", HoraMarcado);
                // _httpClient.DefaultRequestHeaders.Add("IdPuntoAsistencia", IdPuntoAsistencia.ToString());

                // Realizar la solicitud PUT a la API
                var response = await _httpClient.PutAsync(_putSalidasLink, null);

                // Verificar si la solicitud fue exitosa
                if (!response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Error al modificar el registro: " + response.StatusCode + "\nDetalles: " + responseBody + "\nContactar con el administrador.");
                }

                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar la solicitud PUT: " + ex.Message);
                return null;
            }
        }
        private void CleanHeaders()
        {
            // Limpiar los encabezados antes de agregarlos nuevamente
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Tkn", "SavinBio-23%");
            _httpClient.DefaultRequestHeaders.Add("DirMac", dirMac);
        }
        private void Development()
        {
            // GOITIA
            this.dirMac = "14-B3-1F-11-AB-CF";

            this.esProduction = false;
            this._getApiLink = "http://200.105.183.173:8080/savin-rest/ws/biometrico/listar-estructura-biometrico";
            this._postApiLink = "http://200.105.183.173:8080/savin-rest/ws/biometrico/registrar-asistencia";
            this._getAsistenciaLink = "http://200.105.183.173:8080/savin-rest/ws/biometrico/listar-asistencia-personal";
            this._putSalidasLink = "http://200.105.183.173:8080/savin-rest/ws/biometrico/modificar-minutos-atraso";
        }
        private void Production()
        {
            this.dirMac = macAddress();

            this.esProduction = true;
            this._getApiLink = "http://54.177.210.26:8080/savin-rest/ws/biometrico/listar-estructura-biometrico";
            this._postApiLink = "http://54.177.210.26:8080/savin-rest/ws/biometrico/registrar-asistencia";
            this._getAsistenciaLink = "http://54.177.210.26:8080/savin-rest/ws/biometrico/listar-asistencia-personal";
            this._putSalidasLink = "http://54.177.210.26:8080/savin-rest/ws/biometrico/modificar-minutos-atraso";
        }
        public bool getEsProduction()
        {
            return this.esProduction;
        }
    }


}
