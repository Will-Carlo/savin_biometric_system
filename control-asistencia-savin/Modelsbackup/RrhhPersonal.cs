using System;
using System.Collections.Generic;

namespace control_asistencia_savin.Models;

public partial class RrhhPersonal
{
    public int Id { get; set; }

    public int IdCiudad { get; set; }

    public string? Paterno { get; set; }

    public string? Materno { get; set; }

    public string? Nombres { get; set; }

    public byte[]? IndiceDerecho { get; set; }

    public byte[]? IndiceIzquierdo { get; set; }

    public byte[]? PulgarDerecho { get; set; }

    public byte[]? PulgarIzquierdo { get; set; }

    public virtual ICollection<AuxAsistencia> AuxAsistencia { get; set; } = new List<AuxAsistencia>();

    public virtual GenCiudad IdCiudadNavigation { get; set; } = null!;

    public virtual ICollection<RrhhAsistencia> RrhhAsistencia { get; set; } = new List<RrhhAsistencia>();

    public virtual ICollection<RrhhPuntoAsistencia> RrhhPuntoAsistencia { get; set; } = new List<RrhhPuntoAsistencia>();

    public virtual ICollection<RrhhTurnoAsignado> RrhhTurnoAsignados { get; set; } = new List<RrhhTurnoAsignado>();
}
