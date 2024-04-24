using System;
using System.Collections.Generic;

namespace control_asistencia_savin.Models;

public partial class RrhhTurno
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public TimeSpan? HoraIngreso { get; set; }

    public TimeSpan? HoraSalida { get; set; }

    public virtual ICollection<AuxAsistencia> AuxAsistencia { get; set; } = new List<AuxAsistencia>();

    public virtual ICollection<RrhhAsistencia> RrhhAsistencia { get; set; } = new List<RrhhAsistencia>();

    public virtual ICollection<RrhhAsistenciaTemporal> RrhhAsistenciaTemporals { get; set; } = new List<RrhhAsistenciaTemporal>();

    public virtual ICollection<RrhhTurnoAsignado> RrhhTurnoAsignados { get; set; } = new List<RrhhTurnoAsignado>();
}
