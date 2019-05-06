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
    public sealed class DomainContext : IGameRepository, ITeamRepository, IAccountRepository
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
            var newGame = new Game
            {
                Name = createGameRequest.Name,
                StartDate = createGameRequest.StartDate,
                Teams = createGameRequest.SelectedTeams,
                GameState = GameState.Startup,
                Id = Guid.NewGuid()
            };
            newGame.FillFakeQuadrantData();
            _jsonDatabase.games.Add(newGame);
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

        public IEnumerable<Account> GetAllAccounts(params Expression<Func<Account, dynamic>>[] includeProperties)
        {
            return _jsonDatabase.accounts;
        }

        public void CreateAccount(string phone, string password, string name)
        {
            _jsonDatabase.accounts.Add(new Account
            {
                Name = name,
                Password = password,
                Phone = phone,
                Id = Guid.NewGuid()
            });
            SaveChanges();
        }

        public Account GetAccount(string phone, string password)
        {
            return _jsonDatabase.accounts.SingleOrDefault(_ => _.Phone == phone && _.Password == password);
        }
    }

    public class JsonDatabase
    {
        public List<Game> games;
        public List<Team> teams;
        public List<Account> accounts;

        public JsonDatabase()
        {
            games = new List<Game>();
            teams = new List<Team>();
            accounts = new List<Account>();
        }
    }
}
