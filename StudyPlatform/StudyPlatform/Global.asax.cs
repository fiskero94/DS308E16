using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace StudyPlatform
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            DirectoryInfo assignmentDescriptionFilesDirectory = new DirectoryInfo(Server.MapPath("~/filer/afleveringsbeskrivelser/"));
            if (!assignmentDescriptionFilesDirectory.Exists)
                assignmentDescriptionFilesDirectory.Create();
            DirectoryInfo assignmentFilesDirectory = new DirectoryInfo(Server.MapPath("~/filer/afleveringer/"));
            if (!assignmentFilesDirectory.Exists)
                assignmentFilesDirectory.Create();
            DirectoryInfo messageFilesDirectory = new DirectoryInfo(Server.MapPath("~/filer/beskeder/"));
            if (!messageFilesDirectory.Exists)
                messageFilesDirectory.Create();
            DirectoryInfo lessonFilesDirectory = new DirectoryInfo(Server.MapPath("~/filer/lektioner/"));
            if (!lessonFilesDirectory.Exists)
                lessonFilesDirectory.Create();
            DirectoryInfo courseFilesDirectory = new DirectoryInfo(Server.MapPath("~/filer/kurser/"));
            if (!courseFilesDirectory.Exists)
                courseFilesDirectory.Create();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}