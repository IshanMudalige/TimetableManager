using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.BuildingDAO
{
    class Building
    {
        private String b_name;

        public Building() { }

        public Building(String buildingName)
        {
            this.b_name = buildingName;
        }

        public String BuildingName { get => b_name; set => b_name = value; }
    }
}
