using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FacuTheRock.Talks.Net.EFTesting.Database.Exceptions;
using FacuTheRock.Talks.Net.EFTesting.Database.Models;

namespace FacuTheRock.Talks.Net.EFTesting.Database.Repositories
{
    public class TeamsRepository : ITeamsRepository
    {
        private readonly AppDbContext _dbContext;

        public TeamsRepository(AppDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<IEnumerable<Team>> GetAllAsync() =>
            await _dbContext.Teams
                .Include(team => team.Players)
                .ToListAsync();

        public async Task<Team> GetAsync(Guid id) =>
            await _dbContext.Teams
                .Include(team => team.Players)
                .FirstOrDefaultAsync(team => team.Id == id);

        public async Task<Guid> AddAsync(Team team)
        {
            _dbContext.Teams.Add(team);
            await _dbContext.SaveChangesAsync();

            return team.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var teamToDelete = new Team { Id = id };

                _dbContext.Teams.Remove(teamToDelete);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new TeamNotExistsException();
            }
        }
    }
}
