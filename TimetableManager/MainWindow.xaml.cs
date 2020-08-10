using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimetableManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Thickness pressed = new Thickness(0, 0, 0, 3);
            Main.Content = new Page_DaysHours();
            btnDaysHours.BorderBrush = Brushes.DodgerBlue;
            btnDaysHours.BorderThickness = pressed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
           
            Thickness pressed = new Thickness(0, 0, 0, 3);

            switch (index)
            {
                case 0:
                    Main.Content = new Page_DaysHours();
                    ChnageButton();
                    btnDaysHours.BorderBrush = Brushes.DodgerBlue;
                    btnDaysHours.BorderThickness = pressed;
                    break;
                case 1:
                    Main.Content = new Page_Lecturers();
                    ChnageButton();
                    btnLecturer.BorderBrush = Brushes.DodgerBlue;
                    btnLecturer.BorderThickness = pressed;
                    break;
                case 2:
                    Main.Content = new Page_Subject();
                    ChnageButton();
                    btnSubject.BorderBrush = Brushes.DodgerBlue;
                    btnSubject.BorderThickness = pressed;
                    break;
                case 3:
                    Main.Content = new Page_Student();
                    ChnageButton();
                    btnStudent.BorderBrush = Brushes.DodgerBlue;
                    btnStudent.BorderThickness = pressed;
                    break;
                case 4:
                    Main.Content = new Page_Tags();
                    ChnageButton();
                    btnTags.BorderBrush = Brushes.DodgerBlue;
                    btnTags.BorderThickness = pressed;
                    break;
                case 5:
                    Main.Content = new Page_Location();
                    ChnageButton();
                    btnLocations.BorderBrush = Brushes.DodgerBlue;
                    btnLocations.BorderThickness = pressed;
                    break;
                case 6:
                    Main.Content = new Page_Stats();
                    ChnageButton();
                    btnStats.BorderBrush = Brushes.DodgerBlue;
                    btnStats.BorderThickness = pressed;
                    break;
                case 7:
                    Main.Content = new Page_Session();
                    ChnageButton();
                    btnSession.BorderBrush = Brushes.DodgerBlue;
                    btnSession.BorderThickness = pressed;
                    break;
                case 8:
                    Main.Content = new Page_Timetable();
                    ChnageButton();
                    btnTimetable.BorderBrush = Brushes.DodgerBlue;
                    btnTimetable.BorderThickness = pressed;
                    break;
               
            }

        }

        private void ChnageButton()
        {
            Thickness none = new Thickness(0, 0, 0, 0);
            btnDaysHours.BorderThickness = none;
            btnLecturer.BorderThickness = none;
            btnStudent.BorderThickness = none;
            btnSubject.BorderThickness = none;
            btnTags.BorderThickness = none;
            btnLocations.BorderThickness = none;
            btnSession.BorderThickness = none;
            btnTimetable.BorderThickness = none;
            btnStats.BorderThickness = none;
        } 
  
    }
}
