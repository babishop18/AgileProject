using System.ComponentModel.DataAnnotations;

namespace AgileProject.Data.Entities
{
    public class GenreEntity
    {
        [Key]
        public int Id {get; set;}

        [Required]
        public string GenreType {get; set;}

        

    }
}