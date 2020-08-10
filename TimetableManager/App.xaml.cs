using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TimetableManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static string databaseName = "database.db";
        //static string exePath = System.Environment.GetCommandLineArgs()[0];
        static string baseDir = Environment.CurrentDirectory;
        //static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string databasePath = Path.Combine(baseDir, databaseName);
    }
}
