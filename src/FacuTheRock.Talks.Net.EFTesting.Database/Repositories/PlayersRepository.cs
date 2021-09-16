using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using FacuTheRock.Talks.Net.EFTesting.Database.Exceptions;
using FacuTheRock.Talks.Net.EFTesting.Database.Models;

namespace FacuTheRock.Talks.Net.EFTesting.Database.Repositories
{
    public class PlayersRepository : IPlayersRepository
    {
        private readonly AppDbContext _dbContext;

        public PlayersRepository(AppDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<IEnumerable<Player>> GetAllAsync(Guid teamId) =>
            await _dbContext.Players
                .Include(player => player.Team)
                .Where(player => player.TeamId == teamId)
                .ToListAsync();

        public async Task<Player> GetAsync(Guid teamId, Guid id) =>
            await _dbContext.Players
                .Include(player => player.Team)
                .FirstOrDefaultAsync(player =>
                    player.TeamId == teamId &&
                    player.Id == id);

        public async Task<Guid> AddAsync(Player player)
        {
            try
            {
                _dbContext.Players.Add(player);
                await _dbContext.SaveChangesAsync();

                return player.Id;
            }
            catch (DbUpdateException)
            {
                throw new TeamNotExistsException();
            }
        }

        public async Task DeleteAsync(Guid teamId, Guid id)
        {
            try
            {
                var playerToDelete = new Player
                {
                    TeamId = teamId,
                    Id = id
                };

                _dbContext.Players.Remove(playerToDelete);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new TeamNotExistsException();
            }
        }
    }
}
