using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.ParallelSessionsDAO
{
    class ParallelSession
    {
        private int Session_id1;
        private int Session_id2;
        private int Session_id3;
        private int Session_id4;
        private string day;
        private string time;
        private string psubject;
        private int Npsessions;
        private string Ntag;
        private double Nduration;
        private string Nsubjname;
        private string Nsubgrpid;

        public ParallelSession()
        {

        }

        public ParallelSession(int session1, int session2, int session3, int session4, string pday,string ptime, string subject, int nsessions,string ntag,double nduration,string nsubjname,string nsubid )
        {
            this.Session_id1 = session1;
            this.Session_id2 = session2;
            this.Session_id3 = session3;
            this.Session_id4 = session4;
            this.day = pday;
            this.time = ptime;
            this.psubject = subject;
            this.Npsessions = nsessions;
            this.Ntag = ntag;
            this.Nduration = nduration;
            this.Nsubjname = nsubjname;
            this.Nsubgrpid = nsubid;
            

        }

        public int PSession1 { get => Session_id1; set => Session_id1 = value; }
        public int PSession2 { get => Session_id2; set => Session_id2 = value; }
        public int PSession3 { get => Session_id3; set => Session_id3 = value; }
        public int PSession4{ get => Session_id4; set => Session_id4 = value; }

        public string PDAY { get => day; set => day = value; }

        public string PTIME { get => time; set => time = value; }

        public string PSUBJECT { get => psubject; set => psubject = value; }

        public int NormalS { get => Npsessions; set => Npsessions = value; }

        public string NTags { get => Ntag; set => Ntag= value; }

        public double NDuration { get => Nduration; set => Nduration = value; }
        public string NSubjname { get => Nsubjname; set => Nsubjname = value; }

        public string NSubgroupid { get => Nsubgrpid; set => Nsubgrpid = value; }


    }
}
