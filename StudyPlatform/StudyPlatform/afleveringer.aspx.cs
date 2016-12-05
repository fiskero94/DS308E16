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
    public partial class Afleveringer : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["user"] is Student))
                Response.Redirect("login.aspx");
            Master.TitelLabelText = "Afleveringer";
            Student student = (Student)Session["user"];
            List<AssignmentDescription> assignmentDescriptions = new List<AssignmentDescription>();
            foreach (Course course in student.Courses)
                assignmentDescriptions.AddRange(course.AssignmentDescriptions);
            assignmentDescriptions.Sort((a, b) => b.Deadline.CompareTo(a.Deadline));
            foreach (AssignmentDescription assignmentDescription in assignmentDescriptions)
            {
                TableRow row = new TableRow();
                if (assignmentDescription.Documents.Count > 0 || assignmentDescription.Description.Length > 0)
                {
                    row.Attributes["class"] = "clickable";
                    row.Attributes["data-toggle"] = "collapse";
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
                    List<string> documents = assignmentDescription.Assignments.Single(assignment => assignment.Student.ID == student.ID).Documents;
                    foreach (string document in documents)
                    {
                        Button downloadButton = new Button { Text = "Afleveret" };
                        downloadButton.Attributes["class"] = "btn btn-success disabled";
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
                    Button submitButton = new Button { Text = "Aflever" };
                    submitButton.Attributes["class"] = "btn btn-info";
                    submitButton.Click += SubmitButton_Click;
                    submitCell.Controls.Add(submitButton);
                }
                row.Cells.Add(submitCell);
                AssignmentDescriptionsTable.Rows.Add(row);
                if (assignmentDescription.Documents.Count > 0 || assignmentDescription.Description.Length > 0)
                {
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
        }
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            
        }
        protected void DownloadButton_Click(object sender, EventArgs e)
        {

        }
    }
}