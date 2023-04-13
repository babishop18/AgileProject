using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileProject.Models.Genre;

namespace AgileProject.Services.Genre
{
    public class IGenreService
    {
       Task <bool> InputGenreAsync(GenreRegister request);

       Task <bool> RemoveGenreAsync(GenreRegister model);
    }
}