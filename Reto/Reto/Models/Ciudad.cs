using System;
using System.Collections.Generic;

namespace Reto.Models;

public partial class Ciudad
{
    public int CiudadId { get; set; }

    public string Nombre { get; set; } = null!;

    public int DepartamentoId { get; set; }

    public virtual ICollection<Cliente> Clientes { get; } = new List<Cliente>();

    public virtual Departamento Departamento { get; set; } = null!;
}
