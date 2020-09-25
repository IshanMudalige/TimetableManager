using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.Generator
{
    class Session
    {
        int id;
        string lecturer;
        string sub_name;
        string sub_code;
        string tag;
        string grp_id;
        double duration;

        public Session() { }
        public Session(int id, string lecturer, string sub_name, string tag, string grp_id, double duration,string sub_code)
        {
            this.id = id;
            this.lecturer = lecturer;
            this.sub_name = sub_name;
            this.tag = tag;
            this.grp_id = grp_id;
            this.duration = duration;
            this.sub_code = sub_code;
        }

        public int Id { get => id; set => id = value; }
        public string Lecturer { get => lecturer; set => lecturer = value; }
        public string Sub_name { get => sub_name; set => sub_name = value; }
        public string Sub_code { get => sub_code; set => sub_code = value; }
        public string Tag { get => tag; set => tag = value; }
        public string Grp_id { get => grp_id; set => grp_id = value; }
        public double Duration { get => duration; set => duration = value; }
    }
}
