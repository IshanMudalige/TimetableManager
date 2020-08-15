using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.RoomsDAO
{
    class Room
    {
        private String b_name;
        private String r_name;
        private String r_type;
        private int capacity;

        public Room() { }

        public Room(String buildingName, string roomName, string type, int capacity)
        {
            this.b_name = buildingName;
            this.r_name = roomName;
            this.r_type = type;
            this.capacity = capacity;
        }

        public String BuildingName { get => b_name; set => b_name = value; }

        public String RoomName { get => r_name; set => r_name = value; }

        public String RoomType { get => r_type; set => r_type = value; }

        public int Capacity { get => capacity; set => capacity = value; }


    }
}
