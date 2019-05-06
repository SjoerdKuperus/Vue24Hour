using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Vue24Hour.Domain.Model;
using Vue24Hour.Domain.Model.Requests;
using Vue24Hour.Domain.Repository;

namespace Vue24Hour.Persistence
{
    public sealed class DomainContext : IGameRepository, ITeamRepository
    {
        private JsonDatabase _jsonDatabase;
        private string path = "C:\\Ontwik\\Innovation\\Vue24Hour\\Vue24Hour\\MapData\\Database.json";

        public DomainContext()
        {
            var jsonData = File.ReadAllText(path);
            _jsonDatabase = JsonConvert.DeserializeObject<JsonDatabase>(jsonData);
        }

        public Game GetGame(Guid id, params Expression<Func<Game, dynamic>>[] includeProperties)
        {
            return _jsonDatabase.games.Single(_ => _.Id == id);
        }

        public void CreateGame(CreateGameRequest createGameRequest)
        {
            _jsonDatabase.games.Add(new Game
            {
                Name = createGameRequest.Name,
                StartDate = createGameRequest.StartDate,
                Teams = createGameRequest.SelectedTeams,
                GameState = GameState.Startup,
                Id = Guid.NewGuid()
            });
            SaveChanges();
        }

        public void SetGameState(Guid id, GameState gameState)
        {
            var game = _jsonDatabase.games.Single(_ => _.Id == id);
            game.GameState = gameState;
            SaveChanges();
        }

        public IEnumerable<Game> GetAllGames(params Expression<Func<Game, dynamic>>[] includeProperties)
        {
            return _jsonDatabase.games;
        }

        public Team GetTeam(Guid id, params Expression<Func<Team, dynamic>>[] includeProperties)
        {
            return _jsonDatabase.teams.Single(_ => _.Id == id);
        }

        public IEnumerable<Team> GetAllTeams(params Expression<Func<Team, dynamic>>[] includeProperties)
        {
            return _jsonDatabase.teams;
        }

        private void SaveChanges()
        {
            var newData = JsonConvert.SerializeObject(_jsonDatabase);
            File.WriteAllText(path, newData);
        }
    }

    public class JsonDatabase
    {
        public List<Game> games;
        public List<Team> teams;

        public JsonDatabase()
        {
            games = new List<Game>();
            teams = new List<Team>();
        }
    }
}
