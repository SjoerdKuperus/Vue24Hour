using System;

namespace Vue24Hour.Domain.Model.Requests
{
    public class CreateGameRequest
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string[] Teams { get; set; }
    }
}