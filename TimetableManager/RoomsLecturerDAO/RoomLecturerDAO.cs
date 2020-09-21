using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.RoomsLecturerDAO
{
    class RoomLecturerDAO
    {

        public RoomLecturerDAO() { }

        //insert
        public static void insertNewRoomLecturer(RoomLecturer room)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = "INSERT INTO Room_Names_Lecturer(l_name,b_name,r_name) VALUES (@lname,@bname,@rname)";

                command.Parameters.AddWithValue("@lname", room.LecturerName);
                command.Parameters.AddWithValue("@bname", room.LecBuildingName);
                command.Parameters.AddWithValue("@rname", room.LecRoomName);
                

                var t = command.ExecuteNonQuery();
                MessageBox.Show("Successfully Added");
            }
        }




        //getall

        public static List<RoomLecturer> getAll()
        {
            List<RoomLecturer> roomLecturer = new List<RoomLecturer>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT l_name, b_name, r_name FROM Room_Names_Lecturer";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    RoomLecturer room = new RoomLecturer();
                    room.LecturerName = reader["l_name"].ToString();
                    room.LecBuildingName = reader["b_name"].ToString();
                    room.LecRoomName = reader["r_name"].ToString();

                    roomLecturer.Add(room);

                }

            }
            return roomLecturer;
        }

        //delete

        public static void deleteRoomLecturer(string roomName)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "DELETE FROM Room_Names_Lecturer WHERE r_name = @roomName";
                    command.Parameters.AddWithValue("@roomName", roomName);


                    var t = command.ExecuteNonQuery();
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Deleting " + e.Message);
                }
            }
        }
    }
}
