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
        private string lec_time;
        private string lec_day;
        private string faculty;
        private string department;
        

        public NotAvaLec()
        {
        }

        public NotAvaLec(int lecId, string lecname,string day, string time, string faculty ,string department )
        {

            this.lec_id = lecId;
            this.lec_name = lecname;
            this.lec_day = day;
            this.lec_time = time;
            this.faculty = faculty;
            this.department = department;
            
        }

        public int LecID { get => lec_id; set => lec_id = value; }

        public string LecName { get => lec_name; set => lec_name = value; }

        public string LecDay { get => lec_day; set => lec_day = value; }

        public string Lectime { get => lec_time; set =>lec_time = value; }

        public string Faculty { get => faculty; set => faculty = value; }

        public string Department { get => department; set => department = value; }

        

    }

    
}
