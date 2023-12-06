#nullable disable

using Core.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class GenderModel : Record
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public List<PatientModel> Patients { get; set; }

        public List<DoctorModel> Doctors { get; set; }
    }
}
