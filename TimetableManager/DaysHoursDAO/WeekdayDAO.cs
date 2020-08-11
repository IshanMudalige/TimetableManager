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
    }
}
