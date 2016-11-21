using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class Student : Person
    {
        public List<Course> Courses
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.personcourses" + ID);
                uint[] ids = Extractor.ExtractIDs(query.Execute());
                List<Course> courses = new List<Course>();
                foreach (uint id in ids)
                    courses.Add(Getters.GetCourseByID(id));
                return courses;
            }
        }
        public List<Assignment> Assignments
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.personassignments" + ID);
                uint[] ids = Extractor.ExtractIDs(query.Execute());
                List<Assignment> assignments = new List<Assignment>();
                foreach (uint id in ids)
                    assignments.Add(Getters.GetAssignmentByID(id));
                return assignments;
            }
        }
        public List<Lesson> Absences
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.personlessons" + ID);
                uint[] ids = Extractor.ExtractIDs(query.Execute());
                List<Lesson> absences = new List<Lesson>();
                foreach (uint id in ids)
                    absences.Add(Getters.GetLessonByID(id));
                return absences;
            }
        }
        public Student(uint id, string name) : base(id, name)
        {

        }
    }
}