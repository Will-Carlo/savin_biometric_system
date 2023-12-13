using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_asistencia_savin.Models
{
    internal class RrhhPersonal
    {        public int Id { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Nombre { get; set; }
        public byte[] Huella { get; set; }

        // Colecciones para relaciones
        public ICollection<RrhhAsistencia> Asistencias { get; set; }
        public ICollection<RrhhTurnoAsignado> TurnosAsignados { get; set; }
    }
}
