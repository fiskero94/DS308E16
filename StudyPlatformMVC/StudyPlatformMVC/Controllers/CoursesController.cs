using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using StudyPlatformMVC.Exceptions;
using StudyPlatformMVC.Models;
using StudyPlatformMVC.ViewModels;

namespace StudyPlatformMVC.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        [Route("Courses/Index")]
        public ActionResult Index()
        {
            //Session["user"] = Student.GetLatest();
            Person person = (Person)Session["user"];

            if (person is Student)
            {
                List<Course> coursesStudent = new List<Course>();
                coursesStudent = ((Student)Session["user"]).Courses;

                return View(new CourseViewModel { Courses = coursesStudent });
            }

            return RedirectToAction("Index", "News");
        }

        [Route("Courses/Index/{id}")]
        public ActionResult Index(int id)
        {
            Person person = (Person)Session["user"];

            if (person is Student)
            {
                List<Course> coursesStudent = new List<Course>();
                coursesStudent = ((Student)Session["user"]).Courses;

                //coursesStudent = Course.GetByConditions(id);
                return View(new CourseViewModel { Courses = coursesStudent });
            }

            return RedirectToAction("Index", "News");
        }

        [Route("Courses/Courseteacher")]
        public ActionResult Courseteacher()
        {
            Person person = (Person)Session["user"];

            if (person is Teacher)
            {
                List<Course> coursesTeacher = new List<Course>();
                coursesTeacher = ((Teacher)Session["user"]).Courses;

                return View(coursesTeacher);
            }

            return RedirectToAction("Index", "News");
        }



        [Route("Courses/Courseteacher/{id}")]
        public ActionResult Courseteacher(int id)
        {

            Person person = (Person)Session["user"];

            if (person is Teacher)
            {
                List<Course> coursesTeacher = new List<Course>();
                coursesTeacher = ((Teacher)Session["user"]).Courses;

                return View(coursesTeacher);
            }

            return RedirectToAction("Index", "News");
        }

    }
}