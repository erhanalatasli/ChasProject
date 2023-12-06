#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Business.Models.Account
{
	public class AccountRegisterModel
	{
        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(50, ErrorMessage = "{0} must be minimum {1} characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(50, ErrorMessage = "{0} must be minimum {1} characters!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
		[MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
		[MaxLength(50, ErrorMessage = "{0} must be minimum {1} characters!")]
		[DisplayName("User Name")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "{0} is required!")]
		[MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
		[MaxLength(10, ErrorMessage = "{0} must be minimum {1} characters!")]
		public string Password { get; set; }

		[Required(ErrorMessage = "{0} is required!")]
		[MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
		[MaxLength(10, ErrorMessage = "{0} must be minimum {1} characters!")]
		[Compare("Password", ErrorMessage = "{0} and {1} must be same!")]
		[DisplayName("Confirm Password")]
		public string ConfirmPassword { get; set; }
    }
}
