using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.RoomsNotAvailableDAO
{
    class RoomNotAvailableDAO
    {
        public RoomNotAvailableDAO()
        { 
        }


        
        public static void insertRoomNotAvailable(RoomNotAvailable roomNotAvailable)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "INSERT INTO Room_Names_NotAvailable(nab_name,nar_name,description,date,fromt,tot) VALUES (@bname,@rname,@desc,@date,@fromt,@tot)";
                    command.Parameters.AddWithValue("@bname", roomNotAvailable.BuildingNameNA);
                    command.Parameters.AddWithValue("@rname", roomNotAvailable.RoomNameNA);
                    command.Parameters.AddWithValue("@desc", roomNotAvailable.Description);
                    command.Parameters.AddWithValue("@date", roomNotAvailable.Day);
                    command.Parameters.AddWithValue("@fromt", roomNotAvailable.From.ToString());
                    command.Parameters.AddWithValue("@tot", roomNotAvailable.To.ToString());

                    var t = command.ExecuteNonQuery();
                    MessageBox.Show("Successfully Added");


                
            }
        }



        public static List<RoomNotAvailable> getAll()
        {
            List<RoomNotAvailable> roomList = new List<RoomNotAvailable>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT nar_id,nab_name,nar_name,date,fromt,tot FROM Room_Names_NotAvailable";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    RoomNotAvailable room = new RoomNotAvailable();
                    room.Nid = int.Parse(reader["nar_id"].ToString());
                    room.BuildingNameNA = reader["nab_name"].ToString();
                    room.RoomNameNA = reader["nar_name"].ToString();
                    room.Day = reader["date"].ToString();
                    room.From = reader["fromt"].ToString();
                    room.To = reader["tot"].ToString();

                    roomList.Add(room);
                }

            }

            return roomList;
        }

        
        public static void deleteRoomNotAvailable(string nid)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "DELETE FROM Room_Names_NotAvailable WHERE nar_id = @nid";
                    command.Parameters.AddWithValue("@nid", nid);


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
