using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Model;
using StudyPlatform.Classes.Exceptions;
using System.Linq;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace StudyPlatform.Tests.DatabaseTests
{
    [TestClass]
    public class RemoverTests
    {
        public RemoverTests()
        {
            Common.ResetTables();
        }

        [TestMethod]
        public void RemoverRemovePerson_StudentAsParameter_PersonRemoved()
        {
            //ARRANGE
            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);
            Student student = Getters.GetLatestPersons(1).Single() as Student;
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Getters.GetLatestCourses(1).Single();
            Creator.CreateLesson(course, Instances.Description, Instances.Online, Instances.Date, Instances.Rooms, Instances.Filepaths);
            Lesson lesson = Getters.GetLatestLessons(1).Single();

            List<Person> recipients = new List<Person>();
            recipients.Add(Getters.GetPersonByID(1));
            Creator.CreateMessage(student, Instances.Title, Instances.Text, recipients, Instances.Filepaths);
            recipients.Clear();
            recipients.Add(student);
            Creator.CreateMessage(Getters.GetPersonByID(1), Instances.Title, Instances.Text, recipients, Instances.Filepaths);

            course.AddStudent(student);
            lesson.GiveAbsence(student);

            List<Student> absentStudents = lesson.Absences;
            List<Student> courseStudents = course.Students;
            List<Message> allMessages = Lists.Messages;
            List<Person> allUsers = Lists.Persons;

            //ACT
            Remover.RemovePerson(student);

            //ASSERT
            foreach (Message item in Lists.Messages)
            {
                List<Person> items = item.Recipients;
                foreach (Person recipient in items)
                {
                    if(student.ID == recipient.ID)
                        Assert.Fail("deleted person still in recipients");
                }
            }
            List<Message> allMessagesTest = Lists.Messages;
            Assert.AreEqual(allMessagesTest.Count, allMessages.Count - 1);

            List<Student> courseStudentsTest = course.Students;
            Assert.AreEqual(courseStudentsTest.Count, courseStudents.Count - 1);

            List<Person> allUsersTest = Lists.Persons;
            Assert.AreEqual(allUsersTest.Count, allUsers.Count - 1);

            List<Student> absentStudentsTest = lesson.Absences;
            Assert.AreEqual(absentStudentsTest.Count, absentStudents.Count - 1);

            Common.TestTableExists("personsentmessages2", false);
            Common.TestTableExists("personrecievedmessages2", false);
            Common.TestTableExists("personcourses2", false);
            Common.TestTableExists("personassignments2", false);
            Common.TestTableExists("personabsences2", false);
        }
        [TestMethod]
        public void RemoverRemovePerson_TeacherAsParameter_PersonRemoved()
        {
            //ARRANGE
            Creator.CreateTeacher(Instances.Name, Instances.Username, Instances.Password);
            Teacher teacher = Getters.GetLatestPersons(1).Single() as Teacher;
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Getters.GetLatestCourses(1).Single();
            course.AddTeacher(teacher);
            List<Teacher> courseTeachers = course.Teachers;

            //ACT
            Remover.RemovePerson(teacher);

            //ASSERT
            List<Teacher> courseTeachersTest = course.Teachers;
            Assert.AreEqual(courseTeachersTest.Count, courseTeachers.Count - 1);
            Common.TestTableExists("personcourses", false);
        }
        [TestMethod]
        public void RemoverRemovePerson_SecretaryAsParameter_PersonRemoved()
        {
            //ARRANGE
            Creator.CreateSecretary(Instances.Name, Instances.Username, Instances.Password);
            Secretary secretary = Getters.GetLatestPersons(1).Single() as Secretary;
            Creator.CreateNews(secretary, Instances.Title, Instances.Text);
            List<News> allNews = Lists.News;

            //ACT
            Remover.RemovePerson(secretary);

            //ASSERT
            List<News> allNewsTest = Lists.News;
            Assert.AreEqual(allNewsTest.Count, allNews.Count - 1);
        }
        [TestMethod]
        public void RemoverRemoveMessage_ValidParameters_MessageRemoved()
        {
            // Arrange
            Secretary sender = Getters.GetPersonByID(1) as Secretary;
            Creator.CreateSecretary(Instances.Name, Instances.Username, Instances.Password);
            List<Person> recipients = new List<Person>();
            recipients.Add(Getters.GetPersonByID(2) as Secretary);
            Creator.CreateMessage(sender, Instances.Title, Instances.Text, recipients, Instances.Filepaths);
            Message message = Getters.GetMessageByID(1);

            // Act
            Remover.RemoveMessage(message);

            // Assert
            Assert.AreEqual(0, Lists.Messages.Count);
            Common.TestTableExists("messagerecipients1", false);
            Common.TestTableExists("messagedocuments1", false);
            Assert.AreEqual(0, sender.SentMessages.Count);
            Assert.AreEqual(0, recipients[0].RecievedMessages.Count);
        }
        [TestMethod]
        public void RemoverRemoveNews_ValidParameters_NewsRemoved()
        {
            // Arrange
            Creator.CreateNews(Instances.Secretary, Instances.Title, Instances.Text);
            News news = Getters.GetNewsByID(1);

            // Act
            Remover.RemoveNews(news);

            // Assert
            Assert.AreEqual(0, Lists.News.Count);
        }
        [TestMethod]
        public void RemoverRemoveCourse_ValidParameters_CourseRemoved()
        {
            // Arrange
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Getters.GetCourseByID(1);
            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);
            Student student = Getters.GetPersonByID(2) as Student;
            course.AddStudent(student);
            Creator.CreateTeacher(Instances.Name, Instances.Username, Instances.Password);
            Teacher teacher = Getters.GetPersonByID(3) as Teacher;
            course.AddTeacher(teacher);
            Creator.CreateLesson(course, Instances.Description, Instances.Online,
                Instances.Date, Instances.Rooms, Instances.Filepaths);
            Lesson lesson = Getters.GetLessonByID(1);
            Creator.CreateAssignmentDescription(course, Instances.Description, Instances.Date, Instances.Filepaths);
            AssignmentDescription assignmentDescription = Getters.GetAssignmentDescriptionByID(1);
            Creator.CreateCourseGrade(course, student, Instances.Grade, Instances.Comment);
            CourseGrade courseGrade = Getters.GetCourseGradeByID(1);

            // Act
            Remover.RemoveCourse(course);

            // Assert
            Assert.AreEqual(0, Lists.Courses.Count);
            Common.TestTableExists("courseteachers1", false);
            Common.TestTableExists("coursestudents1", false);
            Common.TestTableExists("courselessons1", false);
            Common.TestTableExists("courseassignmentdescriptions1", false);
            Common.TestTableExists("coursecoursegrades1", false);
            Common.TestTableExists("coursedocuments1", false);
            Assert.AreEqual(0, student.Courses.Count);
            Assert.AreEqual(0, teacher.Courses.Count);
            Assert.AreEqual(0, Lists.Lessons.Count);
            Assert.AreEqual(0, Lists.AssignmentDescriptions.Count);
            Assert.AreEqual(0, Lists.CourseGrades.Count);
        }
        [TestMethod]
        public void RemoverRemoveLesson_ValidParameters_LessonRemoved()
        {
            // Arrange
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Getters.GetCourseByID(1);
            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);
            Student student = Getters.GetPersonByID(2) as Student;
            course.AddStudent(student);
            Creator.CreateRoom(Instances.Name);
            List<Room> rooms = new List<Room>();
            rooms.Add(Getters.GetRoomByID(1));
            Creator.CreateLesson(course, Instances.Description, Instances.Online,
                Instances.Date, rooms, Instances.Filepaths);
            Lesson lesson = Getters.GetLessonByID(1);
            lesson.GiveAbsence(student);

            // Act
            Remover.RemoveLesson(lesson);

            // Assert
            Assert.AreEqual(0, Lists.Lessons.Count);
            Common.TestTableExists("lessonrooms1", false);
            Common.TestTableExists("lessonabsences1", false);
            Common.TestTableExists("lessondocuments1", false);
            Assert.AreEqual(0, course.Lessons.Count);
            Assert.AreEqual(0, student.Absences.Count);
            Assert.AreEqual(0, rooms[0].Reservations.Count);
        }
        [TestMethod]
        public void RemoverRemoveRoom_ValidParameters_RoomRemoved()
        {
            // Arrange
            Creator.CreateRoom(Instances.Name);
            Room room = Getters.GetRoomByID(1);
            List<Room> rooms = new List<Room>();
            rooms.Add(room);
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Getters.GetCourseByID(1);
            Creator.CreateLesson(course, Instances.Description, Instances.Online, Instances.Date, rooms, Instances.Filepaths);
            Lesson lesson = Getters.GetLessonByID(1);

            // Act
            Remover.RemoveRoom(room);

            // Assert
            Assert.AreEqual(0, Lists.Rooms.Count);
            Common.TestTableExists("roomreservations1", false);
            Assert.AreEqual(0, lesson.Rooms.Count);
        }
        [TestMethod]
        public void RemoverRemoveAssignmentDescription_ValidParameters_AssignmentDescriptionRemoved()
        {
            // Arrange
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Getters.GetCourseByID(1);
            Creator.CreateAssignmentDescription(course, Instances.Description, Instances.Date, Instances.Filepaths);
            AssignmentDescription assignmentDescription = Getters.GetAssignmentDescriptionByID(1);

            // Act
            Remover.RemoveAssignmentDescription(assignmentDescription);

            // Assert
            Assert.AreEqual(0, Lists.AssignmentDescriptions.Count);
            Common.TestTableExists("assignmentdescriptionassignments1", false);
            Common.TestTableExists("assignmentdescriptiondocuments1", false);
            Assert.AreEqual(0, course.AssignmentDescriptions.Count);
        }
        [TestMethod]
        public void RemoverRemoveAssignment_ValidParameters_AssignmentRemoved()
        {
            // Arrange
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Getters.GetCourseByID(1);
            Creator.CreateAssignmentDescription(course, Instances.Description, Instances.Date, Instances.Filepaths);
            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);
            AssignmentDescription assignmentDescription = Getters.GetAssignmentDescriptionByID(1);
            Student student = Getters.GetPersonByID(2) as Student;
            Creator.CreateAssignment(assignmentDescription, student, Instances.Comment, Instances.Filepaths);
            Assignment assignment = Getters.GetAssignmentByID(1);

            // Act
            Remover.RemoveAssignment(assignment);

            // Assert
            Assert.AreEqual(0, Lists.Assignments.Count);
            Common.TestTableExists("assignmentdocuments1", false);
            Assert.AreEqual(0, assignmentDescription.Assignments.Count);
            Assert.AreEqual(0, student.Assignments.Count);
        }
        [TestMethod]
        public void RemoverRemoveAssignmentGrade_ValidParameters_AssignmentGradeRemoved()
        {
            // Arrange
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Getters.GetCourseByID(1);
            Creator.CreateAssignmentDescription(course, Instances.Description, Instances.Date, Instances.Filepaths);
            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);
            AssignmentDescription assignmentDescription = Getters.GetAssignmentDescriptionByID(1);
            Student student = Getters.GetPersonByID(2) as Student;
            Creator.CreateAssignment(assignmentDescription, student, Instances.Comment, Instances.Filepaths);
            Assignment assignment = Getters.GetAssignmentByID(1);
            Creator.CreateAssignmentGrade(Instances.Grade, Instances.Comment, assignment);
            AssignmentGrade assignmentGrade = Getters.GetAssignmentGradeByID(1);

            // Act
            Remover.RemoveAssignmentGrade(assignmentGrade);

            // Assert
            Assert.AreEqual(null, assignment.Grade);
            Assert.AreEqual(0, Lists.AssignmentGrades.Count);
        }
        [TestMethod]
        public void RemoverRemoveCourseGrade_ValidParameters_CourseGradeRemoved()
        {
            // Arrange
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Getters.GetCourseByID(1);
            Creator.CreateCourseGrade(course, Instances.Student, Instances.Grade, Instances.Comment);
            CourseGrade courseGrade = Getters.GetCourseGradeByID(1);

            // Act
            Remover.RemoveCourseGrade(courseGrade);

            // Assert
            Assert.AreEqual(0, course.CourseGrades.Count);
            Assert.AreEqual(0, Lists.CourseGrades.Count);
        }
    }
}
