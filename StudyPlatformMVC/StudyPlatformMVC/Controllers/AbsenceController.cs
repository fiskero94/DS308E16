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
            Creator.CreateStudent("name", "username", "password");
            Student student = Student.GetByID(Convert.ToUInt32(id));
            Creator.CreateCourse("coursename1", "course1");
            Creator.CreateCourse("coursename2", "course2");
            Course course1 = Course.GetByID(1);
            Course course2 = Course.GetByID(2);
            course1.AddStudent(student);
            course2.AddStudent(student);
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