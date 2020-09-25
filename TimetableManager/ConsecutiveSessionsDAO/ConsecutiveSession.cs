using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.ConsecutiveSessionsDAO
{
    class ConsecutiveSession
    {
        private int Nsession;
        private string Nsub;
        private   string  session_id_1;
        private string session_id_2;
        private string subject;
        private string tag;
        private string day;
        private string time;
        private string newtag;

        public ConsecutiveSession()
        {

        }

        public ConsecutiveSession(int  nsession,string nsub,string session1, string session2, string subname, string tag,string day, string time, string ntag)
        {
            this.Nsession = nsession;
            this.Nsub = nsub;
            this.session_id_1 = session1;
            this.session_id_2 = session2;
            this.subject = subname;
            this.tag = tag;
            this.day = day;
            this.time = time;
            this.newtag = ntag;

        }


        public int NSession { get => Nsession; set => Nsession = value; }
        public string SessionID1 { get => session_id_1; set => session_id_1 = value; }

        public string SessionID2 { get => session_id_2; set => session_id_2 = value; }

        public string NSubject { get => Nsub; set => Nsub = value; }

        public string ConsecutiveSubject { get => subject; set => subject = value; }

        public string Ntag { get => tag; set => tag = value; }

        public string ConsecutiveDay { get => day; set => day = value; }

        public string ConsecutiveTime { get => time; set => time = value; }

        public string ConsecutiveTag { get => newtag; set => newtag = value; }

    }
}
