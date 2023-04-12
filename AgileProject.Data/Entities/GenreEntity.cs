using System.ComponentModel.DataAnnotations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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