﻿using System;
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
		[RegularExpression("^(Admin|Customer)$")]
		public string Classifier{get; set;} // Admin or Customer 
		
	}
}
