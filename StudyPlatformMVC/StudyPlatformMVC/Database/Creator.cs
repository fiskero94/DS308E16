using StudyPlatformMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using StudyPlatformMVC.Exceptions;

namespace StudyPlatformMVC.Database
{
    public static class Creator
    {
        private static void CreatePerson(string name, string username, string password, string type) =>
            Commands.InsertInto("Person", "NULL", name, username, password, type);
        public static void CreateStudent(string name, string username, string password)
        {
            Common.EnsureNotNull(name, username, password);
            Common.EnsureNotEmpty(name, username, password);
            CreatePerson(name, username, password, "Student");
        }
        public static void CreateTeacher(string name, string username, string password)
        {
            Common.EnsureNotNull(name, username, password);
            Common.EnsureNotEmpty(name, username, password);
            CreatePerson(name, username, password, "Teacher");
        }
        public static void CreateSecretary(string name, string username, string password)
        {
            Common.EnsureNotNull(name, username, password);
            Common.EnsureNotEmpty(name, username, password);
            CreatePerson(name, username, password, "Secretary");
        }
        public static void CreateMessage(Person sender, string title, string text,
            List<Person> recipients, List<string> filepaths)
        {
            Common.EnsureNotNull(sender, title, text, recipients, filepaths);
            Common.EnsureNotEmpty(title);
            if (recipients.Count == 0)
                throw new NoRecipientsException();
            Commands.InsertInto("Message", "NULL", sender.ID.ToString(), title, text, "NOW()");
            Message message = Message.GetLatest(1).Single();
            foreach (Person recipient in recipients)
                Commands.InsertInto("MessageRecipient", message.ID.ToString(), recipient.ID.ToString());
            foreach (string filepath in filepaths)
                Commands.InsertInto("MessageFile", message.ID.ToString(), filepath);
        }
        public static void CreateNews(Secretary author, string title, string text)
        {
            Common.EnsureNotNull(author, title, text);
            Common.EnsureNotEmpty(title);
            Commands.InsertInto("News", "NULL", author.ID.ToString(), title, text, "NOW()");
        }
        public static void CreateCourse(string name, string description)
        {
            Common.EnsureNotNull(name, description);
            Common.EnsureNotEmpty(name);
            Commands.InsertInto("Course", "NULL", name, description);
        }
        public static void CreateLesson(Course course, string description, bool online,
            DateTime dateTime, List<Room> rooms, List<string> filepaths)
        {
            Common.EnsureNotNull(dateTime, description, online, rooms, filepaths, course);
            foreach (Room room in rooms)
                if (!room.CheckAvailability(dateTime))
                    throw new RoomUnavailableException(room.Name + " er ikke tilgængelig for tidspunktet " + dateTime);
            Commands.InsertInto("Lesson", "NULL", course.ID.ToString(), description,
                online.ToString().ToUpper(), "FALSE", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Lesson lesson = Lesson.GetLatest(1).Single();
            foreach (Room room in rooms)
                Commands.InsertInto("LessonRoom", lesson.ID.ToString(), room.ID.ToString());
            foreach (string filepath in filepaths)
                Commands.InsertInto("LessonFile", lesson.ID.ToString(), filepath);
        }
        public static void CreateRoom(string name)
        {
            Common.EnsureNotNull(name);
            Common.EnsureNotEmpty(name);
            Commands.InsertInto("Room", "NULL", name);
        }
        public static void CreateAssignmentDescription(Course course, string description,
            DateTime deadline, List<string> filepaths)
        {
            Common.EnsureNotNull(course, description, deadline, filepaths);
            Commands.InsertInto("AssignmentDescription", "NULL", course.ID.ToString(), description,
                "FALSE", deadline.ToString("yyyy-MM-dd HH:mm:ss"));
            AssignmentDescription assignmentDescription = AssignmentDescription.GetLatest(1).Single();
            foreach (string filepath in filepaths)
                Commands.InsertInto("AssignentDescriptionFile", assignmentDescription.ID.ToString(), filepath);
        }
        public static void CreateAssignment(AssignmentDescription assignmentDescription,
            Student student, string comment, List<string> filepaths)
        {
            Common.EnsureNotNull(assignmentDescription, student, comment, filepaths);
            Commands.InsertInto("Assignment", "NULL", assignmentDescription.ID.ToString(),
                student.ID.ToString(), "NULL", comment, "NOW()");
            Assignment assignment = Assignment.GetLatest(1).Single();
            foreach (string filepath in filepaths)
                Commands.InsertInto("AssignmentFile", assignment.ID.ToString(), filepath);
        }
        public static void CreateAssignmentGrade(string grade, string comment, Assignment assignment)
        {
            Common.EnsureNotNull(grade, comment, assignment);
            if (!Common.ValidGrades.Contains(grade))
                throw new InvalidGradeException();
            Commands.InsertInto("AssignmentGrade", "NULL", assignment.ID.ToString(), grade, comment);
            AssignmentGrade assignmentGrade = AssignmentGrade.GetLatest(1).Single();
            Commands.SetValue("Assignment", assignment.ID, "GradeID", assignmentGrade.ID.ToString());

        }
        public static void CreateCourseGrade(Course course, Student student, string grade, string comment)
        {
            Common.EnsureNotNull(grade, comment, course, student);
            if (!Common.ValidGrades.Contains(grade))
                throw new InvalidGradeException();
            if (course.Students.Any(s => s.ID == student.ID))
                throw new StudentCourseGradeExistsException();
            Commands.InsertInto("CourseGrade", "NULL", course.ID.ToString(),
                student.ID.ToString(), grade, comment);
        }
    }
}