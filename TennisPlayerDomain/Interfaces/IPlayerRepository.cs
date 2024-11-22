using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisPlayerDomain.Entities;

namespace TennisPlayerDomain.Interfaces
{
    /// <summary>
    /// This interface defines the contract for data access operations to tennis players.
    /// </summary>
    public interface IPlayerRepository
    {
        Task<List<TennisPlayer>> GetAllTennisPlayersAsync();
        Task<TennisPlayer> GetTennisPlayerByIdAsync(int id);
        Task DeleteTennisPlayerAsync(int id);
    }
}
