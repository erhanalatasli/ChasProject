using Business.Models;
using Core.Repositories.EntityFramework.Bases;
using Core.Results;
using Core.Results.Bases;
using Core.Services.Bases;
using DataAccess.Entities;

namespace Business.Services
{
    public interface ICityService : IService<CityModel>
    {
    }
    public class CityService : ICityService
    {
        private readonly RepoBase<City> _cityRepo;

        public CityService(RepoBase<City> cityRepo)
        {
            _cityRepo = cityRepo;
        }

        public IQueryable<CityModel> Query()
        {
            return _cityRepo.Query().OrderBy(c => c.Name).Select(c => new CityModel()
            {
                Id = c.Id,
                Name = c.Name,

                Districts = c.Districts.OrderBy(d => d.Name).Select(d => new DistrictModel()
                {
                    Id = d.Id,
                    Name = d.Name,
                }).ToList(),

                Hospitals = c.Hospitals.OrderBy(h => h.Name).Select(d => new HospitalModel()
                {
                    Id = d.Id,
                    Name = d.Name,
                }).ToList()
            });
        }

        public Result Add(CityModel model)
        {
            if (_cityRepo.Exists(c => c.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("City with the same name exists!");

            City entity = new City()
            {
                Name = model.Name,
                Guid = model.Guid
            };
            _cityRepo.Add(entity);

            return new SuccessResult("City added successfully.");
        }

        public Result Update(CityModel model)
        {
            //if (_cityRepo.Exists(c => c.Name.ToLower() == model.Name.ToLower().Trim()))
            //    return new ErrorResult("City with the same name exists!");

            City entity = new City()
            {
                Id = model.Id,
                Name = model.Name
            };
            _cityRepo.Update(entity);

            return new SuccessResult("City updated successfully.");
        }

        public Result Delete(int id)
        {
            _cityRepo.Delete(c => c.Id == id);

            return new SuccessResult("City deleted successfully.");
        }

        public void Dispose()
        {
            _cityRepo.Dispose();
        }
    }
}
