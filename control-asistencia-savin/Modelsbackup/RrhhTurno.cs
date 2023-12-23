using System;
using System.Collections.Generic;

namespace control_asistencia_savin.Models2;

public partial class RrhhTurno
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public TimeSpan? HoraIngreso { get; set; }

    public TimeSpan? HoraSalida { get; set; }

    public virtual ICollection<RrhhAsistencia> RrhhAsistencia { get; set; } = new List<RrhhAsistencia>();

    public virtual ICollection<RrhhTurnoAsignado> RrhhTurnoAsignados { get; set; } = new List<RrhhTurnoAsignado>();
}
