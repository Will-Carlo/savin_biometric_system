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
        private String _getApiLink = "http://200.105.183.173:8080/savin-rest/ws/biometrico/listar-estructura-biometrico";
        private String _postApiLink = "http://200.105.183.173:8080/savin-rest/ws/biometrico/registrar-asistencia";
        private String dirMac = "";
        public String nomTienda = "";

        public ApiService()
        {
            //dirMac = macAddress();
            dirMac = "00-E0-4C-36-17-61";
            nomTienda = GetNombreTienda();
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "SavinBio-23%");
            _httpClient.DefaultRequestHeaders.Add("Tkn", "SavinBio-23%");
            _httpClient.DefaultRequestHeaders.Add("DirMac", dirMac);

            //var client = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Get, "http://200.105.183.173:8080/savin-rest/ws/biometrico/listar-estructura-biometrico");
            //request.Headers.Add("Tkn", "SavinBio-23%");
            //request.Headers.Add("DirMac", "00-E0-4C-36-17-C0");
            //if (request != null)
            //{
            //    MessageBox.Show("Conexión exitosa");
            //}
        }

        public async Task<ModelJson> GetDataAsync()
        {
            var response = await _httpClient.GetAsync(_getApiLink);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ModelJson>(json);

                return data;
            }
            else
            {
                throw new HttpRequestException($"No se pudo conectar al servidor: {response.StatusCode}");
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


        public string GetNombreTienda()
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
            // Serializa el objeto RrhhAsistencia a JSON
            var regAsistencia = new
            {
                idTurno = asistencia.IdTurno,
                idPersonal = asistencia.IdPersonal,
                horaMarcado = asistencia.HoraMarcado,
                minutosAtraso = asistencia.MinutosAtraso,
                indTipoMovimiento = asistencia.IndTipoMovimiento
            };

            string jsonContent = JsonConvert.SerializeObject(regAsistencia);

            MessageBox.Show("json" + asistencia 
                                + "\nIdTurno: "+ asistencia.IdTurno
                                + "\nIdPersonal: " + asistencia.IdPersonal
                                + "\nHoraMarcado: " + asistencia.HoraMarcado
                                + "\nMinutosAtraso: " + asistencia.MinutosAtraso
                                + "\nIndTipoMovimiento: " + asistencia.IndTipoMovimiento
                            );

            MessageBox.Show("json" + regAsistencia);

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Cabeceras para la autenticación
            //_httpClient.DefaultRequestHeaders.Add("Tkn", "SavinBio-23%");

            // Realiza la solicitud POST a la API
            var response = await _httpClient.PostAsync(_postApiLink, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Error al registrar a la asistencia: " + response.StatusCode + "\nDetalles: "+ responseBody);
                //throw new HttpRequestException($"Error al registrar la asistencia: {response.StatusCode}");
            }

            return response;
        }

    }


}
