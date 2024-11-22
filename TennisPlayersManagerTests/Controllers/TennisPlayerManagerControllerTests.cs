using Moq;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TennisPlayerApplication.DTOs;
using TennisPlayerApplication.Interfaces;
using PlayersManager.Controllers;
using Xunit;
using Microsoft.Extensions.Logging;

namespace PlayersManager.Tests
{
    public class TennisPlayerManagerControllerTests
    {
        private  Mock<IPlayerService> _mockPlayerService;
        private  Mock<ILogger<TennisPlayerManagerController>> _mockLogger;
        private  TennisPlayerManagerController _controller;

        public TennisPlayerManagerControllerTests()
        {
            _mockPlayerService = new Mock<IPlayerService>();
            _mockLogger = new Mock<ILogger<TennisPlayerManagerController>>();
            _controller = new TennisPlayerManagerController(_mockPlayerService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllPlayers_ReturnsOkResult_WhenPlayersExist()
        {
            // Arrange
            var players = new List<TennisPlayerDTO>
            {
                new TennisPlayerDTO { Id = 1, Firstname = "Tennis Player1 Nizar", Lastname = "LastNamePlayer1 Nasri" },
                new TennisPlayerDTO { Id = 99999, Firstname = "Tennis PlayerNizar9999", Lastname = "LastNamePlayerNasri9999" }
            };
            _mockPlayerService.Setup(service => service.GetAllTennisPlayers()).ReturnsAsync(players);

            // Act
            var result = await _controller.GetAllPlayers();

            // Assert
            var okResult = Assert.IsType<ActionResult<List<TennisPlayerDTO>>>(result);  // Assert that the result is ActionResult<List<TennisPlayerDTO>>
            var okObjectResult = Assert.IsType<OkObjectResult>(okResult.Result);  // Assert that the  result is OkObjectResult
            var returnValue = Assert.IsType<List<TennisPlayerDTO>>(okObjectResult.Value);  // Ensure the value is of the expected type
          
        }

        [Fact]
        public async Task GetAllPlayers_ReturnsNotFound_WhenNoPlayersExist()
        {
            // Arrange
            _mockPlayerService.Setup(service => service.GetAllTennisPlayers()).ReturnsAsync((List<TennisPlayerDTO>)null);

            // Act
            var result = await _controller.GetAllPlayers();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetPlayerById_ReturnsOkResult_WhenPlayerExists()
        {
            // Arrange
            var player = new TennisPlayerDTO { Id = 1, Firstname = "Tennis Player1 Nizar", Lastname = "LastNamePlayer1 Nasri" };
            _mockPlayerService.Setup(service => service.GetTennisPlayerById(1)).ReturnsAsync(player);

            // Act
            var result = await _controller.GetPlayersById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<TennisPlayerDTO>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetPlayerById_ReturnsNotFound_WhenPlayerDoesNotExist()
        {
            // Arrange
            _mockPlayerService.Setup(service => service.GetTennisPlayerById(1)).ReturnsAsync((TennisPlayerDTO)null);

            // Act
            var result = await _controller.GetPlayersById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task DeletePlayerById_ReturnsNoContent_WhenPlayerIsDeleted()
        {
            // Arrange
            var player = new TennisPlayerDTO { Id = 1, Firstname = "Player1", Lastname = "Test" };
            _mockPlayerService.Setup(service => service.GetTennisPlayerById(1)).ReturnsAsync(player);
            _mockPlayerService.Setup(service => service.DeleteTennisPlayer(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeletePlayerById(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeletePlayerById_ReturnsNotFound_WhenPlayerDoesNotExist()
        {
            // Arrange
            _mockPlayerService.Setup(service => service.GetTennisPlayerById(1)).ReturnsAsync((TennisPlayerDTO)null);

            // Act
            var result = await _controller.DeletePlayerById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeletePlayerById_ReturnsStatusCode500_WhenExceptionOccurs()
        {
            // Arrange
            _mockPlayerService.Setup(service => service.GetTennisPlayerById(It.IsAny<int>())).ThrowsAsync(new Exception("Test Exception"));

            // Act
            var result = await _controller.DeletePlayerById(1);

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            var statusCodeResult = result as StatusCodeResult;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
