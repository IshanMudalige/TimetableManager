using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.RoomsAssignSession
{
    class RoomAssignDAO
    {
        /*public RoomAssignDAO() { }

        public static List<RoomAssign> getAll()
        {
            List<RoomAssign> roomList = new List<RoomAssign>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT b_name,r_name,room_type,capacity FROM Room_Names";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Room room = new Room();
                    room.BuildingName = reader["b_name"].ToString();
                    room.RoomName = reader["r_name"].ToString();
                    room.RoomType = reader["room_type"].ToString();
                    room.Capacity = int.Parse(reader["capacity"].ToString());

                    roomList.Add(room);
                }
            }

            return roomList;
        }*/


        /*public static List<NormalSessions> getAll()
        {
            List<NormalSessions> roomList = new List<NormalSessions>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT lecturers,subj_name,subj_code,tag,grp_id,subgrp_id,no_students,duration FROM Sessions";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    NormalSessions session = new NormalSessions();
                    session = reader["lecturers"].ToString();
                    session.Lecturers = reader["lecturers"].ToString();
                    session.Sname = reader["subj_name"].ToString();
                    session.Scode = reader["subj_code"].ToString();
                    session.Tag = reader["tag"].ToString();
                    session.GrpID = reader["grp_id"].ToString();
                    session.SubID = reader["subgrp_id"].ToString();
                    session.NoStu = int.Parse(reader["no_students"].ToString());
                    session.Duration = double.Parse(reader["duration"].ToString());

                    roomList.Add(session);
                }
            }

            return roomList;
        }*/
    }
}
