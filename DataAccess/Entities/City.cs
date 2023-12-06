#nullable disable

using Core.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class City : Record
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public List<Hospital> Hospitals { get; set; }

        public List<District> Districts { get; set; }
    }
}
