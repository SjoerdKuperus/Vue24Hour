using System;
using System.Collections.Generic;
using GeoCoordinatePortable;

namespace Vue24Hour.Domain.Model
{
    public sealed class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public ICollection<Team> Teams { get; set; }
        public GameState GameState { get; set; }
        public ICollection<Quadrant> Quadrants { get; set; }
        public GeoCoordinate GameCenter { get; set; }

        public Game()
        {
            Quadrants = new HashSet<Quadrant>();

            //Temp data.
            GameCenter = new GeoCoordinate(52.0907336, 5.1217543); //Dom van utrecht //52.0907336,5.1217543

            var center = new Quadrant();
            center.CenterPoint = new GeoCoordinate(52.0907336, 5.1217543);
            center.Border.Add(new GeoCoordinate(52.0914795, 5.1198595)); //Start is same as finish
            center.Border.Add(new GeoCoordinate(52.092018, 5.1221396));
            center.Border.Add(new GeoCoordinate(52.091921, 5.122864));
            center.Border.Add(new GeoCoordinate(52.0914032, 5.1233372));
            center.Border.Add(new GeoCoordinate(52.0900502, 5.123933));
            center.Border.Add(new GeoCoordinate(52.0897459, 5.1219568));
            center.Border.Add(new GeoCoordinate(52.0897432, 5.1210768));
            center.Border.Add(new GeoCoordinate(52.0904053, 5.1204616));
            center.Border.Add(new GeoCoordinate(52.0909523, 5.1203076));
            center.Border.Add(new GeoCoordinate(52.0914795, 5.1198595));
            Quadrants.Add(center);
        }
    }

    public class Quadrant
    {
        public double Width = 1000;
        public double Height = 1000;
        public GeoCoordinate CenterPoint { get; set; }
        public ICollection<GeoCoordinate> Border { get; set; }

        public Quadrant()
        {
            CenterPoint = GeoCoordinate.Unknown;
            Border = new HashSet<GeoCoordinate>();
        }
    }

    public enum GameState
    {
        Startup = 0,
        Running = 1,
        Finished = 2,
        Aborted = 3,
    }
}