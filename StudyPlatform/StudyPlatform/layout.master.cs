using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using StudyPlatform.Classes.Model;

namespace StudyPlatform
{
    public partial class Layout : MasterPage
    {
        public string TitelLabelText
        {
            get { return TitelLabel.Text; }
            set { TitelLabel.Text = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
                Response.Redirect("Login.aspx");
            else if (Session["user"] is Student)
                SetupSidePanelContentsTable(StudentLinks);
            else if (Session["user"] is Teacher)
                SetupSidePanelContentsTable(TeacherLinks);
            else SetupSidePanelContentsTable(SecretaryLinks);
            NameLabel.Text = ((Person)Session["user"]).Name;
        }
        private void SetupSidePanelContentsTable(IEnumerable<Pair> links)
        {
            foreach (Pair link in links)
            {
                switch ((string)link.Second)
                {
                    case "Nyheder": AddExpandRow(link, NewsExpandTable); break;
                    case "Skema": AddExpandRow(link, ScheduleExpandTable); break;
                    case "Afleveringer": AddExpandRow(link, AssignmentsExpandTable); break;
                    case "Indbakke": AddExpandRow(link, RecievedMessagesExpandTable); break;
                    case "Udbakke": AddExpandRow(link, SentMessagesExpandTable); break;
                    default: AddGenericRow(link); break;
                }
            }
        }
        private void AddExpandRow(Pair link, Table expandTable)
        {
            TableRow row = new TableRow();
            TableCell linkCell = new TableCell();
            linkCell.Attributes.Add("class", "clickable sidepanel-table-linkcell");
            linkCell.Attributes.Add("onclick", "window.document.location='" + (string)link.First + "';");
            linkCell.Controls.Add(new HtmlGenericControl { InnerText = (string)link.Second + " " });
            linkCell.Controls.Add(IconsByLink[(string)link.First]);
            if (Request.RawUrl.Contains((string)link.First)) // Should probably only request rawurl once
                linkCell.Attributes.Add("style", "background: #75CAEB;");
            TableCell expandCell = new TableCell();
            expandCell.Attributes.Add("class", "clickable sidepanel-table-expandcell");
            expandCell.Attributes.Add("data-toggle", "collapse");
            expandCell.Attributes.Add("data-target", "#" + (string)link.Second + "accordion");
            HtmlGenericControl expandIcon = new HtmlGenericControl("i");
            expandIcon.Attributes.Add("class", "fa fa-chevron-down fa-2x");
            expandCell.Controls.Add(expandIcon);
            row.Cells.Add(linkCell);
            row.Cells.Add(expandCell);
            SidePanelContentsTable.Rows.Add(row);
            TableRow expandRow = new TableRow();
            TableCell expandRowContents = new TableCell { ColumnSpan = 2 };
            var accordion = new HtmlGenericControl("div");
            accordion.Attributes.Add("class", "collapse");
            accordion.Attributes.Add("id", (string)link.Second + "accordion");
            accordion.Controls.Add(expandTable);
            expandRowContents.Controls.Add(accordion);
            expandRow.Cells.Add(expandRowContents);
            SidePanelContentsTable.Rows.Add(expandRow);
        }
        private void AddGenericRow(Pair link)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Attributes.Add("class", "clickable sidepanel-table-linkcell");
            cell.Attributes.Add("onclick", "window.document.location='" + (string)link.First + "';");
            cell.Controls.Add(new HtmlGenericControl { InnerText = (string)link.Second + " " });
            cell.Controls.Add(IconsByLink[(string)link.First]);
            if (Request.RawUrl.Contains((string)link.First)) // Should probably only request rawurl once
                cell.Attributes.Add("style", "background: #75CAEB;");
            row.Cells.Add(cell);
            SidePanelContentsTable.Rows.Add(row);
        }
        private static Table NewsExpandTable
        {
            get
            {
                Table table = new Table();
                table.Attributes.Add("class", "sidepanel-table-inner table table-striped table-hover table-bordered");
                foreach (News news in News.GetAll().OrderByDescending(o => o.DateTimePublished)
                                                   .Take(3))
                {
                    TableRow row = new TableRow();
                    row.Attributes.Add("class", "clickable");
                    row.Attributes.Add("onclick", "window.document.location='/nyheder.aspx';");
                    row.Cells.Add(new TableCell { Text = news.Title });
                    row.Cells.Add(new TableCell { Text = GetTimeSince(news.DateTimePublished) });
                    row.Cells[0].Attributes.Add("class", "col-sm-8");
                    row.Cells[1].Attributes.Add("class", "col-sm-4");
                    table.Rows.Add(row);
                }
                return table;
            }
        }
        private Table ScheduleExpandTable
        {
            get
            {
                Table table = new Table();
                table.Attributes.Add("class", "sidepanel-table-inner table table-striped table-hover table-bordered");
                List<Course> courses = new List<Course>();
                if (Session["user"] is Student)
                    courses = ((Student) Session["user"]).Courses;
                else if (Session["user"] is Teacher)
                    courses = ((Teacher) Session["user"]).Courses;
                List<Lesson> lessons = new List<Lesson>();
                foreach (Course course in courses)
                    lessons.AddRange(course.Lessons.Where(l => l.DateTime > DateTime.Now));
                foreach (Lesson lesson in lessons.OrderBy(o => o.DateTime).Take(3))
                {
                    TableRow row = new TableRow();
                    row.Attributes.Add("class", "clickable");
                    row.Attributes.Add("onclick", "window.document.location='/skema.aspx';");
                    row.Cells.Add(new TableCell { Text = lesson.Course.Name });
                    row.Cells.Add(new TableCell { Text = GetTimeUntil(lesson.DateTime) });
                    row.Cells[0].Attributes.Add("class", "col-sm-8");
                    row.Cells[1].Attributes.Add("class", "col-sm-4");
                    table.Rows.Add(row);
                }
                if (lessons.Count == 0)
                {
                    
                }
                return table;
            }
        }
        private Table AssignmentsExpandTable
        {
            get
            {
                Table table = new Table();
                table.Attributes.Add("class", "sidepanel-table-inner table table-striped table-hover table-bordered");
                List<AssignmentDescription> assignmentDescriptions = new List<AssignmentDescription>();
                foreach (Course course in ((Student)Session["user"]).Courses)
                    assignmentDescriptions.AddRange(course.AssignmentDescriptions);
                foreach (AssignmentDescription assignmentDescription in
                         assignmentDescriptions.Where(ad => ad.HasExpired == false && 
                                                      ad.Assignments.All(a => a.Student.ID != ((Student) Session["user"]).ID))
                                               .OrderBy(o => o.Deadline)
                                               .Take(3))
                {
                    TableRow row = new TableRow();
                    row.Attributes.Add("class", "clickable");
                    row.Attributes.Add("onclick", "window.document.location='/afleveringer.aspx';");
                    row.Cells.Add(new TableCell { Text = assignmentDescription.Title });
                    row.Cells.Add(new TableCell { Text = GetTimeUntil(assignmentDescription.Deadline) });
                    row.Cells[0].Attributes.Add("class", "col-sm-8");
                    row.Cells[1].Attributes.Add("class", "col-sm-4");
                    table.Rows.Add(row);
                }
                return table;
            }
        }
        private Table RecievedMessagesExpandTable
        {
            get
            {
                Table table = new Table();
                table.Attributes.Add("class", "sidepanel-table-inner table table-striped table-hover table-bordered");
                foreach (Message message in ((Person)Session["user"]).RecievedMessages
                         .OrderByDescending(o => o.DateTimeSent).Take(3))
                {
                    TableRow row = new TableRow();
                    row.Attributes.Add("class", "clickable");
                    row.Attributes.Add("onclick", "window.document.location='/indbakke.aspx';");
                    row.Cells.Add(new TableCell { Text = message.Title });
                    row.Cells.Add(new TableCell { Text = GetTimeSince(message.DateTimeSent) });
                    row.Cells[0].Attributes.Add("class", "col-sm-8");
                    row.Cells[1].Attributes.Add("class", "col-sm-4");
                    table.Rows.Add(row);
                }
                return table;
            }
        }
        private Table SentMessagesExpandTable
        {
            get
            {
                Table table = new Table();
                table.Attributes.Add("class", "sidepanel-table-inner table table-striped table-hover table-bordered");
                foreach (Message message in ((Person)Session["user"]).SentMessages
                         .OrderByDescending(o => o.DateTimeSent).Take(3))
                {
                    TableRow row = new TableRow();
                    row.Attributes.Add("class", "clickable");
                    row.Attributes.Add("onclick", "window.document.location='/udbakke.aspx';");
                    row.Cells.Add(new TableCell { Text = message.Title });
                    row.Cells.Add(new TableCell { Text = GetTimeSince(message.DateTimeSent) });
                    row.Cells[0].Attributes.Add("class", "col-sm-8");
                    row.Cells[1].Attributes.Add("class", "col-sm-4");
                    table.Rows.Add(row);
                }
                return table;
            }
        }
        private static string GetTimeUntil(DateTime dateTime)
        {
            TimeSpan timeUntil = dateTime.Subtract(DateTime.Now);
            if (timeUntil.TotalDays >= 1)
                return "om " + (int)timeUntil.TotalDays + " dage";
            if (timeUntil.TotalHours >= 1)
                return "om " + (int)timeUntil.TotalHours + " timer";
            return "om " + (int)timeUntil.TotalMinutes + " minutter";
        }
        private static string GetTimeSince(DateTime dateTime)
        {
            TimeSpan timeSince = DateTime.Now.Subtract(dateTime);
            if (timeSince.TotalDays >= 1)
                return (int)timeSince.TotalDays + " dage siden";
            if (timeSince.TotalHours >= 1)
                return (int)timeSince.TotalHours + " timer siden";
            return (int)timeSince.TotalMinutes + " minutter siden";
        }
        private static readonly List<Pair> StudentLinks = new List<Pair>
        {
            new Pair("/nyheder.aspx", "Nyheder"),
            new Pair("/skema.aspx", "Skema"),
            new Pair("/kurser.aspx", "Kurser"),
            new Pair("/afleveringer.aspx", "Afleveringer"),
            new Pair("/karakterer.aspx", "Karakterer"),
            new Pair("/fravaer.aspx", "Fravær"),
            new Pair("/indbakke.aspx", "Indbakke"),
            new Pair("/udbakke.aspx", "Udbakke")
        };
        private static readonly List<Pair> TeacherLinks = new List<Pair>
        {
            new Pair("/nyheder.aspx", "Nyheder"),
            new Pair("/skema.aspx", "Skema"),
            new Pair("/kurser.aspx", "Kurser"),
            new Pair("/indbakke.aspx", "Indbakke"),
            new Pair("/udbakke.aspx", "Udbakke")
        };
        private static readonly List<Pair> SecretaryLinks = new List<Pair>
        {
            new Pair("/nyheder.aspx", "Nyheder"),
            new Pair("/kurser.aspx", "Kurser"),
            new Pair("/brugere.aspx", "Brugere"),
            new Pair("/indbakke.aspx", "Indbakke"),
            new Pair("/udbakke.aspx", "Udbakke")
        };
        private static readonly Dictionary<string, HtmlGenericControl> IconsByLink = new Dictionary<string, HtmlGenericControl>
        {
            { "/nyheder.aspx", CreateIcon("fa-newspaper-o") },
            { "/skema.aspx", CreateIcon("fa-calendar") },
            { "/kurser.aspx", CreateIcon("fa-book") },
            { "/afleveringer.aspx", CreateIcon("fa-pencil-square-o") },
            { "/karakterer.aspx", CreateIcon("fa-graduation-cap") },
            { "/fravaer.aspx", CreateIcon("fa-area-chart") },
            { "/brugere.aspx", CreateIcon("fa-users") },
            { "/indbakke.aspx", CreateIcon("fa-envelope") },
            { "/udbakke.aspx", CreateIcon("fa-envelope-open") }
        };
        private static HtmlGenericControl CreateIcon(string icon)
        {
            HtmlGenericControl iconControl = new HtmlGenericControl("i");
            iconControl.Attributes.Add("class", "fa " + icon);
            return iconControl;
        }
    }
}