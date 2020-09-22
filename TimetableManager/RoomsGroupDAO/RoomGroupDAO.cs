using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.RoomsGroupDAO
{
    class RoomGroupDAO
    {
        public RoomGroupDAO() { }

        //insert
        public static void insertNewRoomGroup(RoomGroup room)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = "INSERT INTO Room_Names_Group(g_id,sg_id,gb_name,gr_name) VALUES (@gid,@sgid,@bname,@rname)";

                command.Parameters.AddWithValue("@gid", room.GroupIdRoom);
                command.Parameters.AddWithValue("@sgid", room.SubGroupIdRoom);
                command.Parameters.AddWithValue("@bname", room.GroupBuildingName);
                command.Parameters.AddWithValue("@rname", room.GroupRoomName);


                var t = command.ExecuteNonQuery();
                MessageBox.Show("Successfully Added");
            }
        }


        public static List<RoomGroup> getAll()
        {
            List<RoomGroup> roomGroup = new List<RoomGroup>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT g_id, sg_id, gr_name FROM Room_Names_Group";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    RoomGroup room = new RoomGroup();
                    room.GroupIdRoom = reader["g_id"].ToString();
                    room.SubGroupIdRoom = reader["sg_id"].ToString();
                    room.GroupRoomName = reader["gr_name"].ToString();

                    roomGroup.Add(room);

                }

            }
            return roomGroup;
        }



        //remove
        public static void deleteRoomGroup(string subGroupId)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "DELETE FROM Room_Names_Group WHERE sg_id = @sgid";
                    command.Parameters.AddWithValue("@sgid", subGroupId);


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
