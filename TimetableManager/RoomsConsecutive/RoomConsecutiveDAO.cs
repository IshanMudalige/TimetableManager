using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.RoomsConsecutive
{
    class RoomConsecutiveDAO
    {
        public RoomConsecutiveDAO() { }

        public static void insertConsecutiveRoom(RoomConsecutive consecutive)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "INSERT INTO Room_Names_Consecutive(cs_id,subject,day,time,tag,rc_name)VALUES (@cs_id,@subject,@day,@time,@tag,@rc_name)";

                    command.Parameters.AddWithValue("@cs_id", consecutive.ConsSesIdNew);
                    command.Parameters.AddWithValue("@subject", consecutive.SubjectCons);
                    command.Parameters.AddWithValue("@day", consecutive.DayCons);
                    command.Parameters.AddWithValue("@time", consecutive.TimeCons);
                    command.Parameters.AddWithValue("@tag", consecutive.TagCons);
                    command.Parameters.AddWithValue("@rc_name", consecutive.RoomCons);


                    var t = command.ExecuteNonQuery();
                    MessageBox.Show("Successfully Added");
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error occured in inserting " + e.Message);
                }
            }

        }




        public static List<RoomConsecutive> getAll()
        {
            List<RoomConsecutive> roomGroup = new List<RoomConsecutive>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT cs_id,subject,day,time,tag,rc_name FROM Room_Names_Consecutive";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    RoomConsecutive room = new RoomConsecutive();
                    room.ConsSesIdNew = int.Parse(reader["cs_id"].ToString());
                    room.SubjectCons = reader["subject"].ToString();
                    room.DayCons = reader["day"].ToString();
                    room.TimeCons = reader["time"].ToString();
                    room.TagCons = reader["tag"].ToString();
                    room.RoomCons = reader["rc_name"].ToString();

                    roomGroup.Add(room);

                }

            }
            return roomGroup;
        }


        public static void deleteRoomCons(string cid)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "DELETE FROM Room_Names_Consecutive WHERE cs_id = @csid";
                    command.Parameters.AddWithValue("@csid", cid);


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
