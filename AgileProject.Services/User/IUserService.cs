using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileProject.Models.Game;
using AgileProject.Models.User;

namespace AgileProject.Services.User
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserRegister model);
        Task<bool> AddNewGameAsync(GameRegister request);
    }
}