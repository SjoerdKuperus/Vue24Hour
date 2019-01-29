using System;
using System.Linq;
using Vue24Hour.Domain.Model;

namespace Vue24Hour.Models
{
    public class GameItemModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public int QuadrantCount { get; set; }
        public string GameCenter { get; set; }


        public static GameItemModel MapFrom(Game game)
        {
            return new GameItemModel
            {
                Id = game.Id,
                Name = game.Name,
                StartDate = game.StartDate.ToString("dd-MM-yyyy"),
                QuadrantCount = game.Quadrants.Count,
                GameCenter = "" + game.Quadrants.First().CenterPoint.Latitude + ", " + game.Quadrants.First().CenterPoint.Latitude
            };
        }
    }
}
