using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vue24Hour.Domain.Model;
using Vue24Hour.Domain.Model.Requests;
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
                    StartDate = DateTime.Today,
                    GameState = GameState.Startup,
                },
                new Game
                {
                    Id = Guid.NewGuid(),
                    Name = "Spel Oud",
                    StartDate = DateTime.Today.AddDays(-10),
                    GameState = GameState.Startup,
                }
            };
        }

        public Game GetGame(Guid id, params Expression<Func<Game, dynamic>>[] includeProperties)
        {
            return games.Single(_ => _.Id == id);
        }

        public void CreateGame(CreateGameRequest createGameRequest)
        {
            games.Add(new Game
            {
                Name = createGameRequest.Name,
                StartDate = createGameRequest.StartDate,
                Teams = createGameRequest.Teams,
                GameState = GameState.Startup,
                Id = Guid.NewGuid()
            });
        }

        public void SetGameState(Guid id, GameState gameState)
        {
            var game = games.Single(_ => _.Id == id);
            game.GameState = gameState;
        }

        public IEnumerable<Game> GetAllGames(params Expression<Func<Game, dynamic>>[] includeProperties)
        {
            return games;
        }
    }
}
