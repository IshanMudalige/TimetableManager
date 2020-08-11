using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimetableManager.DaysHoursDAO;

namespace TimetableManager
{
    /// <summary>
    /// Interaction logic for Page_DaysHours.xaml
    /// </summary>
    public partial class Page_DaysHours : Page
    {
        public Page_DaysHours()
        {
            InitializeComponent();

            Weekday week = new Weekday();
            week.Title = "week 6";
            week.Hours = 54.45;
            week.No_days = 4;
            week.Slots = 40.30;
            week.Week_type = "Weekday";
            week.Days = "Monday,Tuesday,Wed";
            //WeekdayDAO.insertWeek(week);

            //WeekdayDAO.deleteWeek("week 4");

            WeekdayDAO.updateWeek("week 3",week);
        }

    }
}
