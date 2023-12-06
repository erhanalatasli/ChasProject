#nullable disable

using Core.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Appointment : Record
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int HospitalId { get; set; }

        public Hospital Hospital { get; set; }

        public int ClinicId { get; set; }

        public Clinic Clinic { get; set; }

        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; }        

        public DateTime Date { get; set; }

        public bool IsActive { get; set; }


        [StringLength(200)]
        public string Explanation { get; set; }
    }
}
