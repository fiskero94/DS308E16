using StudyPlatform.Classes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using StudyPlatform.Classes.Exceptions;

namespace StudyPlatform.Classes.Database
{
    public static class Creator
    {
        private static void CreatePerson(string name, string username, string password, string type) =>
            Commands.InsertInto("persons", "NULL", username, password, name, type);
        public static void CreateStudent(string name, string username, string password)
        {
            Common.EnsureNotNull(name, username, password);
            Common.EnsureNotEmpty(name, username, password);
            CreatePerson(name, username, password, "student");
            Student student = Getters.GetLatestPersons(1).Single() as Student;
            Commands.CreateTable("personsentmessages" + student.ID, "messageid INT UNSIGNED NOT NULL");
            Commands.CreateTable("personrecievedmessages" + student.ID, "messageid INT UNSIGNED NOT NULL");
            Commands.CreateTable("personcourses" + student.ID, "courseid INT UNSIGNED NOT NULL");
            Commands.CreateTable("personassignments" + student.ID, "assignmentdescriptionid INT UNSIGNED NOT NULL");
            Commands.CreateTable("personabscences" + student.ID, "lessonid INT UNSIGNED NOT NULL");
        }
        public static void CreateTeacher(string name, string username, string password)
        {
            Common.EnsureNotNull(name, username, password);
            Common.EnsureNotEmpty(name, username, password);
            CreatePerson(name, username, password, "teacher");
            Teacher teacher = Getters.GetLatestPersons(1).Single() as Teacher;
            Commands.CreateTable("personsentmessages" + teacher.ID, "messageid INT UNSIGNED NOT NULL");
            Commands.CreateTable("personrecievedmessages" + teacher.ID, "messageid INT UNSIGNED NOT NULL");
            Commands.CreateTable("personcourses" + teacher.ID, "courseid INT UNSIGNED NOT NULL");
        }
        public static void CreateSecretary(string name, string username, string password)
        {
            Common.EnsureNotNull(name, username, password);
            Common.EnsureNotEmpty(name, username, password);
            CreatePerson(name, username, password, "secretary");
            Secretary secretary = Getters.GetLatestPersons(1).Single() as Secretary;
            Commands.CreateTable("personsentmessages" + secretary.ID, "messageid INT UNSIGNED NOT NULL");
            Commands.CreateTable("personrecievedmessages" + secretary.ID, "messageid INT UNSIGNED NOT NULL");
        }
        public static void CreateMessage(Person sender, string title, string text, List<Person> recipients, List<string> filepaths)
        {
            Common.EnsureNotNull(sender, title, text, recipients, filepaths);
            Common.EnsureNotEmpty(title);
            if (recipients.Count == 0)
                throw new NoRecipientsException();
            Commands.InsertInto("messages", "NULL", sender.ID.ToString(), title, text, "NOW()");
            Message message = Getters.GetLatestMessages(1).Single();
            Commands.CreateTable("messagerecipients" + message.ID, "personid INT UNSIGNED NOT NULL");
            Commands.CreateTable("messageattachments" + message.ID, "filepath TEXT NOT NULL");
            Commands.InsertInto("personsentmessages" + sender.ID, message.ID.ToString());
            foreach (Person recipient in recipients)
            {
                Commands.InsertInto("messagerecipients" + message.ID, recipient.ID.ToString());
                Commands.InsertInto("personrecievedmessages" + recipient.ID, message.ID.ToString());
            }
            foreach (var filepath in filepaths)
                Commands.InsertInto("messageattachments" + message.ID, filepath);
        }
        public static void CreateNews(Secretary author, string title, string text)
        {
            Common.EnsureNotNull(author, title, text);
            Common.EnsureNotEmpty(title);
            Commands.InsertInto("news", "NULL", author.ID.ToString(), title, text, "NOW()");
        }
        public static void CreateCourse(string name, string description)
        {
            Common.EnsureNotNull(name, description);
            Common.EnsureNotEmpty(name);
            Commands.InsertInto("courses", "NULL", name, description);
            Course course = Getters.GetLatestCourses(1).Single();
            Commands.CreateTable("courseteachers" + course.ID, "teacherid INT UNSIGNED NOT NULL");
            Commands.CreateTable("coursestudents" + course.ID, "studentid INT UNSIGNED NOT NULL");
            Commands.CreateTable("courselessons" + course.ID, "lessonid INT UNSIGNED NOT NULL");
            Commands.CreateTable("courseassignmentdescriptions" + course.ID, "assignmentdescriptionid INT UNSIGNED NOT NULL");
            Commands.CreateTable("coursegrades" + course.ID, "gradeid INT UNSIGNED NOT NULL");
            Commands.CreateTable("coursedocuments" + course.ID, "filepath TEXT NOT NULL");
        }
        public static void CreateLesson(DateTime date, string description, bool online, bool active, List<Room> rooms, List<string> filepaths, Course course)
        {
            Common.EnsureNotNull(date, description, online, active, rooms, filepaths, course);
            Commands.InsertInto("lessons", "NULL", course.ID.ToString(), date.ToString("yyyy-MM-dd HH:mm:ss"), description, 
                                online.ToString().ToUpper(), active.ToString().ToUpper());
            Lesson lesson = Getters.GetLatestLessons(1).Single();
            Commands.CreateTable("lessonrooms" + lesson.ID, "roomid INT UNSIGNED NOT NULL");
            Commands.CreateTable("lessonabsences" + lesson.ID, "absenceid INT UNSIGNED NOT NULL");
            Commands.CreateTable("lessondocuments" + lesson.ID, "filepath TEXT NOT NULL");
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
            Common.EnsureNotNull(name);
            Common.EnsureNotEmpty(name);
            Commands.InsertInto("rooms", "NULL", name);
            Room room = Getters.GetLatestRooms(1).Single();
            Commands.CreateTable("roomreservations" + room.ID, "lessonid INT UNSIGNED NOT NULL");
        }
        public static void CreateAssignmentDescription(Course course, string description, DateTime deadline, List<string> filepaths)
        {
            Common.EnsureNotNull(course, description, deadline, filepaths);
            Commands.InsertInto("assignmentdescriptions", "NULL", course.ID.ToString(), description, deadline.ToString("yyyy-MM-dd HH:mm:ss"));
            AssignmentDescription assignmentDescription = Getters.GetLatestAssignmentDescriptions(1).Single();
            Commands.CreateTable("assignmentdescriptionassignments" + assignmentDescription.ID, "assignmentid INT UNSIGNED NOT NULL");
            Commands.CreateTable("assignmentdescriptiondocuments" + assignmentDescription.ID, "filepath TEXT NOT NULL");
            Commands.InsertInto("courseassignmentdescriptions" + course.ID, assignmentDescription.ID.ToString());
            foreach (string filepath in filepaths)
                Commands.InsertInto("assignentdescriptiondocuments" + assignmentDescription.ID, filepath);
        }
        public static void CreateAssignment(AssignmentDescription assignmentDescription, Student student, string comment, List<string> filepaths)
        {
            Common.EnsureNotNull(assignmentDescription, student, comment, filepaths);
            Commands.InsertInto("assignments", "NULL", assignmentDescription.ID.ToString(), student.ID.ToString(), comment, "NULL", "NOW()");
            Assignment assignment = Getters.GetLatestAssignments(1).Single();
            Commands.CreateTable("assignmentdocuments" + assignment.ID, "filepath TEXT NOT NULL");
            foreach (string filepath in filepaths)
                Commands.InsertInto("assignmentdocuments" + assignment.ID, filepath);
            Commands.InsertInto("assignmentdescriptionassignments" + assignmentDescription.ID, assignment.ID.ToString());
            Commands.InsertInto("personassignments" + student.ID, assignment.ID.ToString());
        }
        public static void CreateAssignmentGrade(string grade, string comment, Assignment assignment)
        {
            Common.EnsureNotNull(grade, comment, assignment);
            if (!Grade.ValidGrades.Contains(grade))
                throw new InvalidGradeException();
            Commands.InsertInto("assignmentgrades", "NULL", grade, comment, assignment.ID.ToString());
            AssignmentGrade assignmentGrade = Getters.GetLatestAssignmentGrades(1).Single();
            Commands.SetValue("assignments", assignment.ID, "gradeid", assignmentGrade.ID.ToString());
          
        }
        public static void CreateCourseGrade(string grade, string comment, Course course, Student student)
        {
            Common.EnsureNotNull(grade, comment, course, student);
            if (!Grade.ValidGrades.Contains(grade))
                throw new InvalidGradeException();
            Commands.InsertInto("coursegrades", "NULL", grade, comment, course.ID.ToString(), student.ID.ToString());
            CourseGrade courseGrade = Getters.GetLatestCourseGrades(1).Single();
            Commands.InsertInto("coursegrades" + course.ID, courseGrade.ID.ToString());
        }
    }
}