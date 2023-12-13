using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_asistencia_savin.Models
{
    internal class RrhhAsistencia
    {
        public int Id { get; set; }
        public int IdTurno { get; set; }
        public int IdPersonal { get; set; }
        public string HoraMarcado { get; set; }
        public int MinutosAtraso { get; set; }
        public int IndTipoMovimiento { get; set; }

        // Relaciones de claves foráneas
        public RrhhTurno Turno { get; set; }
        public RrhhPersonal Personal { get; set; }
    }
}
