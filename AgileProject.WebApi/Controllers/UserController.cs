using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileProject.Models.Game;
using AgileProject.Models.Token;
using AgileProject.Models.User;
using AgileProject.Services.Token;
using AgileProject.Services.User;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace AgileProject.WebApi.Controllers
{   
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService; 
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService){
            _userService = userService;
            _tokenService = tokenService;
        }    
        [HttpPost("~/api/Token")]
        public async Task<IActionResult> Token([FromBody] TokenRequest request){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            TokenResponse tokenReponse = await _tokenService.GetTokenAsync(request);
            if(tokenReponse is null){
                return BadRequest("Invalid username or password");
            }
            return Ok(tokenReponse);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegister model){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            bool registerResult = await _userService.RegisterUserAsync(model);
            if(registerResult){
                return Ok("User was registered");

            }
            return BadRequest("User could not be registered");

        }

        // all methods associated with Customer
        [Authorize(Policy = "Customer")]
        [HttpGet("~/Customer/GetAllGames")]
        public async Task<IActionResult> GetListOfAllGames(){
            IEnumerable<GameListItem> games = await _userService.GetListOfAllGamesAsync();
            return Ok(games);
        }
        [Authorize(Policy = "Customer")]
        [HttpGet("~/Customer/GetByGenre")]
        public async Task<IActionResult> GetListOfAllGamesByGenre(string genre){
            IEnumerable<GameListItem> games = await _userService.GetListOfAllGamesByGenreAsync(genre);
            return Ok(games);
        }
        [Authorize(Policy = "Customer")]
        [HttpGet("~/Customer/GetByGameSystem")]
        public async Task<IActionResult> GetListOfAllGamesByGameSystem(string gameSystem){
            IEnumerable<GameListItem> games = await _userService.GetListOfAllGamesByGameSystemAsync(gameSystem);
            return Ok(games);
        }


        // all methods associated with Admin
        [Authorize(Policy = "Admin")]
        [HttpPost("~/Admin/RegisterGame")]
        public async Task<IActionResult> AddNewGame([FromBody] GameRegister request){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            if(await _userService.AddNewGameAsync(request)){
                return Ok("Game added");
            }
            return BadRequest("Game not added");
        }
        [Authorize(Policy = "Admin")]
        [HttpDelete("~/Admin/DeleteGame")]
        public async Task<IActionResult> RemoveGame([FromRoute] int gameId){
            if(await _userService.RemoveGameAsync(gameId)){
                return Ok("note deleted");
            }
            return BadRequest("note not deleted, error");
        }
    }
}