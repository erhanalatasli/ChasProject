#nullable disable

using Core.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class CityModel : Record
    {
        [Required]
        [StringLength(30)]

        public string Name { get; set; }

        public List<DistrictModel> Districts { get; set; }

        public List<HospitalModel> Hospitals { get; set; }
    }
}
