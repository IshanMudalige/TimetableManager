using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.Generator
{
    class StudentGroups
    {
        string groupId;
        string[,] student = new string[8, 5];

        public StudentGroups(){ }
        public StudentGroups(string groupId, string[,] student)
        {
            this.groupId = groupId;
            this.student = student;
        }

        public string GroupId { get => groupId; set => groupId = value; }
        public string[,] Student { get => student; set => student = value; }
    }
}
