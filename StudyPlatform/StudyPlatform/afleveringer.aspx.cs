using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using StudyPlatform.Classes;
using StudyPlatform.Classes.Exceptions;
using StudyPlatform.Classes.Model;

namespace StudyPlatform
{
    public partial class Afleveringer : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["user"] is Student))
                Response.Redirect("login.aspx");
            Master.TitelLabelText = "Afleveringer";
            bool hasAssignmentDescriptions = false;
            if(((Student)Session["user"]).Courses.Count > 0)
                hasAssignmentDescriptions = ((Student) Session["user"]).Courses
                    .Any(course => course.AssignmentDescriptions.Count > 0);
            if (!hasAssignmentDescriptions)
            {
                AssignmentDescriptionsTable.Visible = false;
                SubmitPanel.Visible = false;
                SuccessPanel.Visible = false;
                HtmlGenericControl alert = new HtmlGenericControl("div");
                alert.Attributes["role"] = "alert";
                alert.Attributes["class"] = "alert alert-info";
                alert.InnerHtml = "Du har ikke nogen afleveringer.";
                AlertPanel.Controls.Add(alert);
                return;
            }
            AlertPanel.Visible = false;
            string submitString = Request.QueryString["aflever"];
            string successString = Request.QueryString["succes"];
            if (submitString == null && successString == null)
            {
                SubmitPanel.Visible = false;
                SuccessPanel.Visible = false;
                DrawAssignmentDescriptionsTable();
            }
            else if (submitString != null)
            {
                AssignmentDescription assignmentDescription = null;
                try
                {
                    assignmentDescription = AssignmentDescription.GetByID(Convert.ToUInt32(submitString));
                }
                catch (InvalidIDException)
                {
                    Response.Redirect("afleveringer.aspx");
                }
                // Ensure the following:
                // The user is a part of the course which the AssignmentDescription belongs to
                // The user has not already submitted an Assignment
                // The AssignmentDescription is open for submission (it has not expired)
                if (assignmentDescription.Course.Students.All(s => s.ID != ((Student) Session["user"]).ID) ||
                    assignmentDescription.Assignments.Any(a => a.Student.ID == ((Student) Session["user"]).ID) ||
                    assignmentDescription.HasExpired)
                    Response.Redirect("afleveringer.aspx");
                AssignmentDescriptionsTable.Visible = false;
                SuccessPanel.Visible = false;
                DrawSubmitAssignment(assignmentDescription);
            }
            else
            {
                Assignment assignment = null;
                try
                {
                    assignment = Assignment.GetByID(Convert.ToUInt32(successString));
                }
                catch (InvalidIDException)
                {
                    Response.Redirect("afleveringer.aspx");
                }
                if (assignment.Student.ID == ((Student)Session["user"]).ID)
                {
                    AssignmentDescriptionsTable.Visible = false;
                    SubmitPanel.Visible = false;
                    DrawSuccess(assignment);
                }
                else
                {
                    Response.Redirect("afleveringer.aspx");
                }
            }
        }
        private void DrawAssignmentDescriptionsTable()
        {
            Student student = (Student)Session["user"];
            List<AssignmentDescription> assignmentDescriptions = new List<AssignmentDescription>();
            foreach (Course course in student.Courses)
                assignmentDescriptions.AddRange(course.AssignmentDescriptions);
            assignmentDescriptions.Sort((a, b) => b.Deadline.CompareTo(a.Deadline));
            foreach (AssignmentDescription assignmentDescription in assignmentDescriptions)
            {
                // MainRow
                TableRow row = new TableRow();
                if (assignmentDescription.Documents.Count > 0 || assignmentDescription.Description.Length > 0)
                {
                    row.Cells.Add(Common.CreateAccordionToggleCell("#accordion" + assignmentDescription.ID,
                        assignmentDescription.Title));
                    row.Cells.Add(Common.CreateAccordionToggleCell("#accordion" + assignmentDescription.ID,
                        assignmentDescription.Deadline.ToString()));
                }
                else
                {
                    row.Cells.Add(new TableCell { Text = assignmentDescription.Title });
                    row.Cells.Add(new TableCell { Text = assignmentDescription.Deadline.ToString() });
                }
                TableCell submitCell = new TableCell();
                if (assignmentDescription.Assignments.Any(assignment => assignment.Student.ID == student.ID))
                {
                    // Assignment already submitted
                    Button submittedButton = new Button { Text = "Afleveret" };
                    submittedButton.Attributes["class"] = "btn btn-success disabled";
                    submittedButton.Attributes["style"] = "display: inline-block;";
                    submitCell.Controls.Add(submittedButton);
                    List<string> documents = assignmentDescription.Assignments
                        .Single(assignment => assignment.Student.ID == student.ID).Documents;
                    if (documents.Count == 0)
                    {
                        Button noDocumentsButton = new Button { Text = "Ingen dokumenter" };
                        noDocumentsButton.Attributes["class"] = "btn btn-default disabled";
                        noDocumentsButton.Attributes["style"] = "display: inline-block;";
                        submitCell.Controls.Add(noDocumentsButton);
                    }
                    foreach (string document in documents)
                    {
                        Button downloadButton = new Button { Text = Path.GetFileName(document) };
                        downloadButton.Attributes["class"] = "btn btn-warning";
                        downloadButton.Attributes["style"] = "display: inline-block;";
                        downloadButton.Attributes["path"] = document;
                        downloadButton.Click += DownloadButton_Click;
                        submitCell.Controls.Add(downloadButton);
                    }
                }
                else if (assignmentDescription.Deadline < DateTime.Now)
                {
                    // Assignment deadline passed
                    LinkButton expiredButton = Common.CreateLinkButtonWithIcon("btn btn-danger disabled", "fa-check","Udløbet");
                    submitCell.Controls.Add(expiredButton);
                }
                else
                {
                    // Student is yet to submit assignment
                    HyperLink submitButton = new HyperLink { NavigateUrl = "/afleveringer.aspx?aflever=" + assignmentDescription.ID };
                    submitButton.Controls.Add(Common.CreateIconControl("fa-upload"));
                    submitButton.Controls.Add(Common.CreateTextControl(" Aflever"));
                    submitButton.Attributes["class"] = "btn btn-primary";
                    submitCell.Controls.Add(submitButton);
                }
                row.Cells.Add(submitCell);
                try
                {
                    Assignment assignment = assignmentDescription.Assignments.Single(a => a.Student.ID == ((Student) Session["user"]).ID);
                    TableCell gradeCell = new TableCell { Text = AssignmentGrade.GetByConditions("AssignmentID=" + assignment.ID).Single().Grade };
                    if (assignmentDescription.Documents.Count > 0 || assignmentDescription.Description.Length > 0)
                    {
                        gradeCell.Attributes["class"] = "clickable";
                        gradeCell.Attributes["data-toggle"] = "collapse";
                        gradeCell.Attributes["data-target"] = "#accordion" + assignmentDescription.ID;
                    }
                    row.Cells.Add(gradeCell);
                }
                catch (InvalidOperationException)
                {
                    TableCell emptyCell = new TableCell { Text = "Ingen" };
                    if (assignmentDescription.Documents.Count > 0 || assignmentDescription.Description.Length > 0)
                    {
                        emptyCell.Attributes["class"] = "clickable";
                        emptyCell.Attributes["data-toggle"] = "collapse";
                        emptyCell.Attributes["data-target"] = "#accordion" + assignmentDescription.ID;
                    }
                    row.Cells.Add(emptyCell);
                }
                AssignmentDescriptionsTable.Rows.Add(row);
                if (assignmentDescription.Documents.Count == 0 && assignmentDescription.Description.Length == 0) continue;
                // DetailsRow
                TableRow assignmentDescriptionDetailsRow = new TableRow();
                TableCell assignmentDescriptionDetailsCell = new TableCell { ColumnSpan = 4 };
                var accordion = new HtmlGenericControl("div");
                accordion.Attributes["class"] = "collapse";
                accordion.Attributes["id"] = "accordion" + assignmentDescription.ID;
                if (assignmentDescription.Description.Length > 0)
                {
                    HtmlGenericControl descriptionHeader = new HtmlGenericControl("h4") { InnerText = "Beskrivelse" };
                    accordion.Controls.Add(descriptionHeader);
                    HtmlGenericControl description = new HtmlGenericControl("p") { InnerText = assignmentDescription.Description };
                    accordion.Controls.Add(description);
                }
                if (assignmentDescription.Documents.Count > 0)
                {
                    accordion.Controls.Add(new HtmlGenericControl("br"));
                    HtmlGenericControl documentHeader = new HtmlGenericControl("h4") { InnerText = "Dokumenter" };
                    accordion.Controls.Add(documentHeader);
                    HtmlGenericControl container = new HtmlGenericControl("div");
                    container.Attributes["class"] = "container";
                    Table assignmentDescriptionDocumentsTable = new Table { CssClass = "table table-striped table-hover table-bordered" };
                    TableHeaderRow assignmentDescriptionDocumentsTableHeaderRow = new TableHeaderRow();
                    assignmentDescriptionDocumentsTableHeaderRow.Cells.Add(new TableCell { Text = "Dokumenter" });
                    assignmentDescriptionDocumentsTable.Rows.Add(assignmentDescriptionDocumentsTableHeaderRow);
                    TableRow documentsRow = new TableRow();
                    TableCell documentsCell = new TableCell();
                    foreach (string document in assignmentDescription.Documents)
                        documentsCell.Controls.Add(Common.CreateDownloadButton(document, DownloadButton_Click));
                    documentsRow.Cells.Add(documentsCell);
                    assignmentDescriptionDocumentsTable.Rows.Add(documentsRow);
                    container.Controls.Add(assignmentDescriptionDocumentsTable);
                    accordion.Controls.Add(container);
                }
                assignmentDescriptionDetailsCell.Controls.Add(accordion);
                assignmentDescriptionDetailsRow.Cells.Add(assignmentDescriptionDetailsCell);
                AssignmentDescriptionsTable.Rows.Add(assignmentDescriptionDetailsRow);
            }
        }
        private void DrawSubmitAssignment(AssignmentDescription assignmentDescription)
        {
            SubmitAssignmentDescriptionTitle.Text = "    " + assignmentDescription.Title;
        }
        private void DrawSuccess(Assignment assignment)
        {
            SuccessAssignmentDescription.Text = "til " + assignment.AssignmentDescription.Title;
            SuccessComment.Text = assignment.Comment;
            string documents = "";
            Common.AppendStringArray(ref documents, ", ", assignment.Documents
                .Select(Path.GetFileName).ToArray());
            SuccessDocuments.Text = documents;
        }
        protected void DownloadButton_Click(object sender, EventArgs e) =>
            Common.DownloadFile(sender as LinkButton, this);
        protected void SubmitAssignmentButton_OnClick(object sender, EventArgs e)
        {
            if (!FileUploadControl.HasFiles)
            {
                SubmitResponseLabel.Text = "Du kan ikke aflevere uden et dokument";
                return;
            }
            List<string> filepaths = new List<string>();
            foreach (HttpPostedFile document in FileUploadControl.PostedFiles)
            {
                try
                {
                    filepaths.Add("~/filer/afleveringer/" + Path.GetFileName(document.FileName));
                    document.SaveAs(Path.Combine(Server.MapPath("~/filer/afleveringer/"), document.FileName));
                }
                catch (Exception ex)
                {
                    SubmitResponseLabel.Text = "Fejl: " + ex.Message;
                }
            }
            AssignmentDescription assignmentDescription =
                        AssignmentDescription.GetByID(Convert.ToUInt32(Request.QueryString["aflever"]));
            Assignment assignment = Assignment.New(assignmentDescription, Session["user"] as Student,
                SubmitCommentTextBox.Text, filepaths);
            Response.Redirect("/afleveringer.aspx?succes=" + assignment.ID);
        }
    }
}