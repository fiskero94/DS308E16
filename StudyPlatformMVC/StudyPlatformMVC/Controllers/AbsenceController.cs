using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyPlatformMVC.Database;
using StudyPlatformMVC.Models;
using StudyPlatformMVC.ViewModels;

namespace StudyPlatformMVC.Controllers
{
    public class AbsenceController : Controller
    {
        public ActionResult Index(int id)
        {
            AbsenceViewModel absenceViewModel = new AbsenceViewModel();
            Student student = Student.GetByID(Convert.ToUInt32(id));
            absenceViewModel.Student = student;
            foreach (Course course in student.Courses)
            {
                absenceViewModel.TotalNumberOfLessons += course.Lessons.Count;
                absenceViewModel.TotalNumberOfAssignments += course.AssignmentDescriptions.Count;
                absenceViewModel.CurrentTotalNumberOfLessons += course.CurrentLessons.Count;
                absenceViewModel.CurrentTotalNumberOfAssignments += course.CurrentAssignmentDescriptions.Count;
                absenceViewModel.Courses.Add(course);
            }
            return View(absenceViewModel);
        }
    }
}