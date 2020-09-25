using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.RoomsSubjectDAO
{
    class RoomSubjectDAO
    {
        public RoomSubjectDAO() { }

        //insert
        public static void insertNewRoomSubject(RoomSubject room)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = "INSERT INTO Room_Names_Subject(s_code,b_name,rs_name) VALUES (@scode,@bname,@rname)";

                command.Parameters.AddWithValue("@scode", room.SubjectCode);
                command.Parameters.AddWithValue("@bname", room.SubBuildingName);
                command.Parameters.AddWithValue("@rname", room.SubRoomName);


                var t = command.ExecuteNonQuery();
                MessageBox.Show("Successfully Added");
            }
        }


        //getall

        public static List<RoomSubject> getAll()
        {
            List<RoomSubject> roomSubject = new List<RoomSubject>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT s_code, b_name, rs_name FROM Room_Names_Subject";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    RoomSubject room = new RoomSubject();
                    room.SubjectCode = reader["s_code"].ToString();
                    room.SubBuildingName = reader["b_name"].ToString();
                    room.SubRoomName = reader["rs_name"].ToString();

                    roomSubject.Add(room);

                }

            }
            return roomSubject;
        }


        //remove
        public static void deleteRoomSubject(string subCode)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "DELETE FROM Room_Names_Subject WHERE s_code = @scode";
                    command.Parameters.AddWithValue("@scode", subCode);


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
