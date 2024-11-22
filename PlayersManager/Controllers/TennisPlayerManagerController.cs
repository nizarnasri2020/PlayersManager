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

        #endregion

        #region Constructor
        public TennisPlayerManagerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        #endregion


      







    }
}
