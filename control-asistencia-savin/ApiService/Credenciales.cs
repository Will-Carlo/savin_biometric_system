using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace control_asistencia_savin.ApiService
{
    public class Credenciales
    {
        //************************************************************************
        // LINKS DE PRODUCCIÓN YB DESARROLLO
        //************************************************************************
        public string _getApiLink { get; set;}
        public string _postApiLink { get; set; }
        public string _getAsistenciaLink { get; set; }
        public string _putSalidasLink { get; set; }
        //************************************************************************
        // HEADERS TOKEN
        //************************************************************************
        public string _token { get; set; }
        public string _PssdToken { get; set; }
        //************************************************************************
        // HEADERS MAC
        //************************************************************************
        public string _mac { get; set; }
        public string _PssdMac { get; set; }
        //************************************************************************
        // ¿ES PRODUCCIÓN O DESARROLLO?
        //************************************************************************
        //public bool _esProduction { get; set; }


        //************************************************************************
        public Credenciales(bool esProduction)
        {
            this.EsProduction(esProduction);
            //_esProduction = esProduction;
        }

        private void EsProduction(bool Savin)
        {
            this.Headers();
            if (Savin)
            {
                this.Production();
            }
            else
            {
                this.Development();
            }
        }

        private void Development()
        {
            this._getApiLink = "http://200.105.183.173:8080/savin-rest/ws/biometrico/listar-estructura-biometrico";
            this._postApiLink = "http://200.105.183.173:8080/savin-rest/ws/biometrico/registrar-asistencia";
            this._getAsistenciaLink = "http://200.105.183.173:8080/savin-rest/ws/biometrico/listar-asistencia-personal";
            this._putSalidasLink = "http://200.105.183.173:8080/savin-rest/ws/biometrico/modificar-minutos-atraso";
        }
        private void Production()
        {
            this._getApiLink = "http://54.177.210.26:8080/savin-rest/ws/biometrico/listar-estructura-biometrico";
            this._postApiLink = "http://54.177.210.26:8080/savin-rest/ws/biometrico/registrar-asistencia";
            this._getAsistenciaLink = "http://54.177.210.26:8080/savin-rest/ws/biometrico/listar-asistencia-personal";
            this._putSalidasLink = "http://54.177.210.26:8080/savin-rest/ws/biometrico/modificar-minutos-atraso";
        }
        private void Headers()
        {
            this._token = "Tkn";
            this._PssdToken = "SavinBio-23%";

            this._mac = "DirMac";
            this._PssdMac = MacList(1);
        }

        private string MacList(int n)
        {
            switch (n)
            {
                case 0:
                    return this.macAddress().ToString();
                case 1:
                    // GOITIA
                    return "14-B3-1F-11-AB-CF";
                case 2:
                    // OFICINA LOAYZA
                    return "14-B3-1F-0F-D3-AF";
                case 3:
                    // TIENDA LOAYZA
                    return "34-17-EB-9D-8F-97";
                case 4:
                    // TIENDA COCHA
                    return "1C-BF-CE-65-9D-59";
                case 5:
                    // TIENDA SATÉLITE
                    return "64-00-6A-86-65-DF";
                case 6:
                    // TIENDA CEIBO
                    return "98-90-96-D5-EC-45";
                case 7:
                    // TIENDA OBRAJES
                    return "14-B3-1F-0E-FE-82";
                case 8:
                    // ALMACÉN CENTRAL
                    return "1C-BF-CE-62-20-A8";
                case 9:
                    // SANTA CRUZ
                    return "90-B1-1C-6C-47-28";
                default:
                    return this.macAddress();
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

    }
}
