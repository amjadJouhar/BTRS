using System.ComponentModel.DataAnnotations;

namespace BTRS.Models
{
	public class Login
	{
		[Required(ErrorMessage = "Please Enter Username")]
		public String Username { get; set; }
		[Required(ErrorMessage = "Please Enter Password")]
		public String Password { get; set; }
	}
}
