using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.GroupDAO
{
    class Group
    {
        private int student_count;
        private int group_no;
        private string group_id;
        private int subgroup_no;
        private string subgroup_id;
        private string academic_id;

        public Group() { 
        }

        public Group(string academicId,int studentcount,int groupNo,string groupId,int subgroupNo,string subgroupId)

        {
            this.academic_id = academicId;
            this.student_count = studentcount;
            this.group_no = groupNo;
            this.group_id = groupId;
            this.subgroup_no = subgroupNo;
            this.subgroup_id = subgroupId;
           
        }



        public string AcademicId { get => academic_id; set => academic_id = value; }

        public int StudentCount { get => student_count; set =>student_count = value; }

        public int Groupno { get => group_no; set => group_no = value; }

        public string GroupId { get => group_id; set => group_id = value; }

        public int SubGroupno { get => subgroup_no; set => subgroup_no = value; }

        public string SubGroupId { get => subgroup_id; set => subgroup_id = value; }




    }
}
