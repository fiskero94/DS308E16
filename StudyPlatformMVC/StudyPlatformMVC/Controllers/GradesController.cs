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
            Person person = (Person)Session["user"];
            if((Person)Session["user"] is Student)
            {
                List<CourseGrade> grades = CourseGrade.GetByConditions("StudentID=" + person.ID);
                return View(grades);
            }
            return View(CourseGrade.GetAll());
        }
    }
}