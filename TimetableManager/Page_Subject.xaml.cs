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
using TimetableManager.SubjectDAO;

namespace TimetableManager
{
    /// <summary>
    /// Interaction logic for Page_Subject.xaml
    /// </summary>
    public partial class Page_Subject : Page
    {
        public Page_Subject()
        {
            InitializeComponent();
            PopulateTable(SubjectDetailsDAO.getAll());
        }

        private void searchField_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchField.Text.Equals(""))
            {
                PopulateTable(SubjectDetailsDAO.getAll());
            }
            else
            {
                PopulateTable(SubjectDetailsDAO.search(searchField.Text));
            }
        }

        private void PopulateTable(List<Subject> list)
        {
            var observableList = new ObservableCollection<Subject>();
            list.ForEach(x => observableList.Add(x));

            listView.ItemsSource = observableList;
        }

        private void btnSubAdd_Click(object sender, RoutedEventArgs e)
        {
            if(Validate())
            {
                Subject subject = new Subject();

                subject.Year = cmbYear.Text;
                subject.Semester = cmbSem.Text;
                subject.SubName = txtSub.Text;
                subject.SubCode = txtCode.Text;
                subject.LecHrs = double.Parse(txtLecHrs.Text);
                subject.TuteHrs = double.Parse(txtTuteHrs.Text);
                subject.LabHrs = double.Parse(txtLabHrs.Text);
                subject.EvaluHrs = double.Parse(txtEvaHrs.Text);

                SubjectDetailsDAO.insertSubjects(subject);
                PopulateTable(SubjectDetailsDAO.getAll());
                clear();
            }

        
        }

        public void clear()
        {
            cmbYear.Text = "";
            cmbSem.Text = "";
            txtSub.Text = "";
            txtCode.Text = "";
            txtLecHrs.Text = "";
            txtTuteHrs.Text = "";
            txtLabHrs.Text = "";
            txtEvaHrs.Text = "";
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clear();
            Subject subject = (Subject)listView.SelectedItem;

            if(subject != null)
            {
                cmbYear.Text = subject.Year;
                cmbSem.Text = subject.Semester;
                txtSub.Text = subject.SubName;
                txtCode.Text = subject.SubCode;
                txtLecHrs.Text = subject.LecHrs.ToString();
                txtTuteHrs.Text = subject.TuteHrs.ToString();
                txtLabHrs.Text = subject.LabHrs.ToString();
                txtEvaHrs.Text = subject.EvaluHrs.ToString();
            }
        }

        private void btnSubDelete_Click(object sender, RoutedEventArgs e)
        {
            Subject subject = (Subject)listView.SelectedItem;

            if (subject == null)
            {
                MessageBox.Show("Please Select Required subject from the Table.");
            }
            else
            {
                SubjectDetailsDAO.deleteSubject(subject.SubCode);
                PopulateTable(SubjectDetailsDAO.getAll());
                clear();
            }
        }

        private Boolean Validate()
        {
            if(cmbYear.Text.Equals(""))
            {
                MessageBox.Show("Please Enter a Offered Year");
            }
            else if(cmbSem.Text.Equals(""))
            {
                MessageBox.Show("Please Enter a Offered Semester");
            }
            else if(txtSub.Text.Equals(""))
            {
                MessageBox.Show("Please Enter a Subject Name");
            }
            else if(txtCode.Text.Equals(""))
            {
                MessageBox.Show("Please Enter a Subject Code");
            }
            else if(txtLecHrs.Text.Equals(""))
            {
                MessageBox.Show("Please Enter number of Lecture Hours");
            }
            else if(txtTuteHrs.Text.Equals(""))
            {
                MessageBox.Show("Please Enter number of Tutorial Hours");
            }
            else if(txtLabHrs.Text.Equals(""))
            {
                MessageBox.Show("Please Enter number of Lab Hours");
            }
            else if(txtEvaHrs.Text.Equals(""))
            {
                MessageBox.Show("Please Enter number of Evaluation Hours");
            }
            else
            {
                return true;
            }

            return false;
        }

        private void btnSubUpdate_Click(object sender, RoutedEventArgs e)
        {
            Subject sub = (Subject)listView.SelectedItem;

            if(sub != null)
            {
                if(Validate())
                {
                    Subject subject = new Subject();
                    subject.Year = cmbYear.Text;
                    subject.Semester = cmbSem.Text;
                    subject.SubName = txtSub.Text;
                    subject.SubCode = txtCode.Text;
                    subject.LecHrs = double.Parse(txtLecHrs.Text);
                    subject.TuteHrs = double.Parse(txtTuteHrs.Text);
                    subject.LabHrs = double.Parse(txtLabHrs.Text);
                    subject.EvaluHrs = double.Parse(txtEvaHrs.Text);

                    SubjectDetailsDAO.updateSubject(sub.SubCode , subject);
                    PopulateTable(SubjectDetailsDAO.getAll());
                    clear();
                }
            }
            else
            {
                MessageBox.Show("Please Select the Required Subject from Table");
            }
        }

        private void btnSubClear_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }
    }
}
