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

        public static void insertgroups(Group group)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "INSERT INTO Group (academic_id,student_count,group_no,group_id,subgroup_no,subgroup_id) VALUES (@acdemicid,@studentcount,groupno,groupid,subgroupno,subgroupid)";

                    command.Parameters.AddWithValue("@academicid", group.AcademicId);
                    command.Parameters.AddWithValue("@studentcount", group.StudentCount);
                    command.Parameters.AddWithValue("@groupno", group.Groupno);
                    command.Parameters.AddWithValue("@groupid", group.GroupId);
                    command.Parameters.AddWithValue("@subgroupno", group.SubGroupno);
                    command.Parameters.AddWithValue("@subgroupid", group.SubGroupId);

                    var t = command.ExecuteNonQuery();
                    MessageBox.Show("Successfully Added");
                }
                catch(SQLiteException e)
                {
                    MessageBox.Show("Error occured in inserting " + e.Message);
                }
            }

        }


        //===============================Load Groups======================
        public static List<Group> getAll()
        {
            List<Group> GrpList = new List<Group>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT academic_id,student_count,group_no,group_id,subgroup_no,subgroup_id  FROM  Group";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Group group = new Group();
                        group.AcademicId = reader["academic_id"].ToString();
                        group.StudentCount = int.Parse(reader["student_count"].ToString());
                        group.Groupno = int.Parse(reader["group_no"].ToString());
                        group.GroupId = reader["group_id"].ToString();
                        group.SubGroupno = int.Parse(reader["subgroup_no"].ToString());
                        group.SubGroupId = reader["subgroup_id"].ToString();


                        GrpList.Add(group);

                    }
                }
                catch(SQLiteException e)
                {
                    MessageBox.Show("Error in Loading " + e.Message);
                }
            }

            return GrpList;

        }
    }
}
