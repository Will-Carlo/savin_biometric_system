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
        private String dirMac = "";
        public String nomTienda = "";

        public ApiService()
        {
            dirMac = macAddress();
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

    }


}
