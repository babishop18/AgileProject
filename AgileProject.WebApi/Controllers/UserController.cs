using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileProject.Models.Game;
using AgileProject.Models.User;
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
        






        // all methods associated with Admin
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddNewGame([FromBody] GameRegister request){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            if(await _userService.AddNewGameAsync(request)){
                return Ok("Game added");
            }
            return BadRequest("Game not added");
        }
    }
}