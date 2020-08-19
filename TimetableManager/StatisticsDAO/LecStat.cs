using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.StatisticsDAO
{
    class LecStat
    {
        private string name;
        private string employeeID;
        private string faculty;
        private string department;
        private string center;
        private string building;
        private string category;
        private string level;
        private string rank;

        public LecStat() { }

        public LecStat(string name, string employeeID, string faculty, string department, string center, string building, string category, string level, string rank)
        {
            this.name = name;
            this.employeeID = employeeID;
            this.faculty = faculty;
            this.department = department;
            this.center = center;
            this.building = building;
            this.category = category;
            this.level = level;
            this.rank = rank;
        }

        public string Name { get => name; set => name = value; }

        public string EmployeeID { get => employeeID; set => employeeID = value; }

        public string Faculty { get => faculty; set => faculty = value; }

        public string Department { get => department; set => department = value; }

        public string Center { get => center; set => center = value; }

        public string Building { get => building; set => building = value; }

        public string Category { get => category; set => category = value; }

        public string Level { get => level; set => level = value; }

        public string Rank { get => rank; set => rank = value; }
    }
}

