using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_asistencia_savin.Models
{
    internal class GenCiudad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Colecciones para relaciones uno a muchos


        public ICollection<RrhhPersonal> Personal { get; set; }
        public ICollection<RrhhFeriado> Feriados { get; set; }
        public ICollection<InvSucursal> Sucursales { get; set; }
    }
}
