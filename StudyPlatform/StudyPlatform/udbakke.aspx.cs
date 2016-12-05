using System;
using System.Collections.Generic;
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
                HtmlGenericControl alert = new HtmlGenericControl("div");
                alert.Attributes["role"] = "alert";
                alert.Attributes["class"] = "alert alert-info";
                alert.InnerHtml = "Du har ikke sendt nogen beskeder, tryk på ny besked for at begynde.";
                AlertPanel.Controls.Add(alert);
                return;
            }
            List<Message> messages = ((Person)Session["user"]).SentMessages;
            messages.Sort((a, b) => b.DateTimeSent.CompareTo(a.DateTimeSent));
            foreach (Message message in messages)
            {
                TableRow row = new TableRow();
                row.Attributes["class"] = "clickable";
                //row.Attributes["data-toggle"] = "collapse";
                //row.Attributes["data-target"] = "#accordion" + message.ID;
                row.Cells.Add(Common.CreateTextCell(message.Title, "col-sm-5"));
                row.Cells.Add(Common.CreateTextCell(GetRecipients(message), "col-sm-4"));
                row.Cells.Add(Common.CreateTextCell(message.DateTimeSent.ToShortDateString(), "col-sm-2"));
                Button forwardButton = new Button
                {
                    Text = "Videresend",
                    CssClass = "btn btn-default"
                };
                forwardButton.Attributes["messageid"] = message.ID.ToString();
                forwardButton.Click += ForwardButton_Click;
                TableCell forwardButtonCell = new TableCell();
                forwardButtonCell.Attributes["class"] = "col-sm-1";
                forwardButtonCell.Controls.Add(forwardButton);
                row.Cells.Add(forwardButtonCell);
                SentMessagesTable.Rows.Add(row);
                TableRow textRow = new TableRow();
                TableCell textCell = new TableCell { ColumnSpan = 5 };
                var accordion = new HtmlGenericControl("div") { InnerHtml = message.Text };
                //accordion.Attributes["class"] = "collapse";
                accordion.Attributes["id"] = "accordion" + message.ID;
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
            Message message = Message.GetByID(Convert.ToUInt32(((Button) sender).Attributes["messageid"]));
            Master.MessageTitle = message.Title;
            Master.MessageText = message.Text;
        }
    }
}