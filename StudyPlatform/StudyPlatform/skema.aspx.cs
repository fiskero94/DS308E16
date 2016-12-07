using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudyPlatform.Classes.Model;
using System.Globalization;
using System.IO;
using System.Data;

namespace StudyPlatform
{
    public partial class Skema : Page
    {
        private static readonly string[] TimeSlots = { "08:10", "09:05", "10:00", "10:55", "12:05", "13:00", "13:55", "14:50" };



        // Lav Color Picker metode, hvor user.Course.Count tages, og Colores fremstilles ud fra start color.

        private static readonly Dictionary<string, string> GetCourseColor = new Dictionary<string, string>
        {
            { "Kemi B", "#876ED7" },
            { "Fysik A", "#6A48D7" },
            { "Matematik A", "#200772" },
            { "Dansk A", "#412C84" },
            { "Idræt C", "#3914AF" },
            { "Geografi B", "#7109AA" },
            { "Engelsk A", "#5F2580" },
            { "Samfund B", "#48036F" },
        };



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["user"] is Student || Session["user"] is Teacher))
                Response.Redirect("login.aspx");
            Master.TitelLabelText = "Skema";

            string year = Request.QueryString["aar"];
            string week = Request.QueryString["uge"];
            Person user = (Person)Session["user"];
            int weekNumber = Convert.ToInt32(Request.QueryString["uge"]);

            if (year != null && week != null)
            {
                SortedList<string, List<Lesson>> sortedList = new SortedList<string, List<Lesson>>();

                if (Session["user"] is Student)
                {
                    List<Course> courses = Student.GetByID(Convert.ToUInt32(user.ID)).Courses;
                    sortedList = FindAndSortLessonsForPerson(courses, weekNumber);
                }
                else if (Session["user"] is Teacher)
                {
                    List<Course> courses = Teacher.GetByID(Convert.ToUInt32(user.ID)).Courses;
                    sortedList = FindAndSortLessonsForPerson(courses, weekNumber);
                }
                else
                {
                    Response.Redirect("nyheder.aspx");
                }

                SortedList<string, TableRow> GetTableRow = new SortedList<string, TableRow>();
                GetTableRow.Add("08:10", tableRow1);
                GetTableRow.Add("09:05", tableRow4);
                GetTableRow.Add("10:00", tableRow7);
                GetTableRow.Add("10:55", tableRow10);
                GetTableRow.Add("12:05", tableRow13);
                GetTableRow.Add("13:00", tableRow16);
                GetTableRow.Add("13:55", tableRow19);
                GetTableRow.Add("14:50", tableRow22);

                SortedList<string, int> GetCellNumber = new SortedList<string, int>();
                GetCellNumber.Add("Monday", 1);
                GetCellNumber.Add("Tuesday", 2);
                GetCellNumber.Add("Wednesday", 3);
                GetCellNumber.Add("Thursday", 4);
                GetCellNumber.Add("Friday", 5);

                SortedList<string, int> GetRowNumber = new SortedList<string, int>();
                GetRowNumber.Add("08:10", 1);
                GetRowNumber.Add("09:05", 4);
                GetRowNumber.Add("10:00", 7);
                GetRowNumber.Add("10:55", 10);
                GetRowNumber.Add("12:05", 13);
                GetRowNumber.Add("13:00", 16);
                GetRowNumber.Add("13:55", 19);
                GetRowNumber.Add("14:50", 22);



                foreach (string timeslot in TimeSlots)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        TableCell tableCell = new TableCell { RowSpan = 2 };
                        tableCell.Attributes["Style"] = "background:transparent; position: relative; padding: 0px; margin: 0px;";
                        GetTableRow[timeslot].Cells.Add(tableCell);
                    }

                    foreach (var lesson in sortedList[timeslot])
                    {
                        Button button = new Button();

                        // No line break?!?!?!?!?
                        string strtext = lesson.Course.Name + Environment.NewLine + " - " + lesson.Rooms[0].Name;
                        button.Text = strtext;


                        // MANGLER KURSUS COLOR ///////////////////////////////////
                        button.Attributes.Add("Style", "border:none; position: absolute; width: 100%; height: 100%; margin: 0 auto; left: auto; right: auto; background: " + GetCourseColor[lesson.Course.Name] + ";");
                        // color: black; display: block; 

                        //button.Attributes.Add("Style", "background:" + GetCourseColor[lesson.Course.Name] + ";");

                        scheduleTable.Rows[GetRowNumber[timeslot]]
                                        .Cells[GetCellNumber[lesson.DateTime.DayOfWeek.ToString()]]
                                        .Controls.Add(button);
                    }
                }
            }
            else
            {
                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                System.Globalization.Calendar cal = dfi.Calendar;
                int currentWeek = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
                int currentYear = cal.GetYear(DateTime.Now);

                Response.Redirect("skema.aspx?aar=" + currentYear + "&uge=" + currentWeek);
            }
        }




    protected void Button_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "linkRow1Cell1", "openModal();", true);
        }


        private SortedList<string, List<Lesson>> FindAndSortLessonsForPerson(List<Course> courses, int weekNumber)
        {
            List<Lesson> lessons = new List<Lesson>();
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            System.Globalization.Calendar cal = dfi.Calendar;

            foreach (Course course in courses)
            {
                foreach (Lesson lesson in course.Lessons)
                {
                    if (cal.GetWeekOfYear(lesson.DateTime, dfi.CalendarWeekRule, dfi.FirstDayOfWeek) == weekNumber)
                    {
                        lessons.Add(lesson);
                    }
                }
            }

            List<Lesson> sortedList = lessons.OrderBy(x => x.DateTime.TimeOfDay).ToList();

            TimeSpan timespanLesson1 = new TimeSpan(8, 10, 0);
            TimeSpan timespanLesson2 = new TimeSpan(9, 5, 0);
            TimeSpan timespanLesson3 = new TimeSpan(10, 0, 0);
            TimeSpan timespanLesson4 = new TimeSpan(10, 55, 0);
            TimeSpan timespanLesson5 = new TimeSpan(12, 5, 0);
            TimeSpan timespanLesson6 = new TimeSpan(13, 0, 0);
            TimeSpan timespanLesson7 = new TimeSpan(13, 55, 0);
            TimeSpan timespanLesson8 = new TimeSpan(14, 50, 0);

            List<Lesson> lesson1List = new List<Lesson>();
            List<Lesson> lesson2List = new List<Lesson>();
            List<Lesson> lesson3List = new List<Lesson>();
            List<Lesson> lesson4List = new List<Lesson>();
            List<Lesson> lesson5List = new List<Lesson>();
            List<Lesson> lesson6List = new List<Lesson>();
            List<Lesson> lesson7List = new List<Lesson>();
            List<Lesson> lesson8List = new List<Lesson>();

            SortedList<string, List<Lesson>> coollist = new SortedList<string, List<Lesson>>();
            coollist.Add("08:10", lesson1List);
            coollist.Add("09:05", lesson2List);
            coollist.Add("10:00", lesson3List);
            coollist.Add("10:55", lesson4List);
            coollist.Add("12:05", lesson5List);
            coollist.Add("13:00", lesson6List);
            coollist.Add("13:55", lesson7List);
            coollist.Add("14:50", lesson8List);








            foreach (Lesson lesson in sortedList)
            {
                if (lesson.DateTime.TimeOfDay == timespanLesson1)
                {
                    lesson1List.Add(lesson);
                }
                else if (lesson.DateTime.TimeOfDay == timespanLesson2)
                {
                    lesson2List.Add(lesson);
                }
                else if (lesson.DateTime.TimeOfDay == timespanLesson3)
                {
                    lesson3List.Add(lesson);
                }
                else if (lesson.DateTime.TimeOfDay == timespanLesson4)
                {
                    lesson4List.Add(lesson);
                }
                else if (lesson.DateTime.TimeOfDay == timespanLesson5)
                {
                    lesson5List.Add(lesson);
                }
                else if (lesson.DateTime.TimeOfDay == timespanLesson6)
                {
                    lesson6List.Add(lesson);
                }
                else if (lesson.DateTime.TimeOfDay == timespanLesson7)
                {
                    lesson7List.Add(lesson);
                }
                else if (lesson.DateTime.TimeOfDay == timespanLesson8)
                {
                    lesson8List.Add(lesson);
                }
            }
            return coollist;
        }
    }
}