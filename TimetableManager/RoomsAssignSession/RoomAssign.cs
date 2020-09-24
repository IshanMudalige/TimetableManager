using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.RoomsAssignSession
{
    class RoomAssign
    {
        private int sid;
        private string s_code;
        private string tag;
        private string sub_id;
        private string r_name;

        public RoomAssign() { }

        public RoomAssign(int sid,  string s_code, string tag, string sub_id, string roomName)
        {
            this.sid = sid;
            this.s_code = s_code;
            this.tag = tag;
            this.sub_id = sub_id;
            this.r_name = roomName;
        }

        public int Sid { get => sid; set => sid = value; }

        public string Scode { get => s_code; set => s_code = value; }

        public string Tag { get => tag; set => tag = value; }

        public string SubID { get => sub_id; set => sub_id = value; }

        public string RoomName { get => r_name; set => r_name = value; }

    }
}
