using System;
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
using TimetableManager.ConsecutiveSessionsDAO;
using TimetableManager.ParallelSessionsDAO;
using TimetableManager.NonOverlappingSessionsDAO;

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
            loadfacultyCombo();
            loaddepartmentCombo();
            
            loadSUBCombobox();
            loadSUBGRPCombobox();
            loadGRPTagCombobox();
            loadSUBGRPSubjectCMB();
            loadSUBGRPTagCombobox();
            loadConsecutiveSubject();
            loadSessionid1cmb();
            loadSessionid2cmb();
            loadSubgroupidcmb();
            loadPSessionid1cmb();
            loadPSessionid2cmb();
            loadPSessionid3cmb();
            loadPSessionid4cmb();
            loadNONSubgroupidcmb();
            loadNSessionid1cmb();
            loadNSessionid2cmb();
            loadNSessionid3cmb();
            loadNSessionid4cmb();


            PopulatenotavailableLec(NotAvaLecDao.getAll());
            PopulateTableNormalSess(NorSessionsDetailsDAO.getAllSessions());
            PopulateNotAVAGroup(NotAvailableGroupDetailsDao.getAll());
            PopulateNOTASessions(NotAvailableSessionDAO.getAllNotAvailableS());
            PopulateNotAVASubGroup(NotAvailableSubGroupDAO.getAll());
            PopulateConsecutivetable(ConsecutiveSessionsDao.getAll());
            PopulateParallelsesionstable(ParallelSessionDAO.getAllParallelSesions());
            PopulateNonOverlappingsesionstable(NonOverlappingSessionDAO.getAllNonOverlapSesions());

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
            string grpId = "";
            if (txtGrpID.SelectedIndex >= 0)
                grpId = txtGrpID.SelectedItem.ToString();

            Console.WriteLine("grpid is = " + grpId);

            string[] year = grpId.Split('.');
            string year1 = "";


            foreach (string y in year)
            {
                if (y.Equals("Y1"))
                {
                    year1 = "Y1";
                }
                else if (y.Equals("Y2"))
                {
                    year1 = "Y2";
                }
                else if (y.Equals("Y3"))
                {
                    year1 = "Y3";
                }
                else if (y.Equals("Y4"))
                {
                    year1 = "Y4";
                }
            }

            Console.WriteLine("year is = " + year1);

            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                try
                {

                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT sub_name FROM Subjects WHERE offer_year='" + year1 + "'";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        string Sname = reader["sub_name"].ToString();
                        txtSubNames.Items.Add(Sname);

                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        //Calling the losdSubGrpID method and loadSubjectNames method when GrpID is changing
        private void txtGrpID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadSubGrpId();
            loadSubjectNames();
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
            PopulateTableNormalSess(NorSessionsDetailsDAO.getAllSessions());

        }

        //Display all normal sessions in table view
        private void PopulateTableNormalSess(List<NormalSessions> list)
        {
            var observableList = new ObservableCollection<NormalSessions>();
            list.ForEach(x => observableList.Add(x));

            listViewSession.ItemsSource = observableList;
        }

        //Deleting Normal sessions
        private void btnSesDelete_Click(object sender, RoutedEventArgs e)
        {
            NormalSessions normalSessions = (NormalSessions)listViewSession.SelectedItem;

            if (normalSessions == null)
            {
                MessageBox.Show("Please Select Required subject from the Table.");
            }
            else
            {
                NorSessionsDetailsDAO.deleteNormalSessions(normalSessions.Sid);
                PopulateTableNormalSess(NorSessionsDetailsDAO.getAllSessions());

            }

        }

        //Search normal sessions
        private void searchFieldNorSess_TextChanged(object sender, TextChangedEventArgs e)
        {
            string type = "";
            if(RdLec.IsChecked == true)
            {
                type = "lecturer";
                
            }
            else if(RdSub.IsChecked == true)
            {
                type = "subject";
            }
            else if (RdGrp.IsChecked == true)
            {
                type = "group";
            }


            if (searchFieldNorSess.Text.Equals(""))
            {
                PopulateTableNormalSess(NorSessionsDetailsDAO.getAllSessions());
            }
            else
            {
                PopulateTableNormalSess(NorSessionsDetailsDAO.search(searchFieldNorSess.Text,type));
            }
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

        
    

        //=========================================================Retrive Specific LECTUERS========================================== 
        private void submit_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateNotAvaLECSubmit())
            {
                NotAvaLec notAvaLec = new NotAvaLec();

                notAvaLec.Faculty = selectfaculty.Text;
                notAvaLec.Department = selectDepartment.Text;
                PopulateTableLectuers(NotAvaLecDao.getAllLec(notAvaLec.Faculty, notAvaLec.Department));
            }
           

        }

        private Boolean ValidateNotAvaLECSubmit()
        {
            if (selectfaculty.Text.Equals(""))
            {
                MessageBox.Show("Please Select  Faculty ");
            }
            if (selectDepartment.Text.Equals(""))
            {
                MessageBox.Show("Please Select  Department ");
            }

            else
            {
                return true;
            }

            return false;
        }
        //====================================================ADD NOT Available Lectuers =================================
        private void NAL_ADD_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                NotAvaLec avaLec = new NotAvaLec();

                avaLec.LecID = int.Parse(txtNAID.Text);
                avaLec.LecName = txtNAlec.Text;
                avaLec.LecDay = selectnotavailableDay.Text;
                avaLec.Lectime = selectnotavailabletime.Text;
                NotAvaLecDao.insertnotAvailableLec(avaLec);
                PopulatenotavailableLec(NotAvaLecDao.getAll());
            }
            else
            {
                MessageBox.Show("Please select the required Lectuer from the table");
            }
            
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
                selectnotavailableDay.Text = avaLec.LecDay;
                selectnotavailabletime.Text = avaLec.Lectime.ToString();


            }
        }

        //====clear not avaible===
        public void clearnotavailablelec()
        {
            txtNAID.Text = "";
            txtNAlec.Text = "";
            selectnotavailabletime.Text = "";
            selectnotavailableDay.Text = "";
        }

        private Boolean Validate()
        {
            if (txtNAID.Text.Equals(""))
            {
                MessageBox.Show("Please enter Lectuerid");
            }
            if (txtNAlec.Text.Equals(""))
            {
                MessageBox.Show("Please enter Lectuer Name");
            }
            if (selectnotavailableDay.Text.Equals(""))
            {
                MessageBox.Show("Please enter Not Available Day");
            }
            if (selectnotavailabletime.Text.Equals(""))
            {
                MessageBox.Show("Please enter  Not Available time");
            }
            else
            {
                return true;
            }

            return false;
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
        //===========================-----------------------------Retrive Normal Sessions to not availabel------------------==================================
        private void sessionsubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateNotAvaSessionSubmit())
            {
                NotAvailableSessions notAvailableSessions = new NotAvailableSessions();

                notAvailableSessions.NotAvaSubject = selectsubject.Text;
                PopulatenotavailableSessions(NotAvailableSessionDAO.getAllSesions(notAvailableSessions.NotAvaSubject));
            }
            else
            {

            }
        }



        private void listView1_COPY_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NotAvailableSessions avasessions = (NotAvailableSessions)listView1_COPY.SelectedItem;

            if (avasessions != null)
            {
                txtNASessionid.Text = avasessions.NotAvaSessionID.ToString();
              
                selectStime.Text = avasessions.NotAvaSesionTime.ToString();

                selectNotAvaDays.Text = avasessions.NotAvaSessionDay.ToString();


            }
        }
        //==============================================================ADD NOT AVAILABLE SESSIONS=======================================================
        private void AddSessionBTN_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateNotAvaSession())
            {
                NotAvailableSessions notSessions = new NotAvailableSessions();

                notSessions.NotAvaSessionID = int.Parse(txtNASessionid.Text);
                notSessions.NotAvaSessionDay = selectNotAvaDays.Text;
                notSessions.NotAvaSesionTime = selectStime.Text;
                NotAvailableSessionDAO.insertnotAvailableSession(notSessions);

                PopulateNOTASessions(NotAvailableSessionDAO.getAllNotAvailableS());
            }
           
        }

        private Boolean ValidateNotAvaSession()
        {
            if (txtNASessionid.Text.Equals(""))
            {
                MessageBox.Show("Please Select required  Session ID from the Table");
            }
            if (selectNotAvaDays.Text.Equals(""))
            {
                MessageBox.Show("Please Select Not Available Day");
            }
            if (selectStime.Text.Equals(""))
            {
                MessageBox.Show("Please Select Not Available Time");
            }
            else
            {
                return true;
            }

            return false;
        }

        private Boolean ValidateNotAvaSessionSubmit()
        {
            if (selectsubject.Text.Equals(""))
            {
                MessageBox.Show("Please Select required  Subject ");
            }
      
            else
            {
                return true;
            }

            return false;
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
            selectNotAvaDays.Text = "";
        }

        //==============================================================DELETE NOT AVAILABLE SESSIONS=======================================================
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
        //===========================================================NOT Available Group======================================

        

        //============================LOAD SUBJECT COMBO BOX===============================

        public void loadSUBGRPCombobox()
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
                        selectSubCMB.Items.Add(t);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }

        //============================LOAD Tag COMBO BOX===============================

        public void loadGRPTagCombobox()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT tag FROM Sessions";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string t = reader["tag"].ToString();
                        selectTagCMB.Items.Add(t);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }
        private void PopulatenotavailableGroup(List<NotAvailableGroup> list)
        {


            var observableList = new ObservableCollection<NotAvailableGroup>();
            list.ForEach(x => observableList.Add(x));

            listViewGROUP.ItemsSource = observableList;

        }

        private void PopulateNotAVAGroup(List<NotAvailableGroup> list)
        {


            var observableList = new ObservableCollection<NotAvailableGroup>();
            list.ForEach(x => observableList.Add(x));

            listViewGROUP_COPY.ItemsSource = observableList;

        }
        private void GROUP_subbtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateNotAvaGroupSubmit())
            {
                NotAvailableGroup notAvailableGroup = new NotAvailableGroup();

                notAvailableGroup.NotAvaSubname = selectSubCMB.Text;
                notAvailableGroup.NotAvaGrpTAG = selectTagCMB.Text;
                PopulatenotavailableGroup(NotAvailableGroupDetailsDao.getAllGroups(notAvailableGroup.NotAvaSubname, notAvailableGroup.NotAvaGrpTAG));
            }
        }

        private Boolean ValidateNotAvaGroupSubmit()
        {
            if (selectSubCMB.Text.Equals(""))
            {
                MessageBox.Show("Please Select required  Subject ");
            }
            if (selectTagCMB.Text.Equals(""))
            {
                MessageBox.Show("Please Select required  Tag ");
            }

            else
            {
                return true;
            }

            return false;
        }

        private void listViewGROUP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NotAvailableGroup avagroup = (NotAvailableGroup)listViewGROUP.SelectedItem;

            if (avagroup != null)
            {
                txtNotAvaGID.Text = avagroup.NotAvaGroupID;
                txtNotAvaGSub.Text = avagroup.NotAvaSubname;

                

            }
        }

        private void ADDgrpbtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateNotAvaGroup())
            {
                NotAvailableGroup notGroups = new NotAvailableGroup();

                notGroups.NotAvaGroupID = txtNotAvaGID.Text;
                notGroups.NotAvaSubname = txtNotAvaGSub.Text;
                notGroups.NotAvaGrpDay = selectgrpdayscmb.Text;
                notGroups.NotAvaGrpTime = selectNotAvagrouptime.Text;

                NotAvailableGroupDetailsDao.insertnotAvailableGroup(notGroups);
                PopulateNotAVAGroup(NotAvailableGroupDetailsDao.getAll());

            }
        }

        private Boolean ValidateNotAvaGroup()
        {
            if (txtNotAvaGID.Text.Equals(""))
            {
                MessageBox.Show("Please Select required  Group from the Table");
            }
            if (txtNotAvaGSub.Text.Equals(""))
            {
                MessageBox.Show("Please Select required  Subject from the Table");
            }
            if (selectgrpdayscmb.Text.Equals(""))
            {
                MessageBox.Show("Please Select Not Available Day");
            }
            if (selectgrpdayscmb.Text.Equals(""))
            {
                MessageBox.Show("Please Select Not Available Time");
            }
            else
            {
                return true;
            }

            return false;
        }
        private void listViewGROUP_COPY_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NotAvailableGroup avagrp = (NotAvailableGroup)listViewGROUP_COPY.SelectedItem;

            if (avagrp != null)
            {
                txtNotAvaGID.Text = avagrp.NotAvaGroupID;
                txtNotAvaGSub.Text = avagrp.NotAvaSubname;
                selectgrpdayscmb.Text = avagrp.NotAvaGrpDay;
                selectNotAvagrouptime.Text = avagrp.NotAvaGrpTime;
               
            }
        }

        public void clearnotavailableGroups()
        {
            txtNotAvaGSub.Text = "";
            txtNotAvaGID.Text = "";
            selectgrpdayscmb.Text = "";
            selectNotAvagrouptime.Text = "";
        }

        //======================delete group===============================
        private void Deletegrpbtn_Click(object sender, RoutedEventArgs e)
        {
            NotAvailableGroup notAvailableGroup = (NotAvailableGroup)listViewGROUP_COPY.SelectedItem;

            if (notAvailableGroup == null)
            {
                MessageBox.Show("please select required row from the table");
            }
            else
            {
                NotAvailableGroupDetailsDao.deletenotavailablegroups(notAvailableGroup.NotAvailableGrpID);

                PopulateNotAVAGroup(NotAvailableGroupDetailsDao.getAll());
                clearnotavailableGroups();
            }
        }
        //=====================================================----------------Not Avaiable Subgroups------------------------======================================================  
        //============================LOAD SUBJECT COMBO BOX===============================

        public void loadSUBGRPSubjectCMB()
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
                        cmbsub.Items.Add(t);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }

        //============================LOAD Tag COMBO BOX===============================

        public void loadSUBGRPTagCombobox()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT tag FROM Sessions";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string t = reader["tag"].ToString();
                        cmbtag.Items.Add(t);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }

        //=============Populate not available subgroups=====================

        private void PopulatenotavailableSubGroup(List<NotAvailableSubGroup> list)
        {


            var observableList = new ObservableCollection<NotAvailableSubGroup>();
            list.ForEach(x => observableList.Add(x));

            listViewSUBGROUP.ItemsSource = observableList;

        }

        private void PopulateNotAVASubGroup(List<NotAvailableSubGroup> list)
        {


            var observableList = new ObservableCollection<NotAvailableSubGroup>();
            list.ForEach(x => observableList.Add(x));

            listViewSUBGROUP_COPY.ItemsSource = observableList;

        }

        private void listViewSUBGROUP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NotAvailableSubGroup avasubgroup = (NotAvailableSubGroup)listViewSUBGROUP.SelectedItem;

            if (avasubgroup != null)
            {
                txtnotsubgrpid.Text = avasubgroup.NotAvaSubGrpId;
                txtnotsubgrpsubname.Text = avasubgroup.NotAvaSubGrpSubname;



            }
        }

        private void btnSubgroupsubmit_Click(object sender, RoutedEventArgs e)

        {
            if (ValidateNotAvaSubGroupSubmit())
            {
                NotAvailableSubGroup notAvailablesubGroup = new NotAvailableSubGroup();

                notAvailablesubGroup.NotAvaSubGrpSubname = cmbsub.Text;
                notAvailablesubGroup.NotAvaSubGrpTag = cmbtag.Text;


                PopulatenotavailableSubGroup(NotAvailableSubGroupDAO.getAllSubGroups(notAvailablesubGroup.NotAvaSubGrpSubname, notAvailablesubGroup.NotAvaSubGrpTag));
            }
        }

        private Boolean ValidateNotAvaSubGroupSubmit()
        {
            if (cmbsub.Text.Equals(""))
            {
                MessageBox.Show("Please Select required  Subject ");
            }
            if (cmbtag.Text.Equals(""))
            {
                MessageBox.Show("Please Select required  Tag ");
            }

            else
            {
                return true;
            }

            return false;
        }

        private void btnaddsubgroups_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateNotAvaSubGroup())
            {
                NotAvailableSubGroup notsubGroups = new NotAvailableSubGroup();

                notsubGroups.NotAvaSubGrpId = txtnotsubgrpid.Text;
                notsubGroups.NotAvaSubGrpSubname = txtnotsubgrpsubname.Text;
                notsubGroups.NotAvaSubGrpDays = selectnotavaSubgroupDays.Text;
                notsubGroups.NotAvaSubGrpTime = selectNotavaSubgrpTime.Text;


                NotAvailableSubGroupDAO.insertnotAvailableSubGroup(notsubGroups);
                PopulateNotAVASubGroup(NotAvailableSubGroupDAO.getAll());
            }
        }

        private Boolean ValidateNotAvaSubGroup()
        {
            if (txtnotsubgrpid.Text.Equals(""))
            {
                MessageBox.Show("Please Select required  SubGroup from the Table");
            }
            if (txtnotsubgrpsubname.Text.Equals(""))
            {
                MessageBox.Show("Please Select required  Subject from the Table");
            }
            if (selectnotavaSubgroupDays.Text.Equals(""))
            {
                MessageBox.Show("Please Select Not Available Day");
            }
            if (selectNotavaSubgrpTime.Text.Equals(""))
            {
                MessageBox.Show("Please Select Not Available Time");
            }
            else
            {
                return true;
            }

            return false;
        }

        private void listViewSUBGROUP_COPY_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NotAvailableSubGroup avasgrp = (NotAvailableSubGroup)listViewSUBGROUP_COPY.SelectedItem;

            if (avasgrp != null)
            {
                txtnotsubgrpid.Text = avasgrp.NotAvaSubGrpId;
                txtnotsubgrpsubname.Text = avasgrp.NotAvaSubGrpSubname;

                selectnotavaSubgroupDays.Text = avasgrp.NotAvaSubGrpDays;
                selectNotavaSubgrpTime.Text = avasgrp.NotAvaSubGrpTime;



            }
        }

        public void clearnotavailableSubGroups()
        {
            txtnotsubgrpid.Text = "";
            txtnotsubgrpsubname.Text = "";
            selectnotavaSubgroupDays.Text = "";
            selectNotavaSubgrpTime.Text = "";
        }
        private void btnsubgrpdelete_Click(object sender, RoutedEventArgs e)
        {
            NotAvailableSubGroup notAvaSub = (NotAvailableSubGroup)listViewSUBGROUP_COPY.SelectedItem;

            if (notAvaSub==null)
            {
                MessageBox.Show("please select required row from the table");
            }
            else
            {
                NotAvailableSubGroupDAO.deletenotavailablesubgroups(notAvaSub.NotAvailableSubGrpId);
                PopulateNotAVASubGroup(NotAvailableSubGroupDAO.getAll());
                clearnotavailableSubGroups();

                
            }
        }

        //==============================--------------------------------------Consecutive Session---------------============================================

        //==============---------------Load Consecutive subject========================
        public void loadConsecutiveSubject()
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
                        cmbconsecutiveSub.Items.Add(t);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }

        //=============Populate all c sessions =====================

        private void PopulateCsession(List<ConsecutiveSession> list)
        {


            var observableList = new ObservableCollection<ConsecutiveSession>();
            list.ForEach(x => observableList.Add(x));

            listViewConsecutive.ItemsSource = observableList;

        }

        //=============Populate consecutive sessions =====================

        private void PopulateConsecutivetable(List<ConsecutiveSession> list)
        {


            var observableList = new ObservableCollection<ConsecutiveSession>();
            list.ForEach(x => observableList.Add(x));

            listViewCONSECUTIVE_Copy.ItemsSource = observableList;

        }

        private void consecutivesubmitbtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateConsecutiveSubmit())
            {
                ConsecutiveSession consecutive = new ConsecutiveSession();

                consecutive.NSubject = cmbconsecutiveSub.Text;
                PopulateCsession(ConsecutiveSessionsDao.getAllSesions(consecutive.NSubject));
            }
        }

        private Boolean ValidateConsecutiveSubmit()
        {
            if (cmbconsecutiveSub.Text.Equals(""))
            {
                MessageBox.Show("Please Select required  Subject ");
            }
            else
            {
                return true;
            }

            return false;
        }
        //==============-------------------------------load session id1---------------------------------------======================================
        public void loadSessionid1cmb()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT session_id FROM Sessions";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string t = reader["session_id"].ToString();
                        Csession1.Items.Add(t);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }

        //==============-------------------------------load session id2---------------------------------------======================================
        public void loadSessionid2cmb()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT session_id FROM Sessions";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string t = reader["session_id"].ToString();
                        Csession2.Items.Add(t);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }

        
        private void selectsession1tag_SelectionChanged(object sender, RoutedEventArgs e)
        {


            string s1 = Csession1.SelectedItem.ToString();

                using (SQLiteConnection conn = new SQLiteConnection(App.connString))
                {

                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT tag FROM Sessions WHERE session_id='"+s1+"'";
                    
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string stag1 = reader["tag"].ToString();
                        selectsession1tag.Text = stag1;
                    }


                }
            
        }

        private void selectsession2tag_SelectionChanged(object sender, RoutedEventArgs e)
        {


            string s2 = Csession2.SelectedItem.ToString();

            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT tag FROM Sessions WHERE session_id='" + s2 + "'";

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string stag1 = reader["tag"].ToString();
                    selectsession2tag.Text = stag1;
                }


            }

        }

        private void listViewConsecutive_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConsecutiveSession csession = (ConsecutiveSession)listViewConsecutive.SelectedItem;

            if (csession != null)
            {
                Csubname.Text = csession.NSubject;
         



            }
        }

        private void txtnewtag_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string tag1 = selectsession1tag.Text;
            string tag2 = selectsession2tag.Text;

            txtnewtag.Text = tag1 +"/"+ tag2;
        }

        private void BTNMerged_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateConsecutive())
            {

                ConsecutiveSession cSession = new ConsecutiveSession();

                cSession.SessionID1 = Csession1.Text;
                cSession.SessionID2 = Csession2.Text;
                cSession.ConsecutiveTag = txtnewtag.Text;
                cSession.ConsecutiveSubject = Csubname.Text;
                cSession.ConsecutiveDay = Cday.Text;
                cSession.ConsecutiveTime = Ctime.Text;

                ConsecutiveSessionsDao.insertConsecutivesession(cSession);
                PopulateConsecutivetable(ConsecutiveSessionsDao.getAll());
            }
        }

        private Boolean ValidateConsecutive()
        {
            if (Csession1.Text.Equals(""))
            {
                MessageBox.Show("Please Enter Session 1");
            }
            if (Csession2.Text.Equals(""))
            {
                MessageBox.Show("Please Enter Session 2");
            }
            if (txtnewtag.Text.Equals(""))
            {
                MessageBox.Show("Please enter tag");
            }
            if (Csubname.Text.Equals(""))
            {
                MessageBox.Show("Please Select Subject From the table");
            }
            if (Cday.Text.Equals(""))
            {
                MessageBox.Show("Please Select Day");
            }
            if (Ctime.Text.Equals(""))
            {
                MessageBox.Show("Please Select Time");
            }
            else
            {
                return true;
            }

            return false;
        }
        public void clearnotConsecutiveSessions()
        {
            Csession1.Text = "";
            Csession2.Text = "";
            txtnewtag.Text = "";
            Csubname.Text = "";
            Cday.Text = "";
            Ctime.Text = "";

        }
        private void btnconsecutivedelete_Click(object sender, RoutedEventArgs e)
        {
            ConsecutiveSession notAvaSub = (ConsecutiveSession)listViewCONSECUTIVE_Copy.SelectedItem;

            if (notAvaSub == null)
            {
                MessageBox.Show("please select required row from the table");
            }
            else
            {
                ConsecutiveSessionsDao.deleteConsecutiveSession(notAvaSub.ConsecutiveSubject);
                //ConsecutiveSessionsDao.getAll();
                PopulateConsecutivetable(ConsecutiveSessionsDao.getAll());
                clearnotConsecutiveSessions();
            }
        }

        private void listViewCONSECUTIVE_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            ConsecutiveSession csession = (ConsecutiveSession)listViewCONSECUTIVE_Copy.SelectedItem;

            if (csession != null)
            {
                Csubname.Text = csession.NSubject;
                Csession1.Text = csession.SessionID1;
                Csession2.Text = csession.SessionID2;
                txtnewtag.Text = csession.ConsecutiveTag;
                Csubname.Text = csession.ConsecutiveSubject;
                Cday.Text = csession.ConsecutiveDay;
                Ctime.Text = csession.ConsecutiveTime;


            }
        }

        //========================================Parallel Sessions========================================================================

        //==============-------------------------------load parallel Subgroupids---------------------------------------======================================
        public void loadSubgroupidcmb()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT subgrp_id FROM Sessions";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string t = reader["subgrp_id"].ToString();
                        txtPsubGrp.Items.Add(t);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }

        //=============Populate Noramalp sessions =====================

        private void PopulateNPsesionstable(List<ParallelSession> list)
        {


            var observableList = new ObservableCollection<ParallelSession>();
            list.ForEach(x => observableList.Add(x));

            listViewParellel.ItemsSource = observableList;

        }

        //=============Populate Parallel sessions =====================

        private void PopulateParallelsesionstable(List<ParallelSession> list)
        {


            var observableList = new ObservableCollection<ParallelSession>();
            list.ForEach(x => observableList.Add(x));

            listViewParellelAdded.ItemsSource = observableList;

        }

        //Submit Parallel
        private void btnPsubmit_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateParallelSubmit())
            {
                ParallelSession parallelSession = new ParallelSession();

                parallelSession.NSubgroupid = txtPsubGrp.Text;
                PopulateNPsesionstable(ParallelSessionDAO.getAllSesions(parallelSession.NSubgroupid));
            }
        }

        private Boolean ValidateParallelSubmit()
        {
            if (txtPsubGrp.Text.Equals(""))
            {
                MessageBox.Show("Please Select required  Subject ");
            }
            else
            {
                return true;
            }

            return false;
        }

        //==============-------------------------------load session id1 Parallel---------------------------------------======================================
        public void loadPSessionid1cmb()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT session_id FROM Sessions";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string t = reader["session_id"].ToString();
                        txtSID1.Items.Add(t);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }
        //==============-------------------------------load session id2 Parallel---------------------------------------======================================
        public void loadPSessionid2cmb()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT session_id FROM Sessions";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string t = reader["session_id"].ToString();
                        txtSID2.Items.Add(t);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }

        //==============-------------------------------load session id3 Parallel---------------------------------------======================================
        public void loadPSessionid3cmb()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT session_id FROM Sessions";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string t = reader["session_id"].ToString();
                        txtSID3.Items.Add(t);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }

        //==============-------------------------------load session id4 Parallel---------------------------------------======================================
        public void loadPSessionid4cmb()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT session_id FROM Sessions";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string t = reader["session_id"].ToString();
                        txtSID4.Items.Add(t);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }

        //===================================Parallel Sessions add================================
        private void btnPadd_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateParallel())
            {
                ParallelSession Pseesions = new ParallelSession();

                Pseesions.PSession1 = int.Parse(txtSID1.Text);
                Pseesions.PSession2 = int.Parse(txtSID2.Text);
                Pseesions.PSession3 = int.Parse(txtSID3.Text);
                Pseesions.PSession4 = int.Parse(txtSID4.Text);
                Pseesions.PDAY = txtPday.Text;
                Pseesions.PTIME = txtPtime.Text;

                ParallelSessionDAO.insertParallelsession(Pseesions);
                PopulateParallelsesionstable(ParallelSessionDAO.getAllParallelSesions());
            }
            
        }
        private Boolean ValidateParallel()
        {
            if (txtSID1.Text.Equals(""))
            {
                MessageBox.Show("Please Enter Session ID 1");
            }
            if (txtSID2.Text.Equals(""))
            {
                MessageBox.Show("Please Enter Session ID 2");
            }
            if (txtSID3.Text.Equals(""))
            {
                MessageBox.Show("Please Enter Session ID 3");
            }
            if (txtSID4.Text.Equals(""))
            {
                MessageBox.Show("Please Enter Session ID 4");
            }
            if (txtPday.Text.Equals(""))
            {
                MessageBox.Show("Please Select Day");
            }
            if (txtPtime.Text.Equals(""))
            {
                MessageBox.Show("Please Select Time");
            }
            else
            {
                return true;
            }

            return false;
        }

        //================================================-------------------Non Overlapping Sessions--------------========================================


        //=============Populate Noramaln sessions =====================

        private void PopulatenoramalSTONONsesionstable(List<NonOverlappingSessions> list)
        {


            var observableList = new ObservableCollection<NonOverlappingSessions>();
            list.ForEach(x => observableList.Add(x));

            listViewNonOverlapping.ItemsSource = observableList;

        }

        //=============Populate Parallel sessions =====================

        private void PopulateNonOverlappingsesionstable(List<NonOverlappingSessions> list)
        {


            var observableList = new ObservableCollection<NonOverlappingSessions>();
            list.ForEach(x => observableList.Add(x));

            listViewNonOverAdded.ItemsSource = observableList;

        }

        //==============-------------------------------load session id1 Non overlapping---------------------------------------======================================
        public void loadNSessionid1cmb()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT session_id FROM Sessions";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string t = reader["session_id"].ToString();
                        txtNonSID1.Items.Add(t);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }
        //==============-------------------------------load session id2 Nonoverlaapping---------------------------------------======================================
        public void loadNSessionid2cmb()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT session_id FROM Sessions";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string t = reader["session_id"].ToString();
                        txtNonSID2.Items.Add(t);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }

        //==============-------------------------------load session id3 Nonoverlapping---------------------------------------======================================
        public void loadNSessionid3cmb()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT session_id FROM Sessions";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string t = reader["session_id"].ToString();
                        txtNonSID3.Items.Add(t);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }

        //==============-------------------------------load session id4 Nonoverlapping---------------------------------------======================================
        public void loadNSessionid4cmb()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT session_id FROM Sessions";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string t = reader["session_id"].ToString();
                        txtNonSID4.Items.Add(t);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }

        //==============-------------------------------load NonOverlapping Subgroupids---------------------------------------======================================
        public void loadNONSubgroupidcmb()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT subgrp_id FROM Sessions";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string t = reader["subgrp_id"].ToString();
                        txtNOsubGrp.Items.Add(t);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("-" + e);
                }
            }
        }

        private void btnNOsubmit_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateNonoverlappingSubmit())
            {
                NonOverlappingSessions nonoverlapSession = new NonOverlappingSessions();

                nonoverlapSession.NonSubgroupid = txtNOsubGrp.Text;
                PopulatenoramalSTONONsesionstable(NonOverlappingSessionDAO.getAllSesions(nonoverlapSession.NonSubgroupid));
            }
        }

        private Boolean ValidateNonoverlappingSubmit()
        {
            if (txtNOsubGrp.Text.Equals(""))
            {
                MessageBox.Show("Please Select required  Subject ");
            }
            else
            {
                return true;
            }

            return false;
        }

        private void btnNonadd_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateNonOverlapping())
            {
                NonOverlappingSessions Nonseesions = new NonOverlappingSessions();

                Nonseesions.NonSession1 = int.Parse(txtNonSID1.Text);
                Nonseesions.NonSession2 = int.Parse(txtNonSID2.Text);
                Nonseesions.NonSession3 = int.Parse(txtNonSID3.Text);
                Nonseesions.NonSession4 = int.Parse(txtNonSID4.Text);


                NonOverlappingSessionDAO.insertNonoverlappingsession(Nonseesions);
                PopulateNonOverlappingsesionstable(NonOverlappingSessionDAO.getAllNonOverlapSesions());

            }
            
        }

        private Boolean ValidateNonOverlapping()
        {
            if (txtNonSID1.Text.Equals(""))
            {
                MessageBox.Show("Please Enter Session ID 1");
            }
            if (txtNonSID2.Text.Equals(""))
            {
                MessageBox.Show("Please Enter Session ID 2");
            }
            if (txtNonSID3.Text.Equals(""))
            {
                MessageBox.Show("Please Enter Session ID 3");
            }
            if (txtNonSID4.Text.Equals(""))
            {
                MessageBox.Show("Please Enter Session ID 4");
            }
            else
            {
                return true;
            }

            return false;
        }



    }
}
