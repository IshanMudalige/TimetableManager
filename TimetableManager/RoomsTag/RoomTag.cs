using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.RoomsTag
{
    class RoomTag
    {
        private string tag;
        private string r_type;

        public RoomTag() { }

        public RoomTag(String tag, String roomType)
        {
            this.tag = tag;
            this.r_type = roomType;
        }

        public String Tag { get => tag; set => tag = value; }

        public String RoomTypeTag { get => r_type; set => r_type = value; }
    }
}
