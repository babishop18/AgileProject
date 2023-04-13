using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileProject.Models.GameSystem;
using AgileProject.Services.GameSystem;
using AgileProject.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AgileProject.WebApi.Controllers
{
    public class GameSystemController : ControllerBase
    {
         private readonly ITokenService _tokenService;

        private readonly IGameSystemService _gameSystemService;

        public GameSystemController(ITokenService tokenService, IGameSystemService gameSystemService)
        {
            _tokenService = tokenService;
            _gameSystemService = gameSystemService;
        }

         [Authorize(Policy="Admin")]
        [HttpDelete]
        public async Task<IActionResult> RemoveGameSystem([FromBody] string gameSystemName)
        {
            return await _gameSystemService.RemoveGameSystemAsync(gameSystemName)
                ? Ok("Game system was deleted successfully.")
                : BadRequest("Game system could not be deleted.");
        }

         [Authorize(Policy="Admin")]
        [HttpPost]
        public async Task<IActionResult> InputGameSystem([FromBody] GSRegister request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (await _gameSystemService.InputGameSystemAsync(request))
                return Ok("Game system was created successfully.");

            return BadRequest("Game system could not be created.");
        }
    }
}