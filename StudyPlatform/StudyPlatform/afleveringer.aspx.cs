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
        private AssignmentDescription _assignmentDescription = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["user"] is Student))
                Response.Redirect("login.aspx");
            Master.TitelLabelText = "Afleveringer";
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
                    row.Attributes["class"] = "clickable";
                    //row.Attributes["data-toggle"] = "collapse";
                    row.Attributes["data-target"] = "#accordion" + assignmentDescription.ID;
                }
                row.Cells.Add(new TableCell { Text = assignmentDescription.Description.Length > 0 ? "Ja" : "Nej" });
                row.Cells.Add(new TableCell { Text = assignmentDescription.Documents.Count > 0 ? "Ja" : "Nej" });
                row.Cells.Add(new TableCell { Text = assignmentDescription.Deadline.ToString() });
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
                        Button downloadButton = new Button { Text = Path.GetFileName(document) }; // FIX
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
                    Button expiredButton = new Button { Text = "Udløbet" };
                    expiredButton.Attributes["class"] = "btn btn-danger disabled";
                    submitCell.Controls.Add(expiredButton);
                }
                else
                {
                    // Student is yet to submit assignment
                    HyperLink submitButton = new HyperLink
                    {
                        Text = "Aflever",
                        NavigateUrl = "/afleveringer.aspx?aflever=" + assignmentDescription.ID
                    };
                    submitButton.Attributes["class"] = "btn btn-primary";
                    submitCell.Controls.Add(submitButton);
                }
                row.Cells.Add(submitCell);
                AssignmentDescriptionsTable.Rows.Add(row);
                if (assignmentDescription.Documents.Count == 0 && assignmentDescription.Description.Length == 0) continue;
                // DetailsRow
                TableRow assignmentDescriptionDetailsRow = new TableRow();
                TableCell assignmentDescriptionDetailsCell = new TableCell { ColumnSpan = 4 };
                var accordion = new HtmlGenericControl("div");
                //accordion.Attributes["class"] = "collapse";
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
                AssignmentDescriptionsTable.Rows.Add(assignmentDescriptionDetailsRow);
            }
        }
        private void DrawSubmitAssignment(AssignmentDescription assignmentDescription)
        {
            SubmitAssignmentDescriptionTitle.Text = "    " + "INSERT ASSIGNMENTDESCRIPTION TITLE";
        }
        private void DrawSuccess(Assignment assignment)
        {
            SuccessAssignmentDescription.Text = "til " + "INSERT ASSIGNMENTDESCRIPTION TITLE";
            SuccessComment.Text = assignment.Comment;
            string documents = "";
            Common.AppendStringArray(ref documents, ", ", assignment.Documents
                .Select(Path.GetFileName).ToArray());
            SuccessDocuments.Text = documents;
        }
        protected void DownloadButton_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + 
                Path.GetFileName(((Button)sender).Attributes["path"]));
            Response.TransmitFile(Server.MapPath(((Button)sender).Attributes["path"]));
            Response.End();
        }
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