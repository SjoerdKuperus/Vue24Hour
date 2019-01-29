using System;
using System.Collections.Generic;

namespace Vue24Hour.Domain.Model
{
    public sealed class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}