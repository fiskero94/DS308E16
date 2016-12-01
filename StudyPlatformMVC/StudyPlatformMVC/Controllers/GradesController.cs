using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyPlatformMVC.Models;
using StudyPlatformMVC.Database;
using StudyPlatformMVC.Exceptions;

namespace StudyPlatformMVC.Controllers
{
    public class GradesController : Controller
    {
        // GET: Grades
        public ActionResult Index()
        {
            Student student = ((Student)Session["user"]);
            
            return View(CourseGrade.GetAll());
        }
    }
}