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
            GenreEntity genreExists = await _context.Genres.FirstOrDefaultAsync(g => g.GenreType == request.GenreType)
            if (genreExists == null)
            {
                return false;
            }
            GameSystemEntity gameSystemExists = await _context.GameSystems.FirstOrDefaultAsync(g => g.GameSystemType == request.GameSystemType)
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

    }


}
