using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.Not_AvailableSessionsDAO
{
    class NotAvailableGroup
    {
        private int notavailablegrpid;
        private string not_grpid;
        private string not_sub_name;
        private string not_grp_days;
        private  string not_grp_time;
        private string not_grp_tag;


        public NotAvailableGroup()
        {

        }

        public NotAvailableGroup(int notavailablegrpid,string notavagrpid, string notavasSubname, string notavagrpday, string notavagrptime,string notavatag)
        {
            this.notavailablegrpid = notavailablegrpid;
            this.not_grpid = notavagrpid;
            this.not_sub_name = notavasSubname;
            this.not_grp_days = notavagrpday;
            this.not_grp_time = notavagrptime;
            this.not_grp_tag = notavatag;
        }

        public int NotAvailableGrpID { get => notavailablegrpid; set => notavailablegrpid = value; }
        public string NotAvaGroupID { get => not_grpid; set => not_grpid = value; }

        public string NotAvaSubname { get => not_sub_name; set => not_sub_name = value; }

        public string NotAvaGrpDay { get => not_grp_days; set => not_grp_days = value; }

        public string NotAvaGrpTime { get => not_grp_time; set => not_grp_time = value; }

        public string NotAvaGrpTAG { get => not_grp_tag; set => not_grp_tag = value; }

    }
}
