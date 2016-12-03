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
        public int TotalNumberOfLessons = 0;
        public int TotalNumberOfAssignments = 0;
        public int CurrentTotalNumberOfLessons = 0;
        public int CurrentTotalNumberOfAssignments = 0;
    }
}