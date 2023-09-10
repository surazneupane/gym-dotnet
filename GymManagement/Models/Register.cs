using System;
using System.ComponentModel.DataAnnotations;

namespace GymManagement.Models
{
	public class Register
	{
		[Key]
		public Guid Userid { get; set; } = Guid.NewGuid();

		public string UserName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
	}
}