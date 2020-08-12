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
    /// Interaction logic for Page_Lecturers.xaml
    /// </summary>
    public partial class Page_Lecturers : Page
    {
        public Page_Lecturers()
        {
            InitializeComponent();
            
        }

        private void btnLecRank_Click(object sender, RoutedEventArgs e)
        {
            string level = txtLevel.Text;
            string empID = txtLecID.Text;

            txtLecRank.Text = level + "." + empID;
        }

        private void txtLevel_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string category = cmbLecCategory.Text;

            if(category == "Professor")
            {
                txtLevel.Text = "1";
            }
            else if(category == "Assistant Professor")
            {
                txtLevel.Text = "2";
            }
            else if(category == "Senior Lecturer(HG)")
            {
                txtLevel.Text = "3";
            }
            else if(category == "Senior Lecturer")
            {
                txtLevel.Text = "4";
            }
            else if(category == "Lecturer")
            {
                txtLevel.Text = "5";
            }
            else if(category == "Assistant Lecturer")
            {
                txtLevel.Text = "6";
            }
            else
            {
                txtLevel.Text = "7";
            }
        }
    }
}
