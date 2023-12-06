#nullable disable

using Core.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Gender : Record
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public List<Patient> Patients { get; set; }

        public List<Doctor> Doctors { get; set; }
    }
}
