using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.NonOverlappingSessionsDAO
{
    class NonOverlappingSessionDAO
    {
        public NonOverlappingSessionDAO()
        {

        }

        //==================INSERT Non Overlapping  sessions ===================

        public static void insertNonoverlappingsession(NonOverlappingSessions nonOverlapping)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "INSERT INTO NonOverlapping_Sessions(sessionid_1,sessionid_2,sessionid_3,sessionid_4)VALUES (@sid1,@sid2,@sid3,@sid4)";

                    command.Parameters.AddWithValue("@sid1", nonOverlapping.NonSession1);
                    command.Parameters.AddWithValue("@sid2", nonOverlapping.NonSession2);
                    command.Parameters.AddWithValue("@sid3", nonOverlapping.NonSession3);
                    command.Parameters.AddWithValue("@sid4", nonOverlapping.NonSession4);
                    


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

        public static List<NonOverlappingSessions> getAllSesions(string subid)
        {
            List<NonOverlappingSessions> nonList = new List<NonOverlappingSessions>();
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
                        NonOverlappingSessions Nsessions = new NonOverlappingSessions();

                        Nsessions.NONNormalS = int.Parse(reader["session_id"].ToString());
                        Nsessions.NonTags = reader["tag"].ToString();
                        Nsessions.NonDuration = double.Parse(reader["duration"].ToString());
                        Nsessions.NonSubjname = reader["subj_name"].ToString();





                        nonList.Add(Nsessions);

                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Loading " + e.Message);
                }
            }

            return nonList;

        }

        //------------------------------LOAD All the Sessions-------------------------------------------------------------------

        public static List<NonOverlappingSessions> getAllNonOverlapSesions()
        {
            List<NonOverlappingSessions> NonoverlapList = new List<NonOverlappingSessions>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT sessionid_1,sessionid_2,sessionid_3,sessionid_4 FROM  NonOverlapping_Sessions ";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        NonOverlappingSessions nonsessions = new NonOverlappingSessions();

                        nonsessions.NonSession1 = int.Parse(reader["sessionid_1"].ToString());
                        nonsessions.NonSession2= int.Parse(reader["sessionid_2"].ToString());
                        nonsessions.NonSession3= int.Parse(reader["sessionid_3"].ToString());
                        nonsessions.NonSession4 = int.Parse(reader["sessionid_4"].ToString());






                        NonoverlapList.Add(nonsessions);

                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Loading " + e.Message);
                }
            }

            return NonoverlapList;

        }
    }
}

