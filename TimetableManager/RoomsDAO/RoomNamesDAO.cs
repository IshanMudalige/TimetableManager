using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;
using System.Collections.Generic;

namespace TimetableManager.RoomsDAO
{
    class RoomNamesDAO
    {
        public RoomNamesDAO() { }

        public static void insertNewRoom(Room room)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = "INSERT INTO Room_Names(b_name,r_name,room_type,capacity) VALUES (@bname,@rname,@type,@capacity)";

                command.Parameters.AddWithValue("@bname", room.BuildingName);
                command.Parameters.AddWithValue("@rname", room.RoomName);
                command.Parameters.AddWithValue("@type", room.RoomType);
                command.Parameters.AddWithValue("@capacity", room.Capacity);

                var t = command.ExecuteNonQuery();
                MessageBox.Show("Successfully Added");
            }
        }

        public static List<Room> getAll()
        {
            List<Room> roomList = new List<Room>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT b_name,r_name,room_type,capacity FROM Room_Names";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Room room = new Room();
                    room.BuildingName = reader["b_name"].ToString();
                    room.RoomName = reader["r_name"].ToString();
                    room.RoomType = reader["room_type"].ToString();
                    room.Capacity = int.Parse(reader["capacity"].ToString());

                    roomList.Add(room);
                }
            }

            return roomList;
        }

        public static void deleteRoom(string roomName)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "DELETE FROM Room_Names WHERE r_name = @roomName";
                    command.Parameters.AddWithValue("@roomName", roomName);


                    var t = command.ExecuteNonQuery();
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Deleting " + e.Message);
                }
            }
        }

        public static void updateRoom(string nrName, Room room)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "UPDATE Room_Names " +
                        "SET b_name = @bName," +
                            "r_name = @rName," +
                            "room_type = @type," +
                            "capacity = @capacity " +
                        "WHERE r_name = @nrName";
                    command.Parameters.AddWithValue("@nrName", nrName);
                    command.Parameters.AddWithValue("@bName", room.BuildingName);
                    command.Parameters.AddWithValue("@rName", room.RoomName);
                    command.Parameters.AddWithValue("@type", room.RoomType);
                    command.Parameters.AddWithValue("@capacity", room.Capacity);


                    var t = command.ExecuteNonQuery();
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Updating " + e.Message);
                }

            }
        }

    }
}
