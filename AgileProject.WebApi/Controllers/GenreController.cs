using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileProject.Models.Genre;
using AgileProject.Services.Genre;
using AgileProject.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {

        private readonly ITokenService _tokenService;

        private readonly IGenreService _genreService;

        public GenreController(ITokenService tokenService, IGenreService genreService)
        {
            _tokenService = tokenService;
            _genreService = genreService;
        }

        [Authorize(Policy="Admin")]
        [HttpDelete]
        public async Task<IActionResult> RemoveGenre([FromBody] string genreName)
        {
            return await _genreService.RemoveGenreAsync(genreName)
                ? Ok("Genre was deleted successfully.")
                : BadRequest("Genre could not be deleted.");
        }

        [Authorize(Policy="Admin")]
        [HttpPost]
        public async Task<IActionResult> InputGenre([FromBody] GenreRegister request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (await _genreService.InputGenreAsync(request))
                return Ok("Genre was created successfully.");

            return BadRequest("Genre could not be created.");
        }

    }
}