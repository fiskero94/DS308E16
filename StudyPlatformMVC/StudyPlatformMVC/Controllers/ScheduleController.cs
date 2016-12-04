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
        public ActionResult Index(int userID, int weeknumber)
        {
            Person person = Person.GetByID(Convert.ToUInt32(userID));

            if (person is Student)
            {
                List<Course> Courses = Student.GetByID(Convert.ToUInt32(userID)).Courses;
                List<Lesson> Lessons = new List<Lesson>();

                foreach (Course course in Courses)
                {
                    foreach (Lesson lesson in course.Lessons)
                    {
                        


                        DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                        Calendar cal = dfi.Calendar;

                        if (cal.GetWeekOfYear(lesson.DateTime, dfi.CalendarWeekRule, dfi.FirstDayOfWeek) == weeknumber)
                        {
                            Lessons.Add(lesson);
                        }  
                    }
                }

                // mANGLER SORT BY TIMEOFDAY så alle kl8 lektioner kommer først.
                var list = Lessons.OrderBy(x => x.DateTime.TimeOfDay).ToList();





                return View(Lessons);
            }






            else if(person is Teacher)
            {


            }
            else
            {

            }

            return View();
        }
    }
}