using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.RoomsTag
{
    class RoomTagDAO
    {
        public RoomTagDAO() { }

        public static void insertNewRoomTag(RoomTag room)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = "INSERT INTO Room_Names_Tag(tag_room,rt_type) VALUES (@tag,@type)";

                command.Parameters.AddWithValue("@tag", room.Tag);
                command.Parameters.AddWithValue("@type", room.RoomTypeTag);

                var t = command.ExecuteNonQuery();
                MessageBox.Show("Successfully Added");
            }
        }

        
        public static List<RoomTag> getAll()
        {
            List<RoomTag> roomTag = new List<RoomTag>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT tag_room, rt_type FROM Room_Names_Tag";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    RoomTag room = new RoomTag();
                    room.Tag = reader["tag_room"].ToString();
                    room.RoomTypeTag = reader["rt_type"].ToString();

                    roomTag.Add(room);

                }

            }
            return roomTag;
        }


        public static void deleteRoomTag(string tag)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "DELETE FROM Room_Names_Tag WHERE tag_room = @tag";
                    command.Parameters.AddWithValue("@tag", tag);


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
