using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_asistencia_savin.ApiService
{
    public class RegistroAsistencia
    {
        public string Fecha { get; set; }
        public TimeSpan? EntradaManana { get; set; }
        public TimeSpan? SalidaManana { get; set; }
        public int? MinutosAtrasadoManana { get; set; }
        public TimeSpan? EntradaTarde { get; set; }
        public TimeSpan? SalidaTarde { get; set; }
        public int? MinutosAtrasadoTarde { get; set; }
        public int? TotalAtrasos => (MinutosAtrasadoManana ?? 0) + (MinutosAtrasadoTarde ?? 0);
    }

}
