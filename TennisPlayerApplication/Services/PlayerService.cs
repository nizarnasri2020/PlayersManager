using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TennisPlayerApplication.DTOs;
using TennisPlayerApplication.Interfaces;
using TennisPlayerDomain.Entities;
using TennisPlayerDomain.Interfaces;
using TennisPlayerInfrastructure.Repositories;

namespace TennisPlayerApplication.Services
{
    /// <summary>
    /// PlayerService provide services for managing tennis players
    /// </summary>
    public class PlayerService :IPlayerService
    {
        #region Attributes
        private IPlayerRepository _playerRepository;
        private ILogger<PlayerService> _logger; 

        #endregion
        #region Constructor
        public PlayerService(IPlayerRepository playerRepository, ILogger<PlayerService> logger)
        {
            _playerRepository=playerRepository;
            _logger=logger;
        }
        #endregion
        #region Methods
        /// <summary>
        /// This method map tennis player entity to tennis player DTO
        /// </summary>
        /// <param name="tennisPlayer">Tennis Player is the entity to map</param>
        /// <returns>Tennis Player DTO or null if exception occurred </returns>
        private TennisPlayerDTO MapTennisPlayerDTO (TennisPlayer tennisPlayer)
        {
            try
            {
                return new TennisPlayerDTO
                {
                    Id = tennisPlayer.Id,
                    Firstname = tennisPlayer.Firstname,
                    Lastname = tennisPlayer.Lastname,
                    Shortname = tennisPlayer.Shortname,
                    Sex = tennisPlayer.Sex,
                    CountryCode = tennisPlayer.Country?.Code,
                    CountryPicture = tennisPlayer.Country?.Picture,
                    Picture = tennisPlayer.Picture,
                    Rank = tennisPlayer.Data?.Rank ?? 0,
                    Points = tennisPlayer.Data?.Points ?? 0,
                    Weight = tennisPlayer.Data?.Weight ?? 0,
                    Height = tennisPlayer.Data?.Height ?? 0,
                    Age = tennisPlayer.Data?.Age ?? 0,
                    Last = tennisPlayer.Data?.Last ?? new List<int>()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception Occured while trying to Map Tennis Player.Exception {ex.Message}");
                return null;
            }
         
          
        }
        /// <summary>
        /// Retreives all tennis players sorted by ID .
        /// </summary>
        /// <returns>Task that represents async operation containing list of tennisPlayer DTO</returns>
        public async Task<List<TennisPlayerDTO>> GetAllTennisPlayers()
        {
            try
            {
                var players = await _playerRepository.GetAllTennisPlayersAsync();
                // Sort Players By Id 
                return players
                            .OrderBy(p => p.Id)
                            .Select(p => MapTennisPlayerDTO(p))
                            .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while trying to get all tennis players. Exception : {ex.Message}");
                return null;
            }
           
        }
        /// <summary>
        /// Retreive tennis player by ID.
        /// </summary>
        /// <param name="id">The identifier of the tennis player</param>
        /// <returns>Task that represents async operation containing  tennisPlayer DTO</returns>
        public async Task<TennisPlayerDTO> GetTennisPlayerById(int id)
        {
            try
            {
                var player = await _playerRepository.GetTennisPlayerByIdAsync(id);
                return player != null ? MapTennisPlayerDTO(player) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while trying to get  tennis player with id {id}. Exception : {ex.Message}");
                return null;
            }
          
        }
        /// <summary>
        /// Delete tennis player by id
        /// </summary>
        /// <param name="id">The identifier of the tennis player</param>
        /// <returns>Task respresenting async operation</returns>
        public async Task DeleteTennisPlayer(int id)
        {
            await _playerRepository.DeleteTennisPlayerAsync(id);
        }
        #endregion
    }
}
