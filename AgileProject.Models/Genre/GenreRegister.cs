using System.ComponentModel.DataAnnotations;

namespace AgileProject.Models.Genre
{
    public class GenreRegister
    {
        [Required]
        public string GenreType{get; set;}
    }
}