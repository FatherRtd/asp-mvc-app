using System.ComponentModel.DataAnnotations;

namespace mvcapp.Models.Users
{
	public class UsersViewModel
	{
		public LoginViewModel LoginViewModel { get; set; }
		public RegisterViewModel RegisterViewModel { get; set; }
	}

	public class LoginViewModel
	{
		[Required(ErrorMessage = "Данное поле обязательно")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Данное поле обязательно")]
		public string Password { get; set; }
	}

	public class RegisterViewModel
	{
        [Required(ErrorMessage = "Данное поле обязательно")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        public string SurName { get; set; }

		[Required(ErrorMessage = "Данное поле обязательно")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Данное поле обязательно")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Данное поле обязательно")]
		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		public string RepeatPassword { get; set; }
	}
}
