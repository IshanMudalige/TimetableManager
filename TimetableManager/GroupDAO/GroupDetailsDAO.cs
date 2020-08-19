using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.GroupDAO
{
    class GroupDetailsDAO
    {

        public GroupDetailsDAO()
        {

        }

        //=======================================Insert Group===================

        public static void insertgroups(Groups group)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "INSERT INTO Groups_Info (academic_id,student_count,group_no,group_id,subgroup_no,subgroup_id) VALUES (@academicid,@studentcount,@groupno,@groupid,@subgroupno,@subgroupid)";

                    command.Parameters.AddWithValue("@academicid", group.AcademicId);
                    command.Parameters.AddWithValue("@studentcount", group.StudentCount);
                    command.Parameters.AddWithValue("@groupno", group.Groupno);
                    command.Parameters.AddWithValue("@groupid", group.GroupId);
                    command.Parameters.AddWithValue("@subgroupno", group.SubGroupno);
                    command.Parameters.AddWithValue("@subgroupid", group.SubGroupId);

                    var t = command.ExecuteNonQuery();
                    MessageBox.Show("Successfully Added");
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error occured in inserting " + e.Message);
                }
            }

        }


        //===============================Load Groups======================
        public static List<Groups> getAll()
        {
            List<Groups> GrpList = new List<Groups>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT academic_id,student_count,group_no,group_id,subgroup_no,subgroup_id  FROM  Groups_Info";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Groups group = new Groups();
                        group.AcademicId = reader["academic_id"].ToString();
                        group.StudentCount = int.Parse(reader["student_count"].ToString());
                        group.Groupno = int.Parse(reader["group_no"].ToString());
                        group.GroupId = reader["group_id"].ToString();
                        group.SubGroupno = int.Parse(reader["subgroup_no"].ToString());
                        group.SubGroupId = reader["subgroup_id"].ToString();


                        GrpList.Add(group);

                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Loading " + e.Message);
                }
            }

            return GrpList;

        }

        //===============================Update Groups=====================
        public static void updategroups(string ngroupid, Groups groups)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "UPDATE Groups_Info " +
                        "SET academic_id = @academicid," +
                               "student_count = @studentcount," +
                               "group_no = @groupno," +
                               "group_id = @groupid," +
                               "subgroup_no = @subgroupno," +
                               "subgroup_id = @subgroupid " +
                               "WHERE group_id = @ngroupid";
                    command.Parameters.AddWithValue("@ngroupid", ngroupid);
                    command.Parameters.AddWithValue("@academicid", groups.AcademicId);
                    command.Parameters.AddWithValue("@studentcount", groups.StudentCount);
                    command.Parameters.AddWithValue("@groupno", groups.Groupno);
                    command.Parameters.AddWithValue("@groupid", groups.GroupId);
                    command.Parameters.AddWithValue("@subgroupno", groups.SubGroupno);
                    command.Parameters.AddWithValue("@subgroupid", groups.SubGroupId);

                    var t = command.ExecuteNonQuery();



                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in updating" + e.Message);
                }
            }
        }

        public static void deletegroups(string ngroupid)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);

                    command.CommandText = " DELETE FROM Groups_Info  WHERE group_id = @groupid";
                    command.Parameters.AddWithValue( "@groupid", ngroupid);

                    var s = command.ExecuteNonQuery();

                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Deleting" + e.Message);
                }
            }
        }

        public static List<Groups> searchgroups(string groupid)
        {
            List<Groups> GrpList = new List<Groups>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = "SELECT academic_id,group_id,subgroup_id FROM Groups_Info WHERE group_id LIKE '%" + groupid + "%'";

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Groups groups = new Groups();
                    groups.AcademicId = reader["academic_id"].ToString();
                    groups.GroupId = reader["group_id"].ToString();
                    groups.SubGroupId = reader["subgroup_id"].ToString();


                    GrpList.Add(groups);

                }
            }

            return GrpList;
        }
    
    }

    
}
