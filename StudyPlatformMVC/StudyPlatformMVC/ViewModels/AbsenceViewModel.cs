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

        public int TotalNumberOfLessons;
        public int TotalNumberOfAssignments;
        public double CurrentTotalLessonProcent;
        public double TotalAssignmentProcent;
        public double TotalLessonProcent;
    }
}