using control_asistencia_savin.Models;
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
        [JsonProperty("rrhh_punto_asistencia")]
        public List<RrhhPuntoAsistencia> RrhhPuntoAsistencia { get; set; }
        [JsonProperty("rrhh_turno_asignado")]
        public List<RrhhTurnoAsignado> RrhhTurnoAsignado { get; set; }
        [JsonProperty("inv_almacen")]
        public List<InvAlmacen> InvAlmacen {  get; set; }

    }


}
