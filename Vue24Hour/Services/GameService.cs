using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoCoordinatePortable;
using Vue24Hour.Domain.Model;
using Vue24Hour.Domain.Model.Requests;
using Vue24Hour.Domain.Repository;
using Vue24Hour.Models;

namespace Vue24Hour.Services
{
    public class GameService : IGameService
    {
        private IGameRepository _gameRepository;
        private ITeamRepository _teamRepository;

        public GameService(IGameRepository gameRepository, ITeamRepository teamRepository)
        {
            _gameRepository = gameRepository;
            _teamRepository = teamRepository;
        }

        public Task<IEnumerable<GameItemModel>> GetItems()
        {
            var allGames = _gameRepository.GetAllGames();
            return Task.FromResult(allGames.Select(_ => GameItemModel.MapFrom(_)).AsEnumerable());
        }

        public Task<GameItemModel> GetGame(Guid id)
        {
            var game = GameItemModel.MapFrom(_gameRepository.GetGame(id));
            return Task.FromResult(game);
        }

        public Task CreateGame(CreateGameRequest createGameRequest)
        {
            createGameRequest.SelectedTeams =
                createGameRequest.Teams.Select(_ => _teamRepository.GetTeam(new Guid(_))).ToArray();
            _gameRepository.CreateGame(createGameRequest);
            return Task.CompletedTask;
        }

        public void ActivateGame(Guid id)
        {
            _gameRepository.SetGameState(id, GameState.Running);
        }

        public Task<IEnumerable<TeamItemModel>> GetTeams()
        {
            return Task.FromResult(_teamRepository.GetAllTeams().Select(TeamItemModel.MapFrom));
        }

        public void CreateTestEvents(Guid id)
        {
            var game = _gameRepository.GetGame(id);
            var teams = game.Teams;
            var firstTeam = teams.ToArray()[0];
            var secondTeam = teams.ToArray()[1];
            var gameStartMoment = game.StartDate.Date;

            game.ControlEvents = new List<ControlEvent>()
            {
                new ControlEvent()
                {
                    Id = Guid.NewGuid(),
                    StartDateTime = gameStartMoment,
                    EndDateTime = gameStartMoment.AddMinutes(1),
                    Quadrant = game.Quadrants.First(),
                    Team = firstTeam
                },
                new ControlEvent()
                {
                    Id = Guid.NewGuid(),
                    StartDateTime = gameStartMoment,
                    EndDateTime = gameStartMoment.AddMinutes(1),
                    Quadrant = game.Quadrants.Last(),
                    Team = secondTeam
                },
                new ControlEvent()
                {
                    Id = Guid.NewGuid(),
                    StartDateTime = gameStartMoment.AddMinutes(1),
                    EndDateTime = gameStartMoment.AddMinutes(2),
                    Quadrant = game.Quadrants.ToArray()[1],
                    Team = firstTeam
                },
                new ControlEvent()
                {
                    Id = Guid.NewGuid(),
                    StartDateTime = gameStartMoment.AddMinutes(1),
                    EndDateTime = gameStartMoment.AddMinutes(2),
                    Quadrant = game.Quadrants.ToArray()[14],
                    Team = secondTeam
                }
            };
        }

        public void JoinTeam(Guid teamId, string userName, Guid gameId)
        {
            var game = _gameRepository.GetGame(gameId);
            var team = game.Teams.Single(_ => _.Id == teamId);
            var newPlayer = new Player {Id = Guid.NewGuid(), AccountName = userName, Name = userName};
            team.Players.Add(newPlayer);
        }

        public void ClaimPosition(Guid gameId, string username, string latitude, string longtitude)
        {
            var game = _gameRepository.GetGame(gameId);
            var teamOfPlayer = GetTeamFromUser(username, game);
            var now = DateTime.Now;
            var exactStartTime = new DateTime(game.StartDate.Year, game.StartDate.Month, game.StartDate.Day,
                game.StartTime.Hour, game.StartTime.Minute, game.StartTime.Second);
            var endDateTime = exactStartTime.AddHours(24); //TODO add game duration
            var doubleLatitude = double.Parse(latitude);
            var doubleLongtitude = double.Parse(longtitude);
            var (claimedQuadant, claimedDist) = GetQuadrantFromPosition(doubleLatitude, doubleLongtitude, game.Quadrants);

            var claimEvent = new ControlEvent
            {
                Id = Guid.NewGuid(),
                StartDateTime = now,
                EndDateTime = endDateTime,
                Quadrant = claimedQuadant,
                Team = teamOfPlayer
            };
            game.ControlEvents.Add(claimEvent);
        }

        /// <summary>
        /// Gets the quadrant from the suplied position of the user.
        /// The quadrant returned will also return a distance to the center.
        /// </summary>
        private (Quadrant, int) GetQuadrantFromPosition(double latitude, double longtitude, ICollection<Quadrant> gameQuadrants)
        {
            Quadrant quadrantResult = null;
            double lowestDistance = 100000;

            var userLocation = new GeoCoordinate(latitude, longtitude);

            foreach (var quadrant in gameQuadrants)
            {
                var distanceToCenterPoint = userLocation.GetDistanceTo(quadrant.CenterPoint);
                if(distanceToCenterPoint < lowestDistance)
                {
                    lowestDistance = distanceToCenterPoint;
                    quadrantResult = quadrant;
                }
            }

            return (quadrantResult, (int) lowestDistance);
        }


        private static Team GetTeamFromUser(string username, Game game)
        {
            Team resultTeam = null;
            foreach (var team in game.Teams)
            {
                if (team.Players.Any(p => p.AccountName == username))
                {
                    resultTeam = team;
                    break;
                }
            }
            return resultTeam;
        }
    }
}
