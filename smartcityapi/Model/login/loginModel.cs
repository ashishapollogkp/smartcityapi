using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.login
{
	public class loginModel
	{
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
