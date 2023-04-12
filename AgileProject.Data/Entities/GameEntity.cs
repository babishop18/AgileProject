using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgileProject.Data.Entities
{
    public class GameEntity
    {
        [Key] 
        public int Id{get;set;}
        [Required]
        public string Title{get;set;}
        [Required]
        public string Genre{get; set;}
        [Required]
        public string GameSystem{get;set;}
        
    }
}