using control_asistencia_savin.Models2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_asistencia_savin.ApiService
{
    public class ModelJson
    {
        [JsonProperty("rrhh_turno")]
        public List<RrhhTurno> RrhhTurno { get; set; }
        [JsonProperty("gen_ciudad")]
        public List<GenCiudad> GenCiudad { get; set; }
        [JsonProperty("rrhh_personal")]
        public List<RrhhPersonal> RrhhPersonal { get; set;}
        [JsonProperty("inv_sucursal")]
        public List<InvSucursal> InvSucursal { get; set; }
        [JsonProperty("rrhh_feriado")]
        public List<RrhhFeriado> RrhhFeriado { get; set; }
        [JsonProperty("rrhh_asistencia")]
        public List<RrhhAsistencia> RrhhAsistencia { get; set; }
        [JsonProperty("rrhh_puntos_asistencia")]
        public List<RrhhPuntoAsistencium> RrhhPuntoAsistencia { get; set; }
        [JsonProperty("rrhh_turno_asignado")]
        public List<RrhhTurnoAsignado> RrhhTurnoAsignado { get; set; }

        public ModelJson()
        {
            RrhhTurno = new List<RrhhTurno>();
            GenCiudad = new List<GenCiudad>();
            RrhhPersonal = new List<RrhhPersonal>();
            InvSucursal = new List<InvSucursal>();
            RrhhFeriado = new List<RrhhFeriado>();
            RrhhAsistencia = new List<RrhhAsistencia>();
            RrhhPuntoAsistencia = new List<RrhhPuntoAsistencium>();
            RrhhTurnoAsignado = new List<RrhhTurnoAsignado>();
        }

        public static ModelJson FromJson(string jsonString)
        {
            return JsonConvert.DeserializeObject<ModelJson>(jsonString);
        }
    }


}
