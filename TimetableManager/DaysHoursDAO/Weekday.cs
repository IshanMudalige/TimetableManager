using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.DaysHoursDAO
{
    class Weekday
    {
        private string title;
        private int no_days;
        private double hours;
        private double slots;
        private string days;
        private string week_type;

        public Weekday() { }

        public Weekday(string title, int no_days, double hours, double slots, string days, string week_type)
        {
            this.title = title;
            this.no_days = no_days;
            this.hours = hours;
            this.slots = slots;
            this.days = days;
            this.week_type = week_type;
        }

        public string Title { get => title; set => title = value; }
        public int No_days { get => no_days; set => no_days = value; }
        public double Hours { get => hours; set => hours = value; }
        public double Slots { get => slots; set => slots = value; }
        public string Days { get => days; set => days = value; }
        public string Week_type { get => week_type; set => week_type = value; }
    }
}
