using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.LecturerDAO
{
    class LecturerDetailsDAO
    {
        public LecturerDetailsDAO() 
        {

        }

        public static void insertLecture(Lecturer lecturer)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = "INSERT INTO Lecturer(name,employeeID,faculty,department,center,building,category,level,rank) VALUES (@name,@employeeID,@department,@center,@building,@category,@level,@level+@employeeID)";
                command.Parameters.AddWithValue("@name", lecturer.Name);
                command.Parameters.AddWithValue("@employeeID", lecturer.EmployeeID);
                command.Parameters.AddWithValue("@faculty", lecturer.Faculty);
                command.Parameters.AddWithValue("@department", lecturer.Department);
                command.Parameters.AddWithValue("@center", lecturer.Center);
                command.Parameters.AddWithValue("@building", lecturer.Building);
                command.Parameters.AddWithValue("@category", lecturer.Category);
                command.Parameters.AddWithValue("@level", lecturer.Level);
                command.Parameters.AddWithValue("@rank", lecturer.Rank);

                var t = command.ExecuteNonQuery();

            }
        }
    }
}
