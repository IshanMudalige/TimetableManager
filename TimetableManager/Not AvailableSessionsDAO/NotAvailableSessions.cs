using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.Not_AvailableSessionsDAO
{
    class NotAvailableSessions
    {
        private int not_session_id;
        private double time;
        private string subject;
        private string subgrpid;

        public NotAvailableSessions()
        { 

        }

        public NotAvailableSessions(int notAvasessionid, double notAvatime,string notAvasubject, string notAvaSubgrpId) 
        {
            this.not_session_id = notAvasessionid;
            this.time = notAvatime;
            this.subject = notAvasubject;
            this.subgrpid = notAvaSubgrpId;
        }

        public int NotAvaSessionID { get => not_session_id; set => not_session_id = value; }

        public double NotAvaSesionTime { get => time; set => time = value; }

        public string NotAvaSubject { get => subject; set => subject = value; }

        public string NotAvaSubgroupId { get => subgrpid; set => subgrpid = value; }

    }
}
