using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Model;
using StudyPlatform.Classes.Exceptions;
using System.Linq;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using MySql.Data.MySqlClient;

namespace StudyPlatform.Tests.DatabaseTests
{
    [TestClass]
    public class RemoverTests
    {
        public RemoverTests()
        {
            WebHelper.ResetTables();
        }

        [TestMethod]
        public void RemoverRemovePerson_StudentAsParameter_PersonRemoved()
        {
            // Arrange
            var student = Student.New(Instances.Name, Instances.Username, Instances.Password);
            var studentid = student.ID;

            // Act
            student.Remove();

            // Assert
            try
            {
                Student.GetByID(studentid);
                Assert.Fail();
            }
            catch (InvalidIDException) { }
        }
        [TestMethod]
        public void RemoverRemovePerson_TeacherAsParameter_PersonRemoved()
        {
            // Arrange
            var teacher = Teacher.New(Instances.Name, Instances.Username, Instances.Password);
            var teacherid = teacher.ID;

            // Act
            teacher.Remove();

            // Assert
            try
            {
                Teacher.GetByID(teacherid);
                Assert.Fail();
            }
            catch (InvalidIDException) { }
        }
        [TestMethod]
        public void RemoverRemovePerson_SecretaryAsParameter_PersonRemoved()
        {
            // Arrange
            var secretary = Secretary.New(Instances.Name, Instances.Username, Instances.Password);
            var secretaryid = secretary.ID;

            // Act
            secretary.Remove();

            // Assert
            try
            {
                Secretary.GetByID(secretaryid);
                Assert.Fail();
            }
            catch (InvalidIDException) { }
        }
        [TestMethod]
        public void RemoverRemoveMessage_ValidParameters_MessageRemoved()
        {
            // Arrange
            var message = Message.New(Instances.Student, Instances.Title, Instances.Text, new List<Person> {Instances.Teacher}, Instances.Filepaths);
            var messageid = message.ID;

            // Act
            message.Remove();

            // Assert
            try
            {
                Message.GetByID(messageid);
                Assert.Fail();
            }
            catch (InvalidIDException) { }
        }
        [TestMethod]
        public void RemoverRemoveNews_ValidParameters_NewsRemoved()
        {
            // Arrange
            var news = News.New(Instances.Secretary, Instances.Title, Instances.Text);
            var newsid = news.ID;

            // Act
            news.Remove();

            // Assert
            try
            {
                News.GetByID(newsid);
                Assert.Fail();
            }
            catch (InvalidIDException) { }
        }
        [TestMethod]
        public void RemoverRemoveCourse_ValidParameters_CourseRemoved()
        {
            // Arrange
            var course = Course.New(Instances.Name, Instances.Description);
            var courseid = course.ID;

            // Act
            course.Remove();

            // Assert
            try
            {
                Course.GetByID(courseid);
                Assert.Fail();
            }
            catch (InvalidIDException) { }
        }
        [TestMethod]
        public void RemoverRemoveLesson_ValidParameters_LessonRemoved()
        {
            // Arrange
            var lesson = Lesson.New(Instances.Course, Instances.Description, Instances.Online, Instances.Date, Instances.Rooms, Instances.Filepaths);
            var lessonid = lesson.ID;

            // Act
            lesson.Remove();

            // Assert
            try
            {
                Lesson.GetByID(lessonid);
                Assert.Fail();
            }
            catch (InvalidIDException) { }
        }
        [TestMethod]
        public void RemoverRemoveRoom_ValidParameters_RoomRemoved()
        {
            // Arrange
            var room = Room.New(Instances.Name);
            var roomid = room.ID;

            // Act
            room.Remove();

            // Assert
            try
            {
                Room.GetByID(roomid);
                Assert.Fail();
            }
            catch (InvalidIDException) { }
        }
        [TestMethod]
        public void RemoverRemoveAssignmentDescription_ValidParameters_AssignmentDescriptionRemoved()
        {
            // Arrange
            var assignmentDescription = AssignmentDescription.New(Instances.Course, Instances.Title, Instances.Description,
                Instances.Date, Instances.Filepaths);
            var assignmentDescriptionid = assignmentDescription.ID;

            // Act
            assignmentDescription.Remove();

            // Assert
            try
            {
                AssignmentDescription.GetByID(assignmentDescriptionid);
                Assert.Fail();
            }
            catch (InvalidIDException) { }
        }
        [TestMethod]
        public void RemoverRemoveAssignment_ValidParameters_AssignmentRemoved()
        {
            // Arrange
            var assignment = Assignment.New(Instances.AssignmentDescription, Instances.Student, Instances.Comment,
                Instances.Filepaths);
            var assignmentid = assignment.ID;

            // Act
            assignment.Remove();

            // Assert
            try
            {
                Assignment.GetByID(assignmentid);
                Assert.Fail();
            }
            catch (InvalidIDException) { }
        }
        [TestMethod]
        public void RemoverRemoveAssignmentGrade_ValidParameters_AssignmentGradeRemoved()
        {
            // Arrange
            var assignment = Assignment.New(Instances.AssignmentDescription, Instances.Student, Instances.Comment,
                Instances.Filepaths);
            var assignmentGrade = AssignmentGrade.New(Instances.Grade, Instances.Comment, assignment);
            var assignmentGradeid = assignmentGrade.ID;

            // Act
            assignmentGrade.Remove();

            // Assert
            try
            {
                AssignmentGrade.GetByID(assignmentGradeid);
                Assert.Fail();
            }
            catch (InvalidIDException) { }
        }
        [TestMethod]
        public void RemoverRemoveCourseGrade_ValidParameters_CourseGradeRemoved()
        {
            // Arrange
            var courseGrade = CourseGrade.New(Instances.Course, Instances.Student, Instances.Grade, Instances.Comment);
            var courseGradeid = courseGrade.ID;

            // Act
            courseGrade.Remove();

            // Assert
            try
            {
                CourseGrade.GetByID(courseGradeid);
                Assert.Fail();
            }
            catch (InvalidIDException) { }
        }
    }
}
