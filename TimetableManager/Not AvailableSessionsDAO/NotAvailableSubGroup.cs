using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.Not_AvailableSessionsDAO
{
    class NotAvailableSubGroup
    {
        private int notavasubgroupid;
        private string not_subgrpid;
        private string not_subgrp_subname;
        private string not_subgrp_days;
        private string not_subgrp_time;
        private string not_subgrp_tag;

        public NotAvailableSubGroup()
        {

        }
        public NotAvailableSubGroup( int notavasubgrpid,string notsubid, string notsubgrpsubname,string subgrpdays, string subgrptime, string subtag)
        {
            this.notavasubgroupid = notavasubgrpid;
            this.not_subgrpid = notsubid;
            this.not_subgrp_subname = notsubgrpsubname;
            this.not_subgrp_days = subgrpdays;
            this.not_subgrp_time = subgrptime;
            this.not_subgrp_tag = subtag;
        }


        public int NotAvailableSubGrpId { get => notavasubgroupid; set => notavasubgroupid = value; }
        public string NotAvaSubGrpId { get => not_subgrpid; set => not_subgrpid = value; }

        public string NotAvaSubGrpSubname { get => not_subgrp_subname; set => not_subgrp_subname = value; }

        public string NotAvaSubGrpDays{ get => not_subgrp_days; set => not_subgrp_days = value; }

        public string NotAvaSubGrpTime { get => not_subgrp_time; set => not_subgrp_time = value; }

        public string NotAvaSubGrpTag { get => not_subgrp_tag; set => not_subgrp_tag = value; }





    }
}
