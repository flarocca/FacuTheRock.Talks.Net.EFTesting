using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using FacuTheRock.Talks.Net.EFTesting.Database;
using FacuTheRock.Talks.Net.EFTesting.Database.Exceptions;
using FacuTheRock.Talks.Net.EFTesting.Database.Models;
using FacuTheRock.Talks.Net.EFTesting.Database.Repositories;
using Xunit;

namespace FacuTheRock.Talks.Net.EFTesting.Tests
{
    public class PlayersRepositoryTests
    {
        [Fact]
        public async Task AddAsync_TeamExistsAndPlayerIsValid_PlayerIsAdded()
        {
            // Arrange
            using var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder()
                .UseSqlite(connection)
                .Options;

            var team = new Team
            {
                Name = "Test team",
                FoundedYear = 2020
            };
            Guid playerAddedId;

            using (var context = new AppDbContext(options))
            {
                await context.Database.EnsureCreatedAsync();

                context.Teams.Add(team);
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new PlayersRepository(context);
                var player = new Player
                {
                    FirstName = "Test",
                    LastName = "Player",
                    Position = PositionType.Defender,
                    TeamId = team.Id
                };

                // Act
                playerAddedId = await repository.AddAsync(player);
            }

            using (var context = new AppDbContext(options))
            {
                // Assert
                var result = await context.Players
                    .AnyAsync(player => player.TeamId == team.Id &&
                                        player.Id == playerAddedId);

                Assert.True(result);
            }
        }

        [Fact]
        public async Task AddAsync_TeamDoesNotExistAndPlayerIsValid_TeamNotFoundExceptionIsThrown()
        {
            // Arrange
            using var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder()
                .UseSqlite(connection)
                .Options;

            using (var context = new AppDbContext(options))
            {
                await context.Database.EnsureCreatedAsync();

                var repository = new PlayersRepository(context);
                var player = new Player
                {
                    FirstName = "Test",
                    LastName = "Player",
                    Position = PositionType.Defender,
                    TeamId = Guid.NewGuid()
                };

                // Act && Assert
                await Assert.ThrowsAsync<TeamNotExistsException>(() => repository.AddAsync(player));
            }
        }
    }
}
