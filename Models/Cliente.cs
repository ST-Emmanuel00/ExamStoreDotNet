using System;
using System.Collections.Generic;

namespace Tienda.Models;

public partial class Cliente
{
    public Cliente()
    {
        Venta = new HashSet<Venta>();

    }
    public int ClienteId { get; set; }

    public string? Cedula { get; set; }

    public string? NombreCliente { get; set; }

    public string? ApellidoCliente { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } /*= new List<Venta>();*/
}
