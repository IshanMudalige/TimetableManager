using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.SubjectDAO
{
    class SubjectDetailsDAO
    {

        public SubjectDetailsDAO()
        {

        }

        //============================================Insert Subjects============================================
        public static void insertSubjects(Subject subject)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "INSERT INTO Subjects(offer_year,offer_sem,sub_name,sub_code,lec_hrs,tute_hrs,lab_hrs,evalu_hrs) VALUES (@year,@semester,@sub_name,@sub_code,@lec_hrs,@tute_hrs,@lab_hrs,@evalu_hrs)";
                    command.Parameters.AddWithValue("@year", subject.Year);
                    command.Parameters.AddWithValue("@semester", subject.Semester);
                    command.Parameters.AddWithValue("@sub_name", subject.SubName);
                    command.Parameters.AddWithValue("@sub_code", subject.SubCode);
                    command.Parameters.AddWithValue("@lec_hrs", subject.LecHrs);
                    command.Parameters.AddWithValue("@tute_hrs", subject.TuteHrs);
                    command.Parameters.AddWithValue("@lab_hrs", subject.LabHrs);
                    command.Parameters.AddWithValue("@evalu_hrs", subject.EvaluHrs);

                    var t = command.ExecuteNonQuery();
                    MessageBox.Show("Successfully Added");

                }
                catch(SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Inserting " + e.Message);
                }
            }
        }

        //=======================================Delete Subjects=============================================
        public static void deleteSubject(string code)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "DELETE FROM Subjects WHERE sub_code = @sub_code";
                    command.Parameters.AddWithValue("@sub_code", code);

                    var t = command.ExecuteNonQuery();

                }
                catch(SQLiteException e)
                {
                    MessageBox.Show("Error in Deleting" + e.Message);
                }
            }
        }

        //===========================================Update Subjects===============================================
        public static void updateSubject(string code, Subject subject)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "UPDATE Subjects " +
                        "SET offer_year = @year," +
                            "offer_sem = @semester," +
                            "sub_name = @sub_name," +
                            "sub_code = @sub_code," +
                            "lec_hrs = @lec_hrs," +
                            "tute_hrs = @tute_hrs, " +
                            "lab_hrs = @lab_hrs, " +
                            "evalu_hrs = @evalu_hrs " +
                        "WHERE sub_code = @code";
                    command.Parameters.AddWithValue("@code", code);
                    command.Parameters.AddWithValue("@year", subject.Year);
                    command.Parameters.AddWithValue("@semester", subject.Semester);
                    command.Parameters.AddWithValue("@sub_name", subject.SubName);
                    command.Parameters.AddWithValue("@sub_code", subject.SubCode);
                    command.Parameters.AddWithValue("@lec_hrs", subject.LecHrs);
                    command.Parameters.AddWithValue("@tute_hrs", subject.TuteHrs);
                    command.Parameters.AddWithValue("@lab_hrs", subject.LabHrs);
                    command.Parameters.AddWithValue("@evalu_hrs", subject.EvaluHrs);

                    var t = command.ExecuteNonQuery();

                }
                catch(SQLiteException e)
                {
                    MessageBox.Show("Error in Updating" + e.Message);
                }
            }
        }


        //=====================================Load Subjects======================================
        public static List<Subject> getAll()
        {
            List<Subject> SubList = new List<Subject>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT offer_year,offer_sem,sub_name,sub_code,lec_hrs,tute_hrs,lab_hrs,evalu_hrs FROM Subjects";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Subject subject = new Subject();
                        subject.Year = reader["offer_year"].ToString();
                        subject.Semester = reader["offer_sem"].ToString();
                        subject.SubName = reader["sub_name"].ToString();
                        subject.SubCode = reader["sub_code"].ToString();
                        subject.LecHrs = double.Parse(reader["lec_hrs"].ToString());
                        subject.TuteHrs = double.Parse(reader["tute_hrs"].ToString());
                        subject.LabHrs = double.Parse(reader["lab_hrs"].ToString());
                        subject.EvaluHrs = double.Parse(reader["evalu_hrs"].ToString());

                        SubList.Add(subject);

                    }

                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Loading" + e.Message);
                }
            }

            return SubList;
        }


        //======================================Search Subjects==========================================
        public static List<Subject> search(string code)
        {
            List<Subject> SubList = new List<Subject>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = "SELECT offer_year,offer_sem,sub_name,sub_code,evalu_hrs FROM Subjects WHERE sub_code LIKE '%" + code + "%'";

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Subject subject = new Subject();
                    subject.Year = reader["offer_year"].ToString();
                    subject.Semester = reader["offer_sem"].ToString();
                    subject.SubName = reader["sub_name"].ToString();
                    subject.SubCode = reader["sub_code"].ToString();
                    subject.EvaluHrs = double.Parse(reader["evalu_hrs"].ToString());

                    SubList.Add(subject);

                }
            }

            return SubList;
        }
    }

    
}
