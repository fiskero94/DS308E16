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
using System.Diagnostics.Contracts;
using System.Net.Mime;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
using StudyPlatform.Classes;

namespace StudyPlatform
{
    public partial class Skema : Page
    {
        private static readonly string[] TimeSlots = { "08:10", "09:05", "10:00", "10:55", "12:05", "13:00", "13:55", "14:50" };

        private string year;
        private string week;


        // Lav Color Picker metode, hvor user.Course.Count tages, og Colores fremstilles ud fra start color.

        private static readonly Dictionary<string, string> GetCourseColor = new Dictionary<string, string>
        {
            { "Kemi B", "#6F308A" },
            { "Fysik A", "#DD6726" },
            { "Matematik A", "#B81F34" },
            { "Dansk A", "#7F7E80" },
            { "Idræt C", "#61A547" },
            { "Geografi B", "#4578B4" },
            { "Engelsk A", "#473896" },
            { "Samfund B", "#90278A" },
            { "Historie B", "#7D1615" },
        };


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["user"] is Student || Session["user"] is Teacher))
                Response.Redirect("login.aspx");
            Master.TitelLabelText = "Skema";

            year = Request.QueryString["aar"];
            week = Request.QueryString["uge"];
            CurrentWeekNumber.Text = " " + week + " ";






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







                //// Uge Skift
                //HtmlGenericControl divContainer = new HtmlGenericControl("div");
                //divContainer.Attributes["class"] = "Form-control container";


                //Button buttonLeft = new Button();
                //buttonLeft.ID = "bntLeft";
                //buttonLeft.PostBackUrl = "skema.aspx?aar=" + year + "&uge=" + (Convert.ToInt32(week) - 1);
                //buttonLeft.Attributes["runat"] = "server";
                //buttonLeft.Text = "<---";
                //buttonLeft.Attributes["class"] = "btn btn-default";
                //Panel1.Controls.Add(buttonLeft);

                //Label currentweekLabel = new Label();
                //currentweekLabel.Text = " Uge: " + week + " ";


                //Button buttonRight = new Button();
                //buttonRight.ID = "bntRight";
                //buttonRight.PostBackUrl = "skema.aspx?aar=" + year + "&uge=" + (Convert.ToInt32(week) + 1);
                //buttonRight.Attributes["runat"] = "server";
                //buttonRight.Text = "--->";
                //buttonRight.Attributes["class"] = "btn btn-default";

                //divContainer.Controls.Add(buttonLeft);
                //divContainer.Controls.Add(buttonRight);

                //Panel1.Controls.Add(buttonLeft);
                //Panel1.Controls.Add(currentweekLabel);
                //Panel1.Controls.Add(buttonRight);





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
                        

                        // MANGLER KURSUS COLOR /////////////////////////////////// // color: black; display: block; 
                        button.Attributes.Add("Style", "border:none; position: absolute; width: 100%; height: 100%; margin: 0 auto; left: auto; right: auto; background: " + GetCourseColor[lesson.Course.Name] + "; color: White;");

                        button.ID = DateTime.Now.Millisecond.ToString() + lesson.DateTime.Date.DayOfWeek;
                        button.Click += Button_Click;

                        //button.OnClientClick = "overlayOn()";





                        string unigid = DateTime.Now.Millisecond.ToString() + lesson.DateTime.Date.DayOfWeek + DateTime.Now.Millisecond.ToString();

                        AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();
                        modalPop.ID = "popUp" + button.ID;
                        modalPop.PopupControlID = unigid;
                        modalPop.TargetControlID = button.ID;
                        modalPop.DropShadow = false;
                        modalPop.CancelControlID = "btnCancel" + unigid;

                        //modalPop.BackgroundCssClass = "jsMpeBackground";





                        // MODAL POPUP STUFF

                        //HtmlGenericControl divContainer = new HtmlGenericControl("div");
                        //divContainer.ID = unigid;
                        //divContainer.Attributes["CssClass"] = "modal fade modalPopup";
                        //divContainer.Attributes["role"] = "dialog";

                        //HtmlGenericControl divModalDialog = new HtmlGenericControl("div");
                        //divModalDialog.Attributes["CssClass"] = "modal-dialog";

                        //HtmlGenericControl divContent = new HtmlGenericControl("div");
                        //divContent.Attributes["CssClass"] = "modal-content";

                        //HtmlGenericControl divModalHeader = new HtmlGenericControl("div");
                        //divModalHeader.Attributes["CssClass"] = "modal-header";

                        //Button modalHeaderCloseButton = new Button();
                        //modalHeaderCloseButton.Attributes["type"] = "button";
                        //modalHeaderCloseButton.Attributes["CssClass"] = "close";
                        //modalHeaderCloseButton.Attributes["data-dismiss"] = "modal";
                        //modalHeaderCloseButton.ID = "btnCancel" + unigid;
                        //modalHeaderCloseButton.Text = "&times;";

                        //HtmlGenericControl modalHeaderTitle = new HtmlGenericControl("h4");
                        //modalHeaderTitle.Attributes["CssClass"] = "modal-title";
                        //modalHeaderTitle.InnerText = lesson.Course.Name;

                        //divModalHeader.Controls.Add(modalHeaderCloseButton);
                        //divModalHeader.Controls.Add(modalHeaderTitle);

                        //HtmlGenericControl divModalBody = new HtmlGenericControl("div");
                        //divModalBody.Attributes["CssClass"] = "modal-body";
                        //Label contentDescription = new Label();
                        //contentDescription.Text = lesson.Description;

                        //divModalBody.Controls.Add(contentDescription);

                        //divContent.Controls.Add(divModalHeader);
                        //divContent.Controls.Add(divModalBody);

                        //divModalDialog.Controls.Add(divContent);

                        //divContainer.Controls.Add(divModalDialog);




                        //scheduleTable.Rows[GetRowNumber[timeslot]]
                        //    .Cells[GetCellNumber[lesson.DateTime.DayOfWeek.ToString()]]
                        //    .Controls.Add(button);

                        //Panel1.Controls.Add(modalPop);
                        //Panel1.Controls.Add(divContainer);



                        //////////////////////////////////////////
                        //scheduleTable.Rows[GetRowNumber[timeslot]]
                        //    .Cells[GetCellNumber[lesson.DateTime.DayOfWeek.ToString()]]
                        //    .Controls.Add(modalPop);


                        //scheduleTable.Rows[GetRowNumber[timeslot]]
                        //    .Cells[GetCellNumber[lesson.DateTime.DayOfWeek.ToString()]]
                        //    .Controls.Add(divContainer);
                        //////////////////////////////////////////








                        Panel panel = new Panel();

                        panel.ID = unigid;
                        panel.Attributes["runat"] = "server";
                        panel.CssClass = "modalPopup";
                        panel.Attributes["Style"] = "display: none; position: relative; Height: 66%; Width: 33%; padding: 16px;";
                        panel.BackColor = Color.AliceBlue;




                        panel.BorderColor = Color.FromArgb(231,231,231);


                        HtmlGenericControl titleHeader = new HtmlGenericControl("h2");
                        //divHeader.Attributes["class"] = "header";
                        titleHeader.InnerText = lesson.Course.Name;

                        panel.Controls.Add(titleHeader);

                        HtmlGenericControl TitleLine = new HtmlGenericControl("hr");
                        panel.Controls.Add(TitleLine);



                        string str = "";
                        Common.AppendStringArray(ref str, ", ", lesson.Course.Teachers.Select(teac => teac.Name).ToArray());
                        panel.Controls.Add(new Label() {Text = str});


                        panel.Controls.Add(new HtmlGenericControl("br"));

                        Label dateLabel = new Label();
                        dateLabel.Text = lesson.DateTime.Date.ToLongDateString();
                        panel.Controls.Add(dateLabel);


                        panel.Controls.Add(new HtmlGenericControl("br"));

                        Label statusLabel = new Label();                      
                        if (lesson.Cancelled)
                        {
                            statusLabel.Text = "Status: Aflyst";
                        }
                        else
                        {
                            statusLabel.Text = lesson.Online ? "Status: Virtuel" : "Status: Standard";
                        }
                        panel.Controls.Add(statusLabel); 



                        HtmlGenericControl dateLine = new HtmlGenericControl("hr");
                        panel.Controls.Add(dateLine);


                        HtmlGenericControl descriptionHeader = new HtmlGenericControl("h4");
                        descriptionHeader.InnerText = "Beskrivelse";
                        panel.Controls.Add(descriptionHeader);


                        HtmlGenericControl divBody = new HtmlGenericControl("div");
                        divBody.Attributes["class"] = "body";
                        divBody.InnerText = lesson.Description;

                        HtmlGenericControl bodyLine = new HtmlGenericControl("hr");
                        divBody.Controls.Add(bodyLine);

                        HtmlGenericControl fileHeader = new HtmlGenericControl("h4");
                        fileHeader.InnerText = "Filer";
                        divBody.Controls.Add(fileHeader);


                        foreach (var filepath in lesson.Documents)
                        {
                            Button downloadButton = new Button { Text = Path.GetFileName(filepath) };
                            downloadButton.Attributes["class"] = "btn btn-warning";
                            downloadButton.Attributes["style"] = "display: inline-block;";
                            downloadButton.Attributes["path"] = filepath;
                            downloadButton.Click += DownloadButton_Click;
                            divBody.Controls.Add(downloadButton);
                        }

                        HtmlGenericControl footerLine = new HtmlGenericControl("hr");
                        divBody.Controls.Add(footerLine);


                        Button bnt = new Button();
                        bnt.ID = "btnCancel" + unigid;
                        bnt.Attributes["runat"] = "server";
                        bnt.Attributes["class"] = "btn btn-primary";
                        bnt.Text = "Luk";
                        bnt.Attributes["Style"] = "position: absolute; width: 15%; height: 6%; margin: 0 auto; left: auto; right: 2px; bottom: 2px";





                        divBody.Controls.Add(bnt);

                        panel.Controls.Add(divBody);




                        scheduleTable.Rows[GetRowNumber[timeslot]]
                            .Cells[GetCellNumber[lesson.DateTime.DayOfWeek.ToString()]]
                            .Controls.Add(button);

                        Panel1.Controls.Add(modalPop);

                        Panel1.Controls.Add(panel);


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


        protected void DownloadButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string path = button.Attributes["path"];
            string name = Path.GetFileName(path);
            try
            {
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + name);
                Response.TransmitFile(Server.MapPath(path));
                Response.End();
            }
            catch (Exception)
            {
                button.Text = "Fil forsvundet";
                button.Attributes["class"] = "btn btn-danger disabled";
                Assignment.RemoveDocument(path);
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


        protected void JumpWeekRight_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("skema.aspx?aar=" + year + "&uge=" + (Convert.ToInt32(week) + 1));
        }

        protected void JumpWeekLeft_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("skema.aspx?aar=" + year + "&uge=" + (Convert.ToInt32(week) - 1));
        }
    }
}