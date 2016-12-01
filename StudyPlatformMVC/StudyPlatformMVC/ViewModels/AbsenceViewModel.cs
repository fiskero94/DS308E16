using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatformMVC.Models;

namespace StudyPlatformMVC.ViewModels
{
    public class AbsenceViewModel
    {
        public List<Course> Courses = new List<Course>();
        public Student Student;
        public int TotalNumberOfLessons;
        public int TotalNumberOfAssignments;
        public int CurrentTotalNumberOfLessons;
        public int CurrentTotalNumberOfAssignments;

        public double CurrentTotalAssignmentProcent;
        public double CurrentTotalLessonProcent;
        public double TotalAssignmentProcent;
        public double TotalLessonProcent;

        public double Calc(int number1, int number2)
        {
            if (number2 == 0)
                return 0.0;

            return (number1 * 100) / number2;
        }

        public int Convert(int number1, int number2)
        {
            return number1 - number2;
        }
    }
}