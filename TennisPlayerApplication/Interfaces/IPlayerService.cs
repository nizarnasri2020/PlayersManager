using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisPlayerApplication.DTOs;

namespace TennisPlayerApplication.Interfaces
{
    /// <summary>
    ///  This Interface defines the contract for tennis player operations in the application.
    /// </summary>
    public interface IPlayerService
    {
        Task<List<TennisPlayerDTO>> GetAllTennisPlayers();
        Task<TennisPlayerDTO> GetTennisPlayerById(int id);
        Task DeleteTennisPlayer(int id);
    }
}
