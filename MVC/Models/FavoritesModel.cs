#nullable disable

using System.ComponentModel;

namespace MVC.Models
{
    public class FavoritesModel
    {
        public int HospitalId { get; set; }

        public int UserId { get; set; }

        [DisplayName("Hospital Name")]
        public string HospitalName { get; set; }

        public FavoritesModel(int hospitalId, int userId, string hospitalName)
        {
            HospitalId = hospitalId;
            UserId = userId;
            HospitalName = hospitalName;
        }
    }
}
