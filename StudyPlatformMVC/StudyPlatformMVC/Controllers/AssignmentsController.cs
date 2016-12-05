using StudyPlatformMVC.Models;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace StudyPlatformMVC.Controllers
{
    public class AssignmentsController : Controller
    {

        // GET: Assignments
        public ActionResult Index()
        {
            Session["user"] = Student.GetLatest();
            if ((Person)Session["user"] is Student)
            {
                List<AssignmentDescription> assignmentDescription = new List<AssignmentDescription>();
                foreach (Course course in ((Student)Session["user"]).Courses)
                {
                    assignmentDescription.AddRange(course.AssignmentDescriptions);
                }
                List<Assignment> assignment = ((Student)Session["user"]).Assignments;
                return View(assignment);
            }
            return RedirectToAction("Index", "News");
        }

        [HttpPost]
        public ActionResult Aflever(Filehandler file)
        {

            if (file.file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.file.FileName);
                string path = Path.Combine(Server.MapPath("~/Files"), filename);
                file.file.SaveAs(path);
            }
            return RedirectToAction("Index", "News");
        }
        public ActionResult Aflever()
        {
            return View();
        }
    }
}