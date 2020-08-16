using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager.SubjectDAO
{
    class SubjectDetailsDAO
    {

        public SubjectDetailsDAO()
        {

        }

        public static void insertSubjects(Subject subject)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.connString))
            {
                try
                {

                }
                catch(SQLiteException e)
                {
                    MessageBox.Show("Error Occured in Inserting " + e.Message);
                }
            }
        }
    }

    
}
