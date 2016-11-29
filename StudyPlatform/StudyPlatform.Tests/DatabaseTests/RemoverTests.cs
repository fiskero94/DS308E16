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
            Student student = Student.GetLatest(1).Single();
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Course.GetLatest(1).Single();
            Creator.CreateLesson(course, Instances.Description, Instances.Online, Instances.Date, Instances.Rooms, Instances.Filepaths);
            Lesson lesson = Lesson.GetLatest(1).Single();

            List<Person> recipients = new List<Person>();
            recipients.Add(Person.GetByID(1));
            Creator.CreateMessage(student, Instances.Title, Instances.Text, recipients, Instances.Filepaths);
            recipients.Clear();
            recipients.Add(student);
            Creator.CreateMessage(Person.GetByID(1), Instances.Title, Instances.Text, recipients, Instances.Filepaths);

            course.AddStudent(student);
            lesson.GiveAbsence(student);

            List<Student> absentStudents = lesson.Absences;
            List<Student> courseStudents = course.Students;
            List<Message> allMessages = Message.GetAll();
            List<Person> allUsers = Person.GetAll();

            //ACT
            Remover.RemovePerson(student);

            //ASSERT
            foreach (Message item in Message.GetAll())
            {
                List<Person> items = item.Recipients;
                foreach (Person recipient in items)
                {
                    if(student.ID == recipient.ID)
                        Assert.Fail("deleted person still in recipients");
                }
            }
            List<Message> allMessagesTest = Message.GetAll();
            Assert.AreEqual(allMessagesTest.Count, allMessages.Count - 1);

            List<Student> courseStudentsTest = course.Students;
            Assert.AreEqual(courseStudentsTest.Count, courseStudents.Count - 1);

            List<Person> allUsersTest = Person.GetAll();
            Assert.AreEqual(allUsersTest.Count, allUsers.Count - 1);

            List<Student> absentStudentsTest = lesson.Absences;
            Assert.AreEqual(absentStudentsTest.Count, absentStudents.Count - 1);
        }
        [TestMethod]
        public void RemoverRemovePerson_TeacherAsParameter_PersonRemoved()
        {
            //ARRANGE
            Creator.CreateTeacher(Instances.Name, Instances.Username, Instances.Password);
            Teacher teacher = Teacher.GetLatest(1).Single();
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Course.GetLatest(1).Single();
            course.AddTeacher(teacher);
            List<Teacher> courseTeachers = course.Teachers;

            //ACT
            Remover.RemovePerson(teacher);

            //ASSERT
            List<Teacher> courseTeachersTest = course.Teachers;
            Assert.AreEqual(courseTeachersTest.Count, courseTeachers.Count - 1);
        }
        [TestMethod]
        public void RemoverRemovePerson_SecretaryAsParameter_PersonRemoved()
        {
            //ARRANGE
            Creator.CreateSecretary(Instances.Name, Instances.Username, Instances.Password);
            Secretary secretary = Secretary.GetLatest(1).Single() as Secretary;
            Creator.CreateNews(secretary, Instances.Title, Instances.Text);
            List<News> allNews = News.GetAll();

            //ACT
            Remover.RemovePerson(secretary);

            //ASSERT
            List<News> allNewsTest = News.GetAll();
            Assert.AreEqual(allNewsTest.Count, allNews.Count - 1);
        }
        [TestMethod]
        public void RemoverRemoveMessage_ValidParameters_MessageRemoved()
        {
            // Arrange
            Secretary sender = Secretary.GetByID(1);
            Creator.CreateSecretary(Instances.Name, Instances.Username, Instances.Password);
            List<Person> recipients = new List<Person> { Secretary.GetByID(2) };
            Creator.CreateMessage(sender, Instances.Title, Instances.Text, recipients, Instances.Filepaths);
            Message message = Message.GetByID(1);

            // Act
            Remover.RemoveMessage(message);

            // Assert
            Assert.AreEqual(0, Message.GetAll().Count);
            Assert.AreEqual(0, sender.SentMessages.Count);
            Assert.AreEqual(0, recipients[0].RecievedMessages.Count);
        }
        [TestMethod]
        public void RemoverRemoveNews_ValidParameters_NewsRemoved()
        {
            // Arrange
            Creator.CreateNews(Instances.Secretary, Instances.Title, Instances.Text);
            News news = News.GetByID(1);

            // Act
            Remover.RemoveNews(news);

            // Assert
            Assert.AreEqual(0, News.GetAll().Count);
        }
        [TestMethod]
        public void RemoverRemoveCourse_ValidParameters_CourseRemoved()
        {
            // Arrange
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Course.GetByID(1);
            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);
            Student student = Student.GetByID(2);
            course.AddStudent(student);
            Creator.CreateTeacher(Instances.Name, Instances.Username, Instances.Password);
            Teacher teacher = Teacher.GetByID(3);
            course.AddTeacher(teacher);
            Creator.CreateLesson(course, Instances.Description, Instances.Online,
                Instances.Date, Instances.Rooms, Instances.Filepaths);
            Creator.CreateAssignmentDescription(course, Instances.Description, Instances.Date, Instances.Filepaths);
            Creator.CreateCourseGrade(course, student, Instances.Grade, Instances.Comment);

            // Act
            Remover.RemoveCourse(course);

            // Assert
            Assert.AreEqual(0, Course.GetAll().Count);
            Assert.AreEqual(0, student.Courses.Count);
            Assert.AreEqual(0, teacher.Courses.Count);
            Assert.AreEqual(0, Lesson.GetAll().Count);
            Assert.AreEqual(0, AssignmentDescription.GetAll().Count);
            Assert.AreEqual(0, CourseGrade.GetAll().Count);
        }
        [TestMethod]
        public void RemoverRemoveLesson_ValidParameters_LessonRemoved()
        {
            // Arrange
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Course.GetByID(1);
            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);
            Student student = Student.GetByID(2);
            course.AddStudent(student);
            Creator.CreateRoom(Instances.Name);
            List<Room> rooms = new List<Room> { Room.GetByID(1) };
            Creator.CreateLesson(course, Instances.Description, Instances.Online,
                Instances.Date, rooms, Instances.Filepaths);
            Lesson lesson = Lesson.GetByID(1);
            lesson.GiveAbsence(student);

            // Act
            Remover.RemoveLesson(lesson);

            // Assert
            Assert.AreEqual(0, Lesson.GetAll().Count);
            Assert.AreEqual(0, course.Lessons.Count);
            Assert.AreEqual(0, student.Absences.Count);
            Assert.AreEqual(0, rooms[0].Reservations.Count);
        }
        [TestMethod]
        public void RemoverRemoveRoom_ValidParameters_RoomRemoved()
        {
            // Arrange
            Creator.CreateRoom(Instances.Name);
            Room room = Room.GetByID(1);
            List<Room> rooms = new List<Room>();
            rooms.Add(room);
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Course.GetByID(1);
            Creator.CreateLesson(course, Instances.Description, Instances.Online, Instances.Date, rooms, Instances.Filepaths);
            Creator.CreateLesson(course, Instances.Description, Instances.Online, Instances.Date, rooms, Instances.Filepaths);
            Lesson lesson = Lesson.GetByID(1);

            // Act
            Remover.RemoveRoom(room);

            // Assert
            Assert.AreEqual(0, Room.GetAll().Count);
            Assert.AreEqual(0, lesson.Rooms.Count);
        }
        [TestMethod]
        public void RemoverRemoveAssignmentDescription_ValidParameters_AssignmentDescriptionRemoved()
        {
            // Arrange
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Course.GetByID(1);
            Creator.CreateAssignmentDescription(course, Instances.Description, Instances.Date, Instances.Filepaths);
            AssignmentDescription assignmentDescription = AssignmentDescription.GetByID(1);

            // Act
            Remover.RemoveAssignmentDescription(assignmentDescription);

            // Assert
            Assert.AreEqual(0, AssignmentDescription.GetAll().Count);
            Assert.AreEqual(0, course.AssignmentDescriptions.Count);
        }
        [TestMethod]
        public void RemoverRemoveAssignment_ValidParameters_AssignmentRemoved()
        {
            // Arrange
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Course.GetByID(1);
            Creator.CreateAssignmentDescription(course, Instances.Description, Instances.Date, Instances.Filepaths);
            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);
            AssignmentDescription assignmentDescription = AssignmentDescription.GetByID(1);
            Student student = Student.GetByID(2);
            Creator.CreateAssignment(assignmentDescription, student, Instances.Comment, Instances.Filepaths);
            Assignment assignment = Assignment.GetByID(1);

            // Act
            Remover.RemoveAssignment(assignment);

            // Assert
            Assert.AreEqual(0, Assignment.GetAll().Count);
            Assert.AreEqual(0, assignmentDescription.Assignments.Count);
            Assert.AreEqual(0, student.Assignments.Count);
        }
        [TestMethod]
        public void RemoverRemoveAssignmentGrade_ValidParameters_AssignmentGradeRemoved()
        {
            // Arrange
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Course.GetByID(1);
            Creator.CreateAssignmentDescription(course, Instances.Description, Instances.Date, Instances.Filepaths);
            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);
            AssignmentDescription assignmentDescription = AssignmentDescription.GetByID(1);
            Student student = Person.GetByID(2) as Student;
            Creator.CreateAssignment(assignmentDescription, student, Instances.Comment, Instances.Filepaths);
            Assignment assignment = Assignment.GetByID(1);
            Creator.CreateAssignmentGrade(Instances.Grade, Instances.Comment, assignment);
            AssignmentGrade assignmentGrade = AssignmentGrade.GetByID(1);

            // Act
            Remover.RemoveAssignmentGrade(assignmentGrade);

            // Assert
            Assert.AreEqual(null, assignment.Grade);
            Assert.AreEqual(0, AssignmentGrade.GetAll().Count);
        }
        [TestMethod]
        public void RemoverRemoveCourseGrade_ValidParameters_CourseGradeRemoved()
        {
            // Arrange
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Course.GetByID(1);
            Creator.CreateCourseGrade(course, Instances.Student, Instances.Grade, Instances.Comment);
            CourseGrade courseGrade = CourseGrade.GetByID(1);

            // Act
            Remover.RemoveCourseGrade(courseGrade);

            // Assert
            Assert.AreEqual(0, course.CourseGrades.Count);
            Assert.AreEqual(0, CourseGrade.GetAll().Count);
        }
    }
}
