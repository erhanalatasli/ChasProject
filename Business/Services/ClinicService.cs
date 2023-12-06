using Business.Models;
using Core.Repositories.EntityFramework.Bases;
using Core.Results;
using Core.Results.Bases;
using Core.Services.Bases;
using DataAccess.Entities;

namespace Business.Services
{
	public interface IClinicService : IService<ClinicModel>
	{
	}
	public class ClinicService : IClinicService
	{
		private readonly RepoBase<Clinic> _clinicRepo;

		public ClinicService(RepoBase<Clinic> clinicRepo)
		{
			_clinicRepo = clinicRepo;
		}

		public IQueryable<ClinicModel> Query()
		{
			return _clinicRepo.Query().OrderBy(c => c.Name).Select(c => new ClinicModel()
			{
				Id = c.Id,
				Name = c.Name,
				Guid = c.Guid,

				Doctors = c.Doctors.Select(d => new DoctorModel()
				{
					Id = d.Id,
					NameSurname = d.Name + " " + d.Surname
				}).ToList()
			});
		}

		public Result Add(ClinicModel model)
		{
            if (_clinicRepo.Exists(c => c.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Clinic with the same name exists!");

            Clinic entity = new Clinic()
            {
                Name = model.Name,
				Guid = model.Guid
            };
            _clinicRepo.Add(entity);

            return new SuccessResult("Clinic added successfully.");
        }

		public Result Update(ClinicModel model)
		{
            Clinic entity = new Clinic()
            {
				Id = model.Id,
                Name = model.Name,
                Guid = model.Guid
            };
            _clinicRepo.Update(entity);

            return new SuccessResult("Clinic updated successfully.");
        }

		public Result Delete(int id)
		{
            _clinicRepo.Delete(c => c.Id == id);

            return new SuccessResult("Clinic deleted successfully.");
        }

		public void Dispose()
		{
			_clinicRepo.Dispose();
		}
	}
}
