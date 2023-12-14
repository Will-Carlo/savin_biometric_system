using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_asistencia_savin.Models
{
    internal class RrhhPuntoAsistencia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Responsable { get; set; }
        public string DireccionMac { get; set; }
        public int IdSucursal { get; set; }

        public InvSucursal Sucursal { get; set; }

        // Colección para relación
        public ICollection<RrhhTurnoAsignado> TurnosAsignados { get; set; }
    }
}
