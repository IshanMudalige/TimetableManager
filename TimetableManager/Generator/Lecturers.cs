using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.Generator
{
    class Lecturers
    {
        string lecturerName;
        string[,] lecturer = new string[8, 5];

        public Lecturers(){}

        public Lecturers(string lecturerName, string[,] lecturer)
        {
            this.lecturerName = lecturerName;
            this.lecturer = lecturer;
        }

        public string LecturerName { get => lecturerName; set => lecturerName = value; }
        public string[,] Lecturer { get => lecturer; set => lecturer = value; }
    }
}
