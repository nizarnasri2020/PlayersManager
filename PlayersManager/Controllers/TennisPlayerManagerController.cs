using Microsoft.AspNetCore.Mvc;
using TennisPlayerApplication.DTOs;
using TennisPlayerApplication.Interfaces;

namespace PlayersManager.Controllers
{
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
        #endregion








    }
}
