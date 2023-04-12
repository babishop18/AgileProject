using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileProject.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgileProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<UserEntity> Users{get;set;}
        public DbSet<GameEntity> Games{get;set;}
        public DbSet<GenreEntity> Genres{get;set;}
        public DbSet<GameSystemEntity> GameSystems{get;set;}
    }
}