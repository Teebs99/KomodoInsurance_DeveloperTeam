using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsurance_DeveloperTeam
{
    public class DeveloperTeam
    {
        public int TeamId { get; }
        public string TeamName { get; set; }
        public List<Developer> TeamMembers { get; set; }

        public DeveloperTeam(int id, string name, List<Developer> devs)
        {
            TeamId = id;
            TeamName = name;
            TeamMembers = devs;
        }
        public DeveloperTeam(int id, string name)
        {
            TeamId = id;
            TeamName = name;
            TeamMembers = new List<Developer>();
        }

        public void AddDevToTeam(Developer dev)
        {
            TeamMembers.Add(dev);
            dev.HasTeam = true;
        }
        public void RemoveDev(Developer dev)
        {
            dev.HasTeam = false;
            TeamMembers.Remove(dev);
            
        }

    }
}
