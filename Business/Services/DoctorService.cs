using Business.Models;
using Core.Repositories.EntityFramework.Bases;
using Core.Results;
using Core.Results.Bases;
using Core.Services.Bases;
using DataAccess.Entities;

namespace Business.Services
{
	public interface IDoctorService : IService<DoctorModel>
	{
		List<DoctorModel> GetListByClinic(int? clinicId = null);
	}
	public class DoctorService : IDoctorService
	{
		private readonly RepoBase<Doctor> _doctorRepo;

		public DoctorService(RepoBase<Doctor> doctorRepo)
		{
			_doctorRepo = doctorRepo;
		}

		public IQueryable<DoctorModel> Query()
		{
			return _doctorRepo.Query().Select(d => new DoctorModel()
			{
				Id = d.Id,
				Name = d.Name,
				Surname = d.Surname,
				DateOfBirth = d.DateOfBirth,
				HospitalId = d.HospitalId,
				ClinicId = d.ClinicId,
				GenderId = d.GenderId,
				Guid = d.Guid,
				NameSurname = d.Name + " " + d.Surname + " (" + d.Clinic.Name + ")",
				NameSurnameAppointment = d.Name + " " + d.Surname,

				HospitalDisplay = d.Hospital.Name,
				ClinicDisplay = d.Clinic.Name,
				GenderDisplay = d.Gender.Name,

				Patients = d.DoctorPatients.Select(dp => new PatientModel()
				{
					Id = dp.PatientId,
					NameSurname = dp.Patient.Name + " " + dp.Patient.Surname
				}).ToList(),

				ImageExtension = d.ImageExtension,

				ImgSrcDisplay = d.Image != null ?
					(
						d.ImageExtension == ".jpg" || d.ImageExtension == ".jpeg" ?
						"data:image/jpeg;base64,"
						: "data:image/png;base64,"
					) + Convert.ToBase64String(d.Image)
					: null
			});
		}

		public List<DoctorModel> GetListByClinic(int? clinicId = null)
		{
			if (clinicId is null)
				return new List<DoctorModel>();
			return Query().Where(q => q.ClinicId == clinicId).ToList();
		}
		public Result Add(DoctorModel model)
		{
			Doctor entity = new Doctor()
			{
				Name = model.Name,
				Surname = model.Surname,
				DateOfBirth = model.DateOfBirth,
				HospitalId = model.HospitalId,
				ClinicId = model.ClinicId,
				Guid = model.Guid,
				GenderId = model.GenderId,

				Image = model.Image,
				ImageExtension = model.ImageExtension
			};
			_doctorRepo.Add(entity);

			return new SuccessResult("Doctor added successfully.");
		}

		public Result Update(DoctorModel model)
		{
			_doctorRepo.Delete<DoctorPatient>(dp => dp.DoctorId == model.Id);

			var entity = _doctorRepo.Query().SingleOrDefault(d => d.Id == model.Id);

			entity.Id = model.Id;
			entity.Name = model.Name;
			entity.Surname = model.Surname;
			entity.DateOfBirth = model.DateOfBirth;
			entity.HospitalId = model.HospitalId;
			entity.ClinicId = model.ClinicId;
			entity.Guid = model.Guid;
			entity.GenderId = model.GenderId;

			if (model.Image is not null)
			{
				entity.Image = model.Image;
				entity.ImageExtension = model.ImageExtension;
			}


			_doctorRepo.Update(entity);

			return new SuccessResult("Doctor updated successfully.");
		}

		public Result Delete(int id)
		{
			_doctorRepo.Delete(d => d.Id == id);

			return new SuccessResult("Doctor deleted successfully.");
		}

		public void Dispose()
		{
			_doctorRepo.Dispose();
		}
	}
}
