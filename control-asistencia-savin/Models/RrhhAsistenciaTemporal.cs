using System;
using System.Collections.Generic;

namespace control_asistencia_savin.Models;

public partial class RrhhAsistenciaTemporal
{
    public int Id { get; set; }

    public int IdTurno { get; set; }

    public int IdPersonal { get; set; }

    public string? HoraMarcado { get; set; }

    public int? MinutosAtraso { get; set; }

    public int? IndTipoMovimiento { get; set; }

    public int IdPuntoAsistencia { get; set; }

    public virtual RrhhPersonal IdPersonalNavigation { get; set; } = null!;

    public virtual RrhhPuntoAsistencia IdPuntoAsistenciaNavigation { get; set; } = null!;

    public virtual RrhhTurno IdTurnoNavigation { get; set; } = null!;
}
