using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
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
using TimetableManager.Not_AvailableSessionsDAO;

namespace TimetableManager
{
    /// <summary>
    /// Interaction logic for Page_Session.xaml
    /// </summary>
    public partial class Page_Session : Page
    {
        public Page_Session()
        {
            InitializeComponent();
            loadfacultyCombo();
            loaddepartmentCombo();
            loadcenterCombo();
            loadtimeCombobox();
            PopulatenotavailableLec(NotAvaLecDao.getAll());
          
        }

        private void PopulatenotavailableLec(List<NotAvaLec> list)
        {


            var observableList = new ObservableCollection<NotAvaLec>();
            list.ForEach(x => observableList.Add(x));

            listView_Copy.ItemsSource = observableList;

        }

        private void PopulateTableLectuers(List<NotAvaLec> list)
        {
            var observableList = new ObservableCollection<NotAvaLec>();
            list.ForEach(x => observableList.Add(x));

            listView.ItemsSource = observableList;
        }

        //===============================Faculty Combo Box=====================================

        public void loadfacultyCombo()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT faculty FROM Lecturer";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string faculty = reader["faculty"].ToString();
                        selectfaculty.Items.Add(faculty);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }

        //=============================================Department Combo Box==========================================

        public void loaddepartmentCombo()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT department FROM Lecturer";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string dep = reader["department"].ToString();
                        selectDepartment.Items.Add(dep);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }

        //==========================================Center Combo Box=====================================================

        public void loadcenterCombo()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT center FROM Lecturer";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string ct = reader["center"].ToString();
                        selectCenter.Items.Add(ct);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }

        //==========================================Time Combo Box=====================================================

        public void loadtimeCombobox()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT hours FROM Weekday_Days";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string t = reader["hours"].ToString();
                        selectnotavailabletime.Items.Add(t);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }
        //=========================================================Retrive Specific LECTUERS========================================== 
        private void submit_Click(object sender, RoutedEventArgs e)
        {
            NotAvaLec notAvaLec =  new NotAvaLec() ;

            notAvaLec.Faculty = selectfaculty.Text;
            notAvaLec.Department = selectDepartment.Text;
            notAvaLec.Center = selectCenter.Text;
            PopulateTableLectuers(NotAvaLecDao.getAllLec(notAvaLec.Faculty,notAvaLec.Department,notAvaLec.Center ));
            
        }

        private void NAL_ADD_BTN_Click(object sender, RoutedEventArgs e)
        {
            NotAvaLec notAvaLec =  (NotAvaLec)listView.SelectedItem;
            if(notAvaLec != null)
            {
                NotAvaLec avaLec = new NotAvaLec();
                avaLec.Lectime = double.Parse(selectnotavailabletime.Text);
                NotAvaLecDao.insertnotAvailableLec(notAvaLec.LecID,notAvaLec.LecName,avaLec);
                PopulatenotavailableLec(NotAvaLecDao.getAll());
            }
           
            
           
        }

       

      

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            NotAvaLec avaLec = (NotAvaLec)listView.SelectedItem;

            if (avaLec != null)
            {
                selectnotavailabletime.Text = avaLec.Lectime.ToString();
               
               
            }
           

            
            
        }
    }
}
