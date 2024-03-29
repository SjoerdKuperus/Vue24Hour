using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vue24Hour.Domain.Model.Requests;
using Vue24Hour.Models;

namespace Vue24Hour.Services
{
    public interface IGameService
    {
        Task<IEnumerable<GameItemModel>> GetItems();
        Task<GameItemModel> GetGame(Guid id);
        Task CreateGame(CreateGameRequest createGameRequest);
        void ActivateGame(Guid id);
        Task<IEnumerable<TeamItemModel>> GetTeams();
        void CreateTestEvents(Guid id);
        void JoinTeam(Guid teamId, string userName, Guid gameId);
        void ClaimPosition(Guid gameId, string username, string latitude, string longtitude);
    }
}
