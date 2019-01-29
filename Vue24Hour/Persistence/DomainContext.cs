using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vue24Hour.Domain.Model;
using Vue24Hour.Domain.Repository;

namespace Vue24Hour.Persistence
{
    public sealed class DomainContext : IGameRepository
    {
        //TODO connect to database
        private List<Game> games;

        public DomainContext()
        {
            games = new List<Game>
            {
                new Game
                {
                    Id = Guid.NewGuid(),
                    Name = "Spel 1",
                    StartDate = DateTime.Today
                },
                new Game
                {
                    Id = Guid.NewGuid(),
                    Name = "Spel Oud",
                    StartDate = DateTime.Today.AddDays(-10)
                }
            };
        }

        public Game GetGame(Guid id, params Expression<Func<Game, dynamic>>[] includeProperties)
        {
            return games.Single(_ => _.Id == id);
        }

        public IEnumerable<Game> GetAllGames(params Expression<Func<Game, dynamic>>[] includeProperties)
        {
            return games;
        }
    }
}
