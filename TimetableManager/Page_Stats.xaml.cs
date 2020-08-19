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
using TimetableManager.StatisticsDAO;
using TimetableManager.StudentDAO;

namespace TimetableManager
{
    /// <summary>
    /// Interaction logic for Page_Stats.xaml
    /// </summary>
    public partial class Page_Stats : Page
    {
        public Page_Stats()
        {
            InitializeComponent();
            PopulateStatLec(LecStatDAO.getAll());
            PopulateStatStu(StuStatDAO.getAll());
        }

        private void PopulateStatLec(List<LecStat> list)
        {
            //List<Building> list = BuildingNamesDAO.getAll();

            var observableList = new ObservableCollection<LecStat>();
            list.ForEach(x => observableList.Add(x));

            listviewLecturer.ItemsSource = observableList;
        }


        private void PopulateStatStu(List<StuStat> list)
        {
            //List<Building> list = BuildingNamesDAO.getAll();

            var observableList = new ObservableCollection<StuStat>();
            list.ForEach(x => observableList.Add(x));

            listviewStudents.ItemsSource = observableList;
        }
    }
}
