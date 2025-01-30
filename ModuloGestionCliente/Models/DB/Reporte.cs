using System;
using System.Collections.Generic;

namespace ModuloGestionCliente.Models.DB;

public partial class Reporte
{
    public int IdReporte { get; set; }

    public DateTime FechaReporte { get; set; }

    public string Contenido { get; set; } = null!;

    public int IdUsuario { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
