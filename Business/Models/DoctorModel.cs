#nullable disable

using Core.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Business.Models
{
    public class DoctorModel : Record
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]

        public string Surname { get; set; }

        [DisplayName("Gender")]
        public int GenderId { get; set; }

        [DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Hospital")]
        public int HospitalId { get; set; }

        [DisplayName("Clinic")]
        public int ClinicId { get; set; }

        [DisplayName("Clinic")]
        public string ClinicDisplay { get; set; }

        [DisplayName("Gender")]
        public string GenderDisplay { get; set; }


        [DisplayName("Hospital")]
        public string HospitalDisplay { get; set; }

        [DisplayName("Name Surname")]
        public string NameSurname { get; set; }

		[DisplayName("Name Surname")]
		public string NameSurnameAppointment { get; set; }

		public List<PatientModel> Patients { get; set; }


		#region Binary Data
		[Column(TypeName = "image")]
		[JsonIgnore]
		public byte[] Image { get; set; }

		[StringLength(5)]
		[JsonIgnore]
		public string ImageExtension { get; set; }
		#endregion


		[DisplayName("Certificate")]
		public string ImgSrcDisplay { get; set; }
	}
}
