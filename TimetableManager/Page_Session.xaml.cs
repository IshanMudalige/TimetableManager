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
            string grpId ="";
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
                catch(SQLiteException ex)
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
                catch(SQLiteException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //Getting selected lecturer names from combobox to text area
        private void txtLecNames_SelectionChanged(object sender, RoutedEventArgs e)
        {
            for(int i=0; i<=3; i++)
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
    }
}
