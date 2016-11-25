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
            //STUDENT DELETION
            //SETUP
            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);
            List<Person> allUsers = Lists.Persons;
            Student student = Getters.GetLatestPersons(1).Single() as Student;
            List<string> filepaths = new List<string>();
            filepaths.Add("");
            List<Person> recipients = new List<Person>();
            recipients.Add(Getters.GetPersonByID(1));
            Creator.CreateMessage(student, Instances.Title, Instances.Text, recipients, filepaths);
            recipients.Clear();
            recipients.Add(student);
            Creator.CreateMessage(Getters.GetPersonByID(1), Instances.Title, Instances.Text, recipients, filepaths);
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Getters.GetLatestCourses(1).Single();
            course.AddStudent(student);
            Creator.CreateRoom(Instances.Name);
            List<Room> rooms = new List<Room>();
            Room room = Getters.GetLatestRooms(1).Single();
            rooms.Add(room);
            Creator.CreateLesson(Instances.Date, Instances.Description, Instances.Online, Instances.Active, rooms, filepaths, course);
            Lesson lesson = Getters.GetLatestLessons(1).Single();
            Remover.RemovePerson(student);
            //MESSAGE RECIPIENTS
            foreach (Message item in Lists.Messages)
            {
                List<Person> items = item.Recipients;
                foreach (Person recipient in items)
                {
                    if(student.ID == recipient.ID)
                        Assert.Fail("deleted person still in recipients");
                }
            }
            //PERSON SENT/RECIEVED MESSAGES
            try
            {
                List<Message> sentMessages = student.SentMessages;
            }
            catch (Exception ex)
            {
                if (!(ex is MySqlException && ((MySqlException)ex).Number == 1146))
                    Assert.Fail(ex.Message);
            }
            try
            {
                List<Message> recievedMessages = student.RecievedMessages;
            }
            catch (Exception ex)
            {
                if (!(ex is MySqlException && ((MySqlException)ex).Number == 1146))
                    Assert.Fail(ex.Message);
            }
            course = Getters.GetCourseByID(course.ID);
            foreach (Student coursestudents in course.Students)
            {
                if(student.ID == coursestudents.ID)
                {
                    Assert.Fail("student is still in course");
                }
            }
            foreach (Lesson testLesson in course.Lessons)
            {
                foreach (Student item in testLesson.Absences)
                {
                    if (item.ID == student.ID)
                        Assert.Fail("Student still has absence");
                }
            }
            try
            {
                List<Course> courses = student.Courses;
            }
            catch (Exception ex)
            {
                if (!(ex is MySqlException && ((MySqlException)ex).Number == 1146))
                    Assert.Fail(ex.Message);
            }
            try
            {
                List<Assignment> assignments = student.Assignments;
            }
            catch (Exception ex)
            {
                if (!(ex is MySqlException && ((MySqlException)ex).Number == 1146))
                    Assert.Fail(ex.Message);
            }
            try
            {
                List<Lesson> absences = student.Absences;
            }
            catch (Exception ex)
            {
                if (!(ex is MySqlException && ((MySqlException)ex).Number == 1146))
                    Assert.Fail(ex.Message);
            }
            //PERSON DELETION
            List<Person> allUsersTest = Lists.Persons;
            Assert.AreEqual(allUsersTest.Count, allUsers.Count - 1);
            //TEACHER DELETION
            Creator.CreateTeacher(Instances.Name, Instances.Username, Instances.Password);
            Teacher teacher = Getters.GetLatestPersons(1).Single() as Teacher;
            course.AddTeacher(teacher);
            Remover.RemovePerson(teacher);
            foreach (Teacher testTeacher in course.Teachers)
            {
                if (teacher.ID == testTeacher.ID)
                    Assert.Fail("Teacher still in course");
            }
            try
            {
                List<Course> courses = teacher.Courses;
            }
            catch (Exception ex)
            {
                if (!(ex is MySqlException && ((MySqlException)ex).Number == 1146))
                    Assert.Fail(ex.Message);
            }
        }
        [TestMethod]
        public void RemoverRemovePerson_TeacherAsParameter_PersonRemoved()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void RemoverRemovePerson_SecretaryAsParameter_PersonRemoved()
        {
            throw new NotImplementedException();
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
            Creator.CreateLesson(Instances.Date, Instances.Description, Instances.Online, 
                Instances.Active, Instances.Rooms, Instances.Filepaths, course);
            Lesson lesson = Getters.GetLessonByID(1);
            Creator.CreateAssignmentDescription(course, Instances.Description, Instances.Date, Instances.Filepaths);
            AssignmentDescription assignmentDescription = Getters.GetAssignmentDescriptionByID(1);
            Creator.CreateCourseGrade(Instances.Grade, Instances.Comment, course, student);
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
            Creator.CreateLesson(Instances.Date, Instances.Description, Instances.Online, 
                Instances.Active, rooms, Instances.Filepaths, course);
            Lesson lesson = Getters.GetLessonByID(1);
            lesson.GiveAbsence(student);

            // Act
            Remover.RemoveLesson(lesson);

            // Assert
            Assert.AreEqual(0, Lists.Lessons.Count);
            Common.TestTableExists("lessonsrooms1", false);
            Common.TestTableExists("lessonsabscenes1", false);
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
            Creator.CreateLesson(Instances.Date, Instances.Description, Instances.Online, Instances.Active, rooms, Instances.Filepaths, course);
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
            Assert.AreEqual(0, course.AssignmentDescriptions);
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
            Creator.CreateCourseGrade(Instances.Grade, Instances.Comment, course, Instances.Student);
            CourseGrade courseGrade = Getters.GetCourseGradeByID(1);

            // Act
            Remover.RemoveCourseGrade(courseGrade);

            // Assert
            Assert.AreEqual(0, course.CourseGrades.Count);
            Assert.AreEqual(0, Lists.CourseGrades.Count);
        }
    }
}
