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
                HtmlGenericControl alert = new HtmlGenericControl("div");
                alert.Attributes["role"] = "alert";
                alert.Attributes["class"] = "alert alert-info";
                alert.InnerHtml = "Du har ikke modtaget nogen beskeder.";
                AlertPanel.Controls.Add(alert);
                return;
            }
            List<Message> messages = ((Person) Session["user"]).RecievedMessages;
            messages.Sort((a, b) => b.DateTimeSent.CompareTo(a.DateTimeSent));
            foreach (Message message in messages)
            {
                TableRow row = new TableRow();
                row.Attributes["class"] = "clickable";
                TableCell titleCell = new TableCell {Text = message.Title};
                titleCell.Attributes["data-toggle"] = "collapse";
                titleCell.Attributes["data-target"] = "#accordion" + message.ID;
                row.Cells.Add(titleCell);
                TableCell senderCell = new TableCell { Text = message.Sender.Name };
                senderCell.Attributes["data-toggle"] = "collapse";
                senderCell.Attributes["data-target"] = "#accordion" + message.ID;
                row.Cells.Add(senderCell);
                TableCell dateCell = new TableCell { Text = message.DateTimeSent.ToShortDateString() };
                dateCell.Attributes["data-toggle"] = "collapse";
                dateCell.Attributes["data-target"] = "#accordion" + message.ID;
                row.Cells.Add(dateCell);
                Button replyButton = new Button
                {
                    Text = "Svar",
                    CssClass = "btn btn-default"
                };
                replyButton.Attributes["messageid"] = message.ID.ToString();
                replyButton.Click += ReplyButton_Click;
                TableCell replyButtonCell = new TableCell();
                replyButtonCell.Attributes["class"] = "col-sm-1";
                replyButtonCell.Controls.Add(replyButton);
                row.Cells.Add(replyButtonCell);
                Button forwardButton = new Button
                {
                    Text = "Videresend",
                    CssClass = "btn btn-default"
                };
                forwardButton.Attributes["messageid"] = message.ID.ToString();
                forwardButton.Click += ForwardButton_Click;
                TableCell forwardButtonCell = new TableCell();
                forwardButtonCell.Controls.Add(forwardButton);
                row.Cells.Add(forwardButtonCell);
                RecievedMessagesTable.Rows.Add(row);
                TableRow textRow = new TableRow();
                TableCell textCell = new TableCell { ColumnSpan = 5 };
                var accordion = new HtmlGenericControl("div") { InnerHtml = message.Text };
                accordion.Attributes["class"] = "collapse";
                accordion.Attributes["id"] = "accordion" + message.ID;
                textCell.Controls.Add(accordion);
                textRow.Cells.Add(textCell);
                RecievedMessagesTable.Rows.Add(textRow);
            }
        }
        protected void ReplyButton_Click(object sender, EventArgs e)
        {
            Message message = Message.GetByID(Convert.ToUInt32(((Button) sender).Attributes["messageid"]));
            Master.ReplyMessage(message);
        }
        protected void ForwardButton_Click(object sender, EventArgs e)
        {
            Message message = Message.GetByID(Convert.ToUInt32(((Button)sender).Attributes["messageid"]));
            Master.MessageTitle = message.Title;
            Master.MessageText = message.Text;
            Master.OpenNewMessage();
        }
    }
}