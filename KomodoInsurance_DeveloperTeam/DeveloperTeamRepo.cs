using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsurance_DeveloperTeam
{
    public class DeveloperTeamRepo
    {
        protected readonly List<DeveloperTeam> _devTeams = new List<DeveloperTeam>();

        public bool AddTeam(DeveloperTeam team)
        {
            int count = _devTeams.Count;
            _devTeams.Add(team);
            return count < _devTeams.Count;
        }
        public bool AddTeams(params DeveloperTeam[] args)
        {
            int count = _devTeams.Count();
            foreach (DeveloperTeam team in args)
            {
                _devTeams.Add(team);
            }
            return count < _devTeams.Count;
        }

        public List<DeveloperTeam> GetTeams()
        {
            return _devTeams;
        }
        public DeveloperTeam GetTeamById(int id)
        {
            foreach (var team in _devTeams)
            {
                if (team.TeamId == id)
                {
                    return team;
                }
            }
            return null;
        }
        public bool UpdateTeam(int id, DeveloperTeam team)
        {
            DeveloperTeam devTeam = GetTeamById(id);
            if (devTeam.TeamId == id)
            {
                devTeam.TeamMembers = team.TeamMembers;
                devTeam.TeamName = team.TeamName;
                return true;
            }
            return false;
        }

        public bool DeleteTeam(DeveloperTeam team)
        {
            while(team.TeamMembers.Count > 0)
            {
                team.RemoveDev(team.TeamMembers[0]);
            }
            bool result = _devTeams.Remove(team);
            return result;
        }
    }
}
