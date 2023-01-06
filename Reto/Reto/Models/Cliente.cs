using System;
using System.Collections.Generic;

namespace Reto.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Teléfono { get; set; }

    public string Correo { get; set; } = null!;

    public int Edad { get; set; }

    public int DepartamentoId { get; set; }

    public int CiudadId { get; set; }

    public virtual Ciudad Ciudad { get; set; } = null!;

    public virtual Departamento Departamento { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; } = new List<Reserva>();
}
