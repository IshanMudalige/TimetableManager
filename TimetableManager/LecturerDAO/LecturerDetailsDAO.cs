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
                command.CommandText = "INSERT INTO Lecturer(name,employeeID,faculty,department,center,building,category,level,rank) VALUES (@name,@employeeID,@faculty,@department,@center,@building,@category,@level,@rank)";
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

        public static void deleteLecturer(int employeeID)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = "DELETE FROM Lecturer WHERE employeeID = @employeeID";
                command.Parameters.AddWithValue("@employeeID", employeeID);

                var t = command.ExecuteNonQuery();

            }
        }

        public static void updateLecturer(int empID, Lecturer lecturer)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = "UPDATE Lecturer " +
                    "SET name = @name," +
                        "employeeID = @employeeID," +
                        "faculty = @faculty," +
                        "department = @department," +
                        "center = @center," +
                        "building = @building " +
                        "category = @category " +
                        "level = @level " +
                        "rank = @rank " +
                    "WHERE employeeID = @empID";
                command.Parameters.AddWithValue("@empID", empID);
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

        public static List<Lecturer> getAll()
        {
            List<Lecturer> LecList = new List<Lecturer>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT name,employeeID,faculty,department,center,rank FROM Lecturer";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Lecturer lecturer = new Lecturer();
                    lecturer.Name = reader["name"].ToString();
                    lecturer.EmployeeID = int.Parse(reader["employeeID"].ToString());
                    lecturer.Faculty = reader["faculty"].ToString();
                    lecturer.Department = reader["department"].ToString();
                    lecturer.Center = reader["center"].ToString();
                    lecturer.Rank = reader["rank"].ToString();

                    LecList.Add(lecturer);
                }
            }

            return LecList;
        }

        public static List<Lecturer> search(string category)
        {
            List<Lecturer> LecList = new List<Lecturer>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = "SELECT name,employeeID,faculty,department,center,rank FROM Lecturer WHERE category LIKE '%" + category + "%'";

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Lecturer lecturer = new Lecturer();
                    lecturer.Name = reader["name"].ToString();
                    lecturer.EmployeeID = int.Parse(reader["employeeID"].ToString());
                    lecturer.Faculty = reader["faculty"].ToString();
                    lecturer.Department = reader["department"].ToString();
                    lecturer.Center = reader["center"].ToString();
                    lecturer.Rank = reader["rank"].ToString();

                    LecList.Add(lecturer);
                }
            }

            return LecList;
        }
    }
}
