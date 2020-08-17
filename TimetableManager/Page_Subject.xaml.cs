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

        }
    }
}
