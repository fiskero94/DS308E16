using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Model;
using StudyPlatform.Classes.Exceptions;

namespace StudyPlatform.Tests.DatabaseTests
{
    [TestClass]
    public class CreatorTests
    {
        public CreatorTests()
        {
            string name = Instances.Name;
            string username = Instances.Username;
            string password = Instances.Password;

            Creator.CreateStudent(name, username, password);
        }

        [TestMethod]
        public void CreatorCreateStudent_ValidParameters_NoExceptionThrown()
        {
            // Arrange
            string name = Instances.Name;
            string username = Instances.Username;
            string password = Instances.Password;

            // Act
            Creator.CreateStudent(name, username, password);
        }
        [TestMethod]
        public void CreatorCreateStudent_EmptyStringParameters_ArgumentExceptionThrown()
        {
            // Arrange
            string name = Instances.EmptyString;
            string username = Instances.EmptyString;
            string password = Instances.EmptyString;

            // Act & Assert
            try
            {
                Creator.CreateStudent(name, username, password);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentException))
                    Assert.Fail(); // Exception thrown is not an ArgumentException
            }
        }
        [TestMethod]
        public void CreatorCreateStudent_NullParameters_ArgumentNullExceptionThrown()
        {
            // Arrange
            string name = null;
            string username = null;
            string password = null;

            // Act & Assert
            try
            {
                Creator.CreateStudent(name, username, password);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentNullException))
                    Assert.Fail(); // Exception thrown is not an ArgumentNullException
            }
        }
        [TestMethod]
        public void CreatorCreateTeacher_ValidParameters_NoExceptionThrown()
        {
            // Arrange
            string name = Instances.Name;
            string username = Instances.Username;
            string password = Instances.Password;

            // Act
            Creator.CreateTeacher(name, username, password);
        }
        [TestMethod]
        public void CreatorCreateTeacher_EmptyStringParameters_ArgumentExceptionThrown()
        {
            // Arrange
            string name = Instances.EmptyString;
            string username = Instances.EmptyString;
            string password = Instances.EmptyString;

            // Act & Assert
            try
            {
                Creator.CreateTeacher(name, username, password);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentException))
                    Assert.Fail(); // Exception thrown is not an ArgumentException
            }
        }
        [TestMethod]
        public void CreatorCreateTeacher_NullParameters_ArgumentNullExceptionThrown()
        {
            // Arrange
            string name = null;
            string username = null;
            string password = null;

            // Act & Assert
            try
            {
                Creator.CreateTeacher(name, username, password);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentNullException))
                    Assert.Fail(); // Exception thrown is not an ArgumentNullException
            }
        }
        [TestMethod]
        public void CreatorCreateSecretary_ValidParameters_NoExceptionThrown()
        {
            // Arrange
            string name = Instances.Name;
            string username = Instances.Username;
            string password = Instances.Password;

            // Act
            Creator.CreateSecretary(name, username, password);
        }
        [TestMethod]
        public void CreatorCreateSecretary_EmptyStringParameters_ArgumentExceptionThrown()
        {
            // Arrange
            string name = Instances.EmptyString;
            string username = Instances.EmptyString;
            string password = Instances.EmptyString;

            // Act & Assert
            try
            {
                Creator.CreateSecretary(name, username, password);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentException))
                    Assert.Fail(); // Exception thrown is not an ArgumentException
            }
        }
        [TestMethod]
        public void CreatorCreateSecretary_NullParameters_ArgumentNullExceptionThrown()
        {
            // Arrange
            string name = null;
            string username = null;
            string password = null;

            // Act & Assert
            try
            {
                Creator.CreateSecretary(name, username, password);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentNullException))
                    Assert.Fail(); // Exception thrown is not an ArgumentNullException
            }
        }
        [TestMethod]
        public void CreatorCreateMessage_ValidParameters_NoExceptionThrown()
        {
            // Arrange
            Person sender = Instances.Student;
            string title = Instances.Title;
            string text = Instances.Text;
            List<Person> recipients = new List<Person>();
            recipients.Add(sender);
            List<string> filepaths = new List<string>();

            // Act
            Creator.CreateMessage(sender, title, text, recipients, filepaths);
        }
        [TestMethod]
        public void CreatorCreateMessage_EmptyStringParameters_ArgumentExceptionThrown()
        {
            // Arrange
            Person sender = Instances.Student;
            string title = Instances.EmptyString;
            string text = Instances.EmptyString;
            List<Person> recipients = new List<Person>();
            recipients.Add(sender);
            List<string> filepaths = new List<string>();

            // Act & Assert
            try
            {
                Creator.CreateMessage(sender, title, text, recipients, filepaths);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentException))
                    Assert.Fail(); // Exception thrown is not an ArgumentException
            }
        }
        [TestMethod]
        public void CreatorCreateMessage_NullParameters_ArgumentNullExceptionThrown()
        {
            // Arrange
            Person sender = null;
            string title = null;
            string text = null;
            List<Person> recipients = null;
            List<string> filepaths = null;

            // Act & Assert
            try
            {
                Creator.CreateMessage(sender, title, text, recipients, filepaths);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentNullException))
                    Assert.Fail(); // Exception thrown is not an ArgumentNullException
            }
        }
        // TODO: Add No Recipients
        [TestMethod]
        public void CreatorCreateNews_ValidParameters_NoExceptionThrown()
        {
            // Arrange
            Person author = Instances.Secretary;
            string title = Instances.Title;
            string text = Instances.Text;

            // Act
            Creator.CreateNews(author, title, text);
        }
        [TestMethod]
        public void CreatorCreateNews_EmptyStringParameters_ArgumentExceptionThrown()
        {
            // Arrange
            Person author = Instances.Secretary;
            string title = Instances.EmptyString;
            string text = Instances.EmptyString;

            // Act & Assert
            try
            {
                Creator.CreateNews(author, title, text);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentException))
                    Assert.Fail(); // Exception thrown is not an ArgumentException
            }
        }
        [TestMethod]
        public void CreatorCreateNews_NullParameters_ArgumentNullExceptionThrown()
        {
            // Arrange
            Person author = null;
            string title = null;
            string text = null;

            // Act & Assert
            try
            {
                Creator.CreateNews(author, title, text);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentNullException))
                    Assert.Fail(); // Exception thrown is not an ArgumentNullException
            }
        }
        [TestMethod]
        public void CreatorCreateNews_StudentAsAuthor_ArgumentNotSecretaryExceptionThrown()
        {
            // Arrange
            Person author = Instances.Student;
            string title = Instances.Title;
            string text = Instances.Text;

            // Act & Assert
            try
            {
                Creator.CreateNews(author, title, text);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentNotSecretaryException))
                    Assert.Fail(); // Exception thrown is not an ArgumentNullException
            }
        }
        [TestMethod]
        public void CreatorCreateCourse_ValidParameters_NoExceptionThrown()
        {
            // Arrange
            string name = Instances.Name;
            string description = Instances.Description;

            // Act
            Creator.CreateCourse(name, description);
        }
        [TestMethod]
        public void CreatorCreateCourse_EmptyStringParameters_ArgumentExceptionThrown()
        {
            // Arrange
            string name = Instances.EmptyString;
            string description = Instances.EmptyString;

            // Act & Assert
            try
            {
                Creator.CreateCourse(name, description);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentException))
                    Assert.Fail(); // Exception thrown is not an ArgumentException
            }
        }
        [TestMethod]
        public void CreatorCreateCourse_NullParameters_ArgumentNullExceptionThrown()
        {
            // Arrange
            string name = null;
            string description = null;

            // Act & Assert
            try
            {
                Creator.CreateCourse(name, description);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentNullException))
                    Assert.Fail(); // Exception thrown is not an ArgumentNullException
            }
        }
        [TestMethod]
        public void CreatorCreateLesson_ValidParameters_NoExceptionThrown()
        {
            // Arrange
            DateTime date = Instances.Date;
            string description = Instances.Description;
            bool online = Instances.Online;
            bool active = Instances.Active;
            List<Room> rooms = new List<Room>();
            List<string> filepaths = new List<string>();
            Course course = Instances.Course;

            // Act
            Creator.CreateLesson(date, description, online, active, rooms, filepaths, course);
        }
        [TestMethod]
        public void CreatorCreateLesson_NullParameters_ArgumentNullExceptionThrown()
        {
            // Arrange
            DateTime date = Instances.Date;
            string description = null;
            bool online = Instances.Online;
            bool active = Instances.Active;
            List<Room> rooms = null;
            List<string> filepaths = null;
            Course course = null;

            // Act & Assert
            try
            {
                Creator.CreateLesson(date, description, online, active, rooms, filepaths, course);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentNullException))
                    Assert.Fail(); // Exception thrown is not an ArgumentNullException
            }
        }
        [TestMethod]
        public void CreatorCreateRoom_ValidParameters_InputNameEqualsRoomName()
        {
            // Arrange
            string name = Instances.Name;

            // Act
            Creator.CreateRoom(name);
        }
        [TestMethod]
        public void CreatorCreateRoom_EmptyStringParameters_ArgumentExceptionThrown()
        {
            // Arrange
            string name = Instances.EmptyString;

            // Act & Assert
            try
            {
                Creator.CreateRoom(name);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentException))
                    Assert.Fail(); // Exception thrown is not an ArgumentException
            }
        }
        [TestMethod]
        public void CreatorCreateRoom_NullParameters_ArgumentNullExceptionThrown()
        {
            // Arrange
            string name = null;

            // Act & Assert
            try
            {
                Creator.CreateRoom(name);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentNullException))
                    Assert.Fail(); // Exception thrown is not an ArgumentNullException
            }
        }
        [TestMethod]
        public void CreatorCreateAssignmentDescription_ValidParameters_NoExceptionThrown()
        {
            // Arrange
            Course course = Instances.Course;
            string description = Instances.Description;
            DateTime deadline = Instances.Date;
            List<string> filepaths = new List<string>();

            // Act
            Creator.CreateAssignmentDescription(course, description, deadline, filepaths);
        }
        [TestMethod]
        public void CreatorCreateAssignmentDescription_NullParameters_ArgumentNullExceptionThrown()
        {
            // Arrange
            Course course = null;
            string description = null;
            DateTime deadline = Instances.Date;
            List<string> filepaths = null;

            // Act & Assert
            try
            {
                Creator.CreateAssignmentDescription(course, description, deadline, filepaths);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentNullException))
                    Assert.Fail(); // Exception thrown is not an ArgumentNullException
            }
        }
        [TestMethod]
        public void CreatorCreateAssignment_ValidParameters_NoExceptionThrown()
        {
            // Arrange
            AssignmentDescription assignmentDescription = Instances.AssignmentDescription;
            Student student = Instances.Student;
            string comment = "comment";
            List<string> filepaths = new List<string>();

            // Act
            Creator.CreateAssignment(assignmentDescription, student, comment, filepaths);
        }
        [TestMethod]
        public void CreatorCreateAssignment_NullParameters_ArgumentNullExceptionThrown()
        {
            // Arrange
            AssignmentDescription assignmentDescription = null;
            Student student = null;
            string comment = null;
            List<string> filepaths = null;

            // Act & Assert
            try
            {
                Creator.CreateAssignment(assignmentDescription, student, comment, filepaths);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentNullException))
                    Assert.Fail(); // Exception thrown is not an ArgumentNullException
            }
        }
        [TestMethod]
        public void CreatorCreateAssignmentGrade_ValidParameters_NoExceptionThrown()
        {
            // Arrange
            string grade = Instances.Grade;
            string comment = Instances.Comment;
            Assignment assignment = Instances.Assignment;

            // Act
            Creator.CreateAssignmentGrade(grade, comment, assignment);
        }
        [TestMethod]
        public void CreatorCreateAssignmentGrade_NullParameters_ArgumentNullExceptionThrown()
        {
            // Arrange
            string grade = null;
            string comment = null;
            Assignment assignment = null;

            // Act & Assert
            try
            {
                Creator.CreateAssignmentGrade(grade, comment, assignment);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentNullException))
                    Assert.Fail(); // Exception thrown is not an ArgumentNullException
            }
        }
        [TestMethod]
        public void CreatorCreateAssignmentGrade_11AsGrade_InvalidGradeExceptionThrown()
        {
            // Arrange
            string grade = "11";
            string comment = Instances.Comment;
            Assignment assignment = Instances.Assignment;

            // Act & Assert
            try
            {
                Creator.CreateAssignmentGrade(grade, comment, assignment);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is InvalidGradeException))
                    Assert.Fail(); // Exception thrown is not an InvalidGradeException
            }
        }
        [TestMethod]
        public void CreatorCreateCourseGrade_ValidParameters_NoExceptionThrown()
        {
            // Arrange
            string grade = Instances.Grade;
            string comment = Instances.Comment;
            Course course = Instances.Course;
            Student student = Instances.Student;

            // Act
            Creator.CreateCourseGrade(grade, comment, course, student);
        }
        [TestMethod]
        public void CreatorCreateCourseGrade_NullParameters_ArgumentNullExceptionThrown()
        {
            // Arrange
            Course course = null;
            Student student = null;
            string grade = null;
            string comment = null;

            // Act & Assert
            try
            {
                Creator.CreateCourseGrade(grade, comment, course, student);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentNullException))
                    Assert.Fail(); // Exception thrown is not an ArgumentNullException
            }
        }
        [TestMethod]
        public void CreatorCreateCourseGrade_11AsGrade_InvalidGradeExceptionThrown()
        {
            // Arrange
            string grade = "11";
            string comment = Instances.Comment;
            Course course = Instances.Course;
            Student student = Instances.Student;

            // Act & Assert
            try
            {
                Creator.CreateCourseGrade(grade, comment, course, student);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is InvalidGradeException))
                    Assert.Fail(); // Exception thrown is not an InvalidGradeException
            }
        }
    }
}