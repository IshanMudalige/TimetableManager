using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.StudentDAO
{
    class Student
    {
        private string year;
        private string semester;
        private string programme;
        private string programmid;


        public Student()
        {
        }

        public Student(string year, string semester, string programme,string programmid)
        {
            this.year = year;
            this.semester = semester;
            this.programme = programme;
            this.programmid = programmid;

        }

        public string Year { get => year; set => year = value; }

        public string Semester { get => semester; set => semester = value; }

        public string Programme { get => programme; set => programme = value; }

        public string Programmid { get => programmid; set => programmid = value; }



    }
}
