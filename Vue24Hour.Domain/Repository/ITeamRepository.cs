using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Vue24Hour.Domain.Model;

namespace Vue24Hour.Domain.Repository
{
    public interface ITeamRepository
    {
        Team GetTeam(Guid id, params Expression<Func<Team, dynamic>>[] includeProperties);
        IEnumerable<Team> GetAllTeams(params Expression<Func<Team, dynamic>>[] includeProperties);
    }
}