using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.StatisticsDAO
{
    class SubjDAO
    {

        public SubjDAO()
        {
        }

        public static List<Subj> getAll()
        {
            List<Subj> subje = new List<Subj>();
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand(conn);
                    command.CommandText = @"SELECT offer_year,offer_sem,sub_name,sub_code,lec_hrs,tute_hrs,lab_hrs,evalu_hrs FROM Subjects";
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Subj subject = new Subj();
                        subject.Year = reader["offer_year"].ToString();
                        subject.Semester = reader["offer_sem"].ToString();
                        subject.SubName = reader["sub_name"].ToString();
                        subject.SubCode = reader["sub_code"].ToString();
                        subject.LecHrs = double.Parse(reader["lec_hrs"].ToString());
                        subject.TuteHrs = double.Parse(reader["tute_hrs"].ToString());
                        subject.LabHrs = double.Parse(reader["lab_hrs"].ToString());
                        subject.EvaluHrs = double.Parse(reader["evalu_hrs"].ToString());

                        subje.Add(subject);

                    }

                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Error in Loading" + e.Message);
                }
            }

            return subje;
        }
    }
}
