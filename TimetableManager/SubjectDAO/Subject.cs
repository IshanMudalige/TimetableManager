using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.SubjectDAO
{
    class Subject
    {
        private string year;
        private string semester;
        private string sub_name;
        private string sub_code;
        private double lec_hrs;
        private double tute_hrs;
        private double lab_hrs;
        private double evalu_hrs;

        public Subject() { }

        public Subject(string year, string semester, string sub_name, string sub_code, double lec_hrs, double tute_hrs, double lab_hrs, double evalu_hrs) 
        {
            this.year = year;
            this.semester = semester;
            this.sub_name = sub_name;
            this.sub_code = sub_code;
            this.lec_hrs = lec_hrs;
            this.tute_hrs = tute_hrs;
            this.lab_hrs = lab_hrs;
            this.evalu_hrs = evalu_hrs;
        }

        public string Year { get => year; set => year = value; }

        public string Semester { get => semester; set => semester = value; }

        public string SubName { get => sub_name; set => sub_name = value; }

        public string SubCode { get => sub_code; set => sub_code = value; }

        public double LecHrs { get => lec_hrs; set => lec_hrs = value; }

        public double TuteHrs { get => tute_hrs; set => tute_hrs = value; }

        public double LabHrs { get => lab_hrs; set => lab_hrs = value; }

        public double EvaluHrs { get => evalu_hrs; set => evalu_hrs = value; }
    }
}
