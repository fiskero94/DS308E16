using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using StudyPlatform.Classes.Model;

namespace StudyPlatform
{
    public partial class Layout : MasterPage
    {
        public string TitelLabelText
        {
            get { return TitelLabel.Text; }
            set { TitelLabel.Text = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
                Response.Redirect("Login.aspx");
            else if (Session["user"] is Student)
                foreach (HyperLink link in GetStudentLinks())
                    SidePanel.Controls.Add(link);
            else if (Session["user"] is Teacher)
                foreach (HyperLink link in GetTeacherLinks())
                    SidePanel.Controls.Add(link);
            else foreach (HyperLink link in GetSecretaryLinks())
                SidePanel.Controls.Add(link);
            NameLabel.Text = ((Person) Session["user"]).Name;
        }


        private void CreateStudentSidePanel()
        {
            foreach (HyperLink link in GetStudentLinks())
            {
                if (link.NavigateUrl == "/skema.aspx")
                {
                    
                }
                else if (link.NavigateUrl == "/afleveringer.aspx")
                {

                }
                else if (link.NavigateUrl == "/indbakke.aspx")
                {

                }
                else if (link.NavigateUrl == "/udbakke.aspx")
                {

                }
                else
                {
                    SidePanel.Controls.Add(link);
                }
            }
        }















        private static IEnumerable<HyperLink> GetStudentLinks()
        {
            return new List<HyperLink>
            {
                new HyperLink { NavigateUrl = "/nyheder.aspx", Text = "Nyheder" },
                new HyperLink { NavigateUrl = "/skema.aspx", Text = "Skema" },
                new HyperLink { NavigateUrl = "/kurser.aspx", Text = "Kurser" },
                new HyperLink { NavigateUrl = "/afleveringer.aspx", Text = "Afleveringer" },
                new HyperLink { NavigateUrl = "/karakterer.aspx", Text = "Karakterer" },
                new HyperLink { NavigateUrl = "/fravaer.aspx", Text = "Fravær" },
                new HyperLink { NavigateUrl = "/indbakke.aspx", Text = "Indbakke" },
                new HyperLink { NavigateUrl = "/udbakke.aspx", Text = "Udbakke" }
            };
        }
        private static IEnumerable<HyperLink> GetTeacherLinks()
        {
            return new List<HyperLink>
            {
                new HyperLink { NavigateUrl = "/nyheder.aspx", Text = "Nyheder" },
                new HyperLink { NavigateUrl = "/skema.aspx", Text = "Skema" },
                new HyperLink { NavigateUrl = "/kurser.aspx", Text = "Kurser" },
                new HyperLink { NavigateUrl = "/indbakke.aspx", Text = "Indbakke" },
                new HyperLink { NavigateUrl = "/udbakke.aspx", Text = "Udbakke" }
            };
        }
        private static IEnumerable<HyperLink> GetSecretaryLinks()
        {
            return new List<HyperLink>
            {
                new HyperLink { NavigateUrl = "/nyheder.aspx", Text = "Nyheder" },
                new HyperLink { NavigateUrl = "/kurser.aspx", Text = "Kurser" },
                new HyperLink { NavigateUrl = "/brugere.aspx", Text = "Brugere" },
                new HyperLink { NavigateUrl = "/indbakke.aspx", Text = "Indbakke" },
                new HyperLink { NavigateUrl = "/udbakke.aspx", Text = "Udbakke" }
            };
        }
    }
}