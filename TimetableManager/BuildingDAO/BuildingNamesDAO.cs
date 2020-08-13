using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

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
                command.CommandText = "INSERT INTO Building_Names(b_name) VALUES(@buildingName)";
                command.Parameters.AddWithValue("@buildingName", building.BuildingName);

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
    }
}
