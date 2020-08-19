using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.TagDAO
{
    class TagDetailsDAO
    {

        public TagDetailsDAO()
        {

        }

        //=====insert tag====

        public static void inserttag(Tag tag)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "INSERT INTO Tag(tagname) VALUES (@tagname)";
                    command.Parameters.AddWithValue("@tagname", tag.Tags);

                    var t = command.ExecuteNonQuery();
                    MessageBox.Show("Successfully Added");
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in inserting " + e.Message);
                }
            }
        }



        //=======================================Delete Tagss=============================================
        public static void deleteTags(string tagname)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "DELETE FROM Tag WHERE tagname = @tagname";
                    command.Parameters.AddWithValue("@tagname", tagname);

                    var t = command.ExecuteNonQuery();

                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Deleting" + e.Message);
                }
            }
        }

        //===========================================Update Tags===============================================
        public static void updateTag(string ttagname, Tag tag)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = " UPDATE Tag" +
                        " SET    tagname = @tagname" +
                        " WHERE  tagname = @tagname";
                    command.Parameters.AddWithValue("@tagname", ttagname);
                    command.Parameters.AddWithValue("@tagname", tag.Tags);

                    var t = command.ExecuteNonQuery();

                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Updating" + e.Message);
                }
            }
        }

        //===========================Load tags============================
        public static List<Tag> getAll()
        {
            List<Tag> tagList = new List<Tag>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT tagname FROM Tag";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Tag tag= new Tag();
                    tag.Tags = reader["tagname"].ToString();

                    tagList.Add(tag);

                }

            }
            return tagList;
        }

        //========================Search tag==============================
        public static List<Tag> searchTags(string tname)
        {
            List<Tag > tagList = new List<Tag>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = "SELECT tagname FROM Tag WHERE tagname LIKE '%" + tname + "%'";

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Tag tag = new Tag();
                    tag.Tags = reader["tagname"].ToString();
                   

                    tagList.Add(tag);

                }
            }

            return tagList;
        }

    }
}
