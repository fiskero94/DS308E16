using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyPlatformMVC.Database;
using StudyPlatformMVC.Models;

namespace StudyPlatformMVC.Controllers
{
    public class AbsenceController : Controller
    {
        public ActionResult Index(int id)
        {
            Student student = Student.GetByID(Convert.ToUInt32(id));
            List<Lesson> absenceList = new List<Lesson>();
            foreach (Course course in student.Courses)
            {
                foreach (Lesson studentAbsence in student.Absences)
                    if (studentAbsence.Course.ID == course.ID)
                        absenceList.Add(studentAbsence);
                if (course.Lessons.Count != 0)
                    course.StudentAbsence = (absenceList.Count / course.Lessons.Count) * 100;
            }
            return View(student.Courses);
        }
    }
}