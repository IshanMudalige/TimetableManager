using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using TimetableManager.DaysHoursDAO;
using TimetableManager.Generator;
using TimetableManager.SubjectDAO;



namespace TimetableManager
{
    /// <summary>
    /// Interaction logic for Page_Timetable.xaml
    /// </summary>
    public partial class Page_Timetable : Page
    {
        List<StudentGroups> sgList;
        List<Hall> hList;
        List<Lecturers> lList;
        
        public Page_Timetable()
        {
            InitializeComponent();
            sgList = GeneratorDAO.getAllGroups();
            hList = GeneratorDAO.getAllRooms();
            lList = GeneratorDAO.getAllLecturers();
            List<Session> sesList = GeneratorDAO.getAllSessions();
            List<Subject> subList = SubjectDetailsDAO.getAll();
            List<Weekday> weekList = WeekdayDAO.getAll();

            txGroups.Text = sgList.Count.ToString();
            txSess.Text = sesList.Count.ToString();
            txRoom.Text = hList.Count.ToString();
            txLec.Text = lList.Count.ToString();
            txSub.Text = subList.Count.ToString();
            txWeek.Text = weekList.Count.ToString();
        }

      
        public void Generate(object sender, RoutedEventArgs e)
        {

            AssignToMonday();
           /* sgList = GeneratorDAO.getAllGroups();
            hList = GeneratorDAO.getAllRooms();
            lList = GeneratorDAO.getAllLecturers();
            List<Session> sList = GeneratorDAO.getAllSessions();
            Days days = GeneratorDAO.getDays();

          

            foreach (Session x in sList)
            {
                Subject sub = GeneratorDAO.getSubject(x.Sub_name);
                Console.WriteLine("\nSession - " + x.Lecturer + "/" + x.Sub_name + "/" + x.Tag + "/" + x.Grp_id);
                Lecturers lec = new Lecturers();
                foreach (Lecturers z in lList)
                {
                    if (z.LecturerName.Equals(x.Lecturer))
                    {
                        lec = z;
                        Console.WriteLine("Lecture - "+z.LecturerName);
                    }
                }

                
                foreach (StudentGroups std in sgList)
                {
                       
                        if (std.GroupId.Equals(x.Grp_id))
                        {
                            Console.WriteLine("StGroup - " + std.GroupId);
                            for(int i = 0; i < 4; i++)
                            {
                            int flag = 0;
                                for(int j = 0; j < 5; j++)
                                {

                                    if (std.Student[i, j] == null && lec.Lecturer[i, j] == null)
                                    {
                                        //foreach (Hall h in hList)
                                        //{
                                           // if (h.Type.Equals(x.Tag) && h.Halls[i, j] == null)
                                            //{
                                                std.Student[i, j] = x.Sub_name + " - " + x.Sub_code + "\n" + x.Tag + "\n" + x.Lecturer;
                                                lec.Lecturer[i, j] = x.Sub_name + " - " + x.Sub_code + "\n" + x.Grp_id;
                                               // h.Halls[i, j] = x.Sub_name + " " + x.Sub_code + " " + x.Lecturer;
                                                Console.WriteLine(std.GroupId + " " + lec.LecturerName +" "+" Assigned");
                                                flag = 1;
                                                break;
                                            //}
                                           
                                           
                                        //}
                                    }
                                //if (flag == 1)
                                    //break;
                            }
                            if (flag == 1)
                                break;
                            
                            
                            }
                        
                        }
                    
                }
               
            }*/
           
        }

        //dropdown handler
        private void cbTypeT_DropDownClosed(object sender, EventArgs e)
        {
            GridView myGridView = new GridView();
            myGridView.AllowsColumnReorder = true;
            myGridView.ColumnHeaderToolTip = "Employee Information";

            GridViewColumn column = new GridViewColumn();
            if (cbTypeT.SelectedIndex == 0)
            {
                column.DisplayMemberBinding = new Binding("LecturerName");
                column.Header = "Lecturer Name";
                myGridView.Columns.Add(column);
               
                PopulateLecturerTable(lList);
            }
            else if(cbTypeT.SelectedIndex == 1)
            {
                column.DisplayMemberBinding = new Binding("GroupId");
                column.Header = "Student Groups";
                myGridView.Columns.Add(column);

                PopulateStudentTable(sgList);
            }
            else if(cbTypeT.SelectedIndex == 2)
            {
                column.DisplayMemberBinding = new Binding("HallName");
                column.Header = "Halls";
                myGridView.Columns.Add(column);
                PopulateHallTable(hList);
            }

            listViewT.View = myGridView;
        }

        //populate student table
        private void PopulateStudentTable(List<StudentGroups> list)
        {
            
            var observableList = new ObservableCollection<StudentGroups>();
            list.ForEach(x => observableList.Add(x));

            listViewT.ItemsSource = observableList;
        }

        //populate lecturers table
        private void PopulateLecturerTable(List<Lecturers> list)
        {
            var observableList = new ObservableCollection<Lecturers>();
            list.ForEach(x => observableList.Add(x));

            listViewT.ItemsSource = observableList;
        }

        //populate lecturers table
        private void PopulateHallTable(List<Hall> list)
        {
            var observableList = new ObservableCollection<Hall>();
            list.ForEach(x => observableList.Add(x));

            listViewT.ItemsSource = observableList;
        }

        //list view selection
        private void listViewT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            List<Tables> tableList = new List<Tables>();
            string[] time = {"8:30","9:30","10:30","11:30","12:30","13:30","14:20","15:30","16:30"};

            if (cbTypeT.SelectedIndex == 0)
            {

                Lecturers lec = (Lecturers)listViewT.SelectedItem;
                if (lec != null)
                {
                    heading.Text = lec.LecturerName;
                    for (int i = 0; i < 8 ; i++)
                    {
                        tableList.Add(new Tables() { Time = time[i] , Monday = lec.Lecturer[i, 0], Tuesday = lec.Lecturer[i, 1], Wednesday = lec.Lecturer[i, 2], Thursday = lec.Lecturer[i, 3], Friday = lec.Lecturer[i, 4] });
                       
                    }
                }

            }
            else if (cbTypeT.SelectedIndex == 1)
            {

                StudentGroups std = (StudentGroups)listViewT.SelectedItem;
                if (std != null)
                {
                    heading.Text = std.GroupId;
                    for (int i = 0; i < 8; i++)
                    {
                        tableList.Add(new Tables() { Time = time[i], Monday = std.Student[i, 0], Tuesday = std.Student[i, 1], Wednesday = std.Student[i, 2], Thursday = std.Student[i, 3], Friday = std.Student[i, 4] });
                    }
                }

            }
            else if (cbTypeT.SelectedIndex == 2)
            {

                Hall hall = (Hall)listViewT.SelectedItem;
                if (hall != null)
                {
                    heading.Text = hall.HallName;
                    for (int i = 0; i < 8; i++)
                    {
                        tableList.Add(new Tables() { Time = time[i], Monday = hall.Halls[i, 0], Tuesday = hall.Halls[i, 1], Wednesday = hall.Halls[i, 2], Thursday = hall.Halls[i, 3], Friday = hall.Halls[i, 4] });
                    }
                }

            }
            dgSimple.ItemsSource = tableList;
          
        }

        //printitng timetable
        private void btnPdf_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();

            printDlg.PrintVisual(printView, "Timetable Printing.");
            // Create the print dialog object and set options
           /* PrintDialog pDialog = new PrintDialog();
            pDialog.PageRangeSelection = PageRangeSelection.AllPages;
            pDialog.UserPageRangeEnabled = true;

            // Display the dialog. This returns true if the user presses the Print button.
            Nullable<Boolean> print = pDialog.ShowDialog();
            if (print == true)
            {
                XpsDocument xpsDocument = new XpsDocument("C:\\FixedDocumentSequence.xps", FileAccess.ReadWrite);
                FixedDocumentSequence fixedDocSeq = xpsDocument.GetFixedDocumentSequence();
                pDialog.PrintDocument(fixedDocSeq.DocumentPaginator, "Test print job");
            }*/


        }

        private void AssignToMonday()
        {
            sgList = GeneratorDAO.getAllGroups();
            hList = GeneratorDAO.getAllRooms();
            lList = GeneratorDAO.getAllLecturers();
            List<Session> sList = GeneratorDAO.getAllSessions();
            Days days = GeneratorDAO.getDays();
            int noOfSuccess = 0;


            foreach (Session x in sList)
            {
                Subject sub = GeneratorDAO.getSubject(x.Sub_name);
                Console.WriteLine("\nSession - " + x.Lecturer + "/" + x.Sub_name + "/" + x.Tag + "/" + x.Grp_id);
                WriteLn("--------------------------------------------------------------------------");
                WriteLn("Session - " + x.Lecturer + "/" + x.Sub_name + "/" + x.Tag + "/" + x.Grp_id);
                Lecturers lec = new Lecturers();
                foreach (Lecturers z in lList)
                {
                    if (z.LecturerName.Equals(x.Lecturer))
                    {
                        lec = z;
                        Console.WriteLine("Lecture - " + z.LecturerName);
                        WriteLn("Lecture - " + z.LecturerName);
                    }
                }


                foreach (StudentGroups std in sgList)
                {

                    if (std.GroupId.Equals(x.Grp_id))
                    {
                        Console.WriteLine("StGroup - " + std.GroupId);
                        WriteLn("StGroup - " + std.GroupId);
                        for (int i = 0; i < 8; i++)
                        {
                            int flag = 0;
                            for (int j = 0; j < 5; j++)
                            {

                                if (x.Duration == 1)
                                {
                                   
                                    if (std.Student[i, j] == null && lec.Lecturer[i, j] == null)
                                    {
                                       
                                        foreach (Hall hall in hList)
                                        {
                                           
                                            if (hall.Type.Equals(x.Tag) && hall.Halls[i, j] == null)
                                            {
                                                std.Student[i, j] = x.Sub_name + " - " + x.Sub_code + "(" + x.Tag + ")\n" + x.Lecturer + "\n" + hall.HallName;
                                                lec.Lecturer[i, j] = x.Sub_name + " - " + x.Sub_code + "\n" + x.Grp_id+"\n"+hall.HallName;
                                                hall.Halls[i, j] = x.Grp_id+"\n"+x.Sub_name + " - " + x.Sub_code + "\n" + x.Lecturer;
                                                Console.WriteLine(std.GroupId + " " + lec.LecturerName + " " + hall.HallName + " Assigned Successfully");
                                                WriteLn("Session -" + x.Sub_name+" "+std.GroupId + " " + lec.LecturerName + " " + hall.HallName + " Assigned Successfully");
                                                noOfSuccess++;
                                                flag = 1;
                                                break;
                                            }
                                        }


                                    }
                                }else if (x.Duration == 2)
                                {
                                   
                                    if (std.Student[i, j] == null && lec.Lecturer[i, j] == null && std.Student[i+1, j] == null && lec.Lecturer[i+1, j] == null)
                                    {
                                       
                                        foreach (Hall hall in hList)
                                        {
                                            if (hall.Type.Equals(x.Tag) && hall.Halls[i, j] == null && hall.Halls[i+1, j] == null)
                                            {
                                                std.Student[i, j] = x.Sub_name + " - " + x.Sub_code + "(" + x.Tag + ")\n" + x.Lecturer + "\n" + hall.HallName;
                                                lec.Lecturer[i, j] = x.Sub_name + " - " + x.Sub_code + "\n" + x.Grp_id+"\n"+hall.HallName;
                                                hall.Halls[i, j] = x.Grp_id + "\n" + x.Sub_name + " - " + x.Sub_code + "\n" + x.Lecturer;
                                                std.Student[i+1, j] = x.Sub_name + " - " + x.Sub_code + "(" + x.Tag + ")\n" + x.Lecturer + "\n" + hall.HallName;
                                                lec.Lecturer[i+1, j] = x.Sub_name + " - " + x.Sub_code + "\n" + x.Grp_id + "\n" + hall.HallName;
                                                hall.Halls[i+1, j] = x.Grp_id + "\n" + x.Sub_name + " - " + x.Sub_code + "\n" + x.Lecturer;
                                                Console.WriteLine(std.GroupId + " " + lec.LecturerName + " " + hall.HallName + " Assigned Successfully");
                                                WriteLn("Session -"+x.Sub_name + " " + std.GroupId + " " + lec.LecturerName + " " + hall.HallName + " Assigned Successfully");
                                                noOfSuccess++;
                                                flag = 1;
                                                break;
                                            }
                                        }


                                    }
                                }
                                if (flag == 1)
                                    break;
                            }
                            if (flag == 1)
                                break;


                        }

                    }

                }
                    
            }
            WriteLn("\n> No of Sessions successfully a assigned ---- "+ noOfSuccess);
            WriteLn("> No of Sessions failed to assigned ---- " +(sList.Count-noOfSuccess));
        }

        private void WriteLn(string text)
        {
            
            txlog.Dispatcher.BeginInvoke(new Action(() =>
            {
                txlog.Text += text + Environment.NewLine;
            }));
           
        }

      

    }


}




