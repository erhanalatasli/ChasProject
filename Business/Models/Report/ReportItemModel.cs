#nullable disable

using DataAccess.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models.Report
{
	public class ReportItemModel
	{
		[DisplayName("Patient")]
		public int UserId { get; set; }
        public User User { get; set; }

        [DisplayName("Hospital")]
		public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }

        [DisplayName("Clinic")]
		public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }

        [DisplayName("Doctor")]
		public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public DateTime Date { get; set; }

		public string IsActive { get; set; }

		[StringLength(200)]
		public string Explanation { get; set; }

        [DisplayName("Patient")]
        public string UserDisplay { get; set; }

        [DisplayName("Hospital")]
        public string HospitalDisplay { get; set; }

        [DisplayName("Clinic")]
        public string ClinicDisplay { get; set; }

        [DisplayName("Doctor")]
        public string DoctorDisplay { get; set; }
    }
}
