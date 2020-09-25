using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.Not_AvailableSessionsDAO
{
    class NotAvailableSessionDAO
    {

        public NotAvailableSessionDAO()
        {

        }

        //===================Insert Not available Session======================

        public static void insertnotAvailableSession(NotAvailableSessions notAvailableSessions)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "INSERT INTO Not_Available_Session(not_session_id,not_session_day,not_session_time)VALUES (@nsid,@nsday,@nstime)";

                    command.Parameters.AddWithValue("@nsid", notAvailableSessions.NotAvaSessionID);
                    command.Parameters.AddWithValue("@nsday", notAvailableSessions.NotAvaSessionDay);
                    command.Parameters.AddWithValue("@nstime", notAvailableSessions.NotAvaSesionTime);
                    


                    var t = command.ExecuteNonQuery();
                   MessageBox.Show("Successfully Added");
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error occured in inserting " + e.Message);
                }
            }

        }

        //------------------------------LOAD Sessions-------------------------------------------------------------------

        public static List<NotAvailableSessions> getAllSesions(string subject)
        {
            List<NotAvailableSessions> SList = new List<NotAvailableSessions>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT subj_name,session_id,subgrp_id  FROM  Sessions WHERE subj_name='" + subject + "'";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        NotAvailableSessions notAvailableS = new NotAvailableSessions();

                        notAvailableS.NotAvaSubject = reader["subj_name"].ToString();
                        notAvailableS.NotAvaSessionID = int.Parse(reader["session_id"].ToString());
                        notAvailableS.NotAvaSubgroupId = reader["subgrp_id"].ToString();
                        



                        SList.Add(notAvailableS);

                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Loading " + e.Message);
                }
            }

            return SList;

        }
        //================================LOAD Not Available Sessions========================
        public static List<NotAvailableSessions> getAllNotAvailableS()
        {
            List<NotAvailableSessions> NotAvaSessionList = new List<NotAvailableSessions>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT not_session_id,not_session_day,not_session_time FROM  Not_Available_Session";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        NotAvailableSessions notAvaSessions = new NotAvailableSessions();
                        notAvaSessions.NotAvaSessionID = int.Parse(reader["not_session_id"].ToString());
                        notAvaSessions.NotAvaSessionDay = reader["not_session_day"].ToString();
                        notAvaSessions.NotAvaSesionTime = reader["not_session_time"].ToString();
                         


                        NotAvaSessionList.Add(notAvaSessions);

                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Loading " + e.Message);
                }
            }

            return NotAvaSessionList;

        }

        //==================Delete Not available sessions=================

       
        public static void deletenotavailableSessions(int nsid)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);

                    command.CommandText = " DELETE FROM Not_Available_Session  WHERE not_session_id = @n_notavasid";
                    command.Parameters.AddWithValue("@n_notavasid", nsid);

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
