using System;
using System.Collections.Generic;

namespace control_asistencia_savin.Models;

public partial class InvSucursal
{
    public int Id { get; set; }

    public int? IdCiudad { get; set; }

    public virtual GenCiudad? IdCiudadNavigation { get; set; }
}
