using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.StatisticsDAO
{
    class LecStatDAO
    {
        public LecStatDAO()
        {

        }

        public static List<LecStat> getAll()
        {
            List<LecStat> LecList = new List<LecStat>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT name,employeeID,faculty,department,center,building,category,rank FROM Lecturer";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        LecStat lecturer = new LecStat();
                        lecturer.Name = reader["name"].ToString();
                        lecturer.EmployeeID = reader["employeeID"].ToString();
                        lecturer.Faculty = reader["faculty"].ToString();
                        lecturer.Department = reader["department"].ToString();
                        lecturer.Center = reader["center"].ToString();
                        lecturer.Building = reader["building"].ToString();
                        lecturer.Category = reader["category"].ToString();
                        lecturer.Rank = reader["rank"].ToString();

                        LecList.Add(lecturer);
                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Loading" + e.Message);
                }

            }

            return LecList;
        }
    }


}

