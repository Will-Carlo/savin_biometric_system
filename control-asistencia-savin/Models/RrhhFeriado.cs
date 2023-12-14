using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_asistencia_savin.Models
{
    internal class RrhhFeriado
    {
        public int Id { get; set; }
        public int IdCiudad { get; set; }
        public string Fecha { get; set; }
        public int IndTipoFeriado { get; set; }

        // Propiedad de navegación para la relación con GenCiudad
        public GenCiudad Ciudad { get; set; }
    }
}
