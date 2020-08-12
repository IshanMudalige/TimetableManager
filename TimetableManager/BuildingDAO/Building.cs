using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.BuildingDAO
{
    class Building
    {
        private String buildingName;

        public Building() { }

        public Building(String buildingName)
        {
            this.buildingName = buildingName;
        }

        public String BuildingName { get => buildingName; set => buildingName = value; }
    }
}
