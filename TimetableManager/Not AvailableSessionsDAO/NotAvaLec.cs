using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.Not_AvailableSessionsDAO
{
    class NotAvaLec
    {
        private int lec_id;
        private string lec_name;
        private double time;
        private string faculty;
        private string department;
        private string center;

        public NotAvaLec()
        {
        }

        public NotAvaLec(int lecId, string lecname, double time, string faculty ,string department , string center)
        {

            this.lec_id = lecId;
            this.lec_name = lecname;
            this.time = time;
            this.faculty = faculty;
            this.department = department;
            this.center = center;
        }

        public int LecID { get => lec_id; set => lec_id = value; }

        public string LecName { get => lec_name; set => lec_name = value; }

        public double Lectime { get => time; set => time = value; }

        public string Faculty { get => faculty; set => faculty = value; }

        public string Department { get => department; set => department = value; }

        public string Center{ get => center; set => center = value; }

    }

    
}
