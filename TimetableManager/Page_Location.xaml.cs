using System;
using System.Collections.Generic;
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
using TimetableManager.BuildingDAO;
using System.Collections;
using System.Collections.ObjectModel;

namespace TimetableManager
{
    /// <summary>
    /// Interaction logic for Page_Location.xaml
    /// </summary>
    public partial class Page_Location : Page
    {
        public Page_Location()
        {
            InitializeComponent();

            PopulateTable(BuildingNamesDAO.getAll());
        }

        private void addRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }


        private void PopulateTable(List<Building> list)
        {
            //List<Building> list = BuildingNamesDAO.getAll();

            var observableList = new ObservableCollection<Building>();
            list.ForEach(x => observableList.Add(x));

            listview.ItemsSource = observableList;
        }


        private void updateRoomBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void clearBuildingBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeBuildingBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addBuildingBtn_Click(object sender, RoutedEventArgs e)
        {
            Building building = new Building();
            building.BuildingName = addBuildingName.Text;

            BuildingNamesDAO.insertNewBuilding(building);
            PopulateTable(BuildingNamesDAO.getAll());
        }
    }
}
