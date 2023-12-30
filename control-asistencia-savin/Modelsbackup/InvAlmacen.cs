using System;
using System.Collections.Generic;

namespace control_asistencia_savin.Models;

public partial class InvAlmacen
{
    public int Id { get; set; }

    public virtual ICollection<RrhhPuntoAsistencia> RrhhPuntoAsistencia { get; set; } = new List<RrhhPuntoAsistencia>();
}
