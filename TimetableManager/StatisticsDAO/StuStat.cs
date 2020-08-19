using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.StudentDAO
{
    class StuStat
    {
        private string year;
        private string semester;
        private string programme;
        private string programmid;
        private int count;

        public StuStat()
        {
        }

        public StuStat(string year, string semester, string programme, string programmid, int count)
        {
            this.year = year;
            this.semester = semester;
            this.programme = programme;
            this.programmid = programmid;
            this.count = count;

        }

        public string Year { get => year; set => year = value; }

        public string Semester { get => semester; set => semester = value; }

        public string Programme { get => programme; set => programme = value; }

        public string Programmid { get => programmid; set => programmid = value; }

        public int Count { get => count; set => count = value; }

    }
}
