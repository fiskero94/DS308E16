using StudyPlatform.Classes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using StudyPlatform.Classes.Exceptions;

namespace StudyPlatform.Classes.Database
{
    public static class Creator
    {
        private static void EnsureNotNull(params object[] objects)
        {
            foreach (object obj in objects)
                if (obj == null)
                    throw new ArgumentNullException();
        }
        private static void CreatePerson(string name, string username, string password, string type) =>
            Commands.InsertInto("persons", "NULL", name, username, password, type);
        public static void CreateStudent(string name, string username, string password)
        {
            EnsureNotNull(name, username, password);
            CreatePerson(name, username, password, "student");
            Student student = Lists.Students.Last();
            Commands.CreateTable("personsentmessages" + student.ID, "messageid INT UNSIGNED NOT NULL");
            Commands.CreateTable("personrecievedmessages" + student.ID, "messageid INT UNSIGNED NOT NULL");
            Commands.CreateTable("personcourses" + student.ID, "courseid INT UNSIGNED NOT NULL");
            Commands.CreateTable("personabscences" + student.ID, "lessonid INT UNSIGNED NOT NULL");
        }
        public static void CreateTeacher(string name, string username, string password)
        {
            EnsureNotNull(name, username, password);
            CreatePerson(name, username, password, "teacher");
            Teacher teacher = Lists.Teachers.Last();
            Commands.CreateTable("personsentmessages" + student.ID, "messageid INT UNSIGNED NOT NULL");
            Commands.CreateTable("personrecievedmessages" + student.ID, "messageid INT UNSIGNED NOT NULL");
            Commands.CreateTable("personcourses" + teacher.ID, "courseid INT UNSIGNED NOT NULL");
        }
        public static void CreateSecretary(string name, string username, string password)
        {
            EnsureNotNull(name, username, password);
            CreatePerson(name, username, password, "secretary");
            Secretary secretary = Lists.Secretaries.Last();
            Commands.CreateTable("personsentmessages" + student.ID, "messageid INT UNSIGNED NOT NULL");
            Commands.CreateTable("personrecievedmessages" + student.ID, "messageid INT UNSIGNED NOT NULL");
        }

        public static void CreateMessage(Person sender, string title, string text, List<Person> recipients, List<string> filepaths)
        {
            EnsureNotNull(sender, title, text, recipients, filepaths);
            Commands.InsertInto("messages", "NULL", sender.ID.ToString(), title, text, DateTime.Now.ToString());
            Message message = Lists.Messages.Last();
            Commands.CreateTable("messagerecipients" + message.ID, "messageid INT UNSIGNED NOT NULL");
            Commands.CreateTable("messageattachments" + message.ID, "messageid INT UNSIGNED NOT NULL");
            Commands.InsertInto("personsentmessages" + sender.ID, message.ID.ToString());
            Commands.InsertInto("personrecievedmessages" + sender.ID, message.ID.ToString());
            foreach (Person recipient in recipients)
            {
                Commands.InsertInto("messagerecipients" + message.ID, recipient.ID.ToString());
            }
            foreach (var filepath in filepaths)
            {
                Commands.InsertInto("messageattachments" + message.ID, filepath);
            }
        }
        public static void CreateNews(Person author, string title, string text)
        {
            EnsureNotNull(author, title, text);
            Commands.InsertInto("news", "NULL", author.ID.ToString(), title, text, "NOW()");
        }
        public static void CreateCourse(string name, string description)
        {
            EnsureNotNull(name, description);
            Commands.InsertInto("courses", "NULL", name, description);
            Course course = Lists.Courses.Last();
            Commands.CreateTable("courseteachers" + course.ID, "teacherid INT UNSIGNED NOT NULL");
            Commands.CreateTable("coursestudents" + course.ID, "studentid INT UNSIGNED NOT NULL");
            Commands.CreateTable("courselessons" + course.ID, "lessonid INT UNSIGNED NOT NULL");
            Commands.CreateTable("courseassignmentdescriptions" + course.ID, "assignmentdescriptionid INT UNSIGNED NOT NULL");
            Commands.CreateTable("coursegrades" + course.ID, "gradeid INT UNSIGNED NOT NULL");
            Commands.CreateTable("coursedocuments" + course.ID, "filepath TEXT NOT NULL");
        }
        public static void CreateLesson(DateTime date, string description, bool online, bool active, List<Room> rooms, List<string> filepaths, Course course)
        {
            EnsureNotNull(date, description, online, active, rooms, filepaths, course);
            Commands.InsertInto("lessons", "NULL", course.ID.ToString(), date.ToString("yyyy-MM-dd HH:mm:ss"), description, 
                                online.ToString().ToUpper(), active.ToString().ToUpper());
            Lesson lesson = Lists.lessons.Last();
            Commands.CreateTable("lessonrooms" + lesson.ID, "roomid INT UNSIGNED NOT NULL");
            Commands.CreateTable("lessonabsences" + lesson.ID, "absenceid INT UNSIGNED NOT NULL");
            Commands.CreateTable("lessondocuments" + lesson.ID, "TEXT NOT NULL");
            Commands.InsertInto("courselessons" + course.ID, lesson.ID.ToString());
            foreach (Room room in rooms)
            {
                Commands.InsertInto("lessonrooms" + lesson.ID, room.ID.ToString());
                Commands.InsertInto("roomreservations" + room.ID, lesson.ID.ToString());
            }
            foreach (string filepath in filepaths)
                Commands.InsertInto("lessondocuments" + lesson.ID, filepath);
        }
        public static void CreateRoom(string name)
        {
            EnsureNotNull(name);
            Commands.InsertInto("rooms", "NULL", name);
            Room room = Lists.Rooms.Last();
            Commands.CreateTable("roomreservations" + room.ID, "lessonid INT UNSIGNED NOT NULL");
        }

        public static void CreateAssignmentDescription(Course course, string description, DateTime deadline, List<string> filepaths)
        {
            EnsureNotNull(course, description, deadline, filepaths);
            Commands.InsertInto("assignmentsdescriptions", "NULL", description, deadline.ToString("yyyy-MM-dd HH:mm:ss"));
            AssignmentDescription assignmentDescription = Lists.AssignmentDescriptions.Last();
            Commands.CreateTable("assignmentdescriptionassignments" + assignmentDescription.ID, "assignmentid INT UNSIGNED NOT NULL");
            Commands.CreateTable("assignmentdescriptiondocuments" + assignmentDescription.ID, "filepath TEXT NOT NULL");
            Commands.InsertInto("courseassignmentdescriptions" + course.ID, assignmentDescription.ID.ToString());
            foreach (string filepath in filepaths)
                Commands.InsertInto("assignentdescriptiondocuments" + assignmentDescription.ID, filepath);
        }
        public static void CreateAssignment(AssignmentDescription assignmentDescription, Student student, string comment, List<string> filepaths)
        {
            EnsureNotNull(assignmentDescription, student, comment, filepaths);
            Commands.InsertInto("assignments", "NULL", assignmentDescription.ID.ToString(), student.ID.ToString(), comment, "NULL", "NOW()");
            Assignment assignment = Lists.Assignments.Last();
            Commands.CreateTable("assignmentdocuments" + assignment.ID, "filepath TEXT NOT NULL");
            foreach (string filepath in filepaths)
                Commands.InsertInto("assignmentdocuments" + assignment.ID, filepath);
            Commands.InsertInto("assignmentdescriptionassignments" + assignmentDescription.ID, assignment.ID.ToString());
            Commands.InsertInto("personassignments" + student.ID, assignment.ID.ToString());
        }
        public static void CreateAssignmentGrade(string grade, string comment, Assignment assignment)
        {
            EnsureNotNull(grade, comment, assignment);
            if (!Grade.ValidGrades.Contains(grade))
                throw new InvalidGradeException();
            Commands.InsertInto("assignmentgrades", "NULL", grade, comment, assignment.ID.ToString());
            AssignmentGrade assignmentGrade = Lists.AssignmentGrades.Last();
            Commands.SetValue("assignments", assignment.ID, "gradeid", assignmentGrade.ID.ToString());
          
        }
        public static void CreateCourseGrade(string grade, string comment, Course course, Student student)
        {
            EnsureNotNull(grade, comment, course, student);
            if (!Grade.ValidGrades.Contains(grade))
                throw new InvalidGradeException();
            Commands.InsertInto("coursegrades", "NULL", grade, comment, course.ID.ToString(), student.ID.ToString());
            CourseGrade courseGrade = Lists.CourseGrades.Last();
            Commands.InsertInto("coursegrades" + course.ID, courseGrade.ID.ToString());
        }
    }
}