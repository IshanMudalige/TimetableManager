using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.Generator
{
    class Hall
    {
        string hallName;
        string type;
        int capacity;
        string[,] halls = new string[8, 5];

        public Hall()
        {
         
        }
        public Hall(string hallName, string[,] halls,string type,int capacity)
        {
            this.hallName = hallName;
            this.halls = halls;
            this.type = type;
            this.capacity = capacity;
        }

        public string HallName { get => hallName; set => hallName = value; }
        public string[,] Halls { get => halls; set => halls = value; }
        public string Type { get => type; set => type = value; }
        public int Capacity { get => capacity; set => capacity = value; }
    }
}
