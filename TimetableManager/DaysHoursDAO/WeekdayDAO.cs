using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.DaysHoursDAO
{
    class WeekdayDAO
    {
        public WeekdayDAO()
        {
        
        }

        //--insert query
        public static void insertWeek(Weekday week)
        {
            using(SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = "INSERT INTO Weekday_Days(title,no_days,days,hours,slots,week_type) VALUES (@title,@no_days,@days,@hours,@slots,@week_type)";
                command.Parameters.AddWithValue("@title",week.Title);
                command.Parameters.AddWithValue("@no_days",week.No_days);
                command.Parameters.AddWithValue("@days",week.Days);
                command.Parameters.AddWithValue("@hours",week.Hours);
                command.Parameters.AddWithValue("@slots",week.Slots);
                command.Parameters.AddWithValue("@week_type",week.Week_type);

                var t = command.ExecuteNonQuery();

            }
        }

        //--delete query
        public static void deleteWeek(string title)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = "DELETE FROM Weekday_Days WHERE title = @title";
                command.Parameters.AddWithValue("@title", title);
                

                var t = command.ExecuteNonQuery();

            }
        }

        //--update query
        public static void updateWeek(string ptitle,Weekday week)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = "UPDATE Weekday_Days " +
                    "SET title = @title," +
                        "no_days = @no_days," +
                        "days = @days," +
                        "hours = @hours," +
                        "slots = @slots," +
                        "week_type = @week_type "+
                    "WHERE title = @ptitle";
                command.Parameters.AddWithValue("@ptitle", ptitle);
                command.Parameters.AddWithValue("@title", week.Title);
                command.Parameters.AddWithValue("@no_days", week.No_days);
                command.Parameters.AddWithValue("@days", week.Days);
                command.Parameters.AddWithValue("@hours", week.Hours);
                command.Parameters.AddWithValue("@slots", week.Slots);
                command.Parameters.AddWithValue("@week_type", week.Week_type);


                var t = command.ExecuteNonQuery();

            }
        }

        //--data retrieve query
        public static List<Weekday> getAll()
        {
            List<Weekday> weekList = new List<Weekday>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"SELECT week_type,title,no_days,days,hours,slots FROM Weekday_Days";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Weekday week = new Weekday();
                    week.Week_type = reader["week_type"].ToString();
                    week.Title = reader["title"].ToString();
                    week.No_days = int.Parse(reader["no_days"].ToString());
                    week.Days = reader["days"].ToString();
                    week.Hours = double.Parse(reader["hours"].ToString());
                    week.Slots = double.Parse((string)reader["slots"].ToString());

                    weekList.Add(week);
                }

            }
            return weekList;
        }

        //--search query
        public static List<Weekday> search(string title)
        {
            List<Weekday> weekList = new List<Weekday>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {

                conn.Open();
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = "SELECT week_type,title,no_days,days,hours,slots FROM Weekday_Days WHERE title LIKE '%"+title+"%'";
               
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Weekday week = new Weekday();
                    week.Week_type = reader["week_type"].ToString();
                    week.Title = reader["title"].ToString();
                    week.No_days = int.Parse(reader["no_days"].ToString());
                    week.Days = reader["days"].ToString();
                    week.Hours = double.Parse(reader["hours"].ToString());
                    week.Slots = double.Parse((string)reader["slots"].ToString());

                    weekList.Add(week);
                }

            }
            return weekList;
        }
    }
}
