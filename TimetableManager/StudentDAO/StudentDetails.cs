using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.StudentDAO
{
    class StudentDetails
    {
        public StudentDetails()
        {

        }

        //=======Insert Student===================
        public static void insertStudent(Student student)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "INSERT INTO Student(year,semester,programme,proid) VALUES (@year,@semester,@programme,@proid)";
                    command.Parameters.AddWithValue("@year", student.Year);
                    command.Parameters.AddWithValue("@semester",student.Semester);
                    command.Parameters.AddWithValue("@programme", student.Programme);
                    command.Parameters.AddWithValue("@proid", student.Programmid);

                    var s = command.ExecuteNonQuery();
                    MessageBox.Show("Successfully Added");

                }
                catch(SQLiteException e)
                {
                    MessageBox.Show("Error in Inserting" + e.Message);
                }

            }
        }

        //===========================Load Students================================

        public static List<Student> getAll()
        {
            List<Student> studentlist = new List<Student>();
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
                        Student student = new Student();
                        student.Year = reader["year"].ToString();
                        student.Semester = reader["semester"].ToString();
                        student.Programme = reader["programme"].ToString();
                        student.Programmid = reader["proid"].ToString();

                        studentlist.Add(student);
                    }
                }
                catch(SQLiteException e)
                {
                    MessageBox.Show("Error in Loading " + e.Message);
                }
            }

            return studentlist;
        }

        //=================Search Student=======================
        public static List<Student> searchs(string programme)
        {
            List<Student> studentlist = new List<Student>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = " SELECT year,semester,programme,proid FROM Student WHERE programme LIKE '%" + programme + "%'";

                SQLiteDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    Student student = new Student();
                    student.Year = reader["year"].ToString();
                    student.Semester = reader["semester"].ToString();
                    student.Programme = reader["programme"].ToString();
                    student.Programmid = reader["proid"].ToString();

                    studentlist.Add(student);

                }
            }

            return studentlist;
        }

        //==============DELETE STUDENT====================

        public static void deletestudent (string nproid)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "DELETE FROM Student WHERE proid= @programmeid";
                    command.Parameters.AddWithValue("@programmeid", nproid);


                    var s = command.ExecuteNonQuery();



                }
                catch(SQLiteException e)
                {
                    MessageBox.Show("Error in Deleting" + e.Message);
                }
            }
        }


        //==============Update Student======================
        public static void updatestudent(string nid,  Student student )
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = " UPDATE Student " +
                       "SET   year = @year," +
                             "semester = @semester," +
                             "programme = @programme, " +
                             "proid = @programmid " +
                     "WHERE proid = @nid" ;

                    command.Parameters.AddWithValue("@nid",nid);
                    command.Parameters.AddWithValue("@year", student.Year);
                    command.Parameters.AddWithValue("@semester", student.Semester);
                    command.Parameters.AddWithValue("@programme", student.Programme);
                    command.Parameters.AddWithValue("@programmid", student.Programmid);

                    var s = command.ExecuteNonQuery();
                }
                catch(SQLiteException e)
                {
                    MessageBox.Show("Error in updating" + e.Message);
                }
                
            }
        }

        
    }
}
