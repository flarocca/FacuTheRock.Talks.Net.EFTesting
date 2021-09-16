using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using FacuTheRock.Talks.Net.EFTesting.Database;
using FacuTheRock.Talks.Net.EFTesting.Database.Models;
using FacuTheRock.Talks.Net.EFTesting.Database.Repositories;
using Xunit;

namespace FacuTheRock.Talks.Net.EFTesting.Tests
{
    public class TeamsRepositoryTests
    {
        [Fact]
        public async Task AddAsync_ValidTeam_TeamIsAdded()
        {
            // Arrange
            using var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder()
                .UseSqlite(connection)
                .Options;

            var teamToAdd = new Team
            {
                Name = "Test team",
                FoundedYear = 2020
            };
            Guid teamAddedId;

            using (var context = new AppDbContext(options))
            {
                await context.Database.EnsureCreatedAsync();

                var repository = new TeamsRepository(context);

                // Act
                teamAddedId = await repository.AddAsync(teamToAdd);
            }

            using (var context = new AppDbContext(options))
            {
                // Assert
                var result = await context.Teams
                    .AnyAsync(team => team.Id == teamAddedId);

                Assert.True(result);
            }
        }

        [Fact]
        public async Task GetAsync_TeamWithIdExists_TeamIsRetrieved()
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

            using (var context = new AppDbContext(options))
            {
                await context.Database.EnsureCreatedAsync();

                context.Teams.Add(team);
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                // Act
                var result = await context.Teams
                    .FirstOrDefaultAsync(t => t.Id == team.Id);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(team.Name, result.Name);
            }
        }
    }
}
