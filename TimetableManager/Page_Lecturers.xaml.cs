using System;
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

            if(category == "Professor")
            {
                txtLevel.Text = "1";
            }
            else if(category == "Assistant Professor")
            {
                txtLevel.Text = "2";
            }
            else if(category == "Senior Lecturer(HG)")
            {
                txtLevel.Text = "3";
            }
            else if(category == "Senior Lecturer")
            {
                txtLevel.Text = "4";
            }
            else if(category == "Lecturer")
            {
                txtLevel.Text = "5";
            }
            else if(category == "Assistant Lecturer")
            {
                txtLevel.Text = "6";
            }
            else
            {
                txtLevel.Text = "7";
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
            lecturer.EmployeeID = int.Parse(txtLecID.Text);
            lecturer.Faculty = cmbLecFaculty.Text;
            lecturer.Department = txtLecDepartment.Text;
            lecturer.Center = cmbLecCenter.Text;
            lecturer.Building = txtLecBuilding.Text;
            lecturer.Category = cmbLecCategory.Text;
            lecturer.Level = txtLevel.Text;
            lecturer.Rank = txtLecRank.Text;

            LecturerDetailsDAO.insertLecture(lecturer);
            PopulateTable(LecturerDetailsDAO.getAll());
        }
    }
}
