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

    public virtual InvAlmacen? IdAlmacenNavigation { get; set; }

    public virtual RrhhPersonal? IdPersonalNavigation { get; set; }

    public virtual InvSucursal? IdSucursalNavigation { get; set; }

    public virtual ICollection<RrhhAsistencia> RrhhAsistencia { get; set; } = new List<RrhhAsistencia>();

    public virtual ICollection<RrhhTurnoAsignado> RrhhTurnoAsignados { get; set; } = new List<RrhhTurnoAsignado>();
}
