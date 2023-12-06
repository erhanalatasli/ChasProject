using Business.Models;
using Core.Repositories.EntityFramework.Bases;
using Core.Results;
using Core.Results.Bases;
using Core.Services.Bases;
using DataAccess.Entities;

namespace Business.Services
{

    public interface IUserService : IService<UserModel>
    {
        UserModel GetUserByUserName(string userName);
    }
    public class UserService : IUserService
    {
        private readonly RepoBase<User> _userRepo;

        public UserService(RepoBase<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public IQueryable<UserModel> Query()
        {

            return _userRepo.Query().OrderByDescending(u => u.IsActive).ThenBy(u => u.Name)
                .Select(u => new UserModel()
                {
                    Name = u.Name,
                    Surname = u.Surname,
                    UserName = u.UserName,
                    Password = u.Password,
                    IsActive = u.IsActive,
                    RoleId = u.RoleId,
                    Id = u.Id,
                    NameSurname = u.Name + " " + u.Surname,

                    RoleDisplay = u.Role.Name,

                    Role = new RoleModel()
                    {
                        Name = u.Role.Name
                    }
                });
        }

        public UserModel GetUserByUserName(string userName)
        {
            User user = _userRepo.Query().SingleOrDefault(u => u.UserName == userName);

            return new UserModel()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                UserName = user.UserName,
                Password = user.Password,
                IsActive = user.IsActive,
                RoleId = user.RoleId,
                NameSurname = user.Name + " " + user.Surname,

                RoleDisplay = user.Role?.Name,

                Role = new RoleModel()
                {
                    Name = user.Role?.Name
                }
            };
        }

        public Result Add(UserModel model)
        {
            if (_userRepo.Exists(c => c.UserName.ToLower() == model.UserName.ToLower().Trim()))
                return new ErrorResult("User with the same username exists!");

            User entity = new User()
            {
                Name = model.Name,
                Surname = model.Surname,
                UserName = model.UserName,
                Password = model.Password,
                IsActive = model.IsActive,
                RoleId = model.RoleId,
                Guid = model.Guid,
            };
            _userRepo.Add(entity);

            return new SuccessResult("User added successfully.");
        }

        public Result Update(UserModel model)
        {
            //if (_userRepo.Exists(c => c.Name.ToLower() == model.Name.ToLower().Trim()))
            //    return new ErrorResult("User with the same name exists!");

            User entity = new User()
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                UserName = model.UserName,
                Password = model.Password,
                IsActive = model.IsActive,
                RoleId = model.RoleId,
                Guid = model.Guid
            };
            _userRepo.Update(entity);

            return new SuccessResult("User updated successfully.");
        }

        public Result Delete(int id)
        {
            _userRepo.Delete(u => u.Id == id);

            return new SuccessResult("User deleted successfully.");
        }

        public void Dispose()
        {
            _userRepo.Dispose();
        }
    }
}
