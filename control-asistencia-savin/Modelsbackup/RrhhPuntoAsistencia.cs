using System;
using System.Collections.Generic;

namespace control_asistencia_savin.Models;

public partial class RrhhPuntoAsistencia
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? Responsable { get; set; }

    public string? DireccionMac { get; set; }

    public int? IdSucursal { get; set; }

    public int? IdAlmacen { get; set; }

    public virtual InvAlmacen? IdAlmacenNavigation { get; set; }

    public virtual InvSucursal? IdSucursalNavigation { get; set; }

    public virtual ICollection<RrhhTurnoAsignado> RrhhTurnoAsignados { get; set; } = new List<RrhhTurnoAsignado>();
}
