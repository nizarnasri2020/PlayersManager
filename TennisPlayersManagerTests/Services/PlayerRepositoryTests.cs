using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using TennisPlayerDomain.Entities;
using TennisPlayerDomain.Interfaces;
using TennisPlayerInfrastructure.Repositories;
using Xunit;

public class PlayerRepositoryTests
{
    private readonly Mock<ILogger<PlayerRepository>> _mockLogger;
    private readonly Mock<IPlayerRepository> _mockRepository;

    public PlayerRepositoryTests()
    {
        _mockLogger = new Mock<ILogger<PlayerRepository>>();
        _mockRepository = new Mock<IPlayerRepository>();

       
    }

    [Fact]
    public async Task GetTennisPlayerByIdAsync_ReturnsPlayer_WhenPlayerExists()
    {
        // Arrange
        var playerId = 1;
        var expectedPlayer = new TennisPlayer { Id = playerId, Firstname = "playerfirstname1", Lastname = "playerlastname1" };

        _mockRepository.Setup(repo => repo.GetTennisPlayerByIdAsync(playerId))
                       .ReturnsAsync(expectedPlayer);

        // Act
        var result = await _mockRepository.Object.GetTennisPlayerByIdAsync(playerId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(playerId, result.Id);
  
    }

    [Fact]
    public async Task GetAllTennisPlayersAsync_ReturnsAllPlayers()
    {
        // Arrange
        var players = new List<TennisPlayer>
        {
            new TennisPlayer { Id = 1, Firstname = "Player11", Lastname = "PlayerLastName11" },
            new TennisPlayer { Id = 2, Firstname = "playerr22", Lastname = "PlayerLastName22" }
        };

        _mockRepository.Setup(repo => repo.GetAllTennisPlayersAsync())
                       .ReturnsAsync(players);

        // Act
        var result = await _mockRepository.Object.GetAllTennisPlayersAsync();

        // Assert
        Assert.NotNull(result);
       
    }

   
}
