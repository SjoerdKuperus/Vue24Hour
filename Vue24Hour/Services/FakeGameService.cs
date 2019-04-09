using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vue24Hour.Domain.Model;
using Vue24Hour.Domain.Model.Requests;
using Vue24Hour.Domain.Repository;
using Vue24Hour.Models;

namespace Vue24Hour.Services
{
    public class FakeGameService : IGameService
    {
        private IGameRepository _gameRepository;
        private ITeamRepository _teamRepository;

        public FakeGameService(IGameRepository gameRepository, ITeamRepository teamRepository)
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

        public void CreateGame(CreateGameRequest createGameRequest)
        {
            createGameRequest.SelectedTeams =
                createGameRequest.Teams.Select(_ => _teamRepository.GetTeam(new Guid(_))).ToArray();
            _gameRepository.CreateGame(createGameRequest);
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
    }
}
