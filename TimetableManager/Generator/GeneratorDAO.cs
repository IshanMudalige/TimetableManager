using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimetableManager.DaysHoursDAO;
using TimetableManager.SubjectDAO;

namespace TimetableManager.Generator
{
    class GeneratorDAO
    {
        //get all sessions
        public static List<Session> getAllSessions()
        {
            List<Session> ssList = new List<Session>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT session_id,lecturers,subj_name,subj_code,tag,grp_id,duration FROM Sessions";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Session ss = new Session();
                        ss.Id = int.Parse(reader["session_id"].ToString());
                        ss.Lecturer = reader["lecturers"].ToString();
                        ss.Sub_name = reader["subj_name"].ToString();
                        ss.Sub_code = reader["subj_code"].ToString();
                        ss.Tag = reader["tag"].ToString();
                        ss.Grp_id = reader["grp_id"].ToString();
                        ss.Duration = double.Parse(reader["duration"].ToString());
                       

                        ssList.Add(ss);
                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Retrieving Data " + e.Message);
                }

            }
            return ssList;

        }

        //get days information
        public static Days getDays()
        {
            List<Weekday> dayList = new List<Weekday>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT days FROM Weekday_Days WHERE week_type='Weekday'";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Weekday ss = new Weekday();
                        ss.Days = reader["days"].ToString();
                        dayList.Add(ss);
                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Retrieving Data " + e.Message);
                }

          }

            int mon=0, tue=0, wed=0, thu=0, fri=0;
      
            foreach(Weekday x in dayList)
            {

                string[] days = x.Days.Split(',');
                foreach(string y in days)
                {
                    if (y.Equals("Monday"))
                    {
                        mon++;
                    }
                    else if (y.Equals("Tuesday"))
                    {
                        tue++;
                    }
                    else if( y.Equals("Wednesday"))
                    {
                        wed++;
                    }
                    else if (y.Equals("Thursday"))
                    {
                        thu++;
                    }
                    else if (y.Equals("Friday"))
                    {
                        fri++;
                    }
                }
            }

            Days d = new Days(mon,tue,wed,thu,fri);

            return d;

        }

        //get all groups
        public static List<StudentGroups> getAllGroups()
        {
            List<StudentGroups> list = new List<StudentGroups>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT group_id FROM Groups_Info";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        StudentGroups sg = new StudentGroups();
                        
                        sg.GroupId = reader["group_id"].ToString();
                     
                        list.Add(sg);
                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Retrieving Data " + e.Message);
                }

            }
            return list;

        }

        //get all lecturers
        public static List<Lecturers> getAllLecturers()
        {
            List<Lecturers> list = new List<Lecturers>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT name FROM Lecturer";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lecturers ll = new Lecturers();

                        ll.LecturerName = reader["name"].ToString();

                        list.Add(ll);
                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Retrieving Data " + e.Message);
                }

            }
            return list;

        }

        //get all rooms
        public static List<Hall> getAllRooms()
        {
            List<Hall> list = new List<Hall>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT r_name,room_type,capacity FROM Room_Names";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Hall hh = new Hall();

                        hh.HallName = reader["r_name"].ToString();
                        hh.Type = reader["room_type"].ToString();
                        hh.Capacity = int.Parse(reader["capacity"].ToString());

                        list.Add(hh);
                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Retrieving Data " + e.Message);
                }

            }
            return list;

        }

        //get rooms by type(tag)
        public static List<Hall> getRoomsByType(string type)
        {
            List<Hall> list = new List<Hall>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT r_name,room_type,capacity FROM Room_Names WHERE room_type=@type";
                    command.Parameters.AddWithValue("@type", type);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Hall hh = new Hall();

                        hh.HallName = reader["r_name"].ToString();
                        hh.Type = reader["room_type"].ToString();
                        hh.Capacity = int.Parse(reader["capacity"].ToString());

                        list.Add(hh);
                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Retrieving Data " + e.Message);
                }

            }
            return list;

        }

        //get specific subject
        public static Subject getSubject(String name)
        {
            Subject sub = new Subject();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT sub_name,lec_hrs,sub_code FROM Subjects WHERE sub_name = @name";
                    command.Parameters.AddWithValue("@name", name);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        

                        sub.SubName = reader["sub_name"].ToString();
                        sub.LecHrs = double.Parse(reader["lec_hrs"].ToString());
                        sub.SubCode = reader["sub_code"].ToString();

   
                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Retrieving Data " + e.Message);
                }

            }
            return sub;

        }

        //get all rooms for subjects
        public static List<Model> getSubjectRooms(string scode)
        {
            List<Model> list = new List<Model>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT s_code,rs_name FROM Room_Names_Subject WHERE s_code=@scode";
                    command.Parameters.AddWithValue("@scode",scode);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Model sg = new Model();

                        sg.Key = reader["s_code"].ToString();
                        sg.RoomName = reader["rs_name"].ToString();

                        list.Add(sg);
                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Retrieving Data " + e.Message);
                }

            }
            return list;

        }

        //get all rooms for lecturers
        public static List<Model> getLecturerRooms(String name)
        {
            List<Model> list = new List<Model>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT l_name,r_name FROM Room_Names_Lecturer WHERE l_name=@name ";
                    command.Parameters.AddWithValue("@name", name);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Model sg = new Model();

                        sg.Key = reader["l_name"].ToString();
                        sg.RoomName = reader["r_name"].ToString();

                        list.Add(sg);
                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Retrieving Data " + e.Message);
                }

            }
            return list;

        }

        //get all rooms for groups
        public static List<Model> getGroupRooms()
        {
            List<Model> list = new List<Model>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT g_id,gr_name FROM Room_Names_Group";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Model sg = new Model();

                        sg.Key = reader["g_id"].ToString();
                        sg.RoomName = reader["gr_name"].ToString();

                        list.Add(sg);
                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Retrieving Data " + e.Message);
                }

            }
            return list;

        }

        //get student timetables
        public static List<Model> getStudentTables()
        {
            List<Model> list = new List<Model>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT GroupId,Timetable FROM StudentTimetables";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Model sg = new Model();

                        sg.Key = reader["GroupId"].ToString();
                        sg.RoomName = reader["Timetable"].ToString();

                        list.Add(sg);
                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Retrieving Data " + e.Message);
                }

            }
            return list;

        }

        //save student timetables
        public static void insertStudentTable(Model model)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "INSERT INTO StudentTimetables(GroupId,Timetable) VALUES (@groupid,@timetable)";
                    command.Parameters.AddWithValue("@groupid", model.Key);
                    command.Parameters.AddWithValue("@timetable", model.RoomName);


                    var t = command.ExecuteNonQuery();
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Inserting " + e.Message);
                }

            }
        }

        //get lecturer timetables
        public static List<Model> getLecturerTables()
        {
            List<Model> list = new List<Model>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT name,Timetable FROM LecturerTimetables";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Model sg = new Model();

                        sg.Key = reader["name"].ToString();
                        sg.RoomName = reader["Timetable"].ToString();

                        list.Add(sg);
                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Retrieving Data " + e.Message);
                }

            }
            return list;

        }

        //save lecturer timetables
        public static void insertLecturerTable(Model model)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "INSERT INTO LecturerTimetables(name,Timetable) VALUES (@name,@timetable)";
                    command.Parameters.AddWithValue("@name", model.Key);
                    command.Parameters.AddWithValue("@timetable", model.RoomName);


                    var t = command.ExecuteNonQuery();
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Inserting " + e.Message);
                }

            }
        }

        //get rooms timetables
        public static List<Model> getRoomsTables()
        {
            List<Model> list = new List<Model>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT name,Timetable FROM RoomTimetables";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Model sg = new Model();

                        sg.Key = reader["name"].ToString();
                        sg.RoomName = reader["Timetable"].ToString();

                        list.Add(sg);
                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Retrieving Data " + e.Message);
                }

            }
            return list;

        }

        //save rooms timetables
        public static void insertRoomTable(Model model)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = "INSERT INTO RoomTimetables(name,Timetable) VALUES (@name,@timetable)";
                    command.Parameters.AddWithValue("@name", model.Key);
                    command.Parameters.AddWithValue("@timetable", model.RoomName);


                    var t = command.ExecuteNonQuery();
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Inserting " + e.Message);
                }

            }
        }

        //clear tables
        public static void clearTables()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command1 = new SQLiteCommand(conn);
                    SQLiteCommand command2 = new SQLiteCommand(conn);
                    SQLiteCommand command3 = new SQLiteCommand(conn);
                    command1.CommandText = "DELETE FROM StudentTimetables";
                    command2.CommandText = "DELETE FROM LecturerTimetables";
                    command3.CommandText = "DELETE FROM RoomTimetables";

                    var t = command1.ExecuteNonQuery();
                    var x = command2.ExecuteNonQuery();
                    var y = command3.ExecuteNonQuery();
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Inserting " + e.Message);
                }

            }
        }

        //get not available slots
        public static int[] getNotAvailable(String name)
        {
            int[] arr = new int[2];
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                
                string date, from, to;
                int dt=0;
                int slot = 0;
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT date,fromt,tot FROM Room_Names_NotAvailable WHERE nar_name=@name";
                    command.Parameters.AddWithValue("@name",name);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        
                         date = reader["date"].ToString();
                         from = reader["fromt"].ToString();
                         to = reader["tot"].ToString();

                        if (date.Equals("Monday"))
                        {
                            dt = 0;
                        }
                        else if (date.Equals("Tuesday"))
                        {
                            dt = 1;
                        }
                        else if (date.Equals("Wednesday"))
                        {
                            dt = 2;
                        }
                        else if (date.Equals("Thursday"))
                        {
                            dt = 3;
                        }
                        else if (date.Equals("Friday"))
                        {
                            dt = 4;
                        }

                        if(from.Equals("8:30 AM"))
                        {
                            slot = 0;
                        }
                        else if(from.Equals("9:30 AM"))
                        {
                            slot = 1;
                        }
                        else if (from.Equals("10:30 AM"))
                        {
                            slot = 2;
                        }
                        else if (from.Equals("11:30 AM"))
                        {
                            slot = 3;
                        }
                        else if (from.Equals("12:30 PM"))
                        {
                            slot = 4;
                        }
                        else if (from.Equals("1:30 PM"))
                        {
                            slot = 5;
                        }
                        else if (from.Equals("2:30 PM"))
                        {
                            slot = 6;
                        }
                        else if (from.Equals("3:30 PM"))
                        {
                            slot = 7;
                        }
                        else if (from.Equals("4:30 PM"))
                        {
                            slot = 8;
                        }

                        arr[0] = dt;
                        arr[1] = slot;
                    }
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Retrieving Data " + e.Message);
                }

            }

            return arr;
        }


    }
}
