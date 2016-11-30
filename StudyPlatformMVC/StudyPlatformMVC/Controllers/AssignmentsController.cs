using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyPlatformMVC.Models;

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
    }
}