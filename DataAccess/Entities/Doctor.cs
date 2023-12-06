#nullable disable

using Core.Records.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Doctor : Record
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]

        public string Surname { get; set; }

        public int GenderId { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int HospitalId { get; set; }

        public Hospital Hospital { get; set; }

        public int ClinicId { get; set; }

        public Clinic Clinic { get; set; }

        public List<DoctorPatient> DoctorPatients { get; set; }

        public List<Appointment> Appointments { get; set; }


		#region Binary Data
		[Column(TypeName = "image")]
		public byte[] Image { get; set; }

		[StringLength(5)]
		public string ImageExtension { get; set; }
		#endregion
	}
}
