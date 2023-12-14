using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_asistencia_savin.Models
{
    internal class InvSucursal
    {
        public int Id { get; set; }
        public int IdPuntoAsistencia { get; set; }
        public int IdCiudad { get; set; }

        // Propiedades de navegación para las relaciones
        public RrhhPuntoAsistencia PuntoAsistencia { get; set; }
        public GenCiudad Ciudad { get; set; }

        // Nueva relación uno a muchos
        public ICollection<RrhhPuntoAsistencia> PuntosAsistencia { get; set; }

    }
}
