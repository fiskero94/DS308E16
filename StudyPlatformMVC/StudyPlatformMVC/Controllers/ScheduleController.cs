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


        Func<DateTime, int> weekProjector = d => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(d, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);

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
                    Lessons.AddRange(course.Lessons);
                }

                var lessonsByWeek = from lesson in Lessons group lesson by weekProjector(lesson.DateTime);

                var weeklessons = lessonsByWeek.Where(x => x.Key == weeknumber);

                var list = weeklessons.SelectMany(x => x).ToList();


                return View(list);
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