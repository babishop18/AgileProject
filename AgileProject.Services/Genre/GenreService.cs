using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileProject.Data;
using AgileProject.Data.Entities;
using AgileProject.Models.Genre;
using Microsoft.EntityFrameworkCore;

namespace AgileProject.Services.Genre
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext _context;
        public GenreService(ApplicationDbContext context) => _context = context;

        public async Task<bool> InputGenreAsync(GenreRegister request)
        {
            GenreEntity genreEntity = new GenreEntity
            {
                GenreType = request.GenreType
            };
            _context.Genres.Add(genreEntity);
            int numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> RemoveGenreAsync(string genreName)
        {
            var genreEntity = await _context.Genres.FindAsync(genreName);
            
            if (genreEntity == null)
                
                return false;

            _context.Genres.Remove(genreEntity);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<GenreEntity> GetGenreByType(string GenreType)
        {
            return await _context.Genres.FirstOrDefaultAsync(genre => genre.GenreType.ToLower() == GenreType.ToLower());
        }

        public static Task<bool> DeleteNoteAsync(GenreRegister model)
        {
            throw new NotImplementedException();
        }
    }
}