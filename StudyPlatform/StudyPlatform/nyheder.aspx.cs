using StudyPlatform.Classes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.WebSockets;
using StudyPlatform.Classes;

namespace StudyPlatform
{
    public partial class Nyheder : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NewNewsButton.Attributes["data-toggle"] = "collapse";
            NewNewsButton.Attributes["data-target"] = "#newnews";
            Master.TitelLabelText = "Nyheder";
            if (Session["user"] is Secretary)
                SetupEdit();
            else
                SetupRead();
        }
        private void SetupRead()
        {
            NewNewsButton.Visible = false;
            CreateNewsButton.Visible = false;
            foreach (News news in News.GetAll().OrderBy(o => o.DateTimePublished).Reverse())
            {
                TableRow row = new TableRow();
                row.Attributes["class"] = "clickable";
                row.Attributes["data-toggle"] = "collapse";
                row.Attributes["data-target"] = "#accordion" + news.ID;
                TableCell titleCell = new TableCell { Text = news.Title };
                titleCell.Attributes["class"] = "col-sm-8";
                row.Cells.Add(titleCell);
                TableCell authorCell = new TableCell { Text = news.Author.Name };
                authorCell.Attributes["class"] = "col-sm-2";
                row.Cells.Add(authorCell);
                TableCell dateTimeCell = new TableCell { Text = news.DateTimePublished.ToShortDateString() };
                dateTimeCell.Attributes["class"] = "col-sm-2";
                row.Cells.Add(dateTimeCell);
                NewsTable.Rows.Add(row);
                TableRow textRow = new TableRow();
                TableCell textCell = new TableCell {ColumnSpan = 3};
                var accordion = new HtmlGenericControl("div");
                accordion.Attributes["class"] = "collapse";
                accordion.Attributes["id"] = "accordion" + news.ID;
                accordion.InnerHtml = news.Text;
                textCell.Controls.Add(accordion);
                textRow.Cells.Add(textCell);
                NewsTable.Rows.Add(textRow);
            }
        }
        private void SetupEdit()
        {
            NewsTableHeaderRow.Cells.Add(new TableCell());
            NewsTableHeaderRow.Cells.Add(new TableCell());
            foreach (News news in News.GetAll().OrderBy(o => o.DateTimePublished).Reverse())
            {
                TableRow row = new TableRow();
                row.Attributes["class"] = "clickable";
                TextBox titleTextBox = new TextBox
                {
                    CssClass = "form-control",
                    Text = news.Title
                };
                titleTextBox.Attributes["placeholder"] = "Titel";
                titleTextBox.Attributes["textboxid"] = "title" + news.ID;
                TableCell titleTextBoxCell = new TableCell();
                titleTextBoxCell.Attributes["class"] = "col-sm-6";
                titleTextBoxCell.Attributes["data-toggle"] = "collapse";
                titleTextBoxCell.Attributes["data-target"] = "#accordion" + news.ID;
                titleTextBoxCell.Controls.Add(titleTextBox);
                row.Cells.Add(titleTextBoxCell);
                TableCell authorCell = new TableCell { Text = news.Author.Name };
                authorCell.Attributes["class"] = "col-sm-2";
                authorCell.Attributes["data-toggle"] = "collapse";
                authorCell.Attributes["data-target"] = "#accordion" + news.ID;
                row.Cells.Add(authorCell);
                TableCell dateTimeCell = new TableCell { Text = news.DateTimePublished.ToShortDateString() };
                dateTimeCell.Attributes["class"] = "col-sm-2";
                dateTimeCell.Attributes["data-toggle"] = "collapse";
                dateTimeCell.Attributes["data-target"] = "#accordion" + news.ID;
                row.Cells.Add(dateTimeCell);
                Button saveButton = new Button
                {
                    Text = "Gem",
                    CssClass = "btn btn-info"
                };
                saveButton.Attributes["newsid"] = news.ID.ToString();
                saveButton.CommandArgument = news.ID.ToString();
                saveButton.Command += SaveButton_Click;
                var saveButtonCell = new TableCell();
                saveButtonCell.Attributes["class"] = "col-sm-1";
                saveButtonCell.Controls.Add(saveButton);
                row.Cells.Add(saveButtonCell);
                Button deleteButton = new Button
                {
                    Text = "Slet",
                    CssClass = "btn btn-danger"
                };
                deleteButton.Attributes["newsid"] = news.ID.ToString();
                deleteButton.Click += DeleteButton_Click;
                var deleteButtonCell = new TableCell();
                deleteButtonCell.Attributes["class"] = "col-sm-1";
                deleteButtonCell.Controls.Add(deleteButton);
                row.Cells.Add(deleteButtonCell);
                NewsTable.Rows.Add(row);
                TableRow textRow = new TableRow();
                TableCell textCell = new TableCell {ColumnSpan = 5};
                var accordion = new HtmlGenericControl("div");
                accordion.Attributes["class"] = "collapse";
                accordion.Attributes["id"] = "accordion" + news.ID;
                TextBox textTextBox = new TextBox
                {
                    CssClass = "form-control",
                    TextMode = TextBoxMode.MultiLine,
                    Text = news.Text,
                    Rows = 10
                };
                textTextBox.Attributes["placeholder"] = "Tekst";
                textTextBox.Attributes["textboxid"] = "text" + news.ID;
                accordion.Controls.Add(textTextBox);
                textCell.Controls.Add(accordion);
                textRow.Cells.Add(textCell);
                NewsTable.Rows.Add(textRow);
            }
        }
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string newsId = ((Button) sender).Attributes["newsid"];
            TextBox titleTextBox = WebHelper.FindWebControlByAttribute<TextBox>(NewsTable, "textboxid", "title" + newsId);
            TextBox textTextBox = WebHelper.FindWebControlByAttribute<TextBox>(NewsTable, "textboxid", "text" + newsId);
            News news = News.GetByID(Convert.ToUInt32(newsId));
            try
            {
                news.Title = titleTextBox.Text;
            }
            catch (ArgumentException)
            {
                titleTextBox.Text = news.Title;
            }
            try
            {
                news.Text = textTextBox.Text;
            }
            catch (ArgumentException)
            {
                textTextBox.Text = news.Text;
            }
        }
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            string newsId = ((Button) sender).Attributes["newsid"];
            News news = News.GetByID(Convert.ToUInt32(newsId));
            news.Remove();
            Response.Redirect(Request.RawUrl);
        }
        protected void CreateNewsButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                News.New(Session["user"] as Secretary, TitleTextBox.Text, TextTextBox.Text);
                Response.Redirect(Request.RawUrl);
            }
            catch (ArgumentException)
            {
                if (TitleTextBox.Text == string.Empty && TextTextBox.Text == string.Empty)
                    ResponseLabel.Text = "Nyheden skal have en titel og en tekst.";
                else if (TitleTextBox.Text == string.Empty) ResponseLabel.Text = "Nyheden skal have en titel.";
                else ResponseLabel.Text = "Nyheden skal have en tekst.";
            }
            catch (Exception ex)
            {
                ResponseLabel.Text = "Unhandled exception: " + ex.Message;
            }
        }
    }
}