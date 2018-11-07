using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vue24Hour.Models;

namespace Vue24Hour.Services
{
    public class FakeGameService : IGameService
    {
        public List<GameItemModel> Games { get; set; }

        public FakeGameService()
        {
            var games = new[]
            {
                new GameItemModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Spel 1",
                    StartDate = DateTime.Today.ToString("dd-MM-yyyy")
                },
                new GameItemModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Spel Oud",
                    StartDate = DateTime.Today.AddDays(-10).ToString("dd-MM-yyyy")
                }
            };
            Games = games.ToList();
        }

        public Task<IEnumerable<GameItemModel>> GetItems()
        {
            return Task.FromResult(Games.AsEnumerable());
        }

        public Task<GameItemModel> GetGame(Guid id)
        {
            var result = Games.SingleOrDefault(g => g.Id == id);
            return Task.FromResult(result);
        }
    }
}
