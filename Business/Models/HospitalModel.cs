#nullable disable

using Core.Records.Bases;
using DataAccess.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class HospitalModel : Record
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

		[DisplayName("City")]
		public int CityId { get; set; }

        public City City { get; set; }

		[DisplayName("District")]
		public int DistrictId { get; set; }

        public District District { get; set; }


        [DisplayName("City")]
        public string CityDisplay { get; set; }

		[DisplayName("District")]
		public string DistrictDisplay { get; set; }

        public List<DoctorModel> Doctors { get; set; }
    }
}
