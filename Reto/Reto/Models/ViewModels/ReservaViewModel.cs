using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reto.Models.ViewModels
{
    public class ReservaViewModel
    {
        [Required]
        [Display(Name = "Reserva")]
        public int ReservaId { get; set; }

        [Required]
        [Display(Name = "Identificación")]
        public int ClienteId { get; set; }

        [Required]
        [Display(Name = "Fecha de Evento")]
        public DateTime Fecha { get; set; }

        [Required]
        [Display(Name = "Motivo")]
        public int MotivoId { get; set; }

		[Range(1, 100)]
		[DataType(DataType.Currency)]
		[Column(TypeName = "int")]
		[Display(Name = "Cantidad de personas")]
        [Required]
        public int Cantidad { get; set; }

		[Required]
		[Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        [Display(Name = "Estado")]
		[UIHint("IsActive")]
		public bool Estado { get; set; }
	}
	
}
