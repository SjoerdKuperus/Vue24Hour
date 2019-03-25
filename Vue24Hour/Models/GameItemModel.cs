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
        public string GameState { get; set; }
        public int QuadrantCount { get; set; }
        public string GameCenter { get; set; }
        public double[] CenterLocationCoords { get; set; }
        public QuadrantItemModel[] Quadrants { get; set; }
        public TeamItemModel[] Teams { get; set; }
        public ControlEventModel[] ControlEvents { get; set; }

        public static GameItemModel MapFrom(Game game)
        {
            return new GameItemModel
            {
                Id = game.Id,
                Name = game.Name,
                StartDate = game.StartDate.ToString("dd-MM-yyyy"),
                GameState = game.GameState.ToString(),
                QuadrantCount = game.Quadrants.Count,
                GameCenter = "" + game.GameCenter.Longitude + ", " + game.GameCenter.Latitude,
                CenterLocationCoords = new[] {game.GameCenter.Longitude, game.GameCenter.Latitude},
                Quadrants = game.Quadrants.Select(QuadrantItemModel.MapFrom).ToArray(),
                Teams = game.Teams.Select(TeamItemModel.MapFrom).ToArray(),
                ControlEvents = game.ControlEvents.Select(ControlEventModel.MapFrom).ToArray()
            };
        }
    }

    public class ControlEventModel
    {
        public Guid TeamId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public Guid QuadrantId { get; set; }
        public string Description { get; set; }

        public static ControlEventModel MapFrom(ControlEvent controlEvent)
        {
            return new ControlEventModel
            {
                TeamId = controlEvent.Team.Id,
                QuadrantId = controlEvent.Quadrant.Id,
                StartDateTime = controlEvent.StartDateTime,
                EndDateTime = controlEvent.EndDateTime,
                Description =  "(" + controlEvent.StartDateTime.ToString("hh:mm:ss") + " / " + controlEvent.EndDateTime.ToString("hh:mm:ss") + ") Team " + controlEvent.Team.Name + " takes area " + controlEvent.Quadrant.Id
            };
        }
    }

    public class TeamItemModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public static TeamItemModel MapFrom(Team team)
        {
            return new TeamItemModel {Id = team.Id, Name = team.Name, Color = team.Color};
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
