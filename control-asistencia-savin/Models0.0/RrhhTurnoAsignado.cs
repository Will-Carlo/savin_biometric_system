using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_asistencia_savin.Models
{
    internal class RrhhTurnoAsignado
    {
        public int Id { get; set; }
        public int IdTurno { get; set; }
        public int IdPersonal { get; set; }
        public int IndTipoMarcado { get; set; }
        public int IndMarcadoFijoVariable { get; set; }
        public int IdPuntoAsistencia { get; set; }

        // Relaciones de claves foráneas
        public RrhhTurno Turno { get; set; }
        public RrhhPersonal Personal { get; set; }
        public RrhhPuntoAsistencia PuntoAsistencia { get; set; }
    }
}
