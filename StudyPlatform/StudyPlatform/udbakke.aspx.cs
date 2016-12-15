using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using StudyPlatform.Classes;
using StudyPlatform.Classes.Exceptions;
using StudyPlatform.Classes.Model;

namespace StudyPlatform
{
    public partial class Udbakke : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
                Response.Redirect("login.aspx"); // Why is Layout.Master not doing this?
            Master.TitelLabelText = "Udbakke";
            if (((Person)Session["user"]).SentMessages.Count == 0)
            {
                SentMessagesTable.Visible = false;
                AlertPanel.Controls.Add(Common.CreateAlertDiv("Du har ikke sendt nogen beskeder, tryk på ny besked for at begynde."));
                return;
            }
            List<Message> messages = ((Person)Session["user"]).SentMessages;
            messages.Sort((a, b) => b.DateTimeSent.CompareTo(a.DateTimeSent));
            foreach (Message message in messages)
            {
                TableRow row = new TableRow();
                row.Cells.Add(Common.CreateAccordionToggleCell("#accordion" + message.ID, message.Title));
                row.Cells.Add(Common.CreateAccordionToggleCell("#accordion" + message.ID, GetRecipients(message)));
                row.Cells.Add(Common.CreateAccordionToggleCell("#accordion" + message.ID, message.DateTimeSent.ToShortDateString()));
                LinkButton forwardButton = Common.CreateLinkButtonWithIcon("btn btn-default", "fa-share", "Videresend");
                forwardButton.Attributes.Add("messageid", message.ID.ToString());
                forwardButton.Click += ForwardButton_Click;
                row.Cells.Add(Common.CreateCellWithControls(forwardButton));
                SentMessagesTable.Rows.Add(row);
                TableRow textRow = new TableRow();
                TableCell textCell = new TableCell { ColumnSpan = 4 };
                var accordion = new HtmlGenericControl("div") { InnerHtml = message.Text };
                accordion.Attributes["class"] = "collapse";
                accordion.Attributes["id"] = "accordion" + message.ID;
                if (message.Attachments.Count > 0)
                {
                    HtmlGenericControl container = new HtmlGenericControl("div");
                    container.Attributes["class"] = "container";
                    container.Controls.Add(new HtmlGenericControl("br"));
                    Table messageAttachmentsTable = new Table { CssClass = "table table-striped table-hover table-bordered" };
                    TableHeaderRow messageAttachmentsTableHeaderRow = new TableHeaderRow();
                    messageAttachmentsTableHeaderRow.Cells.Add(new TableCell { Text = "Vedhæftninger" });
                    messageAttachmentsTable.Rows.Add(messageAttachmentsTableHeaderRow);
                    TableRow attachmentRow = new TableRow();
                    TableCell attachmentCell = new TableCell();
                    foreach (string attachment in message.Attachments)
                        attachmentCell.Controls.Add(Common.CreateDownloadButton(attachment, DownloadButton_Click));
                    attachmentRow.Cells.Add(attachmentCell);
                    messageAttachmentsTable.Rows.Add(attachmentRow);
                    container.Controls.Add(messageAttachmentsTable);
                    accordion.Controls.Add(container);
                }
                textCell.Controls.Add(accordion);
                textRow.Cells.Add(textCell);
                SentMessagesTable.Rows.Add(textRow);
            }
        }
        private static string GetRecipients(Message message)
        {
            string[] recipientNames = message.Recipients.Select(person => person.Name).ToArray();
            Array.Sort(recipientNames, (a, b) => string.Compare(a, b, StringComparison.InvariantCultureIgnoreCase));
            string recipients = "";
            Common.AppendStringArray(ref recipients, ", ", recipientNames);
            return recipients;
        }
        protected void ForwardButton_Click(object sender, EventArgs e)
        {
            Message message = Message.GetByID(Convert.ToUInt32(((LinkButton) sender).Attributes["messageid"]));
            Master.MessageTitle = message.Title;
            Master.MessageText = message.Text;
            Master.OpenNewMessage();
        }
        protected void DownloadButton_Click(object sender, EventArgs e) => 
            Common.DownloadFile(sender as LinkButton, this);
    }
}