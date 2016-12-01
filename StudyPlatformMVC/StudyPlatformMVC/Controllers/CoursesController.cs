using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyPlatformMVC.Models;
using StudyPlatformMVC.Controllers;
using StudyPlatformMVC.Database;

namespace StudyPlatformMVC.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        public ActionResult Index()
        {
            Session["user"] = Student.GetLatest();

            if ((Person)Session["user"] is Student)
            {
                List<Course> coursesStudent = new List<Course>();
                coursesStudent = ((Student)Session["user"]).Courses;

                return View(coursesStudent);
            }

            if ((Person)Session["user"] is Teacher)
            {
                List<Course> coursesTeacher = new List<Course>();
                coursesTeacher = ((Teacher)Session["user"]).Courses;

                return View(coursesTeacher);
            }
            
            else
                return RedirectToAction("Index", "News");
        }
    }
}