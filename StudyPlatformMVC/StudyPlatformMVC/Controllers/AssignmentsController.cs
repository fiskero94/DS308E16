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
        public ActionResult Index(int id)
        {
            List<Assignment> assignment = Student.GetByID(Convert.ToUInt32(id)).Assignments;
            
            return View("assignments", assignment);
        }
    }
}