﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.Not_AvailableSessionsDAO
{
    class NotAvaLecDao
    {
        public NotAvaLecDao()
        { 
        
        }


        //==================INSERT NOT AVAILABLE LECTUERS===================

        public static void insertnotAvailableLec(NotAvaLec notAvaLec)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "INSERT INTO Not_Available_Lec(lec_id,lec_name,lec_day,lec_time)VALUES (@lecid,@lecname,@lecday,@lectime)";

                    command.Parameters.AddWithValue("@lecid", notAvaLec.LecID);
                    command.Parameters.AddWithValue("@lecname", notAvaLec.LecName);
                    command.Parameters.AddWithValue("@lecday", notAvaLec.LecDay);
                    command.Parameters.AddWithValue("@lectime", notAvaLec.Lectime);


                    var t = command.ExecuteNonQuery();
                    MessageBox.Show("Successfully Added");
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error occured in inserting " + e.Message);
                }
            }

        }

        //===============================Load Not Available Lec===========================

        public static List<NotAvaLec> getAll()
        {
            List<NotAvaLec> NotAvaLecList = new List<NotAvaLec>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT lec_id,lec_name,lec_day,lec_time FROM  Not_Available_Lec";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        NotAvaLec notAvaLec = new NotAvaLec();
                        notAvaLec.LecID = int.Parse(reader["lec_id"].ToString());
                        notAvaLec.LecName = reader["lec_name"].ToString();
                        notAvaLec.LecDay = reader["lec_day"].ToString();
                        notAvaLec.Lectime = reader["lec_time"].ToString();


                        NotAvaLecList.Add(notAvaLec);

                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Loading " + e.Message);
                }
            }

            return NotAvaLecList;

        }

        //------------------------------LOAD LECTUERS-------------------------------------------------------------------

        public static List<NotAvaLec> getAllLec(string faculty, string department)
        {
            List<NotAvaLec> LecList = new List<NotAvaLec>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT LecID ,name  FROM  Lecturer WHERE faculty='"+faculty+ "' AND  department='" + department + "'";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        NotAvaLec lecturer = new NotAvaLec();
                        lecturer.LecID = int.Parse(reader["LecID"].ToString());
                        lecturer.LecName = reader["name"].ToString();
                      


                        LecList.Add(lecturer);

                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Loading " + e.Message);
                }
            }

            return LecList;

        }

        //====================================Delete Not available Lec==========================
        public static void deletenotavailableLec(int nlecid)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);

                    command.CommandText = "DELETE FROM Not_Available_Lec  WHERE lec_id = @l_lid";
                    command.Parameters.AddWithValue("@l_lid", nlecid);

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
