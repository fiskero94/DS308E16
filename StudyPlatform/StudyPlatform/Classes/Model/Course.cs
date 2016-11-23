using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class Course
    {
        private uint _id;
        private string _name;
        private string _description;

        public Course(uint id, string name, string description)
        {
            _id = id;
            _name = name;
            _description = description;
        }
        
        public uint ID
        {
            get
            {
                return _id;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                else
                {
                    Commands.SetValue("courses", ID, "name", value);
                    _name = value;
                }
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                else
                {
                    Commands.SetValue("courses", ID, "description", value);
                    _description = value;
                }
            }
        }
        public List<Teacher> Teachers
        {
            get
            {
                
            }
        }
        public List<Student> Students
        {
            get
            {

            }
        }
        public List<Lesson> Lessons
        {
            get
            {

            }
        }
        public List<AssignmentDescription> AssignmentDescription
        {
            get
            {

            }
        }
        public List<CourseGrade> CourseGrades
        {
            get
            {

            }
        }
        public List<string> Documents
        {
            get
            {

            }
        }
    }
}