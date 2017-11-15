using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Persistence;

namespace StatlerWaldorfCorp.TeamService.Repository
{
    public class MemoryTeamRepository : ITeamRepository
    {
        protected static ICollection<Team> teams;

        public MemoryTeamRepository()
        {
            if (teams == null)
            {
                teams = new List<Team>();
            }
        }

        public MemoryTeamRepository(ICollection<Team> teams)
        {
            teams = teams;
        }
        public IEnumerable<Team> GetTeams() => teams;

        public Team Get(Guid id)
        {
            return teams.FirstOrDefault(t => t.ID == id);

        }
        public void AddTeam(Team t)
        {
            teams.Add(t);
        }
    }
}
