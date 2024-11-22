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
        
        private TennisPlayerDTO MapTennisPlayerDTO (TennisPlayer tennisPlayer)
        {
           return  new TennisPlayerDTO
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
                Age = tennisPlayer.Data?.Age ?? 0
            };
          
        }

        public async Task<List<TennisPlayerDTO>> GetAllTennisPlayers()
        {
            var players =  await _playerRepository.GetAllTennisPlayersAsync();
            // Sort Players By Id 
            return players
                        .OrderBy(p => p.Id) 
                        .Select(p => MapTennisPlayerDTO(p))
                        .ToList();
        }

        public async Task<TennisPlayerDTO> GetTennisPlayerById(int id)
        {
            var player = await _playerRepository.GetTennisPlayerByIdAsync(id);
            return player != null ? MapTennisPlayerDTO(player) : null;
        }
        public async Task DeleteTennisPlayer(int id)
        {
            await _playerRepository.DeleteTennisPlayerAsync(id);
        }
        #endregion
    }
}
