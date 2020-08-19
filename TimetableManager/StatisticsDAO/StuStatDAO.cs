using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimetableManager.StudentDAO;

namespace TimetableManager.StatisticsDAO
{
    class StuStatDAO
    {

        public StuStatDAO()
        {
        }

        public static List<StuStat> getAll()
        {
            List<StuStat> stuList = new List<StuStat>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT year,semester,programme,proid From Student";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        StuStat student = new StuStat();
                        student.Year = reader["year"].ToString();
                        student.Semester = reader["semester"].ToString();
                        student.Programme = reader["programme"].ToString();
                        student.Programmid = reader["proid"].ToString();

                        stuList.Add(student);
                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Loading" + e.Message);
                }

            }

            return stuList;
        }
    }
}
