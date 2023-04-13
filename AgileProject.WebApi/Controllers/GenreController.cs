using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController
    {
    
        [HttpDelete("{model:GenreRegister}")]
        public async Task<IActionResult> RemoveGenre([FromBody] string genreName)
        {
            return await GenreService.RemoveGenreAsync(genreName)
                ? Ok($"Genre was deleted successfully.")
                : BadRequest($"Genre could not be deleted.");
        }

        [HttpPost("{request:GenreRegister}")]
        public async Task<IActionResult> InputGenre([FromBody] GenreRegister request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (await GenreService.InputGenreAsync(request))
                return Ok("Genre was created successfully.");

            return BadRequest("Genre could not be created.");
        }

    }
}