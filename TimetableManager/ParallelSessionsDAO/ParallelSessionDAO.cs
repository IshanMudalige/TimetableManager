using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.ParallelSessionsDAO
{
    class ParallelSessionDAO
    {
        public ParallelSessionDAO()
        {

        }

        //==================INSERT Parallel sessions ===================

        public static void insertParallelsession(ParallelSession parallelSession)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "INSERT INTO Parallel_Sessions(Session_id1,Session_id2,Session_id3,Session_id4,day,time)VALUES (@sid1,@sid2,@sid3,@sid4,@pday,@ptime)";

                    command.Parameters.AddWithValue("@sid1", parallelSession.PSession1);
                    command.Parameters.AddWithValue("@sid2", parallelSession.PSession2);
                    command.Parameters.AddWithValue("@sid3", parallelSession.PSession3);
                    command.Parameters.AddWithValue("@sid4", parallelSession.PSession4);
                    command.Parameters.AddWithValue("@pday", parallelSession.PDAY);
                    command.Parameters.AddWithValue("@ptime", parallelSession.PTIME);


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

        public static List<ParallelSession> getAllSesions(string subid)
        {
            List<ParallelSession> PList = new List<ParallelSession>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT session_id,tag,duration,subj_name  FROM  Sessions WHERE subgrp_id='" + subid + "'";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ParallelSession psessions = new ParallelSession();

                        psessions.NormalS = int.Parse(reader["session_id"].ToString());
                        psessions.NTags = reader["tag"].ToString();
                        psessions.NDuration = double.Parse(reader["duration"].ToString());
                        psessions.NSubjname = reader["subj_name"].ToString();





                        PList.Add(psessions);

                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Loading " + e.Message);
                }
            }

            return PList;

        }

        //------------------------------LOAD All the Sessions-------------------------------------------------------------------

        public static List<ParallelSession> getAllParallelSesions()
        {
            List<ParallelSession> ParallelList = new List<ParallelSession>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT Session_id1,Session_id2,Session_id3,Session_id4,day,time  FROM  Parallel_Sessions ";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ParallelSession psessions = new ParallelSession();

                        psessions.PSession1 = int.Parse(reader["Session_id1"].ToString());
                        psessions.PSession2 = int.Parse(reader["Session_id2"].ToString());
                        psessions.PSession3 = int.Parse(reader["Session_id3"].ToString());
                        psessions.PSession4 = int.Parse(reader["Session_id4"].ToString());
                        psessions.PDAY = reader["day"].ToString();
                        psessions.PTIME = reader["time"].ToString();






                        ParallelList.Add(psessions);

                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Loading " + e.Message);
                }
            }

            return ParallelList;

        }
    }
}
