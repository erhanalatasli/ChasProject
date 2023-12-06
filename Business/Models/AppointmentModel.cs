#nullable disable

using Core.Records.Bases;
using DataAccess.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class AppointmentModel : Record
    {
        [DisplayName("Applicant")]
        public int UserId { get; set; }

        public User User { get; set; }

        [DisplayName("Hospital")]
        public int HospitalId { get; set; }

        [DisplayName("Clinic")]
        public int ClinicId { get; set; }

        [DisplayName("Doctor")]
        public int DoctorId { get; set; }        

        public DateTime Date { get; set; }

        public bool IsActive { get; set; }


        [StringLength(200)]
        public string Explanation { get; set; }


        [DisplayName("Patient")]
        public string UserDisplay { get; set; }

        [DisplayName("Hospital")]
        public string HospitalDisplay { get; set; }

        [DisplayName("Doctor")]
        public string DoctorDisplay { get; set; }

        [DisplayName("Clinic")]
        public string ClinicDisplay { get; set; }
    }
}
