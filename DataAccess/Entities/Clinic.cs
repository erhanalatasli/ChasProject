#nullable disable

using Core.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Clinic : Record
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public List<Doctor> Doctors { get; set; }
    }
}
