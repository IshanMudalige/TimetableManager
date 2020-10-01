using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.RoomsConsecutive
{
    class RoomConsecutive
    {
        private int csid;
        private string subject;
        private string day;
        private string time;
        private string newtag;
        private string room;

        public RoomConsecutive() { }

        public RoomConsecutive(int csid, string subname, string day, string time, string ntag, string room)
        {
            this.csid = csid;
            this.subject = subname;
            this.day = day;
            this.time = time;
            this.newtag = ntag;
            this.room = room;
        }

        public int ConsSesIdNew { get => csid; set => csid = value; }

        public string SubjectCons { get => subject; set => subject = value; }

        public string DayCons { get => day; set => day = value; }

        public string TimeCons { get => time; set => time = value; }

        public string TagCons { get => newtag; set => newtag = value; }

        public string RoomCons { get => room; set => room = value; }

    }
}
