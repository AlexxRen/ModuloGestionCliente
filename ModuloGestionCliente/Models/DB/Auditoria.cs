using System;
using System.Collections.Generic;

namespace ModuloGestionCliente.Models.DB;

public partial class Auditoria
{
    public int IdAditoria { get; set; }

    public string DetalleAuditoria { get; set; } = null!;

    public DateTime FechaAuditoria { get; set; }

    public string Ubicacion { get; set; } = null!;

    public int IdTransaccion { get; set; }

    public virtual Transaccion IdTransaccionNavigation { get; set; } = null!;
}
