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
        
        public uint ID => _id;

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
        public List<AssignmentDescription> AssignmentDescriptions
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

        public static Course New(string name, string description)
        {
            Creator.CreateCourse(name, description);
            return Getters.GetLatestCourses(1).Single();
        }
        public void Remove() => Remover.RemoveCourse(this);
        public void AddStudent(Student student)
        {
            Commands.InsertInto("personcourses" + student.ID.ToString(), ID.ToString());
            Commands.InsertInto("coursestudents" + ID.ToString(), student.ID.ToString());
        }
        public void AddTeacher(Teacher teacher)
        {
            Commands.InsertInto("personcourses" + teacher.ID, ID.ToString());
            Commands.InsertInto("courseteachers" + ID.ToString(), teacher.ID.ToString());
        }
        public void RemoveStudent(Student student)
        {
            Commands.DeleteFrom("personcourses" + student.ID, "courseid=" + ID);
            Commands.DeleteFrom("coursestudents" + ID, "studentid=" + student.ID);
            //DELETE FROM ASSIGNMENTS IN COURSE
            //DELETE ABSENCE FROM LESSONS IN COURSE
        }
        public void RemoveTeacher(Teacher teacher)
        {
            Commands.DeleteFrom("personcourses" + teacher.ID, "courseid=" + ID);
            Commands.DeleteFrom("courseteachers" + ID, "teacher=" + teacher.ID);
        }
    }
}