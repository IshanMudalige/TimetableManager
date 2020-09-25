using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.Not_AvailableSessionsDAO
{
    class NotAvailableGroupDetailsDao
    {
        public NotAvailableGroupDetailsDao()
        {

        }

        //===================Insert Not available Group======================

        public static void insertnotAvailableGroup(NotAvailableGroup notAvailableGroup)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "INSERT INTO Not_Available_Groups(not_grpid,not_sub_name,not_grp_days,not_grp_time)VALUES (@nid,@nGsubname,@ngrpday,@ngtime)";

                    command.Parameters.AddWithValue("@nid", notAvailableGroup.NotAvaGroupID);
                    command.Parameters.AddWithValue("@nGsubname", notAvailableGroup.NotAvaSubname);
                    command.Parameters.AddWithValue("@ngrpday", notAvailableGroup.NotAvaGrpDay);
                    command.Parameters.AddWithValue("@ngtime", notAvailableGroup.NotAvaGrpTime);





                    var t = command.ExecuteNonQuery();
                    MessageBox.Show("Successfully Added");
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error occured in inserting " + e.Message);
                }
            }

        }


        //===============================Load Not Available Groups===========================

        public static List<NotAvailableGroup> getAll()
        {
            List<NotAvailableGroup> NotAvaGRPList = new List<NotAvailableGroup>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT not_grpid,not_sub_name,not_grp_days,not_grp_time FROM  Not_Available_Groups";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        NotAvailableGroup notAvagrp = new NotAvailableGroup();
                    
                        notAvagrp.NotAvaGroupID = reader["not_grpid"].ToString();
                        notAvagrp.NotAvaSubname = reader["not_sub_name"].ToString();
                        notAvagrp.NotAvaGrpDay = reader["not_grp_days"].ToString();
                        notAvagrp.NotAvaGrpTime = reader["not_grp_time"].ToString();
                        


                        NotAvaGRPList.Add(notAvagrp);

                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Loading " + e.Message);
                }
            }

            return NotAvaGRPList;

        }

        //------------------------------LOAD Groups-------------------------------------------------------------------

        public static List<NotAvailableGroup> getAllGroups(string subject, string tag)
        {
            List<NotAvailableGroup> grpList = new List<NotAvailableGroup>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT  subj_name,grp_id  FROM  Sessions WHERE subj_name='" + subject + "' AND  tag='" + tag + "'";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        NotAvailableGroup grp = new NotAvailableGroup();
                        grp.NotAvaSubname = reader["subj_name"].ToString();
                        grp.NotAvaGroupID = reader["grp_id"].ToString();
                       



                        grpList.Add(grp);

                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Loading " + e.Message);
                }
            }

            return grpList;

        }
    }
}
