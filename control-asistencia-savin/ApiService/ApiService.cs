﻿using control_asistencia_savin.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace control_asistencia_savin.ApiService
{
    public class ApiService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private Credenciales _credenciales;
        public string _dirMac { get; set; }
        public bool _esProduction = true;
        public ApiService()
        {
            _credenciales = new Credenciales(_esProduction);
            _dirMac = _credenciales._PssdMac;
            this.CleanHeaders();
        }
        public async Task<ModelJson?> GetDataAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_credenciales._getApiLink);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("Tu dirección MAC no está registrada.\nDir mac: " + _credenciales._PssdMac + "\nCerrando la aplicación.");
                    Environment.Exit(0);
                    return null;
                }
                else if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(json))
                    {
                        var data = JsonConvert.DeserializeObject<ModelJson>(json);
                        return data;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    throw new HttpRequestException($"No se pudo conectar al servidor: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Captura la excepción y maneja el error aquí
                MessageBox.Show("Error al conectar al servidor: " + ex.Message+ "\nContacte con el administrador.");
                Environment.Exit(0);
                return null;
            }
        }
        public List<AuxAsistencia> GetDataAsistencia(int idPersonal)
        {
            this.CleanHeaders();
            _httpClient.DefaultRequestHeaders.Add("IdPersonal", idPersonal.ToString());
            var response = _httpClient.GetAsync(_credenciales._getAsistenciaLink).Result;

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
            var response = await _httpClient.PostAsync(_credenciales._postApiLink, content);
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
                var response = await _httpClient.PutAsync(_credenciales._putSalidasLink, null);

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
            _httpClient.DefaultRequestHeaders.Add(_credenciales._token, _credenciales._PssdToken);
            _httpClient.DefaultRequestHeaders.Add(_credenciales._mac, _credenciales._PssdMac);
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
