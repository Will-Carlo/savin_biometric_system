using System;
using System.Collections.Generic;

namespace control_asistencia_savin.Models;

public partial class RrhhTurnoAsignado
{
    public int Id { get; set; }

    public int IdTurno { get; set; }

    public int IdPersonal { get; set; }

    public int? IndTipoMarcado { get; set; }

    public int? IndMarcadoFijoVariable { get; set; }

    public int? IdPuntoAsistencia { get; set; }

    public string? Codigo { get; set; }

    public virtual RrhhPersonal IdPersonalNavigation { get; set; } = null!;

    public virtual RrhhPuntoAsistencia? IdPuntoAsistenciaNavigation { get; set; }

    public virtual RrhhTurno IdTurnoNavigation { get; set; } = null!;
}
