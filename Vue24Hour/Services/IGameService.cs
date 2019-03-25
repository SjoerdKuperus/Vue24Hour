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
        void CreateGame(CreateGameRequest createGameRequest);
        void ActivateGame(Guid id);
        Task<IEnumerable<TeamItemModel>> GetTeams();
    }
}
