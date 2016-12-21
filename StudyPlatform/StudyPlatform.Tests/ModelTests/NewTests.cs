using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyPlatform.Classes.Model;

namespace StudyPlatform.Tests.ModelTests
{
    [TestClass]
    public class NewTests
    {
        public NewTests()
        {
            WebHelper.ResetTables();
        }
        [TestMethod]
        public void AssignmentDescriptionNew_ValidParameters_ValuesMatch()
        {
            // Arrange
            var course = Course.New(Instances.Name, Instances.Description);

            // Act
            var assignmentDescription = AssignmentDescription.New(course, Instances.Title, Instances.Description, Instances.Date, Instances.Filepaths);

            // Assert
            Assert.AreEqual(course.ID, assignmentDescription.Course.ID);
            Assert.AreEqual(Instances.Title, assignmentDescription.Title);
            Assert.AreEqual(Instances.Description, assignmentDescription.Description);
            Assert.AreEqual(Instances.Date, assignmentDescription.Deadline);
        }
        [TestMethod]
        public void AssignmentNew_ValidParameters_ValuesMatch()
        {
            // Arrange
            var student = Student.New(Instances.Name, Instances.Username, Instances.Password);
            var assignmentDescription = AssignmentDescription.New(Instances.Course, Instances.Title, Instances.Description, Instances.Date, Instances.Filepaths);

            // Act
            var assignment = Assignment.New(assignmentDescription, student, Instances.Comment, Instances.Filepaths);

            // Assert
            Assert.AreEqual(student.ID, assignment.Student.ID);
            Assert.AreEqual(assignmentDescription.ID, assignment.AssignmentDescription.ID);
            Assert.AreEqual(Instances.Comment, assignment.Comment);
        }
        [TestMethod]
        public void AssignmentGradeNew_ValidParameters_ValuesMatch()
        {
            // Arrange
            var assignment = Assignment.New(Instances.AssignmentDescription, Instances.Student, Instances.Comment, Instances.Filepaths);

            // Act
            var assignmentGrade = AssignmentGrade.New(Instances.Grade, Instances.Comment, assignment);

            // Assert
            Assert.AreEqual(Instances.Grade, assignmentGrade.Grade);
            Assert.AreEqual(Instances.Comment, assignmentGrade.Comment);
            Assert.AreEqual(assignment.ID, assignmentGrade.Assignment.ID);
        }
        [TestMethod]
        public void CourseNew_ValidParameters_ValuesMatch()
        {
            // Act
            var course = Course.New(Instances.Name, Instances.Description);

            // Assert
            Assert.AreEqual(Instances.Name, course.Name);
            Assert.AreEqual(Instances.Description, course.Description);
        }
        [TestMethod]
        public void CourseGradeNew_ValidParameters_ValuesMatch()
        {
            // Arrange
            var course = Course.New(Instances.Name, Instances.Description);
            var student = Student.New(Instances.Name, Instances.Username, Instances.Password);

            // Act
            var courseGrade = CourseGrade.New(course, student, Instances.Grade, Instances.Comment);

            // Assert
            Assert.AreEqual(course.ID, courseGrade.Course.ID);
            Assert.AreEqual(student.ID, courseGrade.Student.ID);
            Assert.AreEqual(Instances.Grade, courseGrade.Grade);
            Assert.AreEqual(Instances.Comment, courseGrade.Comment);
        }
        [TestMethod]
        public void LessonNew_ValidParameters_ValuesMatch()
        {
            // Arrange
            var course = Course.New(Instances.Name, Instances.Description);

            // Act
            var lesson = Lesson.New(course, Instances.Description, Instances.Online, Instances.Date, Instances.Rooms,
                Instances.Filepaths);

            // Assert
            Assert.AreEqual(course.ID, lesson.Course.ID);
            Assert.AreEqual(Instances.Description, lesson.Description);
            Assert.AreEqual(Instances.Online, lesson.Online);
            Assert.AreEqual(Instances.Date, lesson.DateTime);
        }
        [TestMethod]
        public void MessageNew_ValidParameters_ValuesMatch()
        {
            // Arrange
            var student = Student.New(Instances.Name, Instances.Username, Instances.Password);
            var recipient = Student.New(Instances.Name, Instances.Username, Instances.Password);
            var recipients = new List<Person> { recipient };


            // Act
            var message = Message.New(student, Instances.Title, Instances.Text, recipients, Instances.Filepaths);

            // Assert
            Assert.AreEqual(student.ID, message.Sender.ID);
            Assert.AreEqual(Instances.Title, message.Title);
            Assert.AreEqual(Instances.Text, message.Text);
        }
        [TestMethod]
        public void NewsNew_ValidParameters_ValuesMatch()
        {
            // Arrange
            var secretary = Secretary.New(Instances.Name, Instances.Username, Instances.Password);

            // Act
            var news = News.New(secretary, Instances.Title, Instances.Text);

            // Assert
            Assert.AreEqual(secretary.ID, news.Author.ID);
            Assert.AreEqual(Instances.Title, news.Title);
            Assert.AreEqual(Instances.Text, news.Text);
        }
        [TestMethod]
        public void RoomNew_ValidParameters_ValuesMatch()
        {
            // Act
            var room = Room.New(Instances.Name);

            // Assert
            Assert.AreEqual(Instances.Name, room.Name);
        }
        [TestMethod]
        public void StudentNew_ValidParameters_ValuesMatch()
        {
            // Act
            var student = Student.New(Instances.Name, Instances.Username, Instances.Password);

            // Assert
            Assert.AreEqual(Instances.Name, student.Name);
        }
        [TestMethod]
        public void TeacherNew_ValidParameters_ValuesMatch()
        {
            // Act
            var teacher = Teacher.New(Instances.Name, Instances.Username, Instances.Password);

            // Assert
            Assert.AreEqual(Instances.Name, teacher.Name);
        }
        [TestMethod]
        public void SecretaryNew_ValidParameters_ValuesMatch()
        {
            // Act
            var secretary = Secretary.New(Instances.Name, Instances.Username, Instances.Password);

            // Assert
            Assert.AreEqual(Instances.Name, secretary.Name);
        }
    }
}
