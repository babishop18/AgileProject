using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgileProject.Data.Entities
{
    public class GameEntity
    {
        [Key] 
        public int Id{get;set;}
        [Required]
        public string Title{get;set;}
        [Required]
        [ForeignKey(nameof(Genre))]
        public int GenreId{get; set;}
        public GenreEntity Genre{get; set;}
        [Required]
        [ForeignKey(nameof(GameSystem))]
        public int GameSystemId{get; set;}
        public GameSystemEntity GameSystem{get;set;}
        
        
    }
}