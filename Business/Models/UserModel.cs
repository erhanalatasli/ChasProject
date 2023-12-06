#nullable disable

using Core.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class UserModel : Record
    {

		[Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

		[DisplayName("Role")]
		public int RoleId { get; set; }

        [DisplayName("Role")]
        public string RoleDisplay { get; set; }

        public RoleModel Role { get; set; }

        public string NameSurname { get; set; }


        public UserModel(string name, string surname, string userName, string password, bool isActive, int roleId, int id)
        {
            Name = name;
            Surname = surname;
            UserName = userName;
            Password = password;
            IsActive = isActive;
            RoleId = roleId;
            Id = id;

            Role = new RoleModel();
        }

        public UserModel()
        {
            //Role = new RoleModel();
        }
    }
}
