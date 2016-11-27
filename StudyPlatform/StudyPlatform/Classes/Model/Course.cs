using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class Course : Entity<Course>
    {
        private string _name;
        private string _description;

        public Course(uint id, string name, string description) : base(id)
        {
            _name = name;
            _description = description;
        }
        
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                Commands.SetValue("Course", ID, "Name", value);
                _name = value;
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
                Commands.SetValue("Course", ID, "Description", value);
                _description = value;
            }
        }
        public List<Teacher> Teachers => GetRelations<Teacher>("PersonCourse", "PersonID", "CourseID", ID);
        public List<Student> Students => GetRelations<Student>("PersonCourse", "PersonID", "CourseID", ID);
        public List<Lesson> Lessons => Lesson.GetByConditions("CourseID=" + ID);
        public List<AssignmentDescription> AssignmentDescriptions => AssignmentDescription.GetByConditions("CourseID=" + ID);
        public List<CourseGrade> CourseGrades => CourseGrade.GetByConditions("CourseID=" + ID);
        public List<string> Documents => GetDocuments("CourseFile", "CourseID", ID);

        private void AddPerson(Person person) =>
            Commands.InsertInto("PersonCourse", person.ID.ToString(), ID.ToString());
        private void RemovePerson(Person person) =>
            Commands.DeleteFrom("PersonCourse", "PersonID=" + person.ID + " AND CourseID=" + ID);

        public void AddTeacher(Teacher teacher) => AddPerson(teacher);
        public void RemoveTeacher(Teacher teacher) => RemovePerson(teacher);

        public void AddStudent(Student student) => AddPerson(student);
        public void RemoveStudent(Student student)
        {
            foreach (Assignment assignment in student.Assignments)
                if(assignment.AssignmentDescription.Course.ID == ID)
                    Remover.RemoveAssignment(assignment);
            foreach (Lesson lesson in Lessons)
                    lesson.RemoveAbsence(student);
            RemovePerson(student);
        }

        public void AddDocument(string filepath) =>
            Commands.InsertInto("CourseFile", ID.ToString(), filepath);
        public void RemoveDocument(string filepath) =>
            Commands.DeleteFrom("CourseFile", "CourseID=" + ID + " AND Filepath=" + filepath);

        public void Remove() => Remover.RemoveCourse(this);
        public static Course New(string name, string description)
        {
            Creator.CreateCourse(name, description);
            return GetLatest(1).Single();
        }
    }
}