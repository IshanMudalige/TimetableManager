﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
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
using TimetableManager.NormalSessionsDAO;
using System.Collections.ObjectModel;
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
            loadLecturerCombo();
            loadTags();
            loadGroupId();
            loadSubjectNames();
            loadfacultyCombo();
            loaddepartmentCombo();
            loadcenterCombo();
            loadtimeCombobox();
            loadSUBCombobox();
            loadStimeCombobox();
            PopulatenotavailableLec(NotAvaLecDao.getAll());

        }

        //Loading lecturers names to normal sessions
        public void loadLecturerCombo()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT name FROM Lecturer";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        string Lname = reader["name"].ToString();
                        cmbLecNames.Items.Add(Lname);

                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        //Loading tags to normal sessions
        public void loadTags()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT tagname FROM Tag";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        string Tname = reader["tagname"].ToString();
                        txtTag.Items.Add(Tname);

                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        //Loading Group-ID to normal sessions
        public void loadGroupId()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT group_id FROM Groups_Info";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        string GID = reader["group_id"].ToString();
                        txtGrpID.Items.Add(GID);

                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        //Loading Sub-GRP-ID to normal sessions
        public void loadSubGrpId()
        {
            string grpId = "";
            if (txtGrpID.SelectedIndex >= 0)
                grpId = txtGrpID.SelectedItem.ToString();
            Console.WriteLine("grpid is = " + grpId);

            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT subgroup_id FROM Groups_Info WHERE group_id='" + grpId + "'";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        string SubGID = reader["subgroup_id"].ToString();
                        txtSubGrpID.Items.Add(SubGID);

                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        //Loading subject names to normal sessions
        public void loadSubjectNames()
        {


            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                /*string year = "";
                string year1 = "Y1";
                string year2 = "Y2";
                string year3 = "Y3";
                string year4 = "Y4";

                string check = "";
                if (txtGrpID.SelectedIndex >= 0)
                    check = txtGrpID.SelectedItem.ToString();

                //int length = check.Length - check.IndexOf(".") + 1;
                string subCheck = check.Substring(1, check.IndexOf("."));


                if (subCheck.Equals(year1))
                {
                    year = year1;
                }
                else if (subCheck.Equals(year2))
                {
                    year = year2;
                }
                else if (subCheck.Equals(year3))
                {
                    year = year3;
                }
                else
                {
                    year = year4;
                }*/


                try
                {

                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT sub_name FROM Subjects";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        string Sname = reader["sub_name"].ToString();
                        //string Syr = reader["offer_year"].ToString();
                        /*string[] items = new string[] {Sname +"." + Syr};
                        foreach (string item in items)
                        {
                            txtSubNames.Items.Add(item);
                        }*/
                        txtSubNames.Items.Add(Sname);

                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        //Calling the losdSubGrpID method when GrpID is changing
        private void txtGrpID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadSubGrpId();
        }

        //Load no of students to normal sessions
        private void txtNoOfStudents_SelectionChanged(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                string SUBId = "";
                if (txtSubGrpID.SelectedIndex >= 0)
                    SUBId = txtSubGrpID.SelectedItem.ToString();

                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT student_count FROM Groups_Info WHERE subgroup_id= '" + SUBId + "'";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        string Scount = reader["student_count"].ToString();
                        txtNoOfStudents.Text = Scount;

                    }

                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //Load the relavent subject code for selected subject in normal sessions
        private void txtSubjCode_SelectionChanged(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                string SUBname = "";
                if (txtSubNames.SelectedIndex >= 0)
                    SUBname = txtSubNames.SelectedItem.ToString();

                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT sub_code FROM Subjects WHERE sub_name= '" + SUBname + "'";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        string Scode = reader["sub_code"].ToString();
                        txtSubjCode.Text = Scode;

                    }
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //Getting selected lecturer names from combobox to text area
        private void txtLecNames_SelectionChanged(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i <= 3; i++)
            {
                string LecName = "";
                if (cmbLecNames.SelectedIndex >= 0)
                    LecName = cmbLecNames.SelectedItem.ToString();

                string[] names = new string[] { LecName + "," };
                //txtLecNames.Text = LecName + ",";

                foreach (string name in names)
                {

                    txtLecNames.Text = name;
                }
            }

        }

        //Inserting Normal Sessions 
        private void btnSessionAdd_Click(object sender, RoutedEventArgs e)
        {
            NormalSessions normalSessions = new NormalSessions();

            normalSessions.Lecturers = txtLecNames.Text;
            normalSessions.GrpID = txtGrpID.Text;
            normalSessions.SubID = txtSubGrpID.Text;
            normalSessions.NoStu = int.Parse(txtNoOfStudents.Text);
            normalSessions.Sname = txtSubNames.Text;
            normalSessions.Scode = txtSubjCode.Text;
            normalSessions.Tag = txtTag.Text;
            normalSessions.Duration = double.Parse(txtDuration.Text);

            NorSessionsDetailsDAO.InsertNormalSessions(normalSessions);
        }

        //==================================NOT AVAILABLE LEC==============================
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
            NotAvaLec notAvaLec = new NotAvaLec();

            notAvaLec.Faculty = selectfaculty.Text;
            notAvaLec.Department = selectDepartment.Text;
            notAvaLec.Center = selectCenter.Text;
            PopulateTableLectuers(NotAvaLecDao.getAllLec(notAvaLec.Faculty, notAvaLec.Department, notAvaLec.Center));

        }

        //====================================================ADD NOT Available Lectuers =================================
        private void NAL_ADD_BTN_Click(object sender, RoutedEventArgs e)
        {
            
                NotAvaLec avaLec = new NotAvaLec();

                avaLec.LecID = int.Parse(txtNAID.Text);
                avaLec.LecName = txtNAlec.Text;
                avaLec.Lectime = double.Parse(selectnotavailabletime.Text);
                NotAvaLecDao.insertnotAvailableLec( avaLec);
                PopulatenotavailableLec(NotAvaLecDao.getAll());
            
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            NotAvaLec avaLec = (NotAvaLec)listView.SelectedItem;

            if (avaLec != null)
            {
                txtNAID.Text = avaLec.LecID.ToString();
                txtNAlec.Text = avaLec.LecName;
               // selectnotavailabletime.Text = avaLec.Lectime.ToString();

               
            }
           



        }
        //==============================================DELETE NOT AVAILABLE LECTUERS=====================
        private void NAL_DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            NotAvaLec notAva = (NotAvaLec)listView_Copy.SelectedItem;

            if(notAva==null)
            {
                MessageBox.Show("please select required row from the table");
            }
            else
            {
                NotAvaLecDao.deletenotavailableLec(notAva.LecID);
                PopulatenotavailableLec(NotAvaLecDao.getAll());
                clearnotavailablelec();
            }
        }


        //=====================Selection Changed Not Available Lectuers======


        private void listView_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NotAvaLec avaLec = (NotAvaLec)listView_Copy.SelectedItem;

            if (avaLec != null)
            {
                txtNAID.Text = avaLec.LecID.ToString();
                txtNAlec.Text = avaLec.LecName;
                selectnotavailabletime.Text = avaLec.Lectime.ToString();


            }
        }

        //====clear not avaible===
        public void clearnotavailablelec()
        {
            txtNAID.Text = "";
            txtNAlec.Text = "";
            selectnotavailabletime.Text = "";
        }

        //=================================---------------------------Not Available Sessions-----------------====================================================================================

        //==================================NOT AVAILABLE Sessions Populate Table=============================
        private void PopulatenotavailableSessions(List<NotAvailableSessions> list)
        {


            var observableList = new ObservableCollection<NotAvailableSessions>();
            list.ForEach(x => observableList.Add(x));

            listView1.ItemsSource = observableList;

        }

        private void PopulateNOTASessions(List<NotAvailableSessions> list)
        {


            var observableList = new ObservableCollection<NotAvailableSessions>();
            list.ForEach(x => observableList.Add(x));

            listView1_COPY.ItemsSource = observableList;

        }

        //============================LOAD SUBJECT COMBO BOX===============================

        public void loadSUBCombobox()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT subj_name FROM Sessions";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string t = reader["subj_name"].ToString();
                        selectsubject.Items.Add(t);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }
        //===========================-----------------------------Retrive Normal Sessions------------------==================================
        private void sessionsubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            NotAvailableSessions notAvailableSessions = new NotAvailableSessions();

            notAvailableSessions.NotAvaSubject= selectsubject.Text;
            PopulatenotavailableSessions(NotAvailableSessionDAO.getAllSesions(notAvailableSessions.NotAvaSubject));
        }

        private void listView1_COPY_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NotAvailableSessions avasessions = (NotAvailableSessions)listView1_COPY.SelectedItem;

            if (avasessions != null)
            {
                txtNASessionid.Text = avasessions.NotAvaSessionID.ToString();
              
                selectStime.Text = avasessions.NotAvaSesionTime.ToString();


            }
        }
        //==============================================================ADD NOT AVAILABLE SESSIONS=======================================================
        private void AddSessionBTN_Click(object sender, RoutedEventArgs e)
        {
            NotAvailableSessions notSessions = new NotAvailableSessions();

            notSessions.NotAvaSessionID= int.Parse(txtNASessionid.Text);
            notSessions.NotAvaSesionTime = double.Parse(selectStime.Text);
            NotAvailableSessionDAO.insertnotAvailableSession(notSessions);
            
            PopulateNOTASessions(NotAvailableSessionDAO.getAllNotAvailableS());
        }

        //==========================================Not Available Time Combo Box=====================================================

        public void loadStimeCombobox()
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
                        selectStime.Items.Add(t);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NotAvailableSessions avaNOTS = (NotAvailableSessions)listView1.SelectedItem;

            if (avaNOTS != null)
            {
                txtNASessionid.Text = avaNOTS.NotAvaSessionID.ToString();
                //txtNAlec.Text = avaLec.LecName;
                // selectnotavailabletime.Text = avaLec.Lectime.ToString();


            }
        }

        public void clearnotavailableSessions()
        {
            txtNASessionid.Text = "";
            selectStime.Text = "";
        }
        private void DeleteNotAvaSessionBTN_Click(object sender, RoutedEventArgs e)
        {

            NotAvailableSessions notAvaSesion = (NotAvailableSessions)listView1_COPY.SelectedItem;

            if (notAvaSesion == null)
            {
                MessageBox.Show("please select required row from the table");
            }
            else
            {
                NotAvailableSessionDAO.deletenotavailableSessions(notAvaSesion.NotAvaSessionID);
                PopulateNOTASessions(NotAvailableSessionDAO.getAllNotAvailableS());
                clearnotavailableSessions();
            }
        }
    }
}
