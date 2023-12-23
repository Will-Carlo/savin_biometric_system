﻿using System;
using System.Collections.Generic;

namespace control_asistencia_savin.Models;

public partial class RrhhTurno
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? HoraIngreso { get; set; }

    public string? HoraSalida { get; set; }

    public virtual ICollection<RrhhAsistencia> RrhhAsistencia { get; set; } = new List<RrhhAsistencia>();

    public virtual ICollection<RrhhTurnoAsignado> RrhhTurnoAsignados { get; set; } = new List<RrhhTurnoAsignado>();
}
