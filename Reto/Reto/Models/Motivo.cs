using System;
using System.Collections.Generic;

namespace Reto.Models;

public partial class Motivo
{
    public int MotivoId { get; set; }

    public string? Tipo { get; set; }

    public virtual ICollection<Reserva> Reservas { get; } = new List<Reserva>();
}
