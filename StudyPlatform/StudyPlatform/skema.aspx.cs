using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudyPlatform.Classes.Model;
using System.Globalization;

namespace StudyPlatform
{
    public partial class Skema : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["user"] is Student || Session["user"] is Teacher))
                Response.Redirect("login.aspx");
            Master.TitelLabelText = "Skema";


            Person user = (Person)Session["user"];
            int weekNumber = 48;
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



            foreach (var lesson in sortedList["8:10"])
            {
                // No line break?!?!?!?!?
                string strtext = lesson.Course.Name + Environment.NewLine + lesson.Rooms[0].Name;



                tableRow1Cell1.Attributes.Add("style", "background-color:Red");
                tableRow1Cell1.Text = strtext;









                //LinkButton linkButton = new LinkButton();
                //linkButton.ID = "LinkButtonDynamicInPlaceHolder1Id" + i;
                //linkButton.ForeColor = Color.Blue;
                //linkButton.Font.Bold = true;
                //linkButton.Font.Size = 14;
                //linkButton.Font.Underline = false;
                //linkButton.Text = itemList[i].ItemTitle.InnerText;
                //linkButton.Click += new EventHandler(LinkButton_Click);
                //linkButton.Attributes.Add("LinkUrl", itemList[i].ItemLink.InnerText);
                //tableRow1Cell1.Controls.Add(linkButton);


            }
            foreach (var lesson in sortedList["9:05"])
            {

            }
            foreach (var lesson in sortedList["10:00"])
            {
  
            }
            foreach (var lesson in sortedList["10:55"])
            {
 
            }
            foreach (var lesson in sortedList["12:05"])
            {

            }
            foreach (var lesson in sortedList["13:00"])
            {

            }
            foreach (var lesson in sortedList["13:55"])
            {

            }
            foreach (var lesson in sortedList["14:50"])
            {

            }
        }


        private SortedList<string, List<Lesson>> FindAndSortLessonsForPerson(List<Course> courses, int weekNumber)
        {
            List<Lesson> lessons = new List<Lesson>();

            foreach (Course course in courses)
            {
                foreach (Lesson lesson in course.Lessons)
                {
                    DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                    System.Globalization.Calendar cal = dfi.Calendar;

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
            coollist.Add("8:10", lesson1List);
            coollist.Add("9:05", lesson2List);
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