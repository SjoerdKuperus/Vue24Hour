using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Vue24Hour.Domain.Model;
using Vue24Hour.Domain.Model.Requests;

namespace Vue24Hour.Domain.Repository
{
    public interface IGameRepository
    {
        Game GetGame(Guid id, params Expression<Func<Game, dynamic>>[] includeProperties);
        IEnumerable<Game> GetAllGames(params Expression<Func<Game, dynamic>>[] includeProperties);
        void CreateGame(CreateGameRequest createGameRequest);
        void SetGameState(Guid id, GameState gameState);
    }
}
