#nullable disable

using System.ComponentModel;

namespace Business.Models.Report
{
    public class FilterModel
    {
        public string Patient { get; set; }

        public string Hospital { get; set; }

        public string Clinic { get; set; }

        public string Doctor { get; set; }

        [DisplayName("Appointment Date")]
        public DateTime? DateBegin { get; set; }

        public DateTime? DateEnd { get; set; }

        public bool? IsActive { get; set; }
    }
}
