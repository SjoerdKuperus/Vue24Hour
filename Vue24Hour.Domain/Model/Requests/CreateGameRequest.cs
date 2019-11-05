using System;

namespace Vue24Hour.Domain.Model.Requests
{
    public class CreateGameRequest
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string MaximumParticipants { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string[] Teams { get; set; }
        public Team[] SelectedTeams { get; set; }
        public string Manager { get; set; }
    }
}