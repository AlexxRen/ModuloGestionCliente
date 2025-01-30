using System.ComponentModel.DataAnnotations;

namespace ModuloGestionCliente.Models.Buisnes
{
    public class LoginCliente
    {
        public required string Correo { get; set; }
        public required string PSW { get; set; }

    }
}
