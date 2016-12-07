﻿using System;
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
using System.Diagnostics.Contracts;
using System.Net.Mime;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;

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
            { "Historie B", "Pink" },
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


                //HtmlGenericControl div = new HtmlGenericControl("div");

                //Panel panel = new Panel();
                ////panel.ID = "Panel123";

                //panel.ID = "ModalPanel";
                //panel.Attributes["runat"] = "server";
                //panel.BackColor = Color.Gray;
                //panel.Attributes["Style"] = "display: none";

                //div.Controls.Add(panel);
                //bodystuff.Controls.Add(div);


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
                        string t = timeslot + lesson.DateTime.Date.DayOfWeek;

                        //Button button = (Button)Panel1.FindControl(t);


                        // No line break?!?!?!?!?
                        string strtext = lesson.Course.Name + Environment.NewLine + " - " + lesson.Rooms[0].Name;
                        button.Text = strtext;

                        // MANGLER KURSUS COLOR /////////////////////////////////// // color: black; display: block; 
                        button.Attributes.Add("Style", "border:none; position: absolute; width: 100%; height: 100%; margin: 0 auto; left: auto; right: auto; background: " + GetCourseColor[lesson.Course.Name] + ";");



                        button.ID = DateTime.Now.Millisecond.ToString() + lesson.DateTime.Date.DayOfWeek;
                        //button.Click += Button_Click;


                        //scheduleTable.Rows[GetRowNumber[timeslot]]
                        //      .Cells[GetCellNumber[lesson.DateTime.DayOfWeek.ToString()]]
                        //      .Controls.Add(button);



                        string unigid = DateTime.Now.Millisecond.ToString() + lesson.DateTime.Date.DayOfWeek + DateTime.Now.Millisecond.ToString();

                        AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();

                        modalPop.ID = "popUp" + button.ID;

                        modalPop.PopupControlID = unigid;

                        modalPop.TargetControlID = button.ID;

                        modalPop.DropShadow = true;

                        modalPop.CancelControlID = "btnCancel" + unigid;



                        //Panel panel = new Panel();

                        //panel.ID = unigid;
                        //panel.Attributes["runat"] = "server";
                        //panel.BackColor = Color.Gray;
                        //panel.Attributes["Style"] = "display: none";

                        //Button bnt = new Button();
                        //bnt.ID = "btnCancel" + unigid;
                        //bnt.Attributes["runat"] = "server";
                        //bnt.Text = lesson.Course.Name;

                        //panel.Controls.Add(bnt);






                        HtmlGenericControl div = new HtmlGenericControl("div");

                        div.ID = unigid;
                        div.Attributes["Class"] = "modal fade";
                        div.Attributes["role"] = "dialog";

                        HtmlGenericControl div2 = new HtmlGenericControl("div");
                        div2.Attributes["Class"] = "modal-dialog";
                        div.Controls.Add(div2);

                        HtmlGenericControl div3 = new HtmlGenericControl("div");
                        div3.Attributes["Class"] = "modal-content";
                        div2.Controls.Add(div3);

                        HtmlGenericControl div4 = new HtmlGenericControl("div");
                        div3.Attributes["Class"] = "modal-header";

                        Button div4Button = new Button();
                        div4Button.Attributes["type"] = "button";
                        div4Button.Attributes["Class"] = "close";
                        div4Button.Attributes["data-dismiss"] = "modal";
                        div4Button.Text = "&times;";
                        div4.Controls.Add(div4Button);

                        HtmlGenericControl div4H4 = new HtmlGenericControl("h4");
                        div4H4.Attributes["Class"] = "modal-title";
                        div4H4.InnerText = "Modal Header";
                        div3.Controls.Add(div4);

                        HtmlGenericControl div4_2 = new HtmlGenericControl("div");
                        div4_2.Attributes["Class"] = "modal-body";
                        div.InnerText = "Some text in the modal.";
                        div3.Controls.Add(div4_2);

                        HtmlGenericControl div4_3 = new HtmlGenericControl("div");
                        div4_2.Attributes["Class"] = "modal-footer";

                        Button div4_3Button = new Button();
                        div4_3Button.Attributes["type"] = "button";
                        div4_3Button.Attributes["Class"] = "btn btn-default";
                        div4_3Button.Attributes["data-dismiss"] = "modal";
                        div4_3Button.Text = "Close";
                        div4_3.Controls.Add(div4_3Button);
                        div3.Controls.Add(div4_3);

                        div2.Controls.Add(div3);
                        div.Controls.Add(div2);







                        //Panel panel = new Panel();

                        //panel.ID = unigid;
                        //panel.Attributes["runat"] = "server";
                        //panel.CssClass = "modalPopup";
                        //panel.Attributes["Style"] = "display: none";
                        //panel.BackColor = Color.Gray;

                        //HtmlGenericControl divHeader = new HtmlGenericControl("div");
                        //divHeader.Attributes["class"] = "header";
                        //divHeader.InnerText = lesson.Course.Name;

                        //panel.Controls.Add(divHeader);


                        //HtmlGenericControl divBody = new HtmlGenericControl("div");
                        //divBody.Attributes["class"] = "body";
                        //divBody.InnerText = lesson.Description;


                        //Button bnt = new Button();
                        //bnt.ID = "btnCancel" + unigid;
                        //bnt.Attributes["runat"] = "server";
                        //bnt.Text = "Hide Modal Popup";

                        //divBody.Controls.Add(bnt);

                        //panel.Controls.Add(divBody);











                        scheduleTable.Rows[GetRowNumber[timeslot]]
                            .Cells[GetCellNumber[lesson.DateTime.DayOfWeek.ToString()]]
                            .Controls.Add(div);

                        scheduleTable.Rows[GetRowNumber[timeslot]]
                              .Cells[GetCellNumber[lesson.DateTime.DayOfWeek.ToString()]]
                              .Controls.Add(modalPop);

                        scheduleTable.Rows[GetRowNumber[timeslot]]
                            .Cells[GetCellNumber[lesson.DateTime.DayOfWeek.ToString()]]
                            .Controls.Add(button);






















                        //Panel1.Controls.Add(button);


                        //HtmlGenericControl div = new HtmlGenericControl("div");


                        //Panel panel = new Panel();
                        ////panel.ID = "Panel123";

                        //panel.ID = "Panel123";
                        //panel.Attributes["runat"] = "server";
                        //panel.BackColor = Color.Gray;
                        //panel.Attributes["Style"] = "display: none";

                        //div.Controls.Add(panel);






















                        //HtmlGenericControl ajaxModalPopupExtender = new HtmlGenericControl("ajaxToolkit:ModalPopupExtender");



                        //ajaxModalPopupExtender.Attributes["ID"] = "ajaxMPE" + button.ID;
                        //ajaxModalPopupExtender.Attributes["runat"] = "server";
                        //ajaxModalPopupExtender.Attributes["BackgroundCssClass"] = "modalBackground";
                        //ajaxModalPopupExtender.Attributes["TargetControlID"] = button.ID.ToString();
                        //ajaxModalPopupExtender.Attributes["PopupControlID"] = "Panel1";

                        //bodystuff.Controls.Add(ajaxModalPopupExtender);


                        //mpePopUp.BackgroundCssClass = "modalBackground";
                        //mpePopUp.TargetControlID = button.ID;

                        //string d = mpePopUp.TargetControlID;

                        //mpePopUp.PopupControlID = "Panel1";









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
            //PopupExtender mpePopUp = new PopupExtender();

            //mpePopUp = bodystuff.FindControl( );

            //mpePopUp.Show;



 

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