using System;
using System.Collections.Generic;

namespace ModuloGestionCliente.Models.DB;

public partial class Log
{
    public int IdLog { get; set; }

    public string Evento { get; set; } = null!;

    public string Detalle { get; set; } = null!;

    public int IdUsuario { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
