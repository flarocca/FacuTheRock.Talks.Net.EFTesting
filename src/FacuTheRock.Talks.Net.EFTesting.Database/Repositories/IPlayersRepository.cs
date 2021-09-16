using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FacuTheRock.Talks.Net.EFTesting.Database.Models;

namespace FacuTheRock.Talks.Net.EFTesting.Database.Repositories
{
    public interface IPlayersRepository
    {
        Task<IEnumerable<Player>> GetAllAsync(Guid teamId);

        Task<Player> GetAsync(Guid teamId, Guid id);

        Task<Guid> AddAsync(Player team);

        Task DeleteAsync(Guid teamId, Guid id);
    }
}
