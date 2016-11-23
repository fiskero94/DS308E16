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
                    Commands.SetValue("courses", ID, "name", "'" + value + "'");
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
                    Commands.SetValue("courses", ID, "description", "'" + value + "'");
                    _description = value;
                }
            }
        }
        public List<Teacher> Teachers
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.courseteachers" + ID);
                uint[] ids = Extractor.ExtractIDs(query.Execute());
                List<Teacher> teachers = new List<Teacher>();
                foreach (uint id in ids)
                    teachers.Add(Getters.GetPersonByID(id) as Teacher);
                return teachers;
            }
        }
        public List<Student> Students
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.coursestudents" + ID);
                uint[] ids = Extractor.ExtractIDs(query.Execute());
                List<Student> students = new List<Student>();
                foreach (uint id in ids)
                    students.Add(Getters.GetPersonByID(id) as Student);
                return students;
            }
        }
        public List<Lesson> Lessons
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.courselessons" + ID);
                uint[] ids = Extractor.ExtractIDs(query.Execute());
                List<Lesson> lessons = new List<Lesson>();
                foreach (uint id in ids)
                    lessons.Add(Getters.GetLessonByID(id));
                return lessons;
            }
        }
        public List<AssignmentDescription> AssignmentDescription
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.courseassignmentdescriptions" + ID);
                uint[] ids = Extractor.ExtractIDs(query.Execute());
                List<AssignmentDescription> assignmentdescriptions = new List<AssignmentDescription>();
                foreach (uint id in ids)
                    assignmentdescriptions.Add(Getters.GetAssignmentDescriptionByID(id));
                return assignmentdescriptions;
            }
        }
        public List<CourseGrade> CourseGrades
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.coursegrades" + ID);
                uint[] ids = Extractor.ExtractIDs(query.Execute());
                List<CourseGrade> coursegrades = new List<CourseGrade>();
                foreach (uint id in ids)
                    coursegrades.Add(Getters.GetCourseGradeByID(id));
                return coursegrades;
            }
        }
        public List<string> Documents
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.coursedocuments" + ID);
                string[] filepaths = Extractor.ExtractFilepaths(query.Execute());
                return filepaths.ToList();
            }
        }
    }
}