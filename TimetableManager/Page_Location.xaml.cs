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
using System.Data.SQLite;
using TimetableManager.RoomsDAO;

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
            loadBuildingCombo();
            PopulateTableBuilding(BuildingNamesDAO.getAll());
            PopulateTableRooms(RoomNamesDAO.getAll());
        }

        //Building Names COMBO BOX - ROOMS PAGE
        public void loadBuildingCombo()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT b_name FROM Building_Names";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    
                    string bname = reader["b_name"].ToString();
                    selectBuildingName.Items.Add(bname);

                }

            }
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

            PopulateTableBuilding(BuildingNamesDAO.getAll());
        }

        
        //list view buildings
        private void listview_SelectionChanged_Building(object sender, SelectionChangedEventArgs e)
        {
            clearBuilding();

            Building build = (Building)listview.SelectedItem;

            if(build != null)
                addBuildingName.Text = build.BuildingName;
        }

        private void PopulateTableBuilding(List<Building> list)
        {
            //List<Building> list = BuildingNamesDAO.getAll();

            var observableList = new ObservableCollection<Building>();
            list.ForEach(x => observableList.Add(x));

            listview.ItemsSource = observableList;
        }

        //remove buildings
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





        /// <summary>
        /// ADD ROOMS
        /// </summary>

        //Buttons
        private void PopulateTableRooms(List<Room> list)
        {
            //List<Building> list = BuildingNamesDAO.getAll();
            
            var observableList = new ObservableCollection<Room>();
            list.ForEach(x => observableList.Add(x));

            listviewRoom.ItemsSource = observableList;
        }

        private void addRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            Room room = new Room();

            room.BuildingName = selectBuildingName.Text;
            room.RoomName = addRoomName.Text;
            room.RoomType = selectRoomType.Text;
            room.Capacity = int.Parse(capacity.Text);

            RoomNamesDAO.insertNewRoom(room);
            PopulateTableRooms(RoomNamesDAO.getAll());
        }

        private void listview_SelectionChanged_Room(object sender, SelectionChangedEventArgs e)
        {
            clearRooms();

            Room room = (Room)listviewRoom.SelectedItem;

            if (room != null)
            {
                selectBuildingName.Text = room.BuildingName;
                addRoomName.Text = room.RoomName;
                selectRoomType.Text = room.RoomType;
                capacity.Text = room.Capacity.ToString();
            }
        }

        private void updateRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            Room rn = (Room)listviewRoom.SelectedItem;

            Room room = new Room();
            room.BuildingName = selectBuildingName.Text;
            room.RoomName = addRoomName.Text;
            room.RoomType = selectRoomType.Text;
            room.Capacity = int.Parse(capacity.Text);

            RoomNamesDAO.updateRoom(rn.RoomName, room);
            PopulateTableRooms(RoomNamesDAO.getAll());

            clearRooms();
        }

        private void removeRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            Room room = (Room)listviewRoom.SelectedItem;

            if (room == null)
            {
                MessageBox.Show("Please Select Required Week from the Table.");
            }
            else
            {
                RoomNamesDAO.deleteRoom(room.RoomName);
                PopulateTableRooms(RoomNamesDAO.getAll());
                clearRooms();
            }
        }

        //clear fields add update 
        private void clearRooms()
        {
            selectBuildingName.Text = "";
            addRoomName.Text = "";
            selectRoomType.Text = "";
            capacity.Text = "";
        }



        //Text Fields

        private void searchRoom_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void selectBuildingName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void addRoomName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void selectRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void capacity_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void clearRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            selectBuildingName.Text = "";
            addRoomName.Text = "";
            selectRoomType.Text = "";
            capacity.Text = "";
        }

        private void listviewRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        
    }
}
