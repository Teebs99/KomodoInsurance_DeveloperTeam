using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsurance_DeveloperTeam
{
    public class Developer
    {
        public int UserId { get; }
        public bool SoftwareAccess { get; set; }
        public bool HasTeam { get; set; }
        public string Name { get; set; }

        public Developer(int id, string name, bool access)
        {
            UserId = id;
            Name = name;
            SoftwareAccess = access;
            HasTeam = false;
        }
    }
}
