using System;
using System.Collections.Generic;
using GeoCoordinatePortable;

namespace Vue24Hour.Domain.Model
{
    public sealed class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int MaximumParticipants { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartTime { get; set; }
        public ICollection<Team> Teams { get; set; }
        public GameState GameState { get; set; }
        public string Manager { get; set; }
        public ICollection<Quadrant> Quadrants { get; set; }
        public GeoCoordinate GameCenter { get; set; }
        public ICollection<ControlEvent> ControlEvents { get; set; }

        public Game()
        {
            Quadrants = new HashSet<Quadrant>();
            ControlEvents = new HashSet<ControlEvent>();
            Teams = new HashSet<Team>();
        }

        public DateTime EndDateTime
        {
            get
            {
                var exactStartTime = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, StartTime.Hour,
                    StartTime.Minute, StartTime.Second);
                var endDateTime = exactStartTime.AddHours(24); //TODO add game duration 
                return endDateTime;
            }
        }
    }
}