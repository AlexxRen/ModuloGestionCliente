using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModuloGestionCliente.Models.DB;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de Cedula es Incorrecto")]
    public decimal? Cedula { get; set; }

    [Required]
    [MaxLength(25)]
    [DataType(DataType.Password)]
    public string Contrasea { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public decimal TotalCuenta { get; set; }

    public string Direccion { get; set; } = null!;

    public decimal Cuenta { get; set; }

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();


}
