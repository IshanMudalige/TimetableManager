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
using TimetableManager.RoomsGroupDAO;
using TimetableManager.RoomsTag;
using TimetableManager.NormalSessionsDAO;
using TimetableManager.RoomsAssignSession;
using TimetableManager.RoomsNotAvailableDAO;
using TimetableManager.ConsecutiveSessionsDAO;
using TimetableManager.RoomsConsecutive;

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

            loadAcademicIDCombo();

            loadTagsCombo();
            loadRoomTypeTagCombo();

            PopulateTableBuilding(BuildingNamesDAO.getAll());
            PopulateTableRooms(RoomNamesDAO.getAll());
            PopulateTableRoomLecturer(RoomLecturerDAO.getAll());
            PopulateTableRoomSubject(RoomSubjectDAO.getAll());
            PopulateTableRoomGroups(RoomGroupDAO.getAll());
            PopulateTableRoomTag(RoomTagDAO.getAll());
            PopulateTableRoomAssignOld(NorSessionsDetailsDAO.getAllSessionsAssign());

            PopulateTableRoomAssignNew(RoomAssignDAO.getAll());
            //loadRoomAssignCombo();

            PopulateTableRoomNotAvailable(RoomNotAvailableDAO.getAll());


            PopulateTableConsecutiveOld(ConsecutiveSessionsDao.getConsectiveAll());

            loadRoomTypeTagComboConsc();


            PopulateTableConsecutiveNew(RoomConsecutiveDAO.getAll());

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
                    selectBuildingGroup.Items.Add(bname);
                    selectBuildingNameNot.Items.Add(bname);
                }

            }
        }


        //Buidling Page functions

        //Add ZBuidling
        private void addBuildingBtn_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateLocationBuilding())
            {
                Building building = new Building();
                building.BuildingName = addBuildingName.Text;

                BuildingNamesDAO.insertNewBuilding(building);
                PopulateTableBuilding(BuildingNamesDAO.getAll());

                clearBuilding();
            }


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



        private Boolean ValidateLocationBuilding()
        {
            if (addBuildingName.Text.Equals(""))
            {
                MessageBox.Show("Please Add Building name");
            }
            
            else
            {
                return true;
            }

            return false;
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
            if(ValidateRooms())
            { 
                Room room = new Room();

                room.BuildingName = selectBuildingName.Text;
                room.RoomName = addRoomName.Text;
                room.RoomType = selectRoomType.Text;
                room.Capacity = int.Parse(capacity.Text);

                RoomNamesDAO.insertNewRoom(room);
                PopulateTableRooms(RoomNamesDAO.getAll());

                clearRooms();
            }
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

        //clear button
        private void clearRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            selectBuildingName.Text = "";
            addRoomName.Text = "";
            selectRoomType.Text = "";
            capacity.Text = "";
        }

        //validation
        private Boolean ValidateRooms()
        {
            if(selectBuildingName.Text.Equals("") || addRoomName.Text.Equals("") || selectRoomType.Text.Equals("") || capacity.Text.Equals(""))
            {
                MessageBox.Show("Please Fill Empty Fields");
            }
            else
            {
                return true;
            }

            return false;
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
            if(ValidateRoomLec())
            { 
                RoomLecturer room = new RoomLecturer();

                room.LecBuildingName = selectBuildingNameLec.Text;
                room.LecRoomName = selectRoomNameLec.Text;
                room.LecturerName = selectLecturer.Text;

                RoomLecturerDAO.insertNewRoomLecturer(room);
                PopulateTableRoomLecturer(RoomLecturerDAO.getAll());

                //clearRoomLec();
            }

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
        /*
        private void clearRoomLec()
        {
            selectLecturer.SelectedValue.ToString = "";
            selectBuildingNameLec.Text = "";
            locatedRoom.Text = "";
            selectRoomNameLec.Text = "";
        }
        */

        private Boolean ValidateRoomLec()
        {
            if (selectLecturer.Text.Equals("") || selectBuildingNameLec.Text.Equals("") || selectRoomNameLec.Text.Equals(""))
            {
                MessageBox.Show("Please Fill Empty Fields");
            }
            else
            {
                return true;
            }

            return false;
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
            if(ValidateRoomSub())
            { 
                RoomSubject room = new RoomSubject();

                room.SubjectCode = selectSubjectCode.Text;
                room.SubBuildingName = selectBuildingSub.Text;
                room.SubRoomName = selectRoomSub.Text;

                RoomSubjectDAO.insertNewRoomSubject(room);
                PopulateTableRoomSubject(RoomSubjectDAO.getAll());
                ValidateRoomSub();
            }
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

        private Boolean ValidateRoomSub()
        {
            if (selectSubjectCode.Text.Equals("") || selectBuildingSub.Text.Equals("") || selectRoomSub.Text.Equals(""))
            {
                MessageBox.Show("Please Fill Empty Fields");
            }
            else
            {
                return true;
            }

            return false;
        }







        //-----ASSIGN GROUP SPECIFIC ROOM-----




        //load academic id
        public void loadAcademicIDCombo()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT distinct(academic_id) FROM Groups_Info";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    string academicId = reader["academic_id"].ToString();
                    selectAcademicIdRoom.Items.Add(academicId);

                }

            }
        }

        //load group id
        private void selectAcademicIdRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT distinct(group_id) FROM Groups_Info WHERE academic_id = @academicId";
                command.Parameters.AddWithValue("@academicId", selectAcademicIdRoom.SelectedValue.ToString());
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    string groupId = reader["group_id"].ToString();
                    selectGroupIdRoom.Items.Add(groupId);

                }

            }
        }

        private void noOfStudentsGroupRoom_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        
        //load sub-group id
        private void selectGroupIdRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT subgroup_id FROM Groups_Info WHERE group_id = @groupId";
                command.Parameters.AddWithValue("@groupId", selectGroupIdRoom.SelectedValue.ToString());
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string subGroup = reader["subgroup_id"].ToString();
                    selectSubGroupIdRoom.Items.Add(subGroup);
                }


            }
        }

        //load student count in group
        private void selectSubGroupIdRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT student_count FROM Groups_Info WHERE subgroup_id = @subgroupId";
                command.Parameters.AddWithValue("@subgroupId", selectSubGroupIdRoom.SelectedValue.ToString());
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string stCount = reader["student_count"].ToString();
                    noOfStudentsGroupRoom.Text = stCount;
                }


            }
        }

        //select room
        private void selectBuildingGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT distinct(r_name) FROM Room_Names, Groups_Info WHERE (b_name = @bname AND @stCount < capacity)";
                command.Parameters.AddWithValue("@stCount", noOfStudentsGroupRoom.Text.ToString());
                command.Parameters.AddWithValue("@bname", selectBuildingGroup.SelectedValue.ToString());
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    string rname = reader["r_name"].ToString();
                    selectRoomGroup.Items.Add(rname);

                }

            }
        }

        //add room button
        private void addGroupRoom_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateRoomGroup())
            {
                RoomGroup room = new RoomGroup();

                room.GroupIdRoom = selectGroupIdRoom.Text;
                room.SubGroupIdRoom = selectSubGroupIdRoom.Text;
                room.GroupBuildingName = selectBuildingGroup.Text;
                room.GroupRoomName = selectRoomGroup.Text;

                RoomGroupDAO.insertNewRoomGroup(room);
                PopulateTableRoomGroups(RoomGroupDAO.getAll());
            }
        }

        //fill table group_rooms
        private void PopulateTableRoomGroups(List<RoomGroup> list)
        {

            var observableList = new ObservableCollection<RoomGroup>();
            list.ForEach(x => observableList.Add(x));

            listviewGroupRoom.ItemsSource = observableList;
        }

        private void listviewGroupRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //remove button
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            RoomGroup room = (RoomGroup)listviewGroupRoom.SelectedItem;

            if (room == null)
            {
                MessageBox.Show("Please Select a Group from the Table.");
            }
            else
            {
                RoomGroupDAO.deleteRoomGroup(room.SubGroupIdRoom);
                PopulateTableRoomGroups(RoomGroupDAO.getAll());
            }
        }


        private Boolean ValidateRoomGroup()
        {
            if (selectAcademicIdRoom.Text.Equals("") || selectGroupIdRoom.Text.Equals("") || selectSubGroupIdRoom.Text.Equals("") || selectBuildingGroup.Text.Equals("") || selectSubGroupIdRoom.Text.Equals(""))
            {
                MessageBox.Show("Please Fill Empty Fields");
            }
            else
            {
                return true;
            }

            return false;
        }




        //======================================
        //-----ASSIGN TAG SPECIFIC ROOM-----
        //======================================


        //load tags
        public void loadTagsCombo()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT distinct(tagname) FROM Tag";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    string tag = reader["tagname"].ToString();
                    selectTagRoom.Items.Add(tag);

                }

            }
        }

        //load types
        public void loadRoomTypeTagCombo()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT distinct(room_type) FROM Room_Names";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    string roomType = reader["room_type"].ToString();
                    selectRoomTypeTag.Items.Add(roomType);

                }

            }
        }

        //add tag
        private void addTagRoom_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateRoomTag())
            {
                RoomTag room = new RoomTag();

                room.Tag = selectTagRoom.Text;
                room.RoomTypeTag = selectRoomTypeTag.Text;

                RoomTagDAO.insertNewRoomTag(room);
                PopulateTableRoomTag(RoomTagDAO.getAll());
            }
        }
        
        //fill tables
        private void PopulateTableRoomTag(List<RoomTag> list)
        {

            var observableList = new ObservableCollection<RoomTag>();
            list.ForEach(x => observableList.Add(x));

            listviewTagRoom.ItemsSource = observableList;
        }

        //delete tag
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            RoomTag room = (RoomTag)listviewTagRoom.SelectedItem;

            if (room == null)
            {
                MessageBox.Show("Please Select a tag from the Table.");
            }
            else
            {
                RoomTagDAO.deleteRoomTag(room.Tag);
                PopulateTableRoomTag(RoomTagDAO.getAll());
            }
        }


        private Boolean ValidateRoomTag()
        {
            if (selectTagRoom.Text.Equals("") || selectRoomTypeTag.Text.Equals(""))
            {
                MessageBox.Show("Please Fill Empty Fields");
            }
            else
            {
                return true;
            }

            return false;
        }







        //======================================
        //-----ASSIGN SESSIONS -----
        //======================================



        private void PopulateTableRoomAssignOld(List<NormalSessions> list)
        {

            var observableList = new ObservableCollection<NormalSessions>();
            list.ForEach(x => observableList.Add(x));

            listviewSessionsOld.ItemsSource = observableList;

            
        }

        private void listviewSessionsOld_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            NormalSessions room = (NormalSessions)listviewSessionsOld.SelectedItem;

            if (room != null)
            {
                subjectCodeAssign.Text = room.Scode;
                sessionId.Text = room.Sid.ToString();
                groupIdAssign.Text = room.SubID;
                tagAssign.Text = room.Tag;
            }
        }

        

        private void groupIdAssign_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT no_students FROM Sessions WHERE subgrp_id = @subgroupId";
                command.Parameters.AddWithValue("@subgroupId", groupIdAssign.Text.ToString());
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string stCount = reader["no_students"].ToString();
                    noOfStudentsAssign.Text = stCount;
                }


            }
        }

        private void preferTag_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*if(preferSubject.Text.ToString() != null)
            { 
                using (SQLiteConnection conn = new SQLiteConnection(App.connString))
                {

                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT r_name FROM Room_Names, Room_Names_Subject WHERE (r_name = rs_name AND rs_name = @rname)"; 
                    command.Parameters.AddWithValue("@rname", preferSubject.Text.ToString());
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string rtype = reader["r_name"].ToString();
                        selectRoomAssign.Items.Add(rtype);
                    }


                }


            }*/
            
            if (preferSubject.Text.ToString() != null)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.connString))
                {

                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT distinct(r_name) FROM Room_Names WHERE room_type = @type";
                    command.Parameters.AddWithValue("@type", preferTag.Text.ToString());
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        string rname = reader["r_name"].ToString();
                        selectRoomAssign.Items.Add(rname);
                    }
                }

            }
        }

        /*public void loadRoomAssignCombo()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT r_name FROM Room_Names WHERE room_type = @type";
                command.Parameters.AddWithValue("@type", preferTag.Text.ToString());
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    string rname = reader["r_name"].ToString();
                    selectRoomAssign.Items.Add(rname);
                }

            }
        }*/

        private void tagAssign_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT rt_type FROM Room_Names_Tag WHERE tag_room = @tag";
                command.Parameters.AddWithValue("@tag", tagAssign.Text.ToString());
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string rtype = reader["rt_type"].ToString();
                    preferTag.Text = rtype;
                }


            }
        }

        private void subjectCodeAssign_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT rs_name FROM Room_Names_Subject WHERE s_code = @scode";
                command.Parameters.AddWithValue("@scode", subjectCodeAssign.Text.ToString());
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string suname = reader["rs_name"].ToString();
                    preferSubject.Text = suname;
                }


            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (ValidateRoomAssign())
            {
                RoomAssign room = new RoomAssign();

                room.Sid = int.Parse(sessionId.Text);
                room.Scode = subjectCodeAssign.Text;
                room.SubID = groupIdAssign.Text;
                room.Tag = tagAssign.Text;
                room.RoomName = selectRoomAssign.Text;

                RoomAssignDAO.insertNewRoomSessions(room);
                PopulateTableRoomAssignNew(RoomAssignDAO.getAll());
            }
        }


        private void PopulateTableRoomAssignNew(List<RoomAssign> list)
        {

            var observableList = new ObservableCollection<RoomAssign>();
            list.ForEach(x => observableList.Add(x));

            listviewAssignedSession.ItemsSource = observableList;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            RoomAssign room = (RoomAssign)listviewAssignedSession.SelectedItem;

            if (room == null)
            {
                MessageBox.Show("Please Select a tag from the Table.");
            }
            else
            {
                RoomAssignDAO.deleteRoomAssign(room.Sid);
                PopulateTableRoomAssignNew(RoomAssignDAO.getAll());
            }
        }


        private Boolean ValidateRoomAssign()
        {
            if (selectRoomAssign.Text.Equals(""))
            {
                MessageBox.Show("Please Fill Empty Fields");
            }
            else
            {
                return true;
            }

            return false;
        }







        //======================================
        //-----NOT AVAILABLE ROOMS -----
        //======================================

        private void selectBuildingNameNot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectBuildingNameNot.SelectedValue.ToString() != null)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.connString))
                {

                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT r_name FROM Room_Names WHERE b_name = @bname";
                    command.Parameters.AddWithValue("@bname", selectBuildingNameNot.SelectedValue.ToString());
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        string rname = reader["r_name"].ToString();
                        selectRoomNameNot.Items.Add(rname);

                    }

                }
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (ValidateRoomNotAvailable())
            {
                RoomNotAvailable room = new RoomNotAvailable();

                room.BuildingNameNA = selectBuildingNameNot.Text;
                room.RoomNameNA = selectRoomNameNot.Text;
                room.Description = description.Text;
                room.Day = datePick.Text;
                room.From = timeFrom.Text.ToString();
                room.To = timeTo.Text.ToString();

                RoomNotAvailableDAO.insertRoomNotAvailable(room);
                PopulateTableRoomNotAvailable(RoomNotAvailableDAO.getAll());
            }
        }


        private void PopulateTableRoomNotAvailable(List<RoomNotAvailable> list)
        {

            var observableList = new ObservableCollection<RoomNotAvailable>();
            list.ForEach(x => observableList.Add(x));

            listviewNotAvailableRoom.ItemsSource = observableList;
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            RoomNotAvailable room = (RoomNotAvailable)listviewNotAvailableRoom.SelectedItem;

            if (room == null)
            {
                MessageBox.Show("Please Select a tag from the Table.");
            }
            else
            {
                RoomNotAvailableDAO.deleteRoomNotAvailable(room.Nid.ToString());
                PopulateTableRoomNotAvailable(RoomNotAvailableDAO.getAll());
            }
        }

        private Boolean ValidateRoomNotAvailable()
        {
            if (selectBuildingNameNot.Text.Equals("") || selectRoomNameNot.Text.Equals("") || description.Text.Equals("") || datePick.Text.Equals("") || timeFrom.Text.Equals("") || timeTo.Text.Equals(""))
            {
                MessageBox.Show("Please Fill Empty Fields");
            }
            else
            {
                return true;
            }

            return false;
        }







        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>








        private void PopulateTableConsecutiveOld(List<ConsecutiveSession> list)
        {

            var observableList = new ObservableCollection<ConsecutiveSession>();
            list.ForEach(x => observableList.Add(x));

            listviewSessionCons.ItemsSource = observableList;


        }

        private void listviewSessionCons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConsecutiveSession room = (ConsecutiveSession)listviewSessionCons.SelectedItem;

            if (room != null)
            {
                consSesId.Text = room.ConsSesId.ToString();
                consSubName.Text = room.ConsecutiveSubject;
                consDay.Text = room.ConsecutiveDay;
                consTime.Text = room.ConsecutiveTime;
                consTag.Text = room.ConsecutiveTag;
            }
        }



        public void loadRoomTypeTagComboConsc()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT distinct(room_type) FROM Room_Names";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    string roomType = reader["room_type"].ToString();
                    selectConsType.Items.Add(roomType);

                }

            }
        }

        private void selectConsType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT distinct(r_name) FROM Room_Names WHERE room_type = @type";
                command.Parameters.AddWithValue("@type", selectConsType.SelectedValue.ToString());
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    string rname = reader["r_name"].ToString();
                    selectConsRoom.Items.Add(rname);
                }
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            RoomConsecutive room = new RoomConsecutive();

            room.ConsSesIdNew = int.Parse(consSesId.Text.ToString());
            room.SubjectCons = consSubName.Text;
            room.DayCons = consDay.Text;
            room.TimeCons = consTime.Text;
            room.TagCons = consTag.Text;
            room.RoomCons = selectConsRoom.Text;

            RoomConsecutiveDAO.insertConsecutiveRoom(room);
            //PopulateTableRoomNotAvailable(RoomNotAvailableDAO.getAll());
        }



        private void PopulateTableConsecutiveNew(List<RoomConsecutive> list)
        {

            var observableList = new ObservableCollection<RoomConsecutive>();
            list.ForEach(x => observableList.Add(x));

            listviewConsNew.ItemsSource = observableList;


        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            RoomConsecutive room = (RoomConsecutive)listviewConsNew.SelectedItem;

            if (room == null)
            {
                MessageBox.Show("Please Select a tag from the Table.");
            }
            else
            {
                RoomConsecutiveDAO.deleteRoomCons(room.ConsSesIdNew.ToString());
                PopulateTableConsecutiveNew(RoomConsecutiveDAO.getAll());
            }
        }

        
    }
}
