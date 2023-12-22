using System;
using System.Collections.Generic;

namespace control_asistencia_savin.Models;

public partial class RrhhPersonal
{
    public int Id { get; set; }

    public int IdCiudad { get; set; }

    public string? Paterno { get; set; }

    public string? Materno { get; set; }

    public string? Nombre { get; set; }

    public byte[]? HuellaIndDer { get; set; }

    public byte[]? HuellaIndIzq { get; set; }

    public byte[]? HuellaPulgDer { get; set; }

    public byte[]? HuellaPulgIzq { get; set; }

    public virtual GenCiudad IdCiudadNavigation { get; set; } = null!;

    public virtual ICollection<RrhhAsistencia> RrhhAsistencia { get; set; } = new List<RrhhAsistencia>();

    public virtual ICollection<RrhhTurnoAsignado> RrhhTurnoAsignados { get; set; } = new List<RrhhTurnoAsignado>();
}
