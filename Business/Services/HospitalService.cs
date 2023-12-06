using Business.Models;
using Core.Repositories.EntityFramework.Bases;
using Core.Results;
using Core.Results.Bases;
using Core.Services.Bases;
using DataAccess.Entities;

namespace Business.Services
{
    public interface IHospitalService : IService<HospitalModel>
    {
    }
    public class HospitalService : IHospitalService
    {
        private readonly RepoBase<Hospital> _hospitalRepo;

        public HospitalService(RepoBase<Hospital> hospitalRepo)
        {
            _hospitalRepo = hospitalRepo;
        }

        public IQueryable<HospitalModel> Query()
        {
            return _hospitalRepo.Query().OrderBy(h => h.Name).Select(h => new HospitalModel()
            {
                Id = h.Id,
                Name = h.Name,
                DistrictId = h.DistrictId,
                CityId = h.CityId,
                CityDisplay = h.City.Name,
                DistrictDisplay = h.District.Name,

                Doctors = h.Doctors.Select(d => new DoctorModel()
                {
                    Id = d.Id,
                    NameSurname = d.Name + " " + d.Surname + " (" + d.Clinic.Name + ")"

                }).ToList()
            });
        }

		public Result Add(HospitalModel model)
        {
            if (_hospitalRepo.Exists(h => h.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Hospital with the same name exists!");

            Hospital entity = new Hospital()
            {
                Name = model.Name,
                CityId = model.CityId,
                DistrictId = model.DistrictId,
                Guid = model.Guid
            };
            _hospitalRepo.Add(entity);

            return new SuccessResult("Hospital added successfully.");
        }

        public Result Update(HospitalModel model)
        {
            //if (_hospitalRepo.Exists(h => h.Name.ToLower() == model.Name.ToLower().Trim() && h.Id != model.Id))
            //    return new ErrorResult("Hospital with the same title exists!");

            Hospital entity = new Hospital()
            {
                Id = model.Id,
                Name = model.Name,
                CityId = model.CityId,
                DistrictId = model.DistrictId,
                Guid = model.Guid
            };
            _hospitalRepo.Update(entity);

            return new SuccessResult("Hospital updated successfully.");
        }

        public Result Delete(int id)
        {
            _hospitalRepo.Delete(h => h.Id == id);

            return new SuccessResult("Hospital deleted successfully.");
        }

        public void Dispose()
        {
            _hospitalRepo.Dispose();
        }
    }
}
