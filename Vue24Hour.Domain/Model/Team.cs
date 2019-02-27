using System;
using System.Collections.Generic;
using Vue24Hour.Domain.Model.Requests;

namespace Vue24Hour.Domain.Model
{
    public sealed class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Player> Players { get; set; }

        public Team()
        {
            Players = new HashSet<Player>();
        }

        public static Team CreateTeam(string teamName)
        {
            return new Team
            {
                Name = teamName
            };
        }
    }
}