using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using StudyPlatform.Classes;
using StudyPlatform.Classes.Model;

namespace StudyPlatform
{
    public partial class Kurser : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] is Student)
                SetupStudent();
            else if (Session["user"] is Teacher)
                SetupTeacher();
            else SetupSecretary();
        }
        private void SetupStudent()
        {
            string courseParameter = Request.QueryString["kursus"];
            if (courseParameter == null)
            {
                Master.TitelLabelText = "Kurser";
                DisablePanels(CoursePanel);
                SetupCourseSelection(Session["user"] as Student);
            }
            else
            {
                try
                {
                    Course course = Course.GetByID(Convert.ToUInt32(courseParameter));
                    Master.TitelLabelText = "Kurser <small class=\"text-muted\"> / " + course.Name + "</text>";
                    DisablePanels(ActiveCoursesPanel, InactiveCoursesPanel);
                    SetupCourseView(course);
                }
                catch (Exception) { Response.Redirect("kurser.aspx"); }
            }
        }
        private void SetupTeacher()
        {
            string courseParameter = Request.QueryString["kursus"];
            if (courseParameter == null)
            {
                Master.TitelLabelText = "Kurser";
                DisablePanels(CoursePanel);
                SetupCourseSelection(Session["user"] as Teacher);
            }
            else
            {
                try
                {
                    Course course = Course.GetByID(Convert.ToUInt32(courseParameter));
                    Master.TitelLabelText = "Kurser <small class=\"text-muted\"> / " + course.Name + "</text>";
                    DisablePanels(ActiveCoursesPanel, InactiveCoursesPanel);
                    SetupCourseEdit(course);
                }
                catch (Exception) { Response.Redirect("kurser.aspx"); }
            }
        }
        private void SetupSecretary()
        {
            string courseParameter = Request.QueryString["kursus"];
            if (courseParameter == null)
            {
                Master.TitelLabelText = "Kurser";
                DisablePanels(CoursePanel);
                SetupCourseSelectionEdit(Session["user"] as Secretary);
            }
            else
            {
                try
                {
                    Course course = Course.GetByID(Convert.ToUInt32(courseParameter));
                    Master.TitelLabelText = "Kurser <small class=\"text-muted\"> / " + course.Name + "</text>";
                    DisablePanels(ActiveCoursesPanel, InactiveCoursesPanel);
                    SetupCourseEdit(course);
                }
                catch (Exception) { Response.Redirect("kurser.aspx"); }
            }
        }
        private void SetupCourseSelection(Student student)
        {
            DateTime now = DateTime.Now;
            List<Course> activeCourses = new List<Course>();
            List<Course> inactiveCourses = new List<Course>();
            foreach (Course course in student.Courses)
            {
                bool isActive = course.Lessons.Any(lesson => lesson.DateTime > now);
                if (isActive) activeCourses.Add(course);
                else inactiveCourses.Add(course);
            }
            SetupCoursesPanel(ActiveCoursesPanel, "Aktive kurser", activeCourses, "btn btn-primary");
            SetupCoursesPanel(InactiveCoursesPanel, "Inaktive kurser", inactiveCourses, "btn btn-default");
        }
        private void SetupCourseSelection(Teacher teacher)
        {
            DateTime now = DateTime.Now;
            List<Course> activeCourses = new List<Course>();
            List<Course> inactiveCourses = new List<Course>();
            foreach (Course course in teacher.Courses)
            {
                bool isActive = course.Lessons.Any(lesson => lesson.DateTime > now);
                if (isActive) activeCourses.Add(course);
                else inactiveCourses.Add(course);
            }
            SetupCoursesPanel(ActiveCoursesPanel, "Aktive kurser", activeCourses, "btn btn-primary");
            SetupCoursesPanel(InactiveCoursesPanel, "Inaktive kurser", inactiveCourses, "btn btn-default");
        }
        private static void SetupCoursesPanel(Panel panel, string headerText, List<Course> courses, string buttonClass)
        {
            HtmlGenericControl header = new HtmlGenericControl("h4") { InnerText = headerText };
            panel.Controls.Add(header);
            HtmlGenericControl buttonGroup = new HtmlGenericControl("div");
            buttonGroup.Attributes["class"] = "btn-group-vertical col-sm-12";
            foreach (Course course in courses)
            {
                HyperLink linkButton = new HyperLink
                {
                    Text = course.Name,
                    NavigateUrl = "/kurser.aspx?kursus=" + course.ID + "&fane=lektioner"
                };
                linkButton.Attributes["class"] = buttonClass;
                buttonGroup.Controls.Add(linkButton);
            }
            panel.Controls.Add(buttonGroup);
        }
        private static void SetupCourseSelectionEdit(Secretary secretary)
        {
            
        }
        private void SetupCourseView(Course course)
        {
            string tabString = Request.QueryString["fane"];
            if (tabString == null) 
                Response.Redirect("/kurser.aspx?kursus=" + course.ID + "&fane=lektioner");
            // Back Button
            HtmlGenericControl formGroup = new HtmlGenericControl("div");
            formGroup.Attributes["class"] = "form-group";
            Button backButton = new Button { Text = "Tilbage til kurser" };
            backButton.Attributes["class"] = "btn btn-default";
            backButton.Click += BackButton_Click;
            formGroup.Controls.Add(backButton);
            CoursePanel.Controls.Add(formGroup);
            // Header
            HtmlGenericControl jumbotron = new HtmlGenericControl("div");
            jumbotron.Attributes["class"] = "jumbotron col-sm-12";
            jumbotron.Controls.Add(new HtmlGenericControl("h2") { InnerText = course.Name });
            jumbotron.Controls.Add(new HtmlGenericControl("p") { InnerText = course.Description });
            CoursePanel.Controls.Add(jumbotron);
            // Tab Menu
            HtmlGenericControl tabMenu = new HtmlGenericControl("ul");
            tabMenu.Attributes["class"] = "nav nav-tabs";
            HtmlGenericControl lessonsTab = new HtmlGenericControl("li") { InnerHtml = 
                "<a href=\"/kurser.aspx?kursus=" + course.ID + "&fane=lektioner\">Lektioner</a>" };
            HtmlGenericControl assignmentDescriptionsTab = new HtmlGenericControl("li") { InnerHtml = 
                "<a href=\"/kurser.aspx?kursus=" + course.ID + "&fane=afleveringsbeskrivelser\">Afleveringsbeskrivelser</a>" };
            HtmlGenericControl documentsTab = new HtmlGenericControl("li") { InnerHtml = 
                "<a href=\"/kurser.aspx?kursus=" + course.ID + "&fane=dokumenter\">Dokumenter</a>" };
            HtmlGenericControl studentsTab = new HtmlGenericControl("li") { InnerHtml = 
                "<a href=\"/kurser.aspx?kursus=" + course.ID + "&fane=kursister\">Kursister</a>" };
            HtmlGenericControl teachersTab = new HtmlGenericControl("li") { InnerHtml = 
                "<a href=\"/kurser.aspx?kursus=" + course.ID + "&fane=laerer\">Lærer</a>" };
            switch (tabString)
            {
                case "lektioner": lessonsTab.Attributes["class"] = "active"; break;
                case "afleveringsbeskrivelser": assignmentDescriptionsTab.Attributes["class"] = "active"; break;
                case "dokumenter": documentsTab.Attributes["class"] = "active"; break;
                case "kursister": studentsTab.Attributes["class"] = "active"; break;
                case "laerer": teachersTab.Attributes["class"] = "active"; break;
                default: Response.Redirect("/kurser.aspx?kursus=" + course.ID + "&fane=lektioner"); break;
            }
            tabMenu.Controls.Add(lessonsTab);
            tabMenu.Controls.Add(assignmentDescriptionsTab);
            tabMenu.Controls.Add(documentsTab);
            tabMenu.Controls.Add(studentsTab);
            tabMenu.Controls.Add(teachersTab);
            CoursePanel.Controls.Add(tabMenu);
            // Tab Content Start
            HtmlGenericControl tabContent = new HtmlGenericControl("div");
            tabContent.Attributes["class"] = "tab-content";
            // Lessons Tab
            HtmlGenericControl lessonsTabContent = new HtmlGenericControl("div");
            lessonsTabContent.Attributes["class"] = tabString == "lektioner" ? "tab-pane active" : "tab-pane";
            Table lessonsTable = new Table { CssClass = "table table-striped table-hover table-bordered" };
            TableHeaderRow lessonsTableHeaderRow = new TableHeaderRow();
            lessonsTableHeaderRow.Cells.Add(new TableCell { Text = "Lokaler" });
            lessonsTableHeaderRow.Cells.Add(new TableCell { Text = "Dato" });
            lessonsTableHeaderRow.Cells.Add(new TableCell { Text = "Online" });
            lessonsTableHeaderRow.Cells.Add(new TableCell { Text = "Aflyst" });
            lessonsTableHeaderRow.Cells.Add(new TableCell { Text = "Dokumenter" });
            lessonsTableHeaderRow.Cells.Add(new TableCell { Text = "Beskrivelse" });
            lessonsTable.Rows.Add(lessonsTableHeaderRow);
            foreach (Lesson lesson in course.Lessons)
            {
                TableRow lessonRow = new TableRow();
                if (lesson.Documents.Count > 0 || lesson.Description.Length > 0)
                {
                    lessonRow.Attributes["class"] = "clickable";
                    lessonRow.Attributes["data-toggle"] = "collapse";
                    lessonRow.Attributes["data-target"] = "#lessonaccordion" + lesson.ID;
                }
                string[] roomNames = lesson.Rooms.Select(room => room.Name).ToArray();
                string rooms = "";
                Common.AppendStringArray(ref rooms, ", ", roomNames);
                lessonRow.Cells.Add(new TableCell { Text = rooms});
                lessonRow.Cells.Add(new TableCell { Text = lesson.DateTime.ToString() });
                lessonRow.Cells.Add(new TableCell { Text = lesson.Online ? "Ja" : "Nej" });
                lessonRow.Cells.Add(new TableCell { Text = lesson.Cancelled ? "Ja" : "Nej" });
                lessonRow.Cells.Add(new TableCell { Text = lesson.Documents.Count > 0 ? "Ja" : "Nej" });
                lessonRow.Cells.Add(new TableCell { Text = lesson.Description.Length > 0 ? "Ja" : "Nej" });
                lessonsTable.Rows.Add(lessonRow);
                if (lesson.Documents.Count > 0 || lesson.Description.Length > 0)
                {
                    TableRow lessonDetailsRow = new TableRow();
                    TableCell lessonDetailsCell = new TableCell { ColumnSpan = 6 };
                    var accordion = new HtmlGenericControl("div");
                    accordion.Attributes["class"] = "collapse";
                    accordion.Attributes["id"] = "lessonaccordion" + lesson.ID;
                    if (lesson.Description.Length > 0)
                    {
                        HtmlGenericControl descriptionHeader = new HtmlGenericControl("h4") {InnerText = "Beskrivelse"};
                        accordion.Controls.Add(descriptionHeader);
                        HtmlGenericControl description = new HtmlGenericControl("p") {InnerText = lesson.Description};
                        accordion.Controls.Add(description);
                    }
                    if (lesson.Documents.Count > 0)
                    {
                        HtmlGenericControl documentHeader = new HtmlGenericControl("h4") { InnerText = "Dokumenter" };
                        accordion.Controls.Add(documentHeader);
                        HtmlGenericControl container = new HtmlGenericControl("div");
                        container.Attributes["class"] = "container";
                        Table lessonDocumentsTable = new Table { CssClass = "table table-striped table-hover table-bordered" };
                        TableHeaderRow lessonDocumentsTableHeaderRow = new TableHeaderRow();
                        lessonDocumentsTableHeaderRow.Cells.Add(new TableCell { Text = "Dokument" });
                        lessonDocumentsTableHeaderRow.Cells.Add(new TableCell { Text = "Download" });
                        lessonDocumentsTable.Rows.Add(lessonDocumentsTableHeaderRow);
                        foreach (string document in lesson.Documents)
                        {
                            TableRow documentRow = new TableRow();
                            documentRow.Cells.Add(new TableCell { Text = document });
                            TableCell downloadCell = new TableCell();
                            Button downloadButton = new Button { Text = "Download" };
                            downloadButton.Attributes["class"] = "btn btn-info";
                            downloadButton.Attributes["path"] = document;
                            downloadButton.Click += DownloadButton_Click;
                            downloadCell.Controls.Add(downloadButton);
                            documentRow.Cells.Add(downloadCell);
                            lessonDocumentsTable.Rows.Add(documentRow);
                        }
                        container.Controls.Add(lessonDocumentsTable);
                        accordion.Controls.Add(container);
                    }
                    lessonDetailsCell.Controls.Add(accordion);
                    lessonDetailsRow.Cells.Add(lessonDetailsCell);
                    lessonsTable.Rows.Add(lessonDetailsRow);
                }
            }
            lessonsTabContent.Controls.Add(lessonsTable);
            tabContent.Controls.Add(lessonsTabContent);
            // Assignment Descriptions Tab
            HtmlGenericControl assignmentDescriptionsTabContent = new HtmlGenericControl("div");
            assignmentDescriptionsTabContent.Attributes["class"] = tabString == "afleveringsbeskrivelser" ? "tab-pane active" : "tab-pane";
            Table assignmentDescriptionsTable = new Table { CssClass = "table table-striped table-hover table-bordered" };
            TableHeaderRow assignmentDescriptionsTableHeaderRow = new TableHeaderRow();
            assignmentDescriptionsTableHeaderRow.Cells.Add(new TableCell { Text = "Deadline" });
            assignmentDescriptionsTableHeaderRow.Cells.Add(new TableCell { Text = "Beskrivelse" });
            assignmentDescriptionsTableHeaderRow.Cells.Add(new TableCell { Text = "Dokumenter" });
            assignmentDescriptionsTableHeaderRow.Cells.Add(new TableCell { Text = "Du har afleveret" });
            assignmentDescriptionsTable.Rows.Add(assignmentDescriptionsTableHeaderRow);
            foreach (AssignmentDescription assignmentDescription in course.AssignmentDescriptions)
            {
                TableRow assignmentDescriptionRow = new TableRow();
                if (assignmentDescription.Documents.Count > 0 || assignmentDescription.Description.Length > 0)
                {
                    assignmentDescriptionRow.Attributes["class"] = "clickable";
                    assignmentDescriptionRow.Attributes["data-toggle"] = "collapse";
                    assignmentDescriptionRow.Attributes["data-target"] = "#assignmentdescriptionaccordion" + assignmentDescription.ID;
                }
                assignmentDescriptionRow.Cells.Add(new TableCell { Text = assignmentDescription.Deadline.ToString() });
                assignmentDescriptionRow.Cells.Add(new TableCell { Text = assignmentDescription.Description.Length > 0 ? "Ja" : "Nej" });
                assignmentDescriptionRow.Cells.Add(new TableCell { Text = assignmentDescription.Documents.Count > 0 ? "Ja" : "Nej" });
                assignmentDescriptionRow.Cells.Add(new TableCell { Text = assignmentDescription.Assignments
                    .Any(assignment => assignment.Student.ID == ((Person)Session["user"]).ID) ? "Ja" : "Nej" });
                assignmentDescriptionsTable.Rows.Add(assignmentDescriptionRow);
                if (assignmentDescription.Documents.Count > 0 || assignmentDescription.Description.Length > 0)
                {
                    TableRow assignmentDescriptionDetailsRow = new TableRow();
                    TableCell assignmentDescriptionDetailsCell = new TableCell { ColumnSpan = 6 };
                    var accordion = new HtmlGenericControl("div");
                    accordion.Attributes["class"] = "collapse";
                    accordion.Attributes["id"] = "assignmentdescriptionaccordion" + assignmentDescription.ID;
                    if (assignmentDescription.Description.Length > 0)
                    {
                        HtmlGenericControl descriptionHeader = new HtmlGenericControl("h4") { InnerText = "Beskrivelse" };
                        accordion.Controls.Add(descriptionHeader);
                        HtmlGenericControl description = new HtmlGenericControl("p") { InnerText = assignmentDescription.Description };
                        accordion.Controls.Add(description);
                    }
                    if (assignmentDescription.Documents.Count > 0)
                    {
                        HtmlGenericControl documentHeader = new HtmlGenericControl("h4") { InnerText = "Dokumenter" };
                        accordion.Controls.Add(documentHeader);
                        HtmlGenericControl container = new HtmlGenericControl("div");
                        container.Attributes["class"] = "container";
                        Table assignmentDescriptionDocumentsTable = new Table { CssClass = "table table-striped table-hover table-bordered" };
                        TableHeaderRow assignmentDescriptionDocumentsTableHeaderRow = new TableHeaderRow();
                        assignmentDescriptionDocumentsTableHeaderRow.Cells.Add(new TableCell { Text = "Dokument" });
                        assignmentDescriptionDocumentsTableHeaderRow.Cells.Add(new TableCell { Text = "Download" });
                        assignmentDescriptionDocumentsTable.Rows.Add(assignmentDescriptionDocumentsTableHeaderRow);
                        foreach (string document in assignmentDescription.Documents)
                        {
                            TableRow documentRow = new TableRow();
                            documentRow.Cells.Add(new TableCell { Text = document });
                            TableCell downloadCell = new TableCell();
                            Button downloadButton = new Button { Text = "Download" };
                            downloadButton.Attributes["class"] = "btn btn-info";
                            downloadButton.Attributes["path"] = document;
                            downloadButton.Click += DownloadButton_Click;
                            downloadCell.Controls.Add(downloadButton);
                            documentRow.Cells.Add(downloadCell);
                            assignmentDescriptionDocumentsTable.Rows.Add(documentRow);
                        }
                        container.Controls.Add(assignmentDescriptionDocumentsTable);
                        accordion.Controls.Add(container);
                    }
                    assignmentDescriptionDetailsCell.Controls.Add(accordion);
                    assignmentDescriptionDetailsRow.Cells.Add(assignmentDescriptionDetailsCell);
                    assignmentDescriptionsTable.Rows.Add(assignmentDescriptionDetailsRow);
                }
            }
            assignmentDescriptionsTabContent.Controls.Add(assignmentDescriptionsTable);
            tabContent.Controls.Add(assignmentDescriptionsTabContent);
            // Documents Tab
            HtmlGenericControl documentsTabContent = new HtmlGenericControl("div");
            documentsTabContent.Attributes["class"] = tabString == "dokumenter" ? "tab-pane active" : "tab-pane";
            Table documentsTable = new Table { CssClass = "table table-striped table-hover table-bordered" };
            TableHeaderRow documentsTableHeaderRow = new TableHeaderRow();
            documentsTableHeaderRow.Cells.Add(new TableCell { Text = "Dokument" });
            documentsTableHeaderRow.Cells.Add(new TableCell { Text = "Download" });
            documentsTable.Rows.Add(documentsTableHeaderRow);
            foreach (string document in course.Documents)
            {
                TableRow documentRow = new TableRow();
                documentRow.Cells.Add(new TableCell { Text = document });
                TableCell downloadCell = new TableCell();
                Button downloadButton = new Button { Text = "Download" };
                downloadButton.Attributes["class"] = "btn btn-info";
                downloadButton.Attributes["path"] = document;
                downloadButton.Click += DownloadButton_Click;
                downloadCell.Controls.Add(downloadButton);
                documentRow.Cells.Add(downloadCell);
                documentsTable.Rows.Add(documentRow);
            }
            documentsTabContent.Controls.Add(documentsTable);
            tabContent.Controls.Add(documentsTabContent);
            // Students Tab
            HtmlGenericControl studentsTabContent = new HtmlGenericControl("div");
            studentsTabContent.Attributes["class"] = tabString == "kursister" ? "tab-pane active" : "tab-pane";
            Table studentsTable = new Table { CssClass = "table table-striped table-hover table-bordered" };
            TableHeaderRow studentsTableHeaderRow = new TableHeaderRow();
            studentsTableHeaderRow.Cells.Add(new TableCell { Text = "Navn" });
            studentsTable.Rows.Add(studentsTableHeaderRow);
            foreach (Student student in course.Students)
            {
                TableRow studentRow = new TableRow();
                studentRow.Cells.Add(new TableCell { Text = student.Name });
                studentsTable.Rows.Add(studentRow);
            }
            studentsTabContent.Controls.Add(studentsTable);
            tabContent.Controls.Add(studentsTabContent);
            // Teachers Tab
            HtmlGenericControl teachersTabContent = new HtmlGenericControl("div");
            teachersTabContent.Attributes["class"] = tabString == "laerer" ? "tab-pane active" : "tab-pane";
            Table teachersTable = new Table { CssClass = "table table-striped table-hover table-bordered" };
            TableHeaderRow teachersTableHeaderRow = new TableHeaderRow();
            teachersTableHeaderRow.Cells.Add(new TableCell { Text = "Navn" });
            teachersTable.Rows.Add(teachersTableHeaderRow);
            foreach (Teacher teacher in course.Teachers)
            {
                TableRow teacherRow = new TableRow();
                teacherRow.Cells.Add(new TableCell { Text = teacher.Name });
                teachersTable.Rows.Add(teacherRow);
            }
            teachersTabContent.Controls.Add(teachersTable);
            tabContent.Controls.Add(teachersTabContent);
            // Tab Content End
            CoursePanel.Controls.Add(tabContent);
        }
        private static void SetupCourseEdit(Course course)
        {
            
        }
        private static bool IsPersonInCourse(Person person, Course course) => 
            course.Students.Any(student => person.ID == student.ID) || 
            course.Teachers.Any(teacher => person.ID == teacher.ID);
        private static void DisablePanels(params Panel[] panels)
        {
            foreach (Panel panel in panels)
                panel.Visible = false;
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("kurser.aspx");
        }
        protected void DownloadButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}