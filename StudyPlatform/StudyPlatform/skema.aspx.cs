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
using StudyPlatform.Classes;

namespace StudyPlatform
{
    public partial class Skema : Page
    {
        private static readonly string[] TimeSlots = { "08:10", "09:05", "10:00", "10:55", "12:05", "13:00", "13:55", "14:50" };
        private string _year;
        private string _week;
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
            { "Historie B", "#7D1615" }
        };
        private static readonly Dictionary<string, int> GetCellNumber = new Dictionary<string, int>
        {
            { "Monday", 1 },
            { "Tuesday", 2 },
            { "Wednesday", 3 },
            { "Thursday", 4 },
            { "Friday", 5 }
        };
        private static readonly Dictionary<string, int> GetRowNumber = new Dictionary<string, int>
        {
            { "08:10", 1 },
            { "09:05", 4 },
            { "10:00", 7 },
            { "10:55", 10 },
            { "12:05", 13 },
            { "13:00", 16 },
            { "13:55", 19 },
            { "14:50", 22 }
        };
        readonly Func<string, string> SomeDynamicMessage = s => {
            var val = s;
            val = val.Replace("{", "<span class='bold-token'>{");
            val = val.Replace("}", "}</span>");
            return val;
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["user"] is Student || Session["user"] is Teacher))
                Response.Redirect("login.aspx");
            if (_year != null && _week != null)
                RedirectToCurrentWeek();
            
            if (!Page.IsPostBack)
            {
                Master.TitelLabelText = "Skema";
                _year = Request.QueryString["aar"];
                _week = Request.QueryString["uge"];

                if (Convert.ToInt32(_week) < 1 || Convert.ToInt32(_week) > 53 ||
                    Convert.ToInt32(_year) < 1 || Convert.ToInt32(_year) > 9999)
                {
                    RedirectToCurrentWeek();
                }
                if (Convert.ToInt32(_week) < 10)
                {
                    datepickerinut.Value = _year + "-W0" + _week;
                }
                else
                {
                    datepickerinut.Value = _year + "-W" + _week;
                }
                SetupWeekButtons();

                Person user = (Person)Session["user"];
                int weekNumber = Convert.ToInt32(Request.QueryString["uge"]);

                SortedList<string, List<Lesson>> sortedList = new SortedList<string, List<Lesson>>();

                if (Session["user"] is Student)
                {
                    List<Course> courses = Student.GetByID(Convert.ToUInt32(user.ID)).Courses;
                    sortedList = FindAndSortLessonsForPerson(courses, weekNumber);
                }
                else
                {
                    List<Course> courses = Teacher.GetByID(Convert.ToUInt32(user.ID)).Courses;
                    sortedList = FindAndSortLessonsForPerson(courses, weekNumber);
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

                // Table Headers
                CreateTableHeadersText();

                // Creating TableCells with button for each Day in each TimeSlot.
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
                        // TableCell & Button & PopupContent layout 
                        CreateTableCellContent(lesson, timeslot);
                    }
                }
            }
            else // PostBack
            {
                if (datepickerinut.Value.Length > 7)
                {
                    string str = datepickerinut.Value;
                    _year = new string(str.Take(4).ToArray());
                    _week = str.Substring(str.Length - 2);

                    Response.Redirect("skema.aspx?aar=" + _year + "&uge=" + _week);
                }
                else
                {
                    RedirectToCurrentWeek();
                }

            }
        }
        private void CreateTableHeadersText()
        {
            DateTime weekDateTime = GetFirstDateOfWeek(Convert.ToInt32(_year), Convert.ToInt32(_week));
            tableHeaderCellMonday.Text = "Mandag(" + weekDateTime.ToShortDateString() + ")";
            tableHeaderCellTuesday.Text = "Tirsdag(" + weekDateTime.AddDays(1).ToShortDateString() + ")";
            tableHeaderCellWednesday.Text = "Onsdag(" + weekDateTime.AddDays(2).ToShortDateString() + ")";
            tableHeaderCellThursday.Text = "Torsdag(" + weekDateTime.AddDays(3).ToShortDateString() + ")";
            tableHeaderCellFriday.Text = "Fredag(" + weekDateTime.AddDays(4).ToShortDateString() + ")";
        }
        private void RedirectToCurrentWeek()
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            System.Globalization.Calendar cal = dfi.Calendar;
            int currentWeek = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            int currentYear = cal.GetYear(DateTime.Now);
            Response.Redirect("skema.aspx?aar=" + currentYear + "&uge=" + currentWeek);
        }
        private void CreateTableCellContent(Lesson lesson, string timeslot)
        {
            // TableCell Button Layout
            Button button = new Button();

            string lessonButtonName = lesson.Course.Name + " - " + lesson.Rooms[0].Name;
            button.Text = lessonButtonName;

            button.Attributes.Add("Style", "border:none; position: absolute; width: 100%; height: 100%; " +
                                           "margin: 0 auto; left: auto; right: auto; background: " +
                                           GetCourseColor[lesson.Course.Name] + "; color: White;");

            string buttonUniqueId = Guid.NewGuid().ToString();
            button.ID = buttonUniqueId;


            // Add File Icon if lesson contain Doucuments
            if (lesson.Documents.Count > 0)
            {
                Panel p = new Panel();
                p.Attributes["runat"] = "server";

                HtmlGenericControl divContainer = new HtmlGenericControl("div");
                divContainer.Attributes["style"] = "position: absolute; z-index: 999; right: 5px; bottom: 3px; color: white;";

                HtmlGenericControl iContainer = new HtmlGenericControl("span");
                iContainer.Attributes["class"] = "fa fa-file-text";

                divContainer.Controls.Add(iContainer);
                p.Controls.Add(divContainer);
                scheduleTable.Rows[GetRowNumber[timeslot]]
                    .Cells[GetCellNumber[lesson.DateTime.DayOfWeek.ToString()]]
                    .Controls.Add(p);
            }

            // Popup Panel Layout
            Panel panel = new Panel();

            string panelUniqueId = Guid.NewGuid().ToString();
            panel.ID = panelUniqueId;
            panel.Attributes["runat"] = "server";
            panel.CssClass = "modalPopup";
            panel.Attributes["Style"] = "display: none; position: relative; Height: 66%; Width: 33%; padding: 16px; border-radius: 10px; box-shadow: 0px 0px 50px;";
            panel.BackColor = Color.FromArgb(249, 249, 249);
            panel.BorderColor = Color.FromArgb(231, 231, 231);

            // HTML For Popup Content
            HtmlGenericControl titleHeader = new HtmlGenericControl("h2");
            titleHeader.InnerText = lesson.Course.Name;

            panel.Controls.Add(titleHeader);

            HtmlGenericControl TitleLine = new HtmlGenericControl("hr");
            panel.Controls.Add(TitleLine);

            string str = "";
            string input = "";
            Common.AppendStringArray(ref str, ", ", lesson.Course.Teachers.Select(x => x.Name).ToArray());
            input = @"{0}" + str;
            panel.Controls.Add(new Label() { Text = string.Format(SomeDynamicMessage(input), "Lærer: " )});

            panel.Controls.Add(new HtmlGenericControl("br"));

            Label dateLabel = new Label();
            input = @"{0}" + lesson.DateTime.Date.ToLongDateString();
            dateLabel.Text = string.Format(SomeDynamicMessage(input), "Dato: ");
            panel.Controls.Add(dateLabel);

            panel.Controls.Add(new HtmlGenericControl("br"));

            Label statusLabel = new Label();
            if (lesson.Cancelled)
            {
                input = @"{0}" + "Aflyst";
                statusLabel.Text = string.Format(SomeDynamicMessage(input), "Status: ");
            }
            else
            {
                input = lesson.Online ? @"{0}" + "Virtuel" : @"{0}" + "Standard:";
                statusLabel.Text = string.Format(SomeDynamicMessage(input), "Status: ");
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

            // Content Close Button Layout
            Button bnt = new Button();
            bnt.ID = "btnCancel" + panelUniqueId;
            bnt.Attributes["runat"] = "server";
            bnt.Attributes["class"] = "btn btn-primary";
            bnt.Text = "Luk";
            bnt.Attributes["Style"] = "position: absolute; width: 15%; height: 6%; " +
                                      "margin: 0 auto; left: auto; right: 10px; bottom: 10px";

            divBody.Controls.Add(bnt);
            panel.Controls.Add(divBody);
            scheduleTable.Rows[GetRowNumber[timeslot]]
                .Cells[GetCellNumber[lesson.DateTime.DayOfWeek.ToString()]]
                .Controls.Add(button);


            // Modal PopupExtender setup
            ModalPopupExtender modalPop = new ModalPopupExtender();
            modalPop.ID = "popUp" + button.ID;
            modalPop.PopupControlID = panelUniqueId;
            modalPop.TargetControlID = button.ID;
            modalPop.DropShadow = false;
            modalPop.CancelControlID = "btnCancel" + panelUniqueId;
            modalPop.BackgroundCssClass = "modalBackground";


            //modalPop.BehaviorID = "MPE" + modalPop.ID;

            Panel1.Controls.Add(modalPop);
            Panel1.Controls.Add(panel);
        }
        private static DateTime GetFirstDateOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }
        private SortedList<string, List<Lesson>> FindAndSortLessonsForPerson(List<Course> courses, int weekNumber)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            System.Globalization.Calendar cal = dfi.Calendar;
            List<Lesson> lessons = (from course in courses from lesson in course.Lessons
                                    where cal.GetWeekOfYear(lesson.DateTime, dfi.CalendarWeekRule, dfi.FirstDayOfWeek) == 
                                    weekNumber && lesson.DateTime.Year == Convert.ToInt32(_year) select lesson).ToList();
            List<Lesson> sortedList = lessons.OrderBy(x => x.DateTime.TimeOfDay).ToList();
            List<Lesson> lesson1List = new List<Lesson>();
            List<Lesson> lesson2List = new List<Lesson>();
            List<Lesson> lesson3List = new List<Lesson>();
            List<Lesson> lesson4List = new List<Lesson>();
            List<Lesson> lesson5List = new List<Lesson>();
            List<Lesson> lesson6List = new List<Lesson>();
            List<Lesson> lesson7List = new List<Lesson>();
            List<Lesson> lesson8List = new List<Lesson>();
            SortedList<string, List<Lesson>> coollist = new SortedList<string, List<Lesson>>
            {
                {"08:10", lesson1List},
                {"09:05", lesson2List},
                {"10:00", lesson3List},
                {"10:55", lesson4List},
                {"12:05", lesson5List},
                {"13:00", lesson6List},
                {"13:55", lesson7List},
                {"14:50", lesson8List}
            };
            foreach (Lesson lesson in sortedList)
            {
                if (lesson.DateTime.TimeOfDay == new TimeSpan(8, 10, 0))
                {
                    lesson1List.Add(lesson);
                }
                else if (lesson.DateTime.TimeOfDay == new TimeSpan(9, 5, 0))
                {
                    lesson2List.Add(lesson);
                }
                else if (lesson.DateTime.TimeOfDay == new TimeSpan(10, 0, 0))
                {
                    lesson3List.Add(lesson);
                }
                else if (lesson.DateTime.TimeOfDay == new TimeSpan(10, 55, 0))
                {
                    lesson4List.Add(lesson);
                }
                else if (lesson.DateTime.TimeOfDay == new TimeSpan(12, 5, 0))
                {
                    lesson5List.Add(lesson);
                }
                else if (lesson.DateTime.TimeOfDay == new TimeSpan(13, 0, 0))
                {
                    lesson6List.Add(lesson);
                }
                else if (lesson.DateTime.TimeOfDay == new TimeSpan(13, 55, 0))
                {
                    lesson7List.Add(lesson);
                }
                else if (lesson.DateTime.TimeOfDay == new TimeSpan(14, 50, 0))
                {
                    lesson8List.Add(lesson);
                }
            }
            return coollist;
        }
        private void SetupWeekButtons()
        {
            switch (_week)
            {
                case "52":
                    WeekForward.NavigateUrl = "/skema.aspx?aar=" + IncrementYearString() + "&uge=1";
                    WeekBack.NavigateUrl = "/skema.aspx?aar=" + _year + "&uge=" + DecrementWeekString();
                    break;
                case "1":
                    WeekForward.NavigateUrl = "/skema.aspx?aar=" + _year + "&uge=" + IncrementWeekString();
                    WeekBack.NavigateUrl = "/skema.aspx?aar=" + DecrementYearString() + "&uge=52";
                    break;
                default:
                    WeekForward.NavigateUrl = "/skema.aspx?aar=" + _year + "&uge=" + IncrementWeekString();
                    WeekBack.NavigateUrl = "/skema.aspx?aar=" + _year + "&uge=" + DecrementWeekString();
                    break;
            }
        }
        private string IncrementYearString() => (Convert.ToInt32(_year) + 1).ToString();
        private string DecrementYearString() => (Convert.ToInt32(_year) - 1).ToString();
        private string IncrementWeekString() => (Convert.ToInt32(_week) + 1).ToString();
        private string DecrementWeekString() => (Convert.ToInt32(_week) - 1).ToString();
        protected void DownloadButton_Click(object sender, EventArgs e) => 
            WebHelper.DownloadFile(sender as LinkButton, this);
    }
}