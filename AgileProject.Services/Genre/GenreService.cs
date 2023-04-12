using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileProject.Models.Genre;

namespace AgileProject.Services.Genre
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext _context;

        public GenreService(ApplicationDbContext context) => _context = context;

        public async Task<bool> RegisterGenreAsync(GenreRegister model);
    }
}