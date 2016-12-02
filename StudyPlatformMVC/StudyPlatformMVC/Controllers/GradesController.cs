using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyPlatformMVC.Models;
using StudyPlatformMVC.Database;
using StudyPlatformMVC.Exceptions;
using StudyPlatformMVC.ViewModels;

namespace StudyPlatformMVC.Controllers
{
    public class GradesController : Controller
    {
        [Route("Grades/Index")]
        public ActionResult Index()
        {
            //Test
            Session["user"] = Student.GetByConditions("name='Iver Clausen'").Single();
            //Actual
            Person person = (Person)Session["user"];
            if(person == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if(person is Student)
            {
                List<CourseGrade> grades = CourseGrade.GetByConditions("StudentID=" + person.ID);
                return View(new CourseGradeViewModel { Grades = grades });
            }
            return RedirectToAction("Index", "News");
        }
        
        [Route("Grades/Index/{id}")]
        public ActionResult Index(int id)
        {
            //Test
            Session["user"] = Student.GetByConditions("name='Iver Clausen'").Single();
            //Actual
            Person person = (Person)Session["user"];
            CourseGrade currentGrade;
            if (person == null)
            {
                try
                {
                    currentGrade = CourseGrade.GetByID(Convert.ToUInt32(id));
                }
                catch(InvalidIDException)
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            return RedirectToAction("Index", "News");
        }
    }
}