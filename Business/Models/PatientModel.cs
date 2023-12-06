#nullable disable

using Core.Records.Bases;
using DataAccess.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Business.Models
{
    public class PatientModel : Record
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string Surname { get; set; }


        [DisplayName("Gender")]
        public int GenderId { get; set; }

        public Gender Gender { get; set; }


        [DisplayName("Date of Birth")]
        public DateTime DateOfBirth { get; set; }


        [DisplayName("City")]
        public int CityId { get; set; }

        public City City { get; set; }


        [StringLength(200)]
        public string Complaint { get; set; }


        [DisplayName("Is Issurance?")]
        public bool IsIssurance { get; set; }


        [DisplayName("Doctors")]
        public List<int> DoctorIds { get; set; }

        public List<DoctorModel> Doctors { get; set; }


        [DisplayName("Doctor")]
        public int DoctorId { get; set; }

        [DisplayName("Gender")]
        public string GenderDisplay { get; set; }

        [DisplayName("City")]
        public string CityDisplay { get; set; }

        public string NameSurname { get; set; }

		[DisplayName("Clinic")]
		public int ClinicId { get; set; }


        #region Binary Data
        [Column(TypeName = "image")]
        [JsonIgnore]
        public byte[] Image { get; set; }

        [StringLength(5)]
        [JsonIgnore]
        public string ImageExtension { get; set; }
        #endregion


        [DisplayName("Report")]
        public string ImgSrcDisplay { get; set; }
    }
}
