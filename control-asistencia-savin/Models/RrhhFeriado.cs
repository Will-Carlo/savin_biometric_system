using System;
using System.Collections.Generic;

namespace control_asistencia_savin.Models;

public partial class RrhhFeriado
{
    public int Id { get; set; }

    public int IdCiudad { get; set; }

    public string? Fecha { get; set; }

    public int? IndTipoFeriado { get; set; }

    public virtual GenCiudad IdCiudadNavigation { get; set; } = null!;
}
