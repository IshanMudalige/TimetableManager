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

        //--add button
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
            clear();

        }

        //--fill data into tables
        private void PopulateTable(List<Weekday> list)
        {
            //List<Weekday> list = WeekdayDAO.getAll();

            var observableList = new ObservableCollection<Weekday>();
            list.ForEach(x => observableList.Add(x));

            listView.ItemsSource = observableList;
        }

        //--week combobox handler
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

        //--search a week
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

        //--load selected item from list
        private void listview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clear();
            Weekday week = (Weekday)listView.SelectedItem;

            if(week != null)
            { 
                tbTitle.Text = week.Title;
                cbWeek.Text = week.Week_type;
                string days = week.Days;
                if (week.Week_type.Equals("Weekday"))
                {
                    chSat.IsEnabled = false;
                    chSun.IsEnabled = false;
                    chMon.IsEnabled = true;
                    chTue.IsEnabled = true;
                    chWed.IsEnabled = true;
                    chThu.IsEnabled = true;
                    chFri.IsEnabled = true;

                    if (days.Contains("Monday"))
                        chMon.IsChecked = true;
                    if (days.Contains("Tuesday"))
                        chTue.IsChecked = true;
                    if (days.Contains("Wednesday"))
                        chWed.IsChecked = true;
                    if (days.Contains("Thursday"))
                        chThu.IsChecked = true;
                    if (days.Contains("Friday"))
                        chFri.IsChecked = true;
                }
                else if (week.Week_type.Equals("Weekend"))
                {
                    chSat.IsEnabled = true;
                    chSun.IsEnabled = true;
                    chMon.IsEnabled = false;
                    chTue.IsEnabled = false;
                    chWed.IsEnabled = false;
                    chThu.IsEnabled = false;
                    chFri.IsEnabled = false;

                    if (days.Contains("Saturday"))
                        chSat.IsChecked = true;
                    if (days.Contains("Sunday"))
                        chSun.IsChecked = true;
                }

                if (week.Slots == 30)
                    cbSlots.SelectedIndex= 0;
                if (week.Slots == 60)
                    cbSlots.SelectedIndex = 1;
                if (week.Slots == 120)
                    cbSlots.SelectedIndex = 2;
                if (week.Slots == 180)
                    cbSlots.SelectedIndex = 3;

                int hh = (int)(week.Hours / 60);
                int mm = (int)(week.Hours % 60);

                tbHours.Text = hh.ToString();
                tbMinutes.Text = mm.ToString(); 

            }
            
        }

        //--delete button
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Weekday week = (Weekday)listView.SelectedItem;
            WeekdayDAO.deleteWeek(week.Title);
            PopulateTable(WeekdayDAO.getAll());
            clear();
        }

        //--update button
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Weekday weekday = new Weekday();
            weekday.Title = tbTitle.Text;
            weekday.Week_type = cbWeek.Text;

            double time = (double.Parse(tbHours.Text)) * 60 + double.Parse(tbMinutes.Text);
            weekday.Hours = time;

            ArrayList daysList = new ArrayList();
            int daycount = 0;
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
            if ((bool)chThu.IsChecked)
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
            string str = string.Join(",", myArray);

            Console.WriteLine("==" + str);

            weekday.No_days = daycount;
            weekday.Days = str;

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

            Weekday wd = (Weekday)listView.SelectedItem;

            WeekdayDAO.updateWeek(wd.Title,weekday);
            PopulateTable(WeekdayDAO.getAll());
            clear();
        }

        //--clear fields
        private void clear()
        {
            tbTitle.Text = "";
            tbHours.Text = "";
            tbMinutes.Text = "";
            chMon.IsChecked = false;
            chTue.IsChecked = false;
            chWed.IsChecked = false;
            chThu.IsChecked = false;
            chFri.IsChecked = false;
            chSat.IsChecked = false;
            chSun.IsChecked = false;
            cbSlots.SelectedItem = null;
        }


    }

}
