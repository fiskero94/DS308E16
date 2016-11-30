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
            int assignmentCount = 0;
            int studentAssignments = 0;
            int currentTotalLessonhours = 0;
            int totalCoursesLessonHours = 0;
            int totalCoursesAssignmentHours = 0;
            int totalCoursesAssignmentAbsence = 0;
            Student student = Student.GetByID(Convert.ToUInt32(id));
            List<Lesson> absenceList = new List<Lesson>();
            foreach (Course course in student.Courses)
            {
                currentTotalLessonhours += course.Lessons.Count;
                totalCoursesAssignmentHours += course.NumberOfAssignments;
                totalCoursesLessonHours += course.NumberOfLessons;
                assignmentCount = 0;
                studentAssignments = 0;
                course.StudentAbsence = 0;
                course.StudentAbsentLessons = 0;
                foreach (Lesson studentAbsence in student.Absences)
                    if (studentAbsence.Course.ID == course.ID)
                        absenceList.Add(studentAbsence);
                if (course.Lessons.Count != 0)
                {
                    course.StudentAbsence = (absenceList.Count*100)/course.Lessons.Count;
                    course.StudentAbsentLessons = absenceList.Count;
                }
                course.StudentTotalAbsence = (absenceList.Count*100)/course.NumberOfLessons;
                absenceList.Clear();


                foreach (AssignmentDescription courseAssignmentDescription in course.AssignmentDescriptions)
                {
                    assignmentCount++;
                    totalCoursesAssignmentAbsence++;
                    foreach (Assignment assignment in courseAssignmentDescription.Assignments)
                    {
                        if (assignment.Student.ID == student.ID)
                        {
                            studentAssignments++;
                        }
                    }
                }
                course.studentNumberOfAssignments = studentAssignments;
                course.studentAssignments = (studentAssignments*100)/assignmentCount;
                course.studentTotalAssignments = (studentAssignments*100)/course.NumberOfAssignments;

                absenceViewModel.Courses.Add(course);
            }
            ViewBag.totalNumberOfLessons = totalCoursesLessonHours;
            ViewBag.totalNumberOfAssignments = totalCoursesAssignmentHours;
            ViewBag.currentLessonProcent = (student.Absences.Count * 100) / currentTotalLessonhours;
            ViewBag.totalLessonProcent = (student.Absences.Count*100)/totalCoursesLessonHours;
            ViewBag.totalAssignmentProcent = (totalCoursesAssignmentAbsence*100)/totalCoursesAssignmentHours;

            absenceViewModel.CurrentTotalLessonProcent = (student.Absences.Count*100)/currentTotalLessonhours;
            absenceViewModel.TotalNumberOfLessons = totalCoursesLessonHours;
            absenceViewModel.TotalNumberOfAssignments = totalCoursesAssignmentHours;
            absenceViewModel.TotalAssignmentProcent = (totalCoursesAssignmentAbsence*100)/totalCoursesAssignmentHours;
            absenceViewModel.TotalLessonProcent = (student.Absences.Count*100)/totalCoursesLessonHours;


            return View(absenceViewModel);
        }
    }
}