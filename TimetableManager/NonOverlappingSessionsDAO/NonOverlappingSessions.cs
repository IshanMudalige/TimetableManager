using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.NonOverlappingSessionsDAO
{
    class NonOverlappingSessions
    {

        private int sessionid_1;
        private int sessionid_2;
        private int sessionid_3;
        private int sessionid_4;
        
        
        private int Nonpsessions;
        private string Nontag;
        private double Nonduration;
        private string Nonsubjname;
        private string Nonsubgrpid;

        public NonOverlappingSessions()
        {

        }

        public NonOverlappingSessions(int session1, int session2, int session3, int session4,  int nsessions, string ntag, double nduration, string nsubjname, string nsubid)
        {
            this.sessionid_1 = session1;
            this.sessionid_2 = session2;
            this.sessionid_3 = session3;
            this.sessionid_4 = session4;
            this.Nonpsessions = nsessions;
            this.Nontag = ntag;
            this.Nonduration = nduration;
            this.Nonsubjname = nsubjname;
            this.Nonsubgrpid = nsubid;

        }

        public int NonSession1 { get => sessionid_1; set => sessionid_1 = value; }

        public int NonSession2 { get => sessionid_2; set => sessionid_2 = value; }

        public int NonSession3 { get => sessionid_3; set => sessionid_3 = value; }

        public int NonSession4 { get => sessionid_4; set => sessionid_4 = value; }

        public int NONNormalS { get => Nonpsessions; set => Nonpsessions = value; }

        public string NonTags { get => Nontag; set => Nontag = value; }

        public double NonDuration { get => Nonduration; set => Nonduration = value; }
        public string NonSubjname { get => Nonsubjname; set => Nonsubjname = value; }

        public string NonSubgroupid { get => Nonsubgrpid; set => Nonsubgrpid = value; }


    }
}
