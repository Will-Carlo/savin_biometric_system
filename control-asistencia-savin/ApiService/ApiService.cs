using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_asistencia_savin.ApiService
{
    public class ApiService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public ApiService()
        {
            _httpClient.DefaultRequestHeaders.Add("Tkn", "SavinBio-23%"); 
        }

        public async Task<ModelJson> GetDataAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ModelJson>(json);

                return data;
            }
            else
            {
                throw new HttpRequestException($"Error: {response.StatusCode}");
            }
        }
    }


}
