using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vue24Hour.Domain.Model;
using Vue24Hour.Domain.Model.Requests;
using Vue24Hour.Domain.Repository;

namespace Vue24Hour.Persistence
{
    public sealed class DomainContext : IGameRepository, ITeamRepository
    {
        //TODO connect to database
        private List<Game> games;
        private List<Team> teams;

        public DomainContext()
        {
            games = new List<Game>();
            teams = new List<Team>
            {
                new Team
                {
                    Id = Guid.NewGuid(),
                    Name = "Rood",
                    Color = "red"
                },
                new Team
                {
                    Id = Guid.NewGuid(),
                    Name = "Geel",
                    Color = "yellow"
                },
                new Team
                {
                    Id = Guid.NewGuid(),
                    Name = "Blauw",
                    Color = "blue"
                },
                new Team
                {
                    Id = Guid.NewGuid(),
                    Name = "Groen",
                    Color = "green"
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
                Teams = createGameRequest.SelectedTeams,
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

        public Team GetTeam(Guid id, params Expression<Func<Team, dynamic>>[] includeProperties)
        {
            return teams.Single(_ => _.Id == id);
        }

        public IEnumerable<Team> GetAllTeams(params Expression<Func<Team, dynamic>>[] includeProperties)
        {
            return teams;
        }
    }
}
