using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Vue24Hour.Domain.Model;

namespace Vue24Hour.Domain.Repository
{
    public interface IGameRepository
    {
        Game GetGame(Guid id, params Expression<Func<Game, dynamic>>[] includeProperties);
        IEnumerable<Game> GetAllGames(params Expression<Func<Game, dynamic>>[] includeProperties);
    }
}
