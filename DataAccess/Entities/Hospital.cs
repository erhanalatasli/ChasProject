#nullable disable

using Core.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Hospital : Record
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public int DistrictId { get; set; }

        public District District { get; set; }

        public List<Doctor> Doctors { get; set; }
    }
}
