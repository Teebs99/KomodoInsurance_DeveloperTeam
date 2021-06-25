using KomodoInsurance_DeveloperTeam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsurance
{
    class ProgramUI
    {
        protected readonly DeveloperRepo _devRepo = new DeveloperRepo();
        protected readonly DeveloperTeamRepo _teamRepo = new DeveloperTeamRepo();

        public void Run()
        {
            SeedContent();
            DisplayMenu();

        }

        public void SeedContent()
        {
            Developer dev1 = new Developer(1, "John", true);
            Developer dev2 = new Developer(2, "Tyler", false);
            Developer dev3 = new Developer(3, "Caleb", true);
            Developer dev4 = new Developer(4, "Mike", false);

            _devRepo.AddDevelopers(dev1, dev2, dev3, dev4);

            DeveloperTeam team1 = new DeveloperTeam(1, "Team1");
            DeveloperTeam team2 = new DeveloperTeam(2, "Team2");
            DeveloperTeam team3 = new DeveloperTeam(3, "Team3");
            DeveloperTeam team4 = new DeveloperTeam(4, "Team4");

            _teamRepo.AddTeams(team1, team2, team3, team4);

        }

        public void DisplayMenu()
        {
            
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Enter the number of the option you would like to select\n" +
                "1. Show All Developers\n" +
                "2. Show All Teams\n" +
                "3. Create A Developer\n" +
                "4. Create A Team\n" +
                "5. Add Developers To A Team\n" +
                "6. Remove Developers From A Team\n" +
                "7. Remove A Team\n" +
                "8. Delete Developer\n" +
                "9. Generate Monthly Report\n" +
                "10. Exit");

            string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        ShowDevelopers();
                        ToContinue();
                        break;
                    case "2":
                        ShowTeams();
                        ToContinue();
                        break;
                    case "3":
                        CreateDev();
                        break;
                    case "4":
                        CreateTeam();
                        break;
                    case "5":
                        AddDevToTeam();
                        break;
                    case "6":
                        RemoveDevFromTeam();
                        break;
                    case "7":
                        RemoveTeam();
                        break;
                    case "8":
                        DeleteDev();
                        break;
                    case "9":
                        MonthlyReport();
                        break;
                    case "10":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a number between 1 and 9");
                        ToContinue();
                        break;
                }
            }
        }
        public void ToContinue()
        {
            Console.WriteLine("Press Any Key To Continue");
            Console.ReadKey();
        }
        public void PrintDeveloper(Developer dev)
        {
            Console.WriteLine("User Id: " + dev.UserId);
            Console.WriteLine("Name: " + dev.Name);
            Console.WriteLine("Software Access: " + dev.SoftwareAccess);
            Console.WriteLine("Has Team: " + dev.HasTeam);
            Console.WriteLine("-----------------------------------------");
        }
        public void PrintTeam(DeveloperTeam team)
        {
            Console.WriteLine("User Id: " + team.TeamId);
            Console.WriteLine("Name: " + team.TeamName);
            Console.WriteLine("Team Members: ");
            foreach (Developer dev in team.TeamMembers) //Prints the developers on the team
            {
                Console.Write(dev.Name + ",");

            }
            Console.WriteLine("\n--------------------------------------------");
        }
        public void ShowDevelopers()//Loops through the list of developers and prints them
        {
            Console.Clear();
            List<Developer> devs = _devRepo.GetDevelopers();
            foreach (Developer dev in devs)
            {
                PrintDeveloper(dev);
            }
        }
        public void ShowTeams()
        {
            Console.Clear();
            List<DeveloperTeam> teams = _teamRepo.GetTeams();
            foreach (DeveloperTeam team in teams)
            {
                PrintTeam(team);
            }

        }
        public void CreateDev()
        {
            Console.Clear();
            Console.WriteLine("Enter the dev's id");
            int id = int.Parse(Console.ReadLine());
            foreach(Developer dev in _devRepo.GetDevelopers())
            {
                if(dev.UserId == id) //Checks to see if the Id already exists
                {
                    int count = _devRepo.GetDevelopers().Count;
                    List<Developer> devs = _devRepo.GetDevelopers();
                    List<Developer> orderedList = devs.OrderBy(x => x.UserId).ToList();//Orders the List by the User ID
                    Developer lastDev = orderedList[count - 1]; //Gets the last developer in the list
                    id = lastDev.UserId + 1; //Increase the new developers id to one higher than last developer in the list
                    Console.WriteLine("ID Already in Use; ID auto assigned");
                    ToContinue();
                    break;
                }
            }
            Console.WriteLine("Enter the dev's name");
            string name = Console.ReadLine();
            Console.WriteLine("Do they have access to the software? T/F");
            string access = Console.ReadLine().ToLower();
            bool software = (access == "t") ? true : false;
            _devRepo.AddDeveloper(new Developer(id, name, software));
            ToContinue();
        }
        public void CreateTeam()
        {
            Console.Clear();
            Console.WriteLine("Enter the team's id");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the team's name");
            string name = Console.ReadLine();
            _teamRepo.AddTeam(new DeveloperTeam(id, name));
            ToContinue();
        }
        public void AddDevToTeam()
        {
            bool keepAdding = true;
            List<Developer> devs = _devRepo.GetDevelopers();

            Console.Clear();
            ShowTeams();

            Console.WriteLine("Select team by their team id");
            int teamid = int.Parse(Console.ReadLine());
            
            while (keepAdding) //Allows the user to add multiple devs to a team at once
            {
                Console.Clear();
                foreach (Developer dev in devs) //Find every dev that doesn't have a team and prints them to the UI
                {
                    if (!dev.HasTeam) { PrintDeveloper(dev); }
                }
                Console.WriteLine("Select the dev you want to add by ID");
                Developer devToAdd = _devRepo.GetDeveloperById(int.Parse(Console.ReadLine()));
                DeveloperTeam team = _teamRepo.GetTeamById(teamid);
                team.AddDevToTeam(devToAdd);
                Console.WriteLine("Would you like to keep adding devs to this team? Y/N");
                string answer = Console.ReadLine().ToLower();
                keepAdding = (answer == "y");
            }

            Console.Clear();
            PrintTeam(_teamRepo.GetTeamById(teamid)); // Prints the newly updated team

            Console.WriteLine("\nUpdated Team");
            ToContinue();
        }

        public void RemoveDevFromTeam()
        {
            Console.Clear();
            ShowTeams();
            Console.WriteLine("Select Team By Id");
            int teamid = int.Parse(Console.ReadLine());
            DeveloperTeam team = _teamRepo.GetTeamById(teamid);
            Console.Clear();
            foreach (Developer dev in team.TeamMembers) //Gets each dev in the team and prints them out
            {
                PrintDeveloper(dev);
            }
            Console.WriteLine("\nSelect Developer to remove by id");
            int devId = int.Parse(Console.ReadLine());
            Developer devToRemove = _devRepo.GetDeveloperById(devId);
            team.RemoveDev(devToRemove);
            ToContinue();

        }
        public void RemoveTeam()
        {
            Console.Clear();
            ShowTeams();
            Console.WriteLine("\nSelect Team To Remove By Their Id");
            int teamid = int.Parse(Console.ReadLine());
            DeveloperTeam team = _teamRepo.GetTeamById(teamid);
            _teamRepo.DeleteTeam(team);
            ToContinue();
        }
        public void DeleteDev()
        {
            Console.Clear();
            ShowDevelopers();
            Console.WriteLine("\nSelect Dev To Delete By Their Id");
            int devId = int.Parse(Console.ReadLine());
            Developer dev = _devRepo.GetDeveloperById(devId);
            _devRepo.DeleteDeveloper(dev);
            ToContinue();
        }

        public void MonthlyReport()
        {
            Console.Clear();
            List<Developer> devReport = _devRepo.GetMonthlyReport(); //Finds all Dev's without software access
            foreach(Developer dev in devReport) //Loops through all devs without access and prints them
            {
                Console.WriteLine(dev.Name + " does not have software access");
            }
            ToContinue();
        }
    }
}
