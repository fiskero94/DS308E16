using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudyPlatform.Classes.Exceptions;
using StudyPlatform.Classes.Model;

namespace StudyPlatform
{
    public partial class NewMessage : MasterPage
    {
        public string MessageTitle
        {
            get { return TitleTextBox.Text; }
            set { TitleTextBox.Text = value; }
        }
        public string MessageText
        {
            get { return TextTextBox.Text; }
            set { TextTextBox.Text = value; }
        }

        public string TitelLabelText
        {
            get { return Master.TitelLabelText; }
            set { Master.TitelLabelText = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            NewMessageButton.Attributes["data-toggle"] = "collapse";
            NewMessageButton.Attributes["data-target"] = "#newmessage";
            List<Person> persons = Person.GetAll();
            persons.Sort((a,b) => string.Compare(a.Name, b.Name, StringComparison.InvariantCultureIgnoreCase));
            foreach (Person person in persons)
                RecipientsListBox.Items.Add(new ListItem(person.Name, person.ID.ToString()));
        }
        protected void CreateMessageButton_OnClick(object sender, EventArgs e)
        {
            List<Person> recipients = RecipientsListBox.GetSelectedIndices().Select(index =>
                Person.GetByID(Convert.ToUInt32(RecipientsListBox.Items[index].Value))).ToList();
            try
            {
                Message.New(Session["user"] as Person, TitleTextBox.Text, TextTextBox.Text, recipients, new List<string>());
                Response.Redirect(Request.RawUrl);
            }
            catch (ArgumentException)
            {
                ResponseLabel.Text = "Beskeden skal have en titel.";
            }
            catch (NoRecipientsException)
            {
                ResponseLabel.Text = "Beskeden skal have en eller flere modtagere";
            }
            catch (Exception ex)
            {
                ResponseLabel.Text = "Unhandled exception: " + ex.Message;
            }
        }
    }
}