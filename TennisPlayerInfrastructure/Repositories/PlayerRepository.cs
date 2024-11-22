using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TennisPlayerDomain.Entities;
using TennisPlayerDomain.Interfaces;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace TennisPlayerInfrastructure.Repositories
{
    /// <summary>
    /// PlayerRepository is a repository that interacts with an external dataset to manage tennis player information.
    /// PlayerRepository provides methods for retrieving all tennis players, retrieving a specific player by ID, and deleting a player.
    /// </summary>
    public class PlayerRepository : IPlayerRepository
    {
        #region Attributes
        private string _dataSetUri;
        private HttpClient _httpClient;
        private ILogger<PlayerRepository> _logger;
        private JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            // Ensures case-sensitive deserialization
            ContractResolver = new DefaultContractResolver()
        };
        #endregion

        #region Constructor 
        public PlayerRepository(IOptions<DatasetConfig> dataSetConfig, HttpClient httpClient, ILogger<PlayerRepository> logger)
        {
            _dataSetUri = dataSetConfig.Value.Uri;
            _httpClient = httpClient;
            _logger = logger; 
        }
        #endregion

        #region Methods
        /// <summary>
        /// Retrieves  all tennis players asynchronously from an external datasource.
        /// </summary>
        /// <returns>List Of tennis players, or null if data cannot be retreived or deserialized</returns>
        public async Task<List<TennisPlayer>> GetAllTennisPlayersAsync()
        {
            try
            {
               
                // Get Data From Provided Uri 
                var dataResponse = await _httpClient.GetStringAsync(_dataSetUri);
               
                var tennisPlayers = JsonConvert.DeserializeObject<PlayerList> (dataResponse, _settings);

                return tennisPlayers!=null ? tennisPlayers.Players : null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception Occurred when Loading DataSet. Exception {ex.Message}");
                return null;
            }
          
        }
        /// <summary>
        /// Retrieves a specific tennis player by id asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the player to retreive</param>
        /// <returns>Tennis player if found, or null if not found</returns>
        public async Task<TennisPlayer> GetTennisPlayerByIdAsync(int id)
        {
            try
            {
                var tennisPlayers = await GetAllTennisPlayersAsync();
                if (tennisPlayers!= null )
                {
                    var tennisPlayer = tennisPlayers.FirstOrDefault(p=>p.Id==id);
                    if (tennisPlayer!= null)
                    {
                        _logger.LogInformation($"Tennis Player {tennisPlayer.Firstname} with Id {id} is found");
                    }
                    else
                    {
                        _logger.LogInformation($"Tennis Player  with Id {id} not found in the Data Set");
                    }
                    return tennisPlayer;
                }
                else
                {
                    _logger.LogInformation("No Players in the DataSet");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception Occured while trying to Get Tennis Player By Id . Exception {ex.Message}");
                return null; ;
            }  
        }


        /// <summary>
        /// Deletes a tennis player by ID asynchronously from the datasource
        /// </summary>
        /// <param name="id">The identifier of the tennis player to delete</param>
        public async Task DeleteTennisPlayerAsync(int id)
        {
            try
            {
                var tennisPlayers = await GetAllTennisPlayersAsync();
                if (tennisPlayers != null)
                {
                    var PlayerToBeDeleted = tennisPlayers.FirstOrDefault(p=>p.Id==id);

                    if (PlayerToBeDeleted != null)
                    {
                        tennisPlayers.Remove(PlayerToBeDeleted);
                        _logger.LogInformation($"Tennis Player with ID {id} deleted successfully ");
                    }
                    else
                    {
                        _logger.LogInformation($"TennisPlayer with ID {id} not found in the dataset");
                    }
                }
                else
                {
                    _logger.LogInformation("No Players in the DataSet");
                  
                }


            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured when trying to delete player. Exception {ex.Message}");

            }
        }



        #endregion



    }
}
