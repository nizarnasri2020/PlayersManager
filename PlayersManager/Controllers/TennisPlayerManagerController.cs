using Microsoft.AspNetCore.Mvc;
using TennisPlayerApplication.DTOs;
using TennisPlayerApplication.Interfaces;

namespace PlayersManager.Controllers
{
    /// <summary>
    /// TennisPlayerManager Controller responsible for managing tennis players. Provides endpoints that allows users to retrieve and delete tennis players .
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TennisPlayerManagerController : ControllerBase
    {

        #region Attributes
        private IPlayerService _playerService;
        private  ILogger<TennisPlayerManagerController> _logger;
        #endregion

        #region Constructor
        public TennisPlayerManagerController(IPlayerService playerService, ILogger<TennisPlayerManagerController> logger)
        {
            _playerService = playerService;
            _logger = logger;
        }

        #endregion

        #region Endpoints
        /// <summary>
        /// This endpoint retrieves all tennis players .
        /// </summary>
        /// <returns>List of tennis players sorted by player id </returns>
        [HttpGet("GetAllPlayers")]
        public async Task<ActionResult<List<TennisPlayerDTO>>> GetAllPlayers()
        {
            try
            {
                var players = await _playerService.GetAllTennisPlayers();
                if (players != null)
                {
                    return Ok(players);
                }
                else
                {
                    return NotFound();
                }
                
              
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while getting tennis players.Exception {ex.Message}");
                return StatusCode(500);
            }
           
        }
        /// <summary>
        /// This endpoint retrieves specific tennis player by ID
        /// </summary>
        /// <param name="id">ID of player to retrive</param>
        /// <returns>The Tennis Player with the provided ID oe Not Found id the player is not found </returns>
        [HttpGet("GetPlayerById/{id}")]
        public async Task<ActionResult<List<TennisPlayerDTO>>> GetPlayersById(int id)
        {
            try
            {
                var player = await _playerService.GetTennisPlayerById(id);
                if (player == null)
                {
                    return NotFound();
                }
                return Ok(player);


            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while getting tennis player with id {id}.Exception {ex.Message}");
                return StatusCode(500);
            }

        }

        /// <summary>
        /// This endpoint delete tennis player by unique id
        /// </summary>
        /// <param name="id">ID of player to delete</param>
        /// <returns>NoContent if the deletion is successful, or NotFound if the player is not found</returns>
        [HttpDelete("DeletePlayerById/{id}")]
        public async Task<IActionResult> DeletePlayerById(int id)
        {
            try
            {
                var player = await _playerService.GetTennisPlayerById(id);
                if (player == null)
                {
                    _logger.LogWarning($"Tennis Player with Id {id} not found");
                    return NotFound();
                }

                await _playerService.DeleteTennisPlayer(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while trying to delete tennis player with id {id}.Exception {ex.Message}");
                return StatusCode(500);

            }
          
        }


        #endregion








    }
}
