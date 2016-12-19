using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudyPlatform.Classes.Exceptions;
using StudyPlatform.Classes.Model;
using System.Reflection;

namespace StudyPlatform
{
    public partial class NewMessage : MasterPage
    {
        public Message Message;
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
            if (Session["newMessageError"] != null && (bool) Session["newMessageError"])
            {
                ResponseLabel.Text = (string) Session["newMessageErrorText"];
                OpenNewMessage();
                Session["newMessageError"] = false;
            }
        }
        protected void CreateMessageButton_OnClick(object sender, EventArgs e)
        {
            List<Person> recipients = RecipientsListBox.GetSelectedIndices().Select(index =>
                Person.GetByID(Convert.ToUInt32(RecipientsListBox.Items[index].Value))).ToList();
            List<string> filepaths = new List<string>();
            if (FileUploadControl.HasFiles)
            {
                foreach (HttpPostedFile document in FileUploadControl.PostedFiles)
                {
                    try
                    {
                        filepaths.Add("~/filer/beskeder/" + Path.GetFileName(document.FileName));
                        document.SaveAs(Path.Combine(Server.MapPath("~/filer/beskeder/"), document.FileName));
                    }
                    catch (Exception ex)
                    {
                        Session["newMessageError"] = true;
                        Session["newMessageErrorText"] = "Filerne kan ikke hentes: " + ex.Message;
                        Response.Redirect(Request.RawUrl);
                        return;
                    }
                }
            }
            try
            {
                Message.New(Session["user"] as Person, TitleTextBox.Text, TextTextBox.Text, recipients, filepaths);
            }
            catch (ArgumentException)
            {
                Session["newMessageError"] = true;
                Session["newMessageErrorText"] = "Beskeden skal have en titel.";
            }
            catch (NoRecipientsException)
            {
                Session["newMessageError"] = true;
                Session["newMessageErrorText"] = "Beskeden skal have en eller flere modtagere";
            }
            catch (Exception ex)
            {
                Session["newMessageError"] = true;
                Session["newMessageErrorText"] = "Unhandled exception: " + ex.Message;
            }
            finally
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        public void OpenNewMessage() => ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "openNewMessage()", true);

        public void ReplyMessage(Message message)
        {
            foreach (ListItem item in RecipientsListBox.Items)
                item.Selected = item.Value == Convert.ToString(message.Sender.ID);
            MessageTitle = "RE: " + message.Title;
            OpenNewMessage();
        }
    }
}