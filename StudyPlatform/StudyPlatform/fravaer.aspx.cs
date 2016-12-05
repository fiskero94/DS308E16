using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudyPlatform.Classes.Model;

namespace StudyPlatform
{
    public partial class Fravaer : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["user"] is Student))
                Response.Redirect("login.aspx");
            Master.TitelLabelText = "Fravær";
        }
    }
}