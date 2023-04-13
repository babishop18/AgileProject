using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileProject.Data;
using AgileProject.Data.Entities;
using AgileProject.Models.GameSystem;
using Microsoft.EntityFrameworkCore;

namespace AgileProject.Services.GameSystem
{
    public class GameSystemService
    {
        private readonly ApplicationDbContext _context;

        public GameSystemService(ApplicationDbContext context) => _context = context;

        public async Task<bool> InputGameSystemAsync(GSRegister request)
        {
            GameSystemEntity gameSystemEntity = new GameSystemEntity
            {
                GameSystemType = request.GameSystemType
            };
            _context.GameSystems.Add(gameSystemEntity);
            int numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> RemoveGameSystemAsync(string gameSystemType)
        {
            var gameSystemEntity = await _context.GameSystems.FirstOrDefaultAsync(g => g.GameSystemType == gameSystemType);

            if (gameSystemEntity == null)
            {
                return false;
            }
            _context.GameSystems.Remove(gameSystemEntity);
            return await _context.SaveChangesAsync() == 1;

        }
    }
}