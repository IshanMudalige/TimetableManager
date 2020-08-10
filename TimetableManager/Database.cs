using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager
{
    class Database
    {
        public SQLiteConnection conn;
        public Database()
        {
            try
            {
                conn = new SQLiteConnection("Data Source="+App.databasePath);
                conn.Open();
                MessageBox.Show("DB Connected");
            }catch(Exception e)
            {
                MessageBox.Show("DB Fail "+e);
            }
        }
    }
}
