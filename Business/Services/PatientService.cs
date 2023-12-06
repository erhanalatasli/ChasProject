using Business.Models;
using Core.Repositories.EntityFramework.Bases;
using Core.Results;
using Core.Results.Bases;
using Core.Services.Bases;
using DataAccess.Entities;

namespace Business.Services
{
    public interface IPatientService : IService<PatientModel>
    {
    }
    public class PatientService : IPatientService
    {
        private readonly RepoBase<Patient> _patientRepo;

        public PatientService(RepoBase<Patient> patientRepo)
        {
            _patientRepo = patientRepo;
        }

        public IQueryable<PatientModel> Query()
        {
            return _patientRepo.Query().Select(p => new PatientModel()
            {
                Id = p.Id,
                Name = p.Name,
                Surname = p.Surname,
                DateOfBirth = p.DateOfBirth,
                CityId = p.CityId,
                Guid = p.Guid,
                IsIssurance = p.IsIssurance,
                Complaint = p.Complaint,
                GenderId = p.GenderId,
                NameSurname = p.Name + " " + p.Surname,

                CityDisplay = p.City.Name,
                GenderDisplay = p.Gender.Name,


                Doctors = p.DoctorPatients.Select(dp => new DoctorModel()
                {
                    Id = dp.DoctorId,
                    NameSurname = dp.Doctor.Name + " " + dp.Doctor.Surname + " (" + dp.Doctor.Clinic.Name + ")"
                }).ToList(),

                ImageExtension = p.ImageExtension,

                ImgSrcDisplay = p.Image != null ?
                    (
                        p.ImageExtension == ".jpg" || p.ImageExtension == ".jpeg" ?
                        "data:image/jpeg;base64,"
                        : "data:image/png;base64,"
                    ) + Convert.ToBase64String(p.Image)
                    : null
            });
        }
        public Result Add(PatientModel model)
        {
			_patientRepo.Delete<DoctorPatient>(dp => dp.PatientId == model.Id);

			Patient entity = new Patient()
            {
                Name = model.Name,
                Surname = model.Surname,
                DateOfBirth = model.DateOfBirth,
                CityId = model.CityId,
                IsIssurance = model.IsIssurance,
                Complaint = model.Complaint,
                Guid = model.Guid,
                GenderId = model.GenderId,

                DoctorPatients = model.DoctorIds.Select(dp => new DoctorPatient()
                {
                    DoctorId = dp
                }).ToList(),

                Image = model.Image,
                ImageExtension = model.ImageExtension
            };
            _patientRepo.Add(entity);

            return new SuccessResult("Patient added successfully.");
        }
        public Result Update(PatientModel model)
        {
			_patientRepo.Delete<DoctorPatient>(dp => dp.PatientId == model.Id);

            var entity = _patientRepo.Query().SingleOrDefault(p => p.Id == model.Id);


            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.Surname = model.Surname;
            entity.DateOfBirth = model.DateOfBirth;
            entity.CityId = model.CityId;
            entity.IsIssurance = model.IsIssurance;
            entity.Complaint = model.Complaint;
            entity.Guid = model.Guid;
            entity.GenderId = model.GenderId;
            entity.DoctorPatients = model.DoctorIds.Select(dp => new DoctorPatient()
            {
                DoctorId = dp
            }).ToList();

            if (model.Image is not null)
            {
                entity.Image = model.Image;
                entity.ImageExtension = model.ImageExtension;
            }

            _patientRepo.Update(entity);

			return new SuccessResult("Patient updated successfully.");
		}

        public Result Delete(int id)
        {
            _patientRepo.Delete<DoctorPatient>(dp => dp.PatientId == id);
            _patientRepo.Delete(p => p.Id == id);

            return new SuccessResult("Patient deleted successfully.");
        }

        public void Dispose()
        {
            _patientRepo.Dispose();
        }
    }
}
