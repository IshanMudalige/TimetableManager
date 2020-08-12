using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            chSat.IsEnabled = false;
            chSun.IsEnabled = false;

            PopulateTable(WeekdayDAO.getAll());

            

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Weekday weekday = new Weekday();
            weekday.Title = tbTitle.Text;
            weekday.Week_type = cbWeek.Text;

            double time = (double.Parse(tbHours.Text)) * 60 + double.Parse(tbMinutes.Text);
            weekday.Hours = time;

            ArrayList daysList = new ArrayList();
            int daycount=0;
            if ((bool)chMon.IsChecked)
            { 
                daysList.Add("Monday");
                daycount++;
            }
            if ((bool)chTue.IsChecked)
            {   
                daysList.Add("Tuesday");
                daycount++;
            }
            if ((bool)chWed.IsChecked) 
            {  
                daysList.Add("Wednesday");
                daycount++;
            }
            if ((bool) chThu.IsChecked)
            {   
                daysList.Add("Thursday");
                daycount++;
            }
            if ((bool)chFri.IsChecked)
            {  
                daysList.Add("Friday");
                daycount++;
            }
            if ((bool)chSat.IsChecked)
            {
                daysList.Add("Saturday");
                daycount++;
            }
            if ((bool)chSun.IsChecked)
            {  
                daysList.Add("Sunday");
                daycount++;
            }


            string[] myArray = (string[])daysList.ToArray(typeof(string));
            string str = string.Join(",",myArray);

            Console.WriteLine("=="+str);

            weekday.No_days = daycount;
            weekday.Days = str;

            /*string[] daysArray = str.Split(',');

            foreach(string x in daysArray)
            {
                Console.WriteLine(x);
            }*/

            int tuid = cbSlots.SelectedIndex;
            double timeslot = 0;
            if (tuid == 0)
                timeslot = 30;
            else if (tuid == 1)
                timeslot = 60;
            else if (tuid == 2)
                timeslot = 120;
            else if (tuid == 3)
                timeslot = 180;

            weekday.Slots = timeslot;

            WeekdayDAO.insertWeek(weekday);
            PopulateTable(WeekdayDAO.getAll());

        }

        private void PopulateTable(List<Weekday> list)
        {
            //List<Weekday> list = WeekdayDAO.getAll();

            var observableList = new ObservableCollection<Weekday>();
            list.ForEach(x => observableList.Add(x));

            listView.ItemsSource = observableList;
        }

        private void cbWeek_DropDownClosed(object sender, EventArgs e)
        {

            if (cbWeek.SelectedIndex == 0)
            {
                chSat.IsEnabled = false;
                chSun.IsEnabled = false;
                chMon.IsEnabled = true;
                chTue.IsEnabled = true;
                chWed.IsEnabled = true;
                chThu.IsEnabled = true;
                chFri.IsEnabled = true;
            }
            else if (cbWeek.SelectedIndex == 1)
            {
                chSat.IsEnabled = true;
                chSun.IsEnabled = true;
                chMon.IsEnabled = false;
                chTue.IsEnabled = false;
                chWed.IsEnabled = false;
                chThu.IsEnabled = false;
                chFri.IsEnabled = false;
            }
        }

        private void searchField_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchField.Text.Equals(""))
            {
                PopulateTable(WeekdayDAO.getAll());
            }
            else
            {
                PopulateTable(WeekdayDAO.search(searchField.Text));
            }
        }

     
        private void listview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Weekday week = (Weekday)listView.SelectedItem;
            Console.WriteLine(week.Title);
        }
    }
}
