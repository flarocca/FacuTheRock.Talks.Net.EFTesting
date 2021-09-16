using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FacuTheRock.Talks.Net.EFTesting.Database.Models;

namespace FacuTheRock.Talks.Net.EFTesting.Database.Repositories
{
    public interface ITeamsRepository
    {
        Task<IEnumerable<Team>> GetAllAsync();

        Task<Team> GetAsync(Guid id);

        Task<Guid> AddAsync(Team team);

        Task DeleteAsync(Guid id);
    }
}
