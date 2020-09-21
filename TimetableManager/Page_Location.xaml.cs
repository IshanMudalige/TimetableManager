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
using TimetableManager.RoomsLecturerDAO;
using TimetableManager.RoomsSubjectDAO;

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
            loadBuildingComboLec();
            loadBuildingComboSub();
            loadLecturerComboLec();
            loadSubCodeCombo();

            PopulateTableBuilding(BuildingNamesDAO.getAll());
            PopulateTableRooms(RoomNamesDAO.getAll());
            PopulateTableRoomLecturer(RoomLecturerDAO.getAll());
            PopulateTableRoomSubject(RoomSubjectDAO.getAll());
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
                MessageBox.Show("Please Select a Room from the Table.");
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

        private void clearRoomBtn_Click(object sender, RoutedEventArgs e)
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

        /*private void clearLecturerRoom_Click(object sender, RoutedEventArgs e)
        {
            selectLecturer.Text = "";
            selectBuildingNameLec.Text = "";
            locatedRoom.Text = "";
            selectRoomNameLec.Text = "";
        }*/

        private void listviewRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }






        /// ===================== ASSIGN ROOMS =======================================



        ///-----ASSIGN LECTURER SPECIFIC ROOM-----


        //get lecturers
        public void loadLecturerComboLec()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT name FROM Lecturer";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    string lname = reader["name"].ToString();
                    selectLecturer.Items.Add(lname);

                }

            }
        }

        //get selected lecturer based building
        private void selectLecturer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectLecturer.SelectedValue.ToString() != null)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.connString))
                {

                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT building FROM Lecturer WHERE name = @lname";
                    command.Parameters.AddWithValue("@lname", selectLecturer.SelectedValue.ToString());
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string blname = reader["building"].ToString();
                        locatedRoom.Text = blname;
                    }


                }
            }
        }

        
        private void locatedRoom_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        //load building lec
        public void loadBuildingComboLec()
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
                    selectBuildingNameLec.Items.Add(bname);

                }

            }
        }
        
        //select according room
        private void selectBuildingNameLec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectBuildingNameLec.SelectedValue.ToString() != null)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.connString))
                {

                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT r_name FROM Room_Names WHERE b_name = @bname";
                    command.Parameters.AddWithValue("@bname", selectBuildingNameLec.SelectedValue.ToString());
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        string rname = reader["r_name"].ToString();
                        selectRoomNameLec.Items.Add(rname);

                    }

                }
            } 
        }

        //add lec room
        private void addLecturerRoom_Click(object sender, RoutedEventArgs e)
        {
            RoomLecturer room = new RoomLecturer();

            room.LecBuildingName = selectBuildingNameLec.Text;
            room.LecRoomName = selectRoomNameLec.Text;
            room.LecturerName = selectLecturer.Text;

            RoomLecturerDAO.insertNewRoomLecturer(room);
            PopulateTableRoomLecturer(RoomLecturerDAO.getAll());
        }

        private void listviewLecturerRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //load table
        private void PopulateTableRoomLecturer(List<RoomLecturer> list)
        {
            //List<Building> list = BuildingNamesDAO.getAll();

            var observableList = new ObservableCollection<RoomLecturer>();
            list.ForEach(x => observableList.Add(x));

            listviewLecturerRoom.ItemsSource = observableList;
        }

        //remove
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RoomLecturer room = (RoomLecturer)listviewLecturerRoom.SelectedItem;

            if (room == null)
            {
                MessageBox.Show("Please Select a Room from the Table.");
            }
            else
            {
                RoomLecturerDAO.deleteRoomLecturer(room.LecRoomName);
                PopulateTableRoomLecturer(RoomLecturerDAO.getAll());
            }
        }

        private void clearSubjectRoom_Click(object sender, RoutedEventArgs e)
        {
            selectLecturer.Text = "";
            selectBuildingNameLec.Text = "";
            locatedRoom.Text = "";
            selectRoomNameLec.Text = "";
        }



        ///-----ASSIGN SUBJECT SPECIFIC ROOM-----


        public void loadSubCodeCombo()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT sub_code FROM Subjects";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    string scode = reader["sub_code"].ToString();
                    selectSubjectCode.Items.Add(scode);

                }

            }
        }

        private void subjectSelected_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        //load selected subject
        private void selectSubjectCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectSubjectCode.SelectedValue.ToString() != null)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.connString))
                {

                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT sub_name FROM Subjects WHERE sub_code = @scode";
                    command.Parameters.AddWithValue("@scode", selectSubjectCode.SelectedValue.ToString());
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string suname = reader["sub_name"].ToString();
                        subjectSelected.Text = suname;
                    }


                }
            }
        }


        public void loadBuildingComboSub()
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
                    selectBuildingSub.Items.Add(bname);

                }

            }
        }

        //select according room
        private void selectBuildingSub_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectBuildingSub.SelectedValue.ToString() != null)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.connString))
                {

                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT r_name FROM Room_Names WHERE b_name = @bname";
                    command.Parameters.AddWithValue("@bname", selectBuildingSub.SelectedValue.ToString());
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        string rname = reader["r_name"].ToString();
                        selectRoomSub.Items.Add(rname);

                    }

                }
            }
        }

        //add subject room
        private void addSubjectRoom_Click(object sender, RoutedEventArgs e)
        {
            RoomSubject room = new RoomSubject();

            room.SubjectCode = selectSubjectCode.Text;
            room.SubBuildingName = selectBuildingSub.Text;
            room.SubRoomName = selectRoomSub.Text;

            RoomSubjectDAO.insertNewRoomSubject(room);
            PopulateTableRoomSubject(RoomSubjectDAO.getAll());
        }

        //get results table
        private void PopulateTableRoomSubject(List<RoomSubject> list)
        {
            //List<Building> list = BuildingNamesDAO.getAll();

            var observableList = new ObservableCollection<RoomSubject>();
            list.ForEach(x => observableList.Add(x));

            listviewSubjectRoom.ItemsSource = observableList;
        }

        //remove subject room
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RoomSubject room = (RoomSubject)listviewSubjectRoom.SelectedItem;

            if (room == null)
            {
                MessageBox.Show("Please Select a Room from the Table.");
            }
            else
            {
                RoomSubjectDAO.deleteRoomSubject(room.SubjectCode);
                PopulateTableRoomSubject(RoomSubjectDAO.getAll());
            }
        }


        /*
        private void clearSubjectRoom_Click_1(object sender, RoutedEventArgs e)
        {
                selectSubjectCode.Text = "";
                selectBuildingSub.Text = "";
                selectRoomSub.Text = "";
        }*/

        
    }
}
