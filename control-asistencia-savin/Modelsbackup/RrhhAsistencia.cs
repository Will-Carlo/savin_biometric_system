﻿using System;
using System.Collections.Generic;

namespace control_asistencia_savin.Models2;

public partial class RrhhAsistencia
{
    public int Id { get; set; }

    public int IdTurno { get; set; }

    public int IdPersonal { get; set; }

    public string? HoraMarcado { get; set; }

    public int? MinutosAtraso { get; set; }

    public int? IndTipoMovimiento { get; set; }

    public virtual RrhhPersonal IdPersonalNavigation { get; set; } = null!;

    public virtual RrhhTurno IdTurnoNavigation { get; set; } = null!;
}