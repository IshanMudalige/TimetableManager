using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.RoomsSubjectDAO
{
    class RoomSubject
    {
        private string year;
        private string s_code;
        private string s_name;
        private string b_name;
        private String r_name;

        public RoomSubject() { }

        public RoomSubject(String year, String subjectCode, String subjectName, string lecBuilding, string lecRoom)
        {
            this.year = year;
            this.s_code = subjectCode;
            this.s_name = subjectName;
            this.b_name = lecBuilding;
            this.r_name = lecRoom;
        }

        public String Year { get => year; set => year = value; }

        public String SubjectCode { get => s_code; set => s_code = value; }

        public String SubjectName { get => s_name; set => s_name = value; }

        public String SubBuildingName { get => b_name; set => b_name = value; }

        public String SubRoomName { get => r_name; set => r_name = value; }
    }
}
