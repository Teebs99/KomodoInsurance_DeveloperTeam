using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsurance_DeveloperTeam
{
    public class DeveloperRepo
    {
        protected readonly List<Developer> _devRepo = new List<Developer>();

        public bool AddDeveloper(Developer dev)
        {

            int count = _devRepo.Count;
            _devRepo.Add(dev);
            return count < _devRepo.Count;
        }

        public bool AddDevelopers(params Developer[] args)
        {
            int count = _devRepo.Count();
            foreach(Developer dev in args)
            {
                _devRepo.Add(dev);
            }
            return count < _devRepo.Count;
        }

        public List<Developer> GetDevelopers()
        {
            return _devRepo;
        }

        public Developer GetDeveloperById(int id)
        {
            foreach (Developer dev in _devRepo)
            {
                if (dev.UserId == id) { return dev; }
            }
            return null;
        }

        public bool UpdateDeveloper(int id, Developer newDev)
        {
            Developer dev = GetDeveloperById(id);
            if (dev != null)
            {
                dev.Name = newDev.Name;
                dev.SoftwareAccess = newDev.SoftwareAccess;
                return true;
            }
            return false;
        }

        public bool DeleteDeveloper(Developer dev)
        {
            return _devRepo.Remove(dev);
        }
        public bool DeleteDeveloperByID(int id)
        {
            var dev = GetDeveloperById(id);
            return _devRepo.Remove(dev);
        }

        public List<Developer> GetMonthlyReport()
        {
            List<Developer> devsWithoutAccess = new List<Developer>();
            foreach(Developer dev in _devRepo)
            {
                if (!dev.SoftwareAccess)
                {
                    devsWithoutAccess.Add(dev);
                }
            }
            return devsWithoutAccess;
        }

        

    }
}
