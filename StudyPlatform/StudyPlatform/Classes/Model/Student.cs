using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class Student : Person
    {
        public Student(uint id, string name) : base(id, name) {}

        public List<Course> Courses => GetRelations<Course>("StudentCourse", "CourseID", "StudentID", ID);
        public List<Assignment> Assignments => Assignment.GetByConditions("StudentID=" + ID);
        public List<Lesson> Absences => GetRelations<Lesson>("StudentAbsence", "LessonID", "StudentID", ID);
        public List<Assignment> GetActiveAssignmentsByCourse(Course course) => 
            Assignments.Where(assignment => assignment.AssignmentDescription.Course.ID == course.ID && 
            assignment.AssignmentDescription.Deadline < DateTime.Now).ToList();
        public List<Assignment> GetAssignmentsByCourse(Course course) => 
            Assignments.Where(assignment => assignment.AssignmentDescription.Course.ID == course.ID).ToList();
        public List<Assignment> ActiveAssignments => 
            Assignments.Where(assignment => assignment.AssignmentDescription.Deadline < DateTime.Now).ToList();
        public List<Lesson> CourseLessons(Course course) => 
            Absences.Where(absence => absence.Course.ID == course.ID).ToList();

        public static Student New(string name, string username, string password)
        {
            Creator.CreateStudent(name, username, password);
            return GetLatest(1).Single();
        }

        public new static Student GetByID(uint id) => GetRecordByID<Student>(id);
        public new static List<Student> GetAll() => GetAll<Student>();
        public new static List<Student> GetByConditions(params string[] conditions)
            => GetRecordsByConditions<Student>(conditions);
        public new static List<Student> GetLatest(uint count) => GetLatestRecords<Student>(count);
        public new static Student GetLatest() => GetLatestRecord<Student>();
    }
}