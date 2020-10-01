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

            TimetableGenerator();
            GeneratorDAO.clearTables();
            SaveLecturerTimetables();
            SaveRoomTimetables();
            SaveStudentTimetables();
            WriteLn("> All generated timetables saved");
          
        }

        //dropdown handler
        private void cbTypeT_DropDownClosed(object sender, EventArgs e)
        {
            GridView myGridView = new GridView();
            myGridView.AllowsColumnReorder = true;
            myGridView.ColumnHeaderToolTip = "Information";

            GridViewColumn column = new GridViewColumn();
            if (cbTypeT.SelectedIndex == 0)
            {
                column.DisplayMemberBinding = new Binding("LecturerName");
                column.Header = "Lecturer Name";
                myGridView.Columns.Add(column);
               
                PopulateLecturerTable(ReadLecturerTimetables());
            }
            else if(cbTypeT.SelectedIndex == 1)
            {
                column.DisplayMemberBinding = new Binding("GroupId");
                column.Header = "Student Groups";
                myGridView.Columns.Add(column);

                PopulateStudentTable(ReadStudentTimetables());
            }
            else if(cbTypeT.SelectedIndex == 2)
            {
                column.DisplayMemberBinding = new Binding("HallName");
                column.Header = "Halls";
                myGridView.Columns.Add(column);
                PopulateHallTable(ReadRoomTimetables());
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

        private void TimetableGenerator()
        {
            sgList = GeneratorDAO.getAllGroups();
            hList = GeneratorDAO.getAllRooms();
            lList = GeneratorDAO.getAllLecturers();
            List<Session> sList = GeneratorDAO.getAllSessions();
            Days days = GeneratorDAO.getDays();
            int noOfSuccess = 0;

            //================ Go through sessions =================
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

                List<Model> subRoomList = new List<Model>(); 
                subRoomList = GeneratorDAO.getSubjectRooms(sub.SubCode);
                
                //========= Search and get student group=============
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
                                //========================================================= Duration 1 =============================================================
                                if (x.Duration == 1)
                                {

                                    if (std.Student[i, j] == null && lec.Lecturer[i, j] == null)
                                    {
                                        
                                        if (subRoomList != null && subRoomList.Count>0)//Has a specific room for subject
                                        {
                                            WriteLn("Subject has preffered rooms");
                                            foreach (Model m in subRoomList)
                                            {
                                               
                                                foreach (Hall hall in hList)
                                                {
                                                    Console.WriteLine(hall.HallName);
                                                    if (hall.HallName.Equals(m.RoomName) && hall.Halls[i, j] == null)
                                                    {
                                                        std.Student[i, j] = x.Sub_name + " - " + x.Sub_code + "(" + x.Tag + ")\n" + x.Lecturer + "\n" + hall.HallName;
                                                        lec.Lecturer[i, j] = x.Sub_name + " - " + x.Sub_code + "\n" + x.Grp_id + "\n" + hall.HallName;
                                                        hall.Halls[i, j] = x.Grp_id + "\n" + x.Sub_name + " - " + x.Sub_code + "\n" + x.Lecturer;
                                                        Console.WriteLine(std.GroupId + " " + lec.LecturerName + " " + hall.HallName + " Assigned Successfully");
                                                        WriteLn("Session -" + x.Sub_name + " " + std.GroupId + " " + lec.LecturerName + " " + hall.HallName + " Assigned Successfully");
                                                        noOfSuccess++;
                                                        flag = 1;
                                                        Console.WriteLine(x.Sub_code+" saved for" + hall.HallName);
                                                        break;
                                                    }
                                                }
                                                if (flag == 1)
                                                    break;
                                            }
                                        }
                                        else //No specific room for subject
                                        {
                                            
                                            foreach (Hall hall in hList)
                                            {

                                                if (hall.Type.Equals(x.Tag) && hall.Halls[i, j] == null)
                                                {
                                                    std.Student[i, j] = x.Sub_name + " - " + x.Sub_code + "(" + x.Tag + ")\n" + x.Lecturer + "\n" + hall.HallName;
                                                    lec.Lecturer[i, j] = x.Sub_name + " - " + x.Sub_code + "\n" + x.Grp_id + "\n" + hall.HallName;
                                                    hall.Halls[i, j] = x.Grp_id + "\n" + x.Sub_name + " - " + x.Sub_code + "\n" + x.Lecturer;
                                                    Console.WriteLine(std.GroupId + " " + lec.LecturerName + " " + hall.HallName + " Assigned Successfully");
                                                    WriteLn("Session -" + x.Sub_name + " " + std.GroupId + " " + lec.LecturerName + " " + hall.HallName + " Assigned Successfully");
                                                    noOfSuccess++;
                                                    flag = 1;
                                                    break;
                                                }
                                            }


                                        }
                                    }
                                }
                                //======================================================= Duration 2 ===============================================================
                                else if (x.Duration == 2)
                                {
                                    try
                                    {
                                        if (std.Student[i, j] == null && lec.Lecturer[i, j] == null && std.Student[i + 1, j] == null && lec.Lecturer[i + 1, j] == null)
                                        {
                                            if (subRoomList != null && subRoomList.Count>0)
                                            {
                                                foreach (Model m in subRoomList)
                                                {
                                                    foreach (Hall hall in hList)
                                                    {
                                                        if (hall.HallName.Equals(m.RoomName) && hall.Halls[i, j] == null && hall.Halls[i + 1, j] == null)
                                                        {
                                                            std.Student[i, j] = x.Sub_name + " - " + x.Sub_code + "(" + x.Tag + ")\n" + x.Lecturer + "\n" + hall.HallName;
                                                            lec.Lecturer[i, j] = x.Sub_name + " - " + x.Sub_code + "\n" + x.Grp_id + "\n" + hall.HallName;
                                                            hall.Halls[i, j] = x.Grp_id + "\n" + x.Sub_name + " - " + x.Sub_code + "\n" + x.Lecturer;
                                                            std.Student[i + 1, j] = x.Sub_name + " - " + x.Sub_code + "(" + x.Tag + ")\n" + x.Lecturer + "\n" + hall.HallName;
                                                            lec.Lecturer[i + 1, j] = x.Sub_name + " - " + x.Sub_code + "\n" + x.Grp_id + "\n" + hall.HallName;
                                                            hall.Halls[i + 1, j] = x.Grp_id + "\n" + x.Sub_name + " - " + x.Sub_code + "\n" + x.Lecturer;
                                                            Console.WriteLine(std.GroupId + " " + lec.LecturerName + " " + hall.HallName + " Assigned Successfully");
                                                            WriteLn("Session -" + x.Sub_name + " " + std.GroupId + " " + lec.LecturerName + " " + hall.HallName + " Assigned Successfully");
                                                            noOfSuccess++;
                                                            flag = 1;
                                                            break;
                                                        }
                                                    }
                                                    if (flag == 1)
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                foreach (Hall hall in hList)
                                                {
                                                    if (hall.Type.Equals(x.Tag) && hall.Halls[i, j] == null && hall.Halls[i + 1, j] == null)
                                                    {
                                                        std.Student[i, j] = x.Sub_name + " - " + x.Sub_code + "(" + x.Tag + ")\n" + x.Lecturer + "\n" + hall.HallName;
                                                        lec.Lecturer[i, j] = x.Sub_name + " - " + x.Sub_code + "\n" + x.Grp_id + "\n" + hall.HallName;
                                                        hall.Halls[i, j] = x.Grp_id + "\n" + x.Sub_name + " - " + x.Sub_code + "\n" + x.Lecturer;
                                                        std.Student[i + 1, j] = x.Sub_name + " - " + x.Sub_code + "(" + x.Tag + ")\n" + x.Lecturer + "\n" + hall.HallName;
                                                        lec.Lecturer[i + 1, j] = x.Sub_name + " - " + x.Sub_code + "\n" + x.Grp_id + "\n" + hall.HallName;
                                                        hall.Halls[i + 1, j] = x.Grp_id + "\n" + x.Sub_name + " - " + x.Sub_code + "\n" + x.Lecturer;
                                                        Console.WriteLine(std.GroupId + " " + lec.LecturerName + " " + hall.HallName + " Assigned Successfully");
                                                        WriteLn("Session -" + x.Sub_name + " " + std.GroupId + " " + lec.LecturerName + " " + hall.HallName + " Assigned Successfully");
                                                        noOfSuccess++;
                                                        flag = 1;
                                                        break;
                                                    }
                                                }
                                            }


                                        }
                                    }
                                    catch (IndexOutOfRangeException e) { Console.WriteLine(e); }
                                }
                                if (flag == 1)//second for loop
                                    break;

                            }//end of second for loop
                            if (flag == 1)//first for loop
                                break;


                        }//end of first for loop

                    }

                }//end of student loop
                    
            }//end of session loop
            WriteLn("\n> No of Sessions successfully assigned ---- "+ noOfSuccess);
            WriteLn("> No of Sessions failed to assigned -------- " +(sList.Count-noOfSuccess));
        }

        //console writer
        private void WriteLn(string text)
        {
            
            txlog.Dispatcher.BeginInvoke(new Action(() =>
            {
                txlog.Text += text + Environment.NewLine;
            }));
           
        }

        //save student timetables into DB
        private void SaveStudentTimetables()
        {
            
            foreach (StudentGroups x in sgList)
            {
                string[,] board = new string[8, 5];
                board = x.Student;

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < 8; i++)//for each row
                {
                    for (int j = 0; j < 5; j++)//for each column
                    {
                        builder.Append(board[i, j] + "");//append to the output string
                        if (j < board.Length - 1)//if this is not the last row element
                            builder.Append(",");//then add comma (if you don't like commas you can use spaces)
                    }
                    //builder.Append(",");//append new line at the end of the row
                }
                Model model = new Model(x.GroupId,builder.ToString());
                GeneratorDAO.insertStudentTable(model);
            }

            /*string fileName = @"testFile.txt";
            StreamWriter writer = new StreamWriter(fileName);
            writer.WriteLine(builder.ToString());
            writer.Close();*/

        }

        //save lecturer timetables into DB
        private void SaveLecturerTimetables()
        {

            foreach (Lecturers x in lList)
            {
                string[,] board = new string[8, 5];
                board = x.Lecturer;

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < 8; i++)//for each row
                {
                    for (int j = 0; j < 5; j++)//for each column
                    {
                        builder.Append(board[i, j] + "");//append to the output string
                        if (j < board.Length - 1)//if this is not the last row element
                            builder.Append(",");//then add comma (if you don't like commas you can use spaces)
                    }
                   
                }
                Model model = new Model(x.LecturerName, builder.ToString());
                GeneratorDAO.insertLecturerTable(model);
            }

        }

        //save room timetables into DB
        private void SaveRoomTimetables()
        {

            foreach (Hall x in hList)
            {
                string[,] board = new string[8, 5];
                board = x.Halls;

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < 8; i++)//for each row
                {
                    for (int j = 0; j < 5; j++)//for each column
                    {
                        builder.Append(board[i, j] + "");//append to the output string
                        if (j < board.Length - 1)//if this is not the last row element
                            builder.Append(",");//then add comma (if you don't like commas you can use spaces)
                    }

                }
                Model model = new Model(x.HallName, builder.ToString());
                GeneratorDAO.insertRoomTable(model);
            }

        }

        // read student timetables from DB
        private List<StudentGroups> ReadStudentTimetables()
        {

            List<StudentGroups> studentGroups = new List<StudentGroups>();
            List<Model> stdTableList = GeneratorDAO.getStudentTables();
            foreach (Model m in stdTableList)
            {
                string[,] board = new string[8, 5];
                string line = m.RoomName;
                string[] arr = line.Split(',');

                int count = 0;
               
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        board[i, j] = arr[count];
                        count++;
                    }
                }
                StudentGroups stdg = new StudentGroups();
                stdg.GroupId = m.Key;
                stdg.Student = board;
                studentGroups.Add(stdg);

            }

            //string fileName = @"testFile.txt";  
            //StreamReader reader = new StreamReader(fileName);
            //int row = 0;
            //String line = reader.ReadToEnd();
            //while ((line = reader.ReadLine()) != null)
            //{
            //    String[] cols = line.Split(','); //note that if you have used space as separator you have to split on " "
            //    int col = 0;
            //    foreach (String c in cols)
            //    {
            //        board[row,col] = c;
            //        col++;
            //    }
            //    row++;
            //}


            //reader.Close();
           
            return studentGroups;
            
        }

        // read lecturer timetables from DB
        private List<Lecturers> ReadLecturerTimetables()
        {

            List<Lecturers> lecturers = new List<Lecturers>();
            List<Model> lecTableList = GeneratorDAO.getLecturerTables();
            foreach (Model m in lecTableList)
            {
                string[,] board = new string[8, 5];
                string line = m.RoomName;
                string[] arr = line.Split(',');

                int count = 0;

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        board[i, j] = arr[count];
                        count++;
                    }
                }
                Lecturers lec = new Lecturers();
                lec.LecturerName = m.Key;
                lec.Lecturer = board;
                lecturers.Add(lec);
            }
            return lecturers;
        }

        // read rooms timetables from DB
        private List<Hall> ReadRoomTimetables()
        {

            List<Hall> rooms = new List<Hall>();
            List<Model> roomTableList = GeneratorDAO.getRoomsTables();
            foreach (Model m in roomTableList)
            {
                string[,] board = new string[8, 5];
                string line = m.RoomName;
                string[] arr = line.Split(',');

                int count = 0;

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        board[i, j] = arr[count];
                        count++;
                    }
                }
                Hall rm = new Hall();
                rm.HallName = m.Key;
                rm.Halls = board;
                rooms.Add(rm);
            }
            return rooms;
        }

        // clear timetables
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GeneratorDAO.clearTables();
            WriteLn("> All tables cleared");
        }



    }

}




