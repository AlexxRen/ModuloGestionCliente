using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModuloGestionCliente.Models.DB;

public partial class Transaccion
{
    public int IdTransaccion { get; set; }

    public DateTime FechaTransaccion { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Monto mínimo: 0.01")]
    public decimal Monto { get; set; }

    public string Estado { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessage = "Cliente inválido")]
    public int IdCliente { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Origen inválido")]
    public int IdOrigenCli { get; set; }

    public virtual ICollection<Auditoria> Auditoria { get; set; } = new List<Auditoria>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
