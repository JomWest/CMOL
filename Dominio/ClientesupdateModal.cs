using System;
using System.Collections.Generic;
namespace Dominio
{
    public class ClientesupdateModal
    {
        public int IdClientes { get; set; }
         public string Cedula { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }
    }
}