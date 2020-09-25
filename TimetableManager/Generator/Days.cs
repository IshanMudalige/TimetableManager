using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.Generator
{
    class Days
    {
        int mondays;
        int tuesdays;
        int wednesdays;
        int thursdays;
        int fridays;
        public int[] days = new int[5]; 

        public Days(int mondays, int tuesdays, int wednesdays, int thursdays, int fridays)
        {
            this.mondays = mondays;
            this.tuesdays = tuesdays;
            this.wednesdays = wednesdays;
            this.thursdays = thursdays;
            this.fridays = fridays;

            days[0] = mondays;
            days[1] = tuesdays;
            days[2] = wednesdays;
            days[3] = thursdays;
            days[4] = fridays;

        }

        public int Mondays { get => mondays; set => mondays = value; }
        public int Tuesdays { get => tuesdays; set => tuesdays = value; }
        public int Wednesdays { get => wednesdays; set => wednesdays = value; }
        public int Thursdays { get => thursdays; set => thursdays = value; }
        public int Fridays { get => fridays; set => fridays = value; }
    }
}
