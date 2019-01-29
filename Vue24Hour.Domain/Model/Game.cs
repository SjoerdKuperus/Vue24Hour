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

        public Game()
        {
            Quadrants = new HashSet<Quadrant> {new Quadrant()};
        }
    }

    public class Quadrant
    {
        public double Width = 1000;
        public double Height = 1000;
        public GeoCoordinate CenterPoint { get; set; }

        //public Geo
        public Quadrant()
        {
            CenterPoint = new GeoCoordinate(52.0906428, 5.1194815);
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
