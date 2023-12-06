using Business.Models;
using Core.Repositories.EntityFramework.Bases;
using Core.Results.Bases;
using Core.Services.Bases;
using DataAccess.Entities;

namespace Business.Services
{
    public interface IGenderService : IService<GenderModel>
    {
    }
    public class GenderService : IGenderService
    {
        private readonly RepoBase<Gender> _genderRepo;

        public GenderService(RepoBase<Gender> genderRepo)
        {
            _genderRepo = genderRepo;
        }

        public IQueryable<GenderModel> Query()
        {
            return _genderRepo.Query().OrderBy(g => g.Name).Select(g => new GenderModel()
            {
                Id = g.Id,
                Guid = g.Guid,
                Name = g.Name,

                Doctors = g.Doctors.Select(d => new DoctorModel()
                {
                    Id = d.Id,
                    NameSurname = d.Name + " " + d.Surname
                }).ToList(),

                Patients = g.Patients.Select(p => new PatientModel()
                {
                    Id = p.Id,
                    NameSurname = p.Name + " " + p.Surname
                }).ToList()
            });
        }

        public Result Add(GenderModel model)
        {
            throw new NotImplementedException();
        }

        public Result Update(GenderModel model)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _genderRepo.Dispose();
        }
    }
}
