using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.RoomsLecturerDAO
{
    class RoomLecturer
    {

        private string l_name;
        private string l_located;
        private string b_name;
        private String r_name;

        public RoomLecturer() { }

        public RoomLecturer (string lecName, string lecLocated, string lecBuilding, string lecRoom)
        {
            this.l_name = lecName;
            this.l_located = lecLocated;
            this.b_name = lecBuilding;
            this.r_name = lecRoom;
        }

        public String LecturerName { get => l_name; set => l_name = value; }

        public String LecturerLocated { get => l_located; set => l_located = value; }

        public String LecBuildingName { get => b_name; set => b_name = value; }

        public String LecRoomName { get => r_name; set => r_name = value; }
    }
}
