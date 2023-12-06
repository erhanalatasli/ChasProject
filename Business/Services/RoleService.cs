using Business.Models;
using Core.Repositories.EntityFramework.Bases;
using Core.Results;
using Core.Results.Bases;
using Core.Services.Bases;
using DataAccess.Entities;

namespace Business.Services
{
    public interface IRoleService : IService<RoleModel>
    {
    }
    public class RoleService : IRoleService
    {
        private readonly RepoBase<Role> _roleRepo;

        public RoleService(RepoBase<Role> roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public IQueryable<RoleModel> Query()
        {
            return _roleRepo.Query().Select(r => new RoleModel()
            {
                Id = r.Id,
                Name = r.Name,
                Guid = r.Guid,

                Users = r.Users.Select(u => new UserModel()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Password = u.Password,
                    IsActive = u.IsActive,
                    Guid = u.Guid,
                    RoleId = u.RoleId
                }).ToList()
            });
        }

        public Result Add(RoleModel model)
        {
            if (_roleRepo.Exists(r => r.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Role with the same name exists!");

            Role entity = new Role()
            {
                Name = model.Name,
                Guid = model.Guid,
            };
            _roleRepo.Add(entity);

            return new SuccessResult("Role added successfully.");
        }

        public Result Update(RoleModel model)
        {
            //if (_roleRepo.Exists(r => r.Name.ToLower() == model.Name.ToLower().Trim()))
            //    return new ErrorResult("Role with the same name exists!");

            Role entity = new Role()
            {
                Id = model.Id,
                Name = model.Name,
                Guid = model.Guid,
            };
            _roleRepo.Update(entity);

            return new SuccessResult("Role updated successfully.");
        }

        public Result Delete(int id)
        {
            _roleRepo.Delete(r => r.Id == id);

            return new SuccessResult("Role deleted successfully.");
        }

        public void Dispose()
        {
            _roleRepo.Dispose();
        }
    }
}
