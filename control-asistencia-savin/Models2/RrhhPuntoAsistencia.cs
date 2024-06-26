﻿using System;
using System.Collections.Generic;

namespace control_asistencia_savin.Models;

public partial class RrhhPuntoAsistencia
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public int? MinutosTolerancia { get; set; }

    public int? IdPersonal { get; set; }

    public string? DireccionMac { get; set; }

    public int? IdSucursal { get; set; }

    public int? IdAlmacen { get; set; }

    public int? PermiteObservacion { get; set; }

    public virtual ICollection<RrhhAsistencia> RrhhAsistencia { get; set; } = new List<RrhhAsistencia>();

    public virtual ICollection<RrhhAsistenciaTemporal> RrhhAsistenciaTemporals { get; set; } = new List<RrhhAsistenciaTemporal>();

    public virtual ICollection<RrhhTurnoAsignado> RrhhTurnoAsignados { get; set; } = new List<RrhhTurnoAsignado>();
}
