using System.ComponentModel.DataAnnotations;

namespace Reto.Models.ViewModels
{
	public class JoinViewModel
	{
		[Required]
		[Display(Name = "Reserva")]
		public int ReservaId { get; set; }

		[Required]
		[Display(Name = "Identificación")]
		public int ClienteId { get; set; }
		public string ClienteNombre { get; set; }
		public string Ciudad { get; set; }

		[Required]
		[Display(Name = "Fecha de Evento")]
		public DateTime Fecha { get; set; }

		[Required]
		[Display(Name = "Motivo")]
		public string MotivoId { get; set; }

		[Display(Name = "Cantidad de personas")]
		[Required]
		public int Cantidad { get; set; }

		[Display(Name = "Observaciones")]
		[Required]
		public string Observaciones { get; set; }

		[Display(Name = "Estado")]
		[UIHint("IsActive")]
		public bool Estado { get; set; }
	}
}
