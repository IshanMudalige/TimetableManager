using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.NormalSessionsDAO
{
    class NorSessionsDetailsDAO
    {
        public NorSessionsDetailsDAO()
        {

        }

        //=======================================Insert Normal Sessions===================================================
        public static void InsertNormalSessions(NormalSessions normalSessions)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "INSERT INTO Sessions(lecturers,subj_name,subj_code,tag,grp_id,subgrp_id,no_students,duration) VALUES (@lecturers,@s_name,@s_code,@tag,@grp_id,@sub_id,@no_students,@duration)";
                    command.Parameters.AddWithValue("@lecturers", normalSessions.Lecturers);
                    command.Parameters.AddWithValue("@s_name", normalSessions.Sname);
                    command.Parameters.AddWithValue("@s_code", normalSessions.Scode);
                    command.Parameters.AddWithValue("@tag", normalSessions.Tag);
                    command.Parameters.AddWithValue("@grp_id", normalSessions.GrpID);
                    command.Parameters.AddWithValue("@sub_id", normalSessions.SubID);
                    command.Parameters.AddWithValue("@no_students", normalSessions.NoStu);
                    command.Parameters.AddWithValue("@duration", normalSessions.Duration);

                    var t = command.ExecuteNonQuery();
                    MessageBox.Show("Successfully Added");


                }
                catch(SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Inserting " + e.Message);
                }
            }
        }

    }
}
