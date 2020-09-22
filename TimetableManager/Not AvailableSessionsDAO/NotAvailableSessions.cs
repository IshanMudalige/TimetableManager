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

        public NotAvailableSessions()
        { 

        }

        public NotAvailableSessions(int notAvasessionid, double notAvatime) 
        {
            this.not_session_id = notAvasessionid;
            this.time = notAvatime;
        }

        public int NotAvaSessionID { get => not_session_id; set => not_session_id = value; }

        public double NotAvaSesionTime { get => time; set => time = value; }

    }
}
