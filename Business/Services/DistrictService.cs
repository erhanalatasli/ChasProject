using Business.Models;
using Core.Repositories.EntityFramework.Bases;
using Core.Results;
using Core.Results.Bases;
using Core.Services.Bases;
using DataAccess.Entities;

namespace Business.Services
{
	public interface IDistrictService : IService<DistrictModel>
    {
		List<DistrictModel> GetListByCity(int? cityId = null);
	}
    public class DistrictService : IDistrictService
    {
        private readonly RepoBase<District> _districtRepo;

        public DistrictService(RepoBase<District> districtRepo)
        {
            _districtRepo = districtRepo;
        }

        public IQueryable<DistrictModel> Query()
        {
            return _districtRepo.Query().Select(d => new DistrictModel()
            {
                Id = d.Id,
                Name = d.Name,
                CityId = d.CityId,
                City = d.City,

                CityDisplay = d.City.Name,

                Hospitals = d.Hospitals.Select(d => new HospitalModel()
                {
                    Id = d.Id,
                    Name = d.Name

                }).ToList()
            });
        }

		public List<DistrictModel> GetListByCity(int? cityId = null)
		{
			if (cityId is null)
				return new List<DistrictModel>();
			return Query().Where(q => q.CityId == cityId).ToList();
		}

		public Result Add(DistrictModel model)
        {
            if (_districtRepo.Exists(d => d.Name.ToLower() == model.Name.ToLower().Trim() && d.CityId == model.CityId))
                return new ErrorResult("There is this district name for the city!");

            District entity = new District()
            {
                Name = model.Name,
                CityId = model.CityId
            };
            _districtRepo.Add(entity);

            return new SuccessResult("District added successfully");
        }

        public Result Update(DistrictModel model)
        {
            //if (_districtRepo.Exists(d => d.Name.ToLower() == model.Name.ToLower().Trim() && d.CityId == model.CityId && d.Id != model.Id))
            //    return new ErrorResult("There is this district name for the city!");

            District entity = new District()
            {
                Id = model.Id,
                Name = model.Name,
                CityId = model.CityId
            };
            _districtRepo.Update(entity);

            return new SuccessResult("District updated successfully");
        }

        public Result Delete(int id)
        {
            _districtRepo.Delete(d => d.Id == id);

            return new SuccessResult("District deleted successfully");
        }

        public void Dispose()
        {
            _districtRepo.Dispose();
        }
    }
}
