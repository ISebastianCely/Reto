using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reto.Models.ViewModels
{
	public class ClienteViewModel
	{
		[Range(1, 9999999999)]
		[DataType(DataType.Currency)]
		[Column(TypeName = "int")]
		[Required]
		[Display(Name = "Identificación")]
		public int ClienteId { get; set; }

		[RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
		[Required]
		[Display(Name = "Nombre")]
		public string Nombre { get; set; }

		[Required]
		[Display(Name = "Teléfono")]
		public string Telefono { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Correo")]
		public string Correo { get; set; }

		[Range(18, 100)]
		[DataType(DataType.Currency)]
		[Column(TypeName = "int")]
		[Display(Name = "Edad")]
		[Required]
		public int Edad { get; set; }

		[Display(Name = "Departamento")]
		[Required]
		public int DepartamentoId { get; set; }

		[Display(Name = "Ciudad")]
		[Required]
		public int CiudadId { get; set; }	
	}
}
