#nullable disable

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class DoctorPatient
    {
        [Key]
        [Column(Order = 0)]
        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        [Key]
        [Column(Order = 1)]
        public int PatientId { get; set; }

        public Patient Patient { get; set; }
    }
}
