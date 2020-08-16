using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;

namespace TimetableManager.BuildingDAO
{
    class BuildingNamesDAO
    {

        public BuildingNamesDAO()
        {

        }

        public static void insertNewBuilding(Building building)
        {
            using(SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = "INSERT INTO Building_Names(b_name) VALUES(@b_name)";
                command.Parameters.AddWithValue("@b_name", building.BuildingName);

                var t = command.ExecuteNonQuery();
                //MessageBox.Show("Successfully Added");
            }
        }

        public static void deleteBuilding(String bname)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = "DELETE FROM Building_Names WHERE b_name = @bname";
                command.Parameters.AddWithValue("@bname", bname);

                var t = command.ExecuteNonQuery();

            }
        }

        public static List<Building> getAll()
        {
            List<Building> buildingList = new List<Building>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT b_name FROM Building_Names";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Building buildName = new Building();
                    buildName.BuildingName = reader["b_name"].ToString();

                    buildingList.Add(buildName);

                }

            }
            return buildingList;
        }

        public static void updateBuilding(String nbname, Building building)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "UPDATE Building_Names " + "SET b_name = @bname" + " WHERE b_name = @nbname";
                    command.Parameters.AddWithValue("@nbname",nbname);
                    command.Parameters.AddWithValue("@bname", building.BuildingName);

                    var t = command.ExecuteNonQuery();
                }
                catch(SQLiteException e)
                {
                    MessageBox.Show("Error in Updating" + e.Message);
                }
            }
        }
    }
}
