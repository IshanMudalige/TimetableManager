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
        private string not_session_day;
        private string  not_session_time;
        private string subject;
        private string subgrpid;

        public NotAvailableSessions()
        { 

        }

        public NotAvailableSessions(int notAvasessionid,string notAvasessionDay, string notAvatime,string notAvasubject, string notAvaSubgrpId) 
        {
            this.not_session_id = notAvasessionid;
            this.not_session_day = notAvasessionDay;
            this.not_session_time = notAvatime;
            this.subject = notAvasubject;
            this.subgrpid = notAvaSubgrpId;
        }

        public int NotAvaSessionID { get => not_session_id; set => not_session_id = value; }

        public string NotAvaSessionDay { get => not_session_day; set => not_session_day = value; }

        public string NotAvaSesionTime { get => not_session_time; set => not_session_time = value; }

        public string NotAvaSubject { get => subject; set => subject = value; }

        public string NotAvaSubgroupId { get => subgrpid; set => subgrpid = value; }

    }
}
