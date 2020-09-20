using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.NormalSessionsDAO
{
    class NormalSessions
    {
        private string lecturers;
        private string s_name;
        private string s_code;
        private string tag;
        private string grp_id;
        private string sub_id;
        private int no_students;
        private double duration;


        public NormalSessions() { }

        public NormalSessions(string lecturers, string s_name, string s_code, string tag, string grp_id, string sub_id, int no_students, double duration)
        {
            this.lecturers = lecturers;
            this.s_name = s_name;
            this.s_code = s_code;
            this.tag = tag;
            this.grp_id = grp_id;
            this.sub_id = sub_id;
            this.no_students = no_students;
            this.duration = duration;
        }

        public string Lecturers { get => lecturers; set => lecturers = value; }

        public string Sname { get => s_name; set => s_name = value; }

        public string Scode { get => s_code; set => s_code = value; }

        public string Tag { get => tag; set => tag = value; }

        public string GrpID { get => grp_id; set => grp_id = value; }

        public string SubID { get => sub_id; set => sub_id = value; }

        public int NoStu { get => no_students; set => no_students = value; }

        public double Duration { get => duration; set => duration = value; }

    }
 
}
