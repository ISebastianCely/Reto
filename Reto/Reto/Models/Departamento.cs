using System;
using System.Collections.Generic;

namespace Reto.Models;

public partial class Departamento
{
    public int DepartamentoId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Ciudad> Ciudads { get; } = new List<Ciudad>();

    public virtual ICollection<Cliente> Clientes { get; } = new List<Cliente>();
}
