#nullable disable

using Core.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class RoleModel : Record
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public List<UserModel> Users { get; set; }
    }
}
