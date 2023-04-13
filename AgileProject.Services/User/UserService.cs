using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileProject.Data;
using AgileProject.Data.Entities;
using AgileProject.Models.Game;
using AgileProject.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AgileProject.Services.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> RegisterUserAsync(UserRegister model)
        {
            if (await GetUserByUsername(model.Username) != null)
            {
                return false;
            }
            UserEntity entity = new UserEntity
            {
                Username = model.Username,
                Classifier = model.Classifier
            };
            PasswordHasher<UserEntity> passwordHasher = new PasswordHasher<UserEntity>();
            entity.Password = passwordHasher.HashPassword(entity, model.Password);
            _context.Users.Add(entity);
            int numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;


        }
        public async Task<UserEntity> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Username.ToLower() == username.ToLower());
        }

        public async Task<bool> AddNewGameAsync(GameRegister request)
        {
            GenreEntity genreExists = await _context.Genres.FirstOrDefaultAsync(g => g.GenreType == request.GenreType);
            if (genreExists == null)
            {
                return false;
            }
            GameSystemEntity gameSystemExists = await _context.GameSystems.FirstOrDefaultAsync(g => g.GameSystemType == request.GameSystemType);
            if (gameSystemExists == null)
            {
                return false;
            }
            GameEntity gameEntity = new GameEntity
            {
                Title = request.Title,
                GenreId = genreExists.Id,
                GameSystemId = gameSystemExists.Id

            };
            _context.Games.Add(gameEntity);
            int numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }
        public async Task<bool> RemoveGameAsync(int gameId)
        {
            GameEntity gameEntity = await _context.Games.FindAsync(gameId);
            if (gameEntity != null)
            {
                _context.Games.Remove(gameEntity);
            }
            return await _context.SaveChangesAsync() == 1;

        }
        public async Task<IEnumerable<GameListItem>> GetListOfAllGamesAsync()
        {

            IEnumerable<GameListItem> games = await _context.Games.Include(gameEntity => gameEntity.Genre).Include(gameEntity => gameEntity.GameSystem).Select(gameEntity => new GameListItem
            {
                Id = gameEntity.Id,
                Title = gameEntity.Title,
                Genre = gameEntity.Genre.GenreType,
                GameSystem = gameEntity.GameSystem.GameSystemType

            }).ToListAsync();

            return games;
        }
        public async Task<IEnumerable<GameListItem>> GetListOfAllGamesByGenreAsync(string genreName)
        {
            System.Console.WriteLine("\n\n\n\n");
            System.Console.WriteLine("Checking to see if genre exist");
            System.Console.WriteLine("\n\n\n\n");
            GenreEntity genre = await _context.Genres.Include(g => g.Games).ThenInclude(game => game.GameSystem).FirstOrDefaultAsync(g => g.GenreType == genreName);
            System.Console.WriteLine("\n\n\n\n");
            System.Console.WriteLine("We see that the genre exists and now accessing the Genre virtual array");
            System.Console.WriteLine("\n\n\n\n");

            if (genre == null)
            {
                return null;
            }
            IEnumerable<GameListItem> gameListItems = genre.Games.Select(game => new GameListItem
            {
                Id = game.Id,
                Title = game.Title,
                Genre = genre.GenreType,
                GameSystem = game.GameSystem.GameSystemType

            }).ToList();
            return gameListItems;

        }
        public async Task<IEnumerable<GameListItem>> GetListOfAllGamesByGameSystemAsync(string gameSystemName)
        {
            GameSystemEntity gameSystem = await _context.GameSystems.Include(g => g.Games).ThenInclude(game => game.Genre).FirstOrDefaultAsync(g => g.GameSystemType == gameSystemName);
            if (gameSystem == null)
            {
                return null;
            }
            IEnumerable<GameListItem> gameListItems = gameSystem.Games.Select(game => new GameListItem
            {
                Id = game.Id,
                Title = game.Title,
                Genre = game.Genre.GenreType,
                GameSystem = gameSystem.GameSystemType
            });
            return gameListItems;


        }


    }
}