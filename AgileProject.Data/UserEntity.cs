using System;
using System.ComponentModel.DataAnnotations;

namespace AgileProject.Data
{
	public class UserEntity
	{
		[Key]
		public int UserId { get; set; }
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Phone { get; set; }
	}
}
