using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Model
{
    public class CourseGrade : Grade
    {
        private string _comment;
        private uint _courseid;
        private string _grade;
        private uint _studentid;

        public CourseGrade(uint id, string grade, string comment, uint courseid, uint studentid) : base(id, grade, comment)
        {
            ID = id;
            _grade = grade;
            _comment = comment;
            _courseid = courseid;
            _studentid = studentid;
        }
        public Course Course
        {
            get
            {
                return Getters.GetCourseByID(_courseid);
            }
        }
        public string Grade
        {
            get
            {
                return _grade;
            }
        }
    }
}