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
using TimetableManager.StudentDAO;
using System.Data.SQLite;
using TimetableManager.GroupDAO;

namespace TimetableManager
{
    /// <summary>
    /// Interaction logic for Page_Student.xaml
    /// </summary>
    public partial class Page_Student : Page
    {
        public Page_Student()
        {
            InitializeComponent();
            loadAcademicIDCombo();
            PopulateTableStudent(StudentDetails.getAll());
            PopulateTableGroup(GroupDetailsDAO.getAll());
           

        }

        //Student Year Combo Box -Group page

        public void loadAcademicIDCombo()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT proid FROM Student";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string acid = reader["proid"].ToString();
                        selectacdemicId.Items.Add(acid);
                    }
                }catch(Exception e)
                {
                    MessageBox.Show("-"+e);
                }
            }
        }

        //============Add Student===================================================================
        private void addstudent_Click(object sender, RoutedEventArgs e)
        {
            Student student = new Student();

            student.Year = txtyear.Text;
            student.Semester = txtsem.Text;
            student.Programme = txtProgramm.Text;
            student.Programmid = txtProid.Text;

            StudentDetails.insertStudent((student));


        }

        private void PopulateTableStudent(List<Student> list)
        {
            var observableList = new ObservableCollection<Student>();
            list.ForEach(x => observableList.Add(x));

            listView.ItemsSource = observableList;
        }


        //==============================================Search student====================================
        private void searchstudent_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchstudent.Text.Equals(""))
            {
                PopulateTableStudent(StudentDetails.getAll());
            }
            else
            {
                PopulateTableStudent(StudentDetails.searchs(searchstudent.Text));
            }
        }


        //==============================================Delete student====================================
        private void btndeletestudent_Click(object sender, RoutedEventArgs e)
        {
            Student student = (Student)listView.SelectedItem;

            if (student == null)
            {
                MessageBox.Show("Please Select required Student from the table.. ");

            }
            else
            {
                StudentDetails.deletestudent(student.Programmid);
                PopulateTableStudent(StudentDetails.getAll());
                clear();

            }
        }

        //==============================================listview  student====================================
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clear();
            Student student = (Student)listView.SelectedItem;

            if (student != null)
            {
                txtyear.Text = student.Year;
                txtsem.Text = student.Semester;
                txtProgramm.Text = student.Programme;
                txtProid.Text = student.Programmid;

            }
        }

        public void clear()
        {
            txtyear.Text = "";
            txtsem.Text = "";
            txtProgramm.Text = "";
            txtProid.Text = "";
        }



        //==============================================update student====================================

        private void updatestudent_Click(object sender, RoutedEventArgs e)
        {
            Student stu = (Student)listView.SelectedItem;

            if (stu != null)
            {
                if (ValidatedFields())
                {
                    Student student = new Student();
                    student.Year = txtyear.Text;
                    student.Semester = txtsem.Text;
                    student.Programme = txtProgramm.Text;
                    student.Programmid = txtProid.Text;

                    StudentDetails.updatestudent(stu.Programmid,student);
                    PopulateTableStudent(StudentDetails.getAll());
                    clear();
                }
            }
            else
            {
                MessageBox.Show("Pleses select required programm from table");

            }
        }

        private Boolean ValidatedFields()
        {
            if (txtyear.Text.Equals(""))
            {
                MessageBox.Show("Please enter year");
            }
            if (txtsem.Text.Equals(""))
            {
                MessageBox.Show("Please enter semester");
            }
            if (txtProgramm.Text.Equals(""))
            {
                MessageBox.Show("Please enter programme");
            }
            if (txtProid.Text.Equals(""))
            {
                MessageBox.Show("Please enter programme id");
            }
            else
            {
                return true;
            }

            return false;
        }

        private void txtProid_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string year = txtyear.Text;
            string semester = txtsem.Text;
            string programme = txtProgramm.Text;

            txtProid.Text = year + "." + semester + "." + programme;
        }


        //=================================================================GROUP========================================================

        private void PopulateTableGroup(List<Groups> list)
        {
            var observableList = new ObservableCollection<Groups>();
            list.ForEach(x => observableList.Add(x));

            listView2.ItemsSource = observableList;
        }

        private void addgroup_Click(object sender, RoutedEventArgs e)
        {
            Groups group = new Groups();


            group.AcademicId = selectacdemicId.Text;
            group.StudentCount = int.Parse(txtstudentcount.Text);
            group.Groupno = int.Parse(txtgroupno.Text);
            group.GroupId = txtgroupid.Text;
            group.SubGroupno = int.Parse(txtsubgroupno.Text);
            group.SubGroupId = txtsubgroupid.Text;

            GroupDetailsDAO.insertgroups(group);
            PopulateTableGroup(GroupDetailsDAO.getAll());
            cleargrp();
        }

        public void cleargrp()
        {
            selectacdemicId.Text = "";
            txtstudentcount.Text = "";
            txtgroupno.Text = "";
            txtgroupid.Text = "";
            txtsubgroupno.Text = "";
            txtsubgroupid.Text = "";
        }

        private void updategroup_Click(object sender, RoutedEventArgs e)
        {
            Groups groups1 = (Groups)listView2.SelectedItem;

            if(groups1 != null)
            {
                if (Validate())
                {
                    Groups groups = new Groups();
                    groups.AcademicId = selectacdemicId.Text;
                    groups.StudentCount = int.Parse(txtstudentcount.Text);
                    groups.Groupno = int.Parse(txtgroupno.Text);
                    groups.GroupId = txtgroupid.Text;
                    groups.SubGroupno = int.Parse(txtsubgroupno.Text);
                    groups.SubGroupId = txtsubgroupid.Text;

                    GroupDetailsDAO.updategroups(groups1.GroupId, groups);
                    PopulateTableGroup(GroupDetailsDAO.getAll());
                    cleargrp();





                }
                else
                {
                    MessageBox.Show("Please select the required Groups fromthe table");
                }
            }
        }

        private Boolean Validate()
        {
            if (selectacdemicId.Text.Equals(""))
            {
                MessageBox.Show("Please enter academicid");
            }
            if (txtstudentcount.Text.Equals(""))
            {
                MessageBox.Show("Please enter student count");
            }
            if (txtgroupno.Text.Equals(""))
            {
                MessageBox.Show("Please enter group no");
            }
            if (txtgroupid.Text.Equals(""))
            {
                MessageBox.Show("Please enter group id");
            }
            if (txtsubgroupno.Text.Equals(""))
            {
                MessageBox.Show("Please enter subgroup no");
            }
            if (txtsubgroupid.Text.Equals(""))
            {
                MessageBox.Show("Please enter subgroup  id");
            }
            else
            {
                return true;
            }

            return false;
        }

        private void searchgrp_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (searchgrp.Text.Equals(""))
            {
                PopulateTableGroup(GroupDetailsDAO.getAll());
            }
            else
            {
                PopulateTableGroup(GroupDetailsDAO.searchgroups(searchgrp.Text));
            }
        }

       
                
         
        private void listView2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cleargrp();

            Groups group = (Groups)listView2.SelectedItem;

            if(group != null)
            {
                selectacdemicId.Text = group.AcademicId;
                txtstudentcount.Text = group.StudentCount.ToString();
                txtgroupno.Text = group.Groupno.ToString();
                txtgroupid.Text = group.GroupId;
                txtsubgroupno.Text = group.SubGroupno.ToString();
                txtsubgroupid.Text = group.SubGroupId;

            }
        }

        //===================Generate Group Id=========================
        private void txtgroupid_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string academic_id = selectacdemicId.Text;
            string  group_no = txtgroupno.Text;

            txtgroupid.Text = academic_id + "." + group_no;


        }

        //====================Generate subgroup Id===================================================
        private void txtsubgroupid_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string group_id = txtgroupid.Text;
            string subgroup_no = txtsubgroupno.Text;

            txtsubgroupid.Text = group_id + "." + subgroup_no;
        }

        private void btndeletegrp_Click(object sender, RoutedEventArgs e)
        {
            Groups group = (Groups)listView2.SelectedItem;

            if (group == null)
            {
                MessageBox.Show("please select required group from the table");

            }
            else
            {
                GroupDetailsDAO.deletegroups(group.GroupId);
                PopulateTableGroup(GroupDetailsDAO.getAll());
                cleargrp();
            }
        }
    } 
}
