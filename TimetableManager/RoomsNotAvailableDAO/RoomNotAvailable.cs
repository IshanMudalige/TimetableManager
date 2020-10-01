using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.RoomsNotAvailableDAO
{
    class RoomNotAvailable
    {
        private String b_name;
        private String r_name;
        private String description;
        private String day;
        private String from;
        private String to;

        public RoomNotAvailable () {}

        public RoomNotAvailable(String b_name, String r_name, String description, String day, String from, String to)
        {
            this.b_name = b_name;
            this.r_name = r_name;
            this.description = description;
            this.day = day;
            this.from = from;
            this.to = to;
        }

        public String BuildingNameNA { get => b_name; set => b_name = value; }

        public String RoomNameNA { get => r_name; set => r_name = value; }

        public String Description { get => description; set => description = value; }

        public String Day { get => day; set => day = value; }

        public String From { get => from; set => from = value; }

        public String To { get => to; set => to = value; }

    }
}
