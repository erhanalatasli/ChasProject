using Business.Models;
using Business.Models.Account;
using Core.Results;
using Core.Results.Bases;
using DataAccess.Enums;
using System.Net.Http;

namespace Business.Services
{
	public interface IAccountService
	{
		Result Login(AccountLoginModel accountLoginModel, UserModel userResultModel);

		Result Register(AccountRegisterModel model);
	}
	public class AccountService : IAccountService
	{
		private readonly IUserService _userService;

		public AccountService(IUserService userService)
		{
			_userService = userService;
		}

		public Result Login(AccountLoginModel accountLoginModel, UserModel userResultModel)
		{
			var user = _userService.Query().SingleOrDefault(u => u.UserName == accountLoginModel.UserName && u.Password == accountLoginModel.Password && u.IsActive);

			if (user is null)
				return new ErrorResult("Invalid user or password!");

			userResultModel.UserName = user.UserName;

            userResultModel.Role = new RoleModel();
            userResultModel.Role.Name = user.Role.Name;
			userResultModel.Id = user.Id;

			return new SuccessResult();
		}

        public Result Register(AccountRegisterModel model)
		{
			UserModel userModel = new UserModel()
			{
				Name = model.Name,
				Surname = model.Surname,
				UserName = model.UserName,
				Password = model.Password,
				IsActive = true,
				RoleId = (int)Roles.Patient
			};

			return _userService.Add(userModel);
		}
	}
}
