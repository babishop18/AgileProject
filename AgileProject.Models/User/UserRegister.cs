using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgileProject.Models.User
{
    public class UserRegister
    {
       
		
		[Required]
		public string Username { get; set; }
        [Required]
		
		public string Password { get; set; }
		
		[Required]
        [RegularExpression("^(Admin|Customer)$")]
		public string Classifier{get; set;} // Admin or Customer 
    }
}

// {
//     "Username": ";ladjflk;dsa",
//     "Password": ";laksdjfkl;ja",
//     "Classifer": "Admin"
// }