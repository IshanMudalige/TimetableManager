using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.RoomsGroupDAO
{
    class RoomGroup
    {
        private string g_id;
        private string sg_id;
        private string b_name;
        private string r_name;

        public RoomGroup() { }

        public RoomGroup(String groupId, String subgroupId, String groupBuilding, String groupRoom)
        {
            this.g_id = groupId;
            this.sg_id = subgroupId;
            this.b_name = groupBuilding;
            this.r_name = groupRoom;
        }

        public String GroupIdRoom { get => g_id; set => g_id = value; }

        public String SubGroupIdRoom { get => sg_id; set => sg_id = value; }

        public String GroupBuildingName { get => b_name; set => b_name = value; }

        public String GroupRoomName { get => r_name; set => r_name = value; }
    }

    
   
}
