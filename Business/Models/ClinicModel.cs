#nullable disable

using Core.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class ClinicModel : Record
	{
		[Required]
		[StringLength(200)]
		public string Name { get; set; }

		public List<DoctorModel> Doctors { get; set; }
	}
}
