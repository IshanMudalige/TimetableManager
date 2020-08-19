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
using TimetableManager.LecturerDAO;

namespace TimetableManager
{
    /// <summary>
    /// Interaction logic for Page_Lecturers.xaml
    /// </summary>
    public partial class Page_Lecturers : Page
    {
        public Page_Lecturers()
        {
            InitializeComponent();
            loadBuildingCombo();
            PopulateTable(LecturerDetailsDAO.getAll());

        }



        private void btnLecRank_Click(object sender, RoutedEventArgs e)
        {
            string level = txtLevel.Text;
            string empID = txtLecID.Text;

            txtLecRank.Text = level + "." + empID;
            //txtLecRank.Text = string.Join(level,".",empID);
        }

        private void txtLevel_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string category = cmbLecCategory.Text;

            if (category == "Professor")
            {
                txtLevel.Text = "1";
            }
            else if (category == "Assistant Professor")
            {
                txtLevel.Text = "2";
            }
            else if (category == "Senior Lecturer(HG)")
            {
                txtLevel.Text = "3";
            }
            else if (category == "Senior Lecturer")
            {
                txtLevel.Text = "4";
            }
            else if (category == "Lecturer")
            {
                txtLevel.Text = "5";
            }
            else if (category == "Assistant Lecturer")
            {
                txtLevel.Text = "6";
            }
            else if(category == "Instructors")
            {
                txtLevel.Text = "7";
            }
            else
            {
                txtLevel.Text = "";
            }
        }

        private void PopulateTable(List<Lecturer> list)
        {
            var observableList = new ObservableCollection<Lecturer>();
            list.ForEach(x => observableList.Add(x));

            listView.ItemsSource = observableList;
        }

        private void searchField_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchField.Text.Equals(""))
            {
                PopulateTable(LecturerDetailsDAO.getAll());
            }
            else
            {
                PopulateTable(LecturerDetailsDAO.search(searchField.Text));
            }
        }

        private void btnLecAdd_Click(object sender, RoutedEventArgs e)
        {
            Lecturer lecturer = new Lecturer();

            lecturer.Name = txtLecName.Text;
            lecturer.EmployeeID = txtLecID.Text;
            lecturer.Faculty = cmbLecFaculty.Text;
            lecturer.Department = txtLecDepartment.Text;
            lecturer.Center = cmbLecCenter.Text;
            lecturer.Building = txtLecBuilding.Text;
            lecturer.Category = cmbLecCategory.Text;
            lecturer.Level = txtLevel.Text;
            lecturer.Rank = txtLecRank.Text;

            LecturerDetailsDAO.insertLecture(lecturer);
            PopulateTable(LecturerDetailsDAO.getAll());
            clear();
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clear();
            Lecturer lecturer = (Lecturer)listView.SelectedItem;

            if (lecturer != null)
            {
                txtLecName.Text = lecturer.Name;
                txtLecID.Text = lecturer.EmployeeID;
                cmbLecFaculty.Text = lecturer.Faculty;
                txtLecDepartment.Text = lecturer.Department;
                cmbLecCenter.Text = lecturer.Center;
                txtLecBuilding.Text = lecturer.Building;
                cmbLecCategory.Text = lecturer.Category;
                txtLevel.Text = lecturer.Level;
                txtLecRank.Text = lecturer.Rank;

            }
        }

        public void clear()
        {
            txtLecName.Text = "";
            txtLecID.Text = "";
            cmbLecFaculty.Text = "";
            txtLecDepartment.Text = "";
            cmbLecCenter.Text = "";
            txtLecBuilding.Text = "";
            cmbLecCategory.Text = "";
            txtLevel.Text = "";
            txtLecRank.Text = "";
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Lecturer lecturer = (Lecturer)listView.SelectedItem;

            if (lecturer == null)
            {
                MessageBox.Show("Please Select Required lecturer from the Table.");
            }
            else
            {
                LecturerDetailsDAO.deleteLecturer(lecturer.EmployeeID);
                PopulateTable(LecturerDetailsDAO.getAll());
                clear();
            }
        }

        private void btnLecUpdate_Click(object sender, RoutedEventArgs e)
        {
            Lecturer lec = (Lecturer)listView.SelectedItem;

            if (lec != null)
            {
                if (ValidateFields())
                {
                    Lecturer lecturer = new Lecturer();
                    lecturer.Name = txtLecName.Text;
                    lecturer.EmployeeID = txtLecID.Text;
                    lecturer.Faculty = cmbLecFaculty.Text;
                    lecturer.Department = txtLecDepartment.Text;
                    lecturer.Center = cmbLecCenter.Text;
                    lecturer.Building = txtLecBuilding.Text;
                    lecturer.Category = cmbLecCategory.Text;
                    lecturer.Level = txtLevel.Text;
                    lecturer.Rank = txtLecRank.Text;

                    LecturerDetailsDAO.updateLecturer(lec.EmployeeID, lecturer);
                    PopulateTable(LecturerDetailsDAO.getAll());
                    clear();
                }

            }
            else
            {
                MessageBox.Show("Please Select the Required Lecturer from Table");
            }
        }

        private Boolean ValidateFields()
        {
            if (txtLecName.Text.Equals(""))
            {
                MessageBox.Show("Please Enter a Name");
            }
            if (txtLecID.Text.Equals(""))
            {
                MessageBox.Show("Please Enter a EmployeeID");
            }
            if (cmbLecFaculty.Text.Equals(""))
            {
                MessageBox.Show("Please Select a Faculty");
            }
            if (txtLecDepartment.Text.Equals(""))
            {
                MessageBox.Show("Please Enter a Department");
            }
            if (cmbLecCenter.Text.Equals(""))
            {
                MessageBox.Show("Please Select a Center");
            }
            if (txtLecBuilding.Text.Equals(""))
            {
                MessageBox.Show("Please Enter a Building");
            }
            if (cmbLecCategory.Text.Equals(""))
            {
                MessageBox.Show("Please Select a Category");
            }
            if (txtLevel.Text.Equals(""))
            {
                MessageBox.Show("Please Get a Level");
            }
            if (txtLecRank.Text.Equals(""))
            {
                MessageBox.Show("Please Get a Rank");
            }
            else
            {
                return true;
            }

            return false;
        }

        public void loadBuildingCombo()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT b_name FROM Building_Names";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        string bname = reader["b_name"].ToString();
                        txtLecBuilding.Items.Add(bname);

                    }
                }
                catch(SQLiteException e)
                {
                    MessageBox.Show(e.Message);
                }
                

            }
        }

    }
}
