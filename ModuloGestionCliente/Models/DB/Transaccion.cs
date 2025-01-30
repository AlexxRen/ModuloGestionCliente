using System;
using System.Collections.Generic;

namespace ModuloGestionCliente.Models.DB;

public partial class Transaccion
{
    public int IdTransaccion { get; set; }

    public DateTime FechaTransaccion { get; set; }

    public decimal Monto { get; set; }

    public string Estado { get; set; } = null!;

    public int IdOrigenCli { get; set; }

    public int IdCliente { get; set; }

    public virtual ICollection<Auditoria> Auditoria { get; set; } = new List<Auditoria>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
