using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_asistencia_savin.Models
{
    internal class RrhhTurno
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string HoraIngreso { get; set; } // Formato 'HH:MM:SS' o 'HH:MM'
        public string HoraSalida{ get; set; } // Formato 'HH:MM:SS' o 'HH:MM'

        // Colecciones para relaciones uno a muchos
        public ICollection<RrhhAsistencia> Asistencias { get; set; }
        public ICollection<RrhhTurnoAsignado> TurnosAsignados { get; set; }

    }
}
