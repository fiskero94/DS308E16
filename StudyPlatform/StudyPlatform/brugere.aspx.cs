using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudyPlatform.Classes;
using StudyPlatform.Classes.Model;

namespace StudyPlatform
{
    public partial class Brugere : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["user"] is Secretary))
                Response.Redirect("login.aspx");
            Master.TitelLabelText = "Brugere";
            TypeDropDownList.Attributes["style"] = "height:38px;";
            List<Person> persons = Person.GetAll();
            persons.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.InvariantCultureIgnoreCase));
            foreach (Person person in persons)
            {
                if (person.Name == "Admin") continue;
                TableRow row = new TableRow();
                row.Cells.Add(Common.CreateTextCell(person.ID.ToString(), "col-sm-1"));
                row.Cells.Add(Common.CreateTextCell(person.Name, "col-sm-8"));
                string type;
                if (person is Student) type = "Kursist";
                else if (person is Teacher) type = "Lærer";
                else type = "Sekretær";
                row.Cells.Add(Common.CreateTextCell(type, "col-sm-2"));
                Button deleteButton = new Button
                {
                    Text = "Slet",
                    CssClass = "btn btn-danger"
                };
                deleteButton.Click += DeleteButton_OnClick;
                deleteButton.Attributes["id"] = person.ID.ToString();
                deleteButton.Attributes["class"] = "col-sm-1";
                TableCell buttonCell = new TableCell();
                buttonCell.Controls.Add(deleteButton);
                row.Cells.Add(buttonCell);
                UsersTable.Rows.Add(row);
            }
        }
        protected void DeleteButton_OnClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Person person = Person.GetByID(Convert.ToUInt32(button.Attributes["id"]));
            Person.GetByID(Convert.ToUInt32(button.Attributes["id"])).Remove();
            Response.Redirect(person.ID == ((Person)Session["user"]).ID ? "Login.aspx" : Request.RawUrl);
        }

        protected void NewButton_OnClick(object sender, EventArgs e)
        {
            if (NewTable.Rows.Count > 1)
                NewTable.Rows.RemoveAt(1);
            try
            {
                switch (TypeDropDownList.SelectedItem.Text)
                {
                    case "Kursist":
                        Student.New(NameTextBox.Text, UsernameTextBox.Text, PasswordTextBox.Text);
                        break;
                    case "Lærer":
                        Teacher.New(NameTextBox.Text, UsernameTextBox.Text, PasswordTextBox.Text);
                        break;
                    case "Sekretær":
                        Secretary.New(NameTextBox.Text, UsernameTextBox.Text, PasswordTextBox.Text);
                        break;
                }
                Response.Redirect(Request.RawUrl);
            }
            catch (ArgumentException)
            {
                TableRow row = new TableRow();
                string nameResponse = "";
                string usernameResponse = "";
                string passwordResponse = "";
                if (NameTextBox.Text == "")
                    nameResponse = "Feltet må ikke være tomt";
                if (UsernameTextBox.Text == "")
                    usernameResponse = "Feltet må ikke være tomt";
                if (PasswordTextBox.Text == "")
                    passwordResponse = "Feltet må ikke være tomt";
                row.Cells.Add(new TableCell { Text = nameResponse, CssClass = "text-danger" });
                row.Cells.Add(new TableCell { Text = usernameResponse, CssClass = "text-danger" });
                row.Cells.Add(new TableCell { Text = passwordResponse, CssClass = "text-danger" });
                row.Cells.Add(new TableCell());
                row.Cells.Add(new TableCell());
                NewTable.Rows.Add(row);
            }
        }
    }
}