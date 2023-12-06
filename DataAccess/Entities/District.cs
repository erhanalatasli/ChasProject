#nullable disable

using Core.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class District : Record
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public List<Hospital> Hospitals { get; set; }
    }
}
