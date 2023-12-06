using Business.Models;
using Core.Repositories.EntityFramework.Bases;
using Core.Results;
using Core.Results.Bases;
using Core.Services.Bases;
using DataAccess.Entities;

namespace Business.Services
{
    public interface IAppointmentService : IService<AppointmentModel>
    {
    }
    public class AppointmentService : IAppointmentService
    {
        private readonly RepoBase<Appointment> _appointmentRepo;

        public AppointmentService(RepoBase<Appointment> appointmentRepo)
        {
            _appointmentRepo = appointmentRepo;
        }

        public IQueryable<AppointmentModel> Query()
        {
            return _appointmentRepo.Query().Select(a => new AppointmentModel()
            {
                Id = a.Id,
                UserId = a.UserId,
                HospitalId = a.HospitalId,
                ClinicId = a.ClinicId,
                DoctorId = a.DoctorId,
                Guid = a.Guid,
                Date = a.Date,
                IsActive = a.IsActive,
                Explanation = a.Explanation,

				UserDisplay = a.User.Name + " " + a.User.Surname,
                HospitalDisplay = a.Hospital.Name,
                ClinicDisplay = a.Clinic.Name,
                DoctorDisplay = a.Doctor.Name + " " + a.Doctor.Surname,                
            });
        }

        public Result Add(AppointmentModel model)
        {
			if (_appointmentRepo.Exists(a => a.UserId == model.UserId && a.DoctorId == model.DoctorId))
				return new ErrorResult("This appointment has already been made!");

			Appointment entity = new Appointment()
			{
				UserId = model.UserId,
                HospitalId = model.HospitalId,
                ClinicId = model.ClinicId,
                DoctorId = model.DoctorId,
                Date = model.Date,
                IsActive = true,
                Explanation = model.Explanation,
                Guid = model.Guid
			};

			_appointmentRepo.Add(entity);

			return new SuccessResult("Appointment created successfully.");
		}

        public Result Update(AppointmentModel model)
        {
			Appointment entity = new Appointment()
			{
                Id = model.Id,
				UserId = model.UserId,
				HospitalId = model.HospitalId,
				ClinicId = model.ClinicId,
				DoctorId = model.DoctorId,
				Date = model.Date,
				IsActive = model.IsActive,
				Explanation = model.Explanation,
				Guid = model.Guid
			};
			_appointmentRepo.Update(entity);

			return new SuccessResult("Appointment updated successfully.");
		}

        public Result Delete(int id)
        {
			_appointmentRepo.Delete(a => a.Id == id);

			return new SuccessResult("Appointment deleted successfully.");
		}

        public void Dispose()
        {
            _appointmentRepo.Dispose();
        }
    }
}



