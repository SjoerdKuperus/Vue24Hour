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
        public double[] CenterLocationCoords { get; set; }
        public QuadrantItemModel[] Quadrants { get; set; }

        public static GameItemModel MapFrom(Game game)
        {
            return new GameItemModel
            {
                Id = game.Id,
                Name = game.Name,
                StartDate = game.StartDate.ToString("dd-MM-yyyy"),
                QuadrantCount = game.Quadrants.Count,
                GameCenter = "" + game.GameCenter.Longitude + ", " + game.GameCenter.Latitude,
                CenterLocationCoords = new[] {game.GameCenter.Longitude, game.GameCenter.Latitude},
                Quadrants = game.Quadrants.Select(QuadrantItemModel.MapFrom).ToArray()
            };
        }
    }

    public class QuadrantItemModel
    {
        public double[] CenterPoint { get; set; }
        public BorderItemModel[] Border { get; set; }

        public static QuadrantItemModel MapFrom(Quadrant quadrant)
        {
            var quadrantItem = new QuadrantItemModel();
            quadrantItem.CenterPoint = new[] {quadrant.CenterPoint.Longitude, quadrant.CenterPoint.Latitude};
            quadrantItem.Border = new BorderItemModel[quadrant.Border.Count];
            var index = 0;
            foreach (var border in quadrant.Border)
            {
                var newBorderItemModel = new BorderItemModel {BorderPoint = new[] {border.Longitude, border.Latitude}};
                quadrantItem.Border[index] = newBorderItemModel;
                index++;
            }

            return quadrantItem;
        }
    }

    public class BorderItemModel
    {
        public double[] BorderPoint { get; set; }
    }
}
