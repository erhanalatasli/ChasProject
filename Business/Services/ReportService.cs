using Business.Models.Report;
using Core.Repositories.EntityFramework.Bases;
using DataAccess.Entities;

namespace Business.Services
{
	public interface IReportService
	{
		List<ReportItemModel> GetList(FilterModel filter = null);
	}
	public class ReportService : IReportService
	{
		private readonly RepoBase<Appointment> _appointmentRepo;
		private readonly RepoBase<User> _userRepo;
		private readonly RepoBase<Hospital> _hospitalRepo;
		private readonly RepoBase<Clinic> _clinicRepo;
		private readonly RepoBase<Doctor> _doctorRepo;

		public ReportService(RepoBase<Appointment> appointmentRepo, RepoBase<User> userRepo, RepoBase<Hospital> hospitalRepo, RepoBase<Clinic> clinicRepo, RepoBase<Doctor> doctorRepo)
		{
			_appointmentRepo = appointmentRepo;
			_userRepo = userRepo;
			_hospitalRepo = hospitalRepo;
			_clinicRepo = clinicRepo;
			_doctorRepo = doctorRepo;
		}

        public IQueryable<ReportItemModel> GetQuery()
		{
			var appointmentQuery = _appointmentRepo.Query();
			var userQuery = _userRepo.Query();
			var hospitalQuery = _hospitalRepo.Query();
			var clinicQuery = _clinicRepo.Query();
			var doctorQuery = _doctorRepo.Query();

			IQueryable<ReportItemModel> query;

			query = from a in appointmentQuery
					join u in userQuery
					on a.UserId equals u.Id
					join h in hospitalQuery
					on a.HospitalId equals h.Id
					join c in clinicQuery
					on a.ClinicId equals c.Id
					join d in doctorQuery
					on a.DoctorId equals d.Id

					select new ReportItemModel()
					{
						UserId = u.Id,
						HospitalId = h.Id,
						ClinicId = c.Id,
						DoctorId = d.Id,
						Date = a.Date,
						IsActive = a.IsActive ? "Yes" : "No",
						Explanation = a.Explanation,

						UserDisplay = a.User.Name + " " + a.User.Surname,
						HospitalDisplay = a.Hospital.Name,
						ClinicDisplay = a.Clinic.Name,
						DoctorDisplay = a.Doctor.Name + " " + a.Doctor.Surname
					};

			return query;
		}

        public List<ReportItemModel> GetList(FilterModel filter = null)
        {
			var query = GetQuery();

            #region Sıralama
            query = query.OrderBy(q => q.UserDisplay);
            #endregion

            #region Filtreleme
			if (filter is not null)
			{
				if (!string.IsNullOrWhiteSpace(filter.Patient))
					query = query.Where(q => q.UserDisplay.ToUpper().Contains(filter.Patient.ToUpper().Trim()));

                if (!string.IsNullOrWhiteSpace(filter.Hospital))
                    query = query.Where(q => q.HospitalDisplay.ToUpper().Contains(filter.Hospital.ToUpper().Trim()));

                if (!string.IsNullOrWhiteSpace(filter.Clinic))
                    query = query.Where(q => q.ClinicDisplay.ToUpper().Contains(filter.Clinic.ToUpper().Trim()));

                if (!string.IsNullOrWhiteSpace(filter.Doctor))
                    query = query.Where(q => q.DoctorDisplay.ToUpper().Contains(filter.Doctor.ToUpper().Trim()));

				if (filter.DateBegin.HasValue)
					query = query.Where(q => q.Date >= filter.DateBegin.Value);

                if (filter.DateEnd.HasValue)
                    query = query.Where(q => q.Date <= filter.DateEnd.Value);
            }
            #endregion

            return query.ToList();
        }
    }
}
