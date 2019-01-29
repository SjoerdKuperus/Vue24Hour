using System;
using System.Collections.Generic;

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
    }

    public class Quadrant
    {
    }

    public enum GameState
    {
        Startup = 0,
        Running = 1,
        Finished = 2,
        Aborted = 3,
    }
}
