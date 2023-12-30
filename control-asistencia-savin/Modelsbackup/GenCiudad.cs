using System;
using System.Collections.Generic;

namespace control_asistencia_savin.Models;

public partial class GenCiudad
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<InvSucursal> InvSucursals { get; set; } = new List<InvSucursal>();

    public virtual ICollection<RrhhFeriado> RrhhFeriados { get; set; } = new List<RrhhFeriado>();

    public virtual ICollection<RrhhPersonal> RrhhPersonals { get; set; } = new List<RrhhPersonal>();
}
