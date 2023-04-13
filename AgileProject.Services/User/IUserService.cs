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
        Task<bool> RemoveGameAsync(int gameId);
        Task<IEnumerable<GameListItem>> GetListOfAllGamesAsync();
        Task<IEnumerable<GameListItem>> GetListOfAllGamesByGenreAsync(string genreName);
        Task<IEnumerable<GameListItem>> GetListOfAllGamesByGameSystemAsync(string genreName);
        
    }
}