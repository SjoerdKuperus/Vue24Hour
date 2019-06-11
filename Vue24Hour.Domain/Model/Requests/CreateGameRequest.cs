using System;

namespace Vue24Hour.Domain.Model.Requests
{
    public class CreateGameRequest
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int MaximumParticipants { get; set; }
        public DateTime StartDate { get; set; }
        public string[] Teams { get; set; }
        public Team[] SelectedTeams { get; set; }
    }
}