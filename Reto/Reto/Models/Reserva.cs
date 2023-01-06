using System;
using System.Collections.Generic;

namespace Reto.Models;

public partial class Reserva
{
    public int ReservaId { get; set; }

    public int ClienteId { get; set; }

    public DateTime Fecha { get; set; }

    public TimeSpan Hora { get; set; }

    public int Cantidad { get; set; }

    public int MotivoId { get; set; }

    public string? Observaciones { get; set; }

    public bool? Estado { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Motivo Motivo { get; set; } = null!;
}
