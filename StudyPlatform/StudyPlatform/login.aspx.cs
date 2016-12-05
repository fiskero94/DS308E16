using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudyPlatform.Classes.Model;

namespace StudyPlatform
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["user"] = null;
        }

        protected void LoginButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                Session["user"] = Person.GetByConditions("username='" + UsernameTextBox.Text + "'", "password='" + PasswordTextBox.Text + "'").Single();
                if (Session["user"] is Student || Session["user"] is Teacher) Response.Redirect("skema.aspx");
                else Response.Redirect("nyheder.aspx");
            }
            catch (Exception)
            {
                ResponseLabel.Text = "Ugyldigt brugernavn eller adgangskode";
            }
        }
    }
}