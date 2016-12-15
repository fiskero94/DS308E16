using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using StudyPlatform.Classes;
using StudyPlatform.Classes.Model;

namespace StudyPlatform
{
    public partial class Indbakke : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
                Response.Redirect("login.aspx"); // Why is Layout.Master not doing this?
            Master.TitelLabelText = "Indbakke";
            if (((Person)Session["user"]).RecievedMessages.Count == 0)
            {
                RecievedMessagesTable.Visible = false;
                AlertPanel.Controls.Add(Common.CreateAlertDiv("Du har ikke modtaget nogen beskeder."));
                return;
            }
            List<Message> messages = ((Person) Session["user"]).RecievedMessages;
            messages.Sort((a, b) => b.DateTimeSent.CompareTo(a.DateTimeSent));
            foreach (Message message in messages)
            {
                TableRow row = new TableRow();
                row.Cells.Add(Common.CreateAccordionToggleCell("#accordion" + message.ID, message.Title));
                row.Cells.Add(Common.CreateAccordionToggleCell("#accordion" + message.ID, message.Sender.Name));
                row.Cells.Add(Common.CreateAccordionToggleCell("#accordion" + message.ID, message.DateTimeSent.ToShortDateString()));
                LinkButton replyButton = Common.CreateLinkButtonWithIcon("btn btn-default", "fa-reply", "Svar");
                replyButton.Attributes.Add("messageid", message.ID.ToString());
                replyButton.Click += ReplyButton_Click;
                row.Cells.Add(Common.CreateCellWithControls(replyButton));
                LinkButton forwardButton = Common.CreateLinkButtonWithIcon("btn btn-default", "fa-share", "Videresend");
                forwardButton.Attributes.Add("messageid", message.ID.ToString());
                forwardButton.Click += ForwardButton_Click;
                row.Cells.Add(Common.CreateCellWithControls(forwardButton));
                RecievedMessagesTable.Rows.Add(row);
                TableRow textRow = new TableRow();
                TableCell textCell = new TableCell { ColumnSpan = 5 };
                var accordion = new HtmlGenericControl("div") { InnerHtml = message.Text };
                accordion.Attributes.Add("class", "collapse");
                accordion.Attributes.Add("id", "accordion" + message.ID);
                if (message.Attachments.Count > 0)
                {
                    HtmlGenericControl container = new HtmlGenericControl("div");
                    container.Attributes.Add("class", "container");
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
                RecievedMessagesTable.Rows.Add(textRow);
            }
        }
        protected void ReplyButton_Click(object sender, EventArgs e)
        {
            Message message = Message.GetByID(Convert.ToUInt32(((LinkButton) sender).Attributes["messageid"]));
            Master.ReplyMessage(message);
        }
        protected void ForwardButton_Click(object sender, EventArgs e)
        {
            Message message = Message.GetByID(Convert.ToUInt32(((LinkButton)sender).Attributes["messageid"]));
            Master.MessageTitle = message.Title;
            Master.MessageText = message.Text;
            Master.OpenNewMessage();
        }
        protected void DownloadButton_Click(object sender, EventArgs e) =>
            Common.DownloadFile(sender as LinkButton, this);
    }
}