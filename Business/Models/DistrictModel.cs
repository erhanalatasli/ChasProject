#nullable disable

using Core.Records.Bases;
using DataAccess.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class DistrictModel : Record
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [DisplayName("City")]
        public int CityId { get; set; }

        public City City { get; set; }

        [DisplayName("City")]
        public string CityDisplay { get; set; }

        public List<HospitalModel> Hospitals { get; set; }
    }
}
