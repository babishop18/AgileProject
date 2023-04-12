using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace AgileProject.WebApi.Controllers
{   
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly 




        // all methods associated with Customer
        [Authorize(Policy = "Customer")]






        // all methods associated with Admin
        [Authorize(Policy = "Admin")]
    }
}