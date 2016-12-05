using StudyPlatformMVC.Database;
using StudyPlatformMVC.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyPlatformMVC.Controllers
{
    public class ScheduleController : Controller
    {

        [Route("Schedule/Index/{userID}/{weeknumber}")]
        public ActionResult Index(int userID, int weekNumber)
        {
            Person person = Person.GetByID(Convert.ToUInt32(userID));

            if (person is Student)
            {
                List<Course> courses = Student.GetByID(Convert.ToUInt32(userID)).Courses;
                return View(FindAndSortLessonsForPerson(courses, weekNumber));
            }
            else if(person is Teacher)
            {
                List<Course> courses = Teacher.GetByID(Convert.ToUInt32(userID)).Courses;
                return View(FindAndSortLessonsForPerson(courses, weekNumber));
            }
            else // Secretary
            {
                return View();
            }
        }


        private List<Lesson> FindAndSortLessonsForPerson(List<Course> courses, int weekNumber)
        {
            List<Lesson> lessons = new List<Lesson>();

            foreach (Course course in courses)
            {
                foreach (Lesson lesson in course.Lessons)
                {
                    DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                    Calendar cal = dfi.Calendar;

                    if (cal.GetWeekOfYear(lesson.DateTime, dfi.CalendarWeekRule, dfi.FirstDayOfWeek) == weekNumber)
                    {
                        lessons.Add(lesson);
                    }
                }
            }

            // mANGLER SORT BY TIMEOFDAY så alle kl8 lektioner kommer først.
            List<Lesson> sortedList = lessons.OrderBy(x => x.DateTime.TimeOfDay).ToList();

            return sortedList;
        }




    }
}