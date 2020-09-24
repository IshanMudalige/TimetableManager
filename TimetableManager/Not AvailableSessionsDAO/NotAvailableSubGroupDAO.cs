using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.Not_AvailableSessionsDAO
{
    class NotAvailableSubGroupDAO
    {
        public NotAvailableSubGroupDAO()
        {

        }

        //===================Insert Not available Group======================

        public static void insertnotAvailableSubGroup(NotAvailableSubGroup notAvailableSubGroup)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "INSERT INTO Not_Available_SubGroups(not_subgrpid,not_subgrp_subname,not_subgrp_days,not_subgrp_time)VALUES (@nid,@nGsubname,@ngrpday,@ngtime)";

                    command.Parameters.AddWithValue("@nid", notAvailableSubGroup.NotAvaSubGrpId);
                    command.Parameters.AddWithValue("@nGsubname", notAvailableSubGroup.NotAvaSubGrpSubname);
                    command.Parameters.AddWithValue("@ngrpday", notAvailableSubGroup.NotAvaSubGrpDays);
                    command.Parameters.AddWithValue("@ngtime", notAvailableSubGroup.NotAvaSubGrpTime);





                    var t = command.ExecuteNonQuery();
                    MessageBox.Show("Successfully Added");
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error occured in inserting " + e.Message);
                }
            }

        }


        //===============================Load Not Available SubGroups===========================

        public static List<NotAvailableSubGroup> getAll()
        {
            List<NotAvailableSubGroup> NotAvaSubGRPList = new List<NotAvailableSubGroup>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT not_subgrpid,not_subgrp_subname,not_subgrp_days,not_subgrp_time FROM  Not_Available_Subgroups";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        NotAvailableSubGroup notAvasubgrp = new NotAvailableSubGroup();

                        notAvasubgrp.NotAvaSubGrpId = reader["not_subgrpid"].ToString();
                        notAvasubgrp.NotAvaSubGrpSubname = reader["not_subgrp_subname"].ToString();
                        notAvasubgrp.NotAvaSubGrpDays = reader["not_subgrp_days"].ToString();
                        notAvasubgrp.NotAvaSubGrpTime = reader["not_subgrp_time"].ToString();


                        NotAvaSubGRPList.Add(notAvasubgrp);

                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Loading " + e.Message);
                }
            }

            return NotAvaSubGRPList;

        }


        //------------------------------LOAD Sub Groups-------------------------------------------------------------------

        public static List<NotAvailableSubGroup> getAllSubGroups(string subject, string tag)
        {
            List<NotAvailableSubGroup> grpList = new List<NotAvailableSubGroup>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT  subj_name,subgrp_id  FROM  Sessions WHERE subj_name='" + subject + "' AND  tag='" + tag + "'";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                         NotAvailableSubGroup  grp = new NotAvailableSubGroup();
                        grp.NotAvaSubGrpSubname = reader["subj_name"].ToString();
                        grp.NotAvaSubGrpId= reader["subgrp_id"].ToString();




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

        //==================Delete Not available Subgroups=================


        public static void deletenotavailablesubgroups(string nsubid)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);

                    command.CommandText = " DELETE FROM Not_Available_Subgroups  WHERE not_subgrpid = @n_notavasid";
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

