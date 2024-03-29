using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using BAMCIS.GeoJSON;
using GeoCoordinatePortable;
using Newtonsoft.Json;
using Vue24Hour.Domain.Model;
using Vue24Hour.Domain.Model.Requests;
using Vue24Hour.Domain.Repository;

namespace Vue24Hour.Persistence
{
    public sealed class DomainContext : IGameRepository, ITeamRepository, IAccountRepository
    {
        private JsonDatabase _jsonDatabase;
        private string path;

        public DomainContext()
        {
            path = AppDomain.CurrentDomain.BaseDirectory + "\\MapData\\Database.json";
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
                Location = createGameRequest.Location,
                MaximumParticipants = int.Parse(createGameRequest.MaximumParticipants),
                StartDate = DateTime.Today,
                StartTime = DateTime.Now,
                Teams = createGameRequest.SelectedTeams,
                Manager = createGameRequest.Manager,
                GameState = GameState.Startup,
                Id = Guid.NewGuid()
            };
            FillFakeQuadrantData(newGame);
            _jsonDatabase.games.Add(newGame);
            SaveChanges();
        }

        public void FillFakeQuadrantData(Game game)
        {
            //Temp data.
            game.GameCenter = new GeoCoordinate(52.0889063, 5.1217543); //Dom van utrecht //52.0907336,5.1217543

            // Read of the features from geoJson file.

            // get the JSON file content
            var path = AppDomain.CurrentDomain.BaseDirectory + "\\MapData\\TestDataV2.geojson";
            var jsonData = File.ReadAllText(path);

            GeoJson data = JsonConvert.DeserializeObject<GeoJson>(jsonData);
            FeatureCollection features = data as FeatureCollection;
            var polygons = new Dictionary<string, Polygon>();
            var points = new Dictionary<string, Point>();
            if (features != null)
            {
                foreach (var feature in features.Features)
                {
                    var geometry = feature.Geometry;
                    if (geometry.Type == GeoJsonType.Polygon)
                    {
                        var polygon = geometry as Polygon;
                        if (polygon != null)
                        {
                            polygons.Add(feature.Properties["Name"].ToString(), polygon);
                        }
                    }
                    else if (geometry.Type == GeoJsonType.Point)
                    {
                        var point = geometry as Point;
                        if (point != null)
                        {
                            points.Add(feature.Properties["Name"].ToString(), point);
                        }
                    }
                }

                foreach (var polygon in polygons)
                {
                    var newQuadrant = new Quadrant();
                    var centerPoint = points[polygon.Key];
                    newQuadrant.CenterPoint = new GeoCoordinate(centerPoint.GetLatitude(), centerPoint.GetLongitude());

                    foreach (var polygonCoords in polygon.Value.Coordinates)
                    {
                        var positions = polygonCoords.Coordinates;
                        foreach (var point in positions)
                        {
                            newQuadrant.Border.Add(new GeoCoordinate(point.Latitude, point.Longitude));
                        }
                    }
                    game.Quadrants.Add(newQuadrant);
                }
            }
        }

        public void SetGameState(Guid id, GameState gameState)
        {
            var game = _jsonDatabase.games.Single(_ => _.Id == id);
            game.GameState = gameState;
            SaveChanges();
        }

        public IEnumerable<Game> GetAllGames(params Expression<Func<Game, dynamic>>[] includeProperties)
        {
            return _jsonDatabase.games.OrderBy(_ => _.StartDate);
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
