using System;
using System.Collections.Generic;

namespace Vue24Hour.Domain.Model.Requests
{
    public class CreateGameRequest
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}