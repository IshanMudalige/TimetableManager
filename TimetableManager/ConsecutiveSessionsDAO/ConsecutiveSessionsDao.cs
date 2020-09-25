using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.ConsecutiveSessionsDAO
{
    class ConsecutiveSessionsDao
    {
        public ConsecutiveSessionsDao()
        {

        }

        //==================INSERT Consecutive sessions ===================

        public static void insertConsecutivesession(ConsecutiveSession consecutive)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "INSERT INTO Consecutive_Sessions(session_id_1,session_id_2,subject,day,time,newtag)VALUES (@sid1,@sid2,@sub,@cday,@ctime,@ctag)";

                    command.Parameters.AddWithValue("@sid1", consecutive.SessionID1);
                    command.Parameters.AddWithValue("@sid2", consecutive.SessionID2);
                    command.Parameters.AddWithValue("@sub", consecutive.ConsecutiveSubject);
                    command.Parameters.AddWithValue("@cday", consecutive.ConsecutiveDay);
                    command.Parameters.AddWithValue("@ctime", consecutive.ConsecutiveTime);
                    command.Parameters.AddWithValue("@ctag", consecutive.ConsecutiveTag);


                    var t = command.ExecuteNonQuery();
                    MessageBox.Show("Successfully Added");
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error occured in inserting " + e.Message);
                }
            }

        }

        //------------------------------LOAD All the Sessions-------------------------------------------------------------------

        public static List<ConsecutiveSession> getAllSesions(string subject)
        {
            List<ConsecutiveSession> CList = new List<ConsecutiveSession>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT session_id,tag,subj_name  FROM  Sessions WHERE subj_name='" + subject + "'";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ConsecutiveSession consecutive = new ConsecutiveSession();

                        consecutive.NSession = int.Parse(reader["session_id"].ToString());
                        consecutive.Ntag = reader["tag"].ToString();
                        consecutive.NSubject = reader["subj_name"].ToString();

                        




                        CList.Add(consecutive);

                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Loading " + e.Message);
                }
            }

            return CList;

        }

        public static List<ConsecutiveSession> getAll()
        {
            List<ConsecutiveSession> CsList = new List<ConsecutiveSession>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT subject,day,time,newtag FROM  Consecutive_Sessions";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ConsecutiveSession csession = new ConsecutiveSession();

                        csession.ConsecutiveSubject= reader["subject"].ToString();
                        csession.ConsecutiveDay = reader["day"].ToString();
                        csession.ConsecutiveTime = reader["time"].ToString();
                        csession.ConsecutiveTag = reader["newtag"].ToString();



                        CsList.Add(csession);

                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Loading " + e.Message);
                }
            }

            return CsList;

        }

        //DElete Consecutive Sessions
        //==================Delete Not available Subgroups=================


        public static void deleteConsecutiveSession(string nsubid)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);

                    command.CommandText = " DELETE FROM Consecutive_Sessions  WHERE subject = @n_notavasid";
                    command.Parameters.AddWithValue("@n_notavasid", nsubid);

                    var s = command.ExecuteNonQuery();

                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Deleting" + e.Message);
                }
            }
        }


    }

}

