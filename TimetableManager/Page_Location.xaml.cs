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

            PopulateTableBuilding(BuildingNamesDAO.getAll());
        }


        //Buidling Page functions

        //Add ZBuidling
        private void addBuildingBtn_Click(object sender, RoutedEventArgs e)
        {
            Building building = new Building();
            building.BuildingName = addBuildingName.Text;

            BuildingNamesDAO.insertNewBuilding(building);
            PopulateTableBuilding(BuildingNamesDAO.getAll());

            clearBuilding(); 
        }

        //Update Building
        private void editBuildingBtn_Click(object sender, RoutedEventArgs e)
        {
            Building bn = (Building)listview.SelectedItem;

            Building building = new Building();
            building.BuildingName = addBuildingName.Text;

            BuildingNamesDAO.updateBuilding(bn.BuildingName, building);
        }

        
        private void listview_SelectionChanged_Building(object sender, SelectionChangedEventArgs e)
        {
            clearBuilding();
            Building build = (Building)listview.SelectedItem;

            addBuildingName.Text = build.BuildingName;
        }

        private void PopulateTableBuilding(List<Building> list)
        {
            //List<Building> list = BuildingNamesDAO.getAll();

            var observableList = new ObservableCollection<Building>();
            list.ForEach(x => observableList.Add(x));

            listview.ItemsSource = observableList;
        }

        private void removeBuildingBtn_Click(object sender, RoutedEventArgs e)
        {
            Building building = (Building)listview.SelectedItem;
            BuildingNamesDAO.deleteBuilding(building.BuildingName);
            PopulateTableBuilding(BuildingNamesDAO.getAll());
            clearBuilding();
        }


        //--clear fields
        private void clearBuilding()
        {
            addBuildingName.Text = "";
        } 



        //Add Rooms

        private void addRoomBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void updateRoomBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeRoomBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
