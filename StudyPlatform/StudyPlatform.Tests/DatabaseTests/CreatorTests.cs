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
        [TestMethod]
        public void CreatorCreateStudent_ValidParameters_InputNameEqualsStudentName()
        {
            // Arrange
            string name = "John Doe";
            string username = "johndoe";
            string password = "1234";

            // Act
            Creator.CreateStudent(name, username, password);

            // Assert
            Student student = Getters.GetLatestPersons(1).Single() as Student;
            Assert.IsTrue(name.Equals(student.Name));
        }
        [TestMethod]
        public void CreatorCreateStudent_EmptyStringParameters_ArgumentExceptionThrown()
        {
            // Arrange
            string name = "";
            string username = "";
            string password = "";

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
        public void CreatorCreateTeacher_ValidParameters_InputValuesEqualInstanceValues()
        {
            // Arrange
            string name = "John Doe";
            string username = "johndoe";
            string password = "1234";

            // Act
            Creator.CreateTeacher(name, username, password);

            // Assert
            Student student = Getters.GetLatestPersons(1).Single() as Student;
            Assert.IsTrue(name.Equals(student.Name));
        }
        [TestMethod]
        public void CreatorCreateTeacher_EmptyStringParameters_ArgumentExceptionThrown()
        {
            // Arrange
            string name = "";
            string username = "";
            string password = "";

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
        public void CreatorCreateSecretary_ValidParameters_InputNameEqualsStudentName()
        {
            // Arrange
            string name = "John Doe";
            string username = "johndoe";
            string password = "1234";

            // Act
            Creator.CreateSecretary(name, username, password);

            // Assert
            Student student = Getters.GetLatestPersons(1).Single() as Student;
            Assert.IsTrue(name.Equals(student.Name));
        }
        [TestMethod]
        public void CreatorCreateSecretary_EmptyStringParameters_ArgumentExceptionThrown()
        {
            // Arrange
            string name = "";
            string username = "";
            string password = "";

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
        public void CreatorCreateMessage_ValidParameters_InputNameEqualsStudentName()
        {
            // Arrange
            Creator.CreateStudent("name", "username", "password");
            Person sender = Getters.GetLatestPersons(1).Single();
            string title = "title";
            string text = "text";
            List<Person> recipients = new List<Person>();
            recipients.Add(sender);
            List<string> filepaths = new List<string>();
            filepaths.Add("filepath");

            // Act
            Creator.CreateMessage(sender, title, text, recipients, filepaths);

            // Assert
            Message message = Getters.GetLatestMessages(1).Single();
            Assert.IsTrue(message.Sender.Name.Equals(sender.Name));
            Assert.IsTrue(message.Title.Equals(title));
            Assert.IsTrue(message.Text.Equals(text));
            Assert.IsTrue(message.Recipients.First().Name.Equals("name"));
            Assert.IsTrue(message.Attachments.First().Equals("filepath"));
        }
        [TestMethod]
        public void CreatorCreateMessage_EmptyStringParameters_ArgumentExceptionThrown()
        {
            // Arrange
            Creator.CreateStudent("name", "username", "password");
            Person sender = Getters.GetLatestPersons(1).Single();
            string title = "";
            string text = "";
            List<Person> recipients = new List<Person>();
            recipients.Add(sender);
            List<string> filepaths = new List<string>();
            filepaths.Add("filepath");

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
        [TestMethod]
        public void CreatorCreateNews_ValidParameters_InputNameEqualsStudentName()
        {
            // Arrange
            Creator.CreateSecretary("name", "username", "password");
            Person author = Getters.GetLatestPersons(1).Single() as Secretary;
            string title = "title";
            string text = "text";

            // Act
            Creator.CreateNews(author, title, text);

            // Assert
            News news = Getters.GetLatestNews(1).Single();
            Assert.IsTrue(news.Author.Name.Equals(author.Name));
            Assert.IsTrue(news.Title.Equals(title));
            Assert.IsTrue(news.Text.Equals(text));
        }
        [TestMethod]
        public void CreatorCreateNews_EmptyStringParameters_ArgumentExceptionThrown()
        {
            // Arrange
            Creator.CreateSecretary("name", "username", "password");
            Person author = Getters.GetLatestPersons(1).Single() as Secretary;
            string title = "";
            string text = "";

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
            Creator.CreateStudent("name", "username", "password");
            Person author = Getters.GetLatestPersons(1).Single() as Student;
            string title = "title";
            string text = "text";

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
        public void CreatorCreateCourse_ValidParameters_InputNameEqualsStudentName()
        {
            // Arrange
            string name = "coursename";
            string description = "descriptivetext";

            // Act
            Creator.CreateCourse(name, description);

            // Assert
            Course course = Getters.GetLatestCourses(1).Single();
            Assert.IsTrue(name.Equals(course.Name));
            Assert.IsTrue(description.Equals(course.Description));
        }
        [TestMethod]
        public void CreatorCreateCourse_EmptyStringParameters_ArgumentExceptionThrown()
        {
            // Arrange
            string name = "";
            string description = "";

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
        public void CreatorCreateLesson_ValidParameters_InputNameEqualsStudentName()
        {
            // Arrange
            DateTime date = DateTime.Now;
            string description = "descriptivetext";
            bool online = true;
            bool active = true;
            List<Room> rooms = new List<Room>();
            List<string> filepaths = new List<string>();
            string courseName = "TestCourse";
            string courseDescription = "TestDescription";
            Creator.CreateCourse(courseName, courseDescription);
            Course course = Getters.GetLatestCourses(1).Single();

            // Act
            Creator.CreateLesson(date, description, online, active, rooms, filepaths, course);

            // Assert
            Lesson lesson = Getters.GetLatestLessons(1).Single();
            Assert.IsTrue(date.Equals(lesson.Date));
            Assert.IsTrue(description.Equals(lesson.Description));
            Assert.IsTrue(online.Equals(lesson.Online));
            Assert.IsTrue(active.Equals(lesson.Active));
            Assert.IsTrue(courseName.Equals(lesson.Course.Name));
            Assert.IsTrue(courseDescription.Equals(lesson.Course.Description));
        }
        [TestMethod]
        public void CreatorCreateLesson_NullParameters_ArgumentNullExceptionThrown()
        {
            // Arrange
            DateTime date = DateTime.Now;
            string description = null;
            bool online = true;
            bool active = true;
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
            string name = "G308";

            // Act
            Creator.CreateRoom(name);

            // Assert
            Room room = Getters.GetLatestRooms(1).Single();
            Assert.IsTrue(name.Equals(room.Name));
        }
        [TestMethod]
        public void CreatorCreateRoom_EmptyStringParameters_ArgumentExceptionThrown()
        {
            // Arrange
            string name = "";

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
        public void CreatorCreateAssignmentDescription_ValidParameters_InputNameEqualsStudentName()
        {
            // Arrange
            string courseName = "TestCourse";
            string courseDescription = "TestDescription";
            Creator.CreateCourse(courseName, courseDescription);
            Course course = Getters.GetLatestCourses(1).Single();
            string description = "descriptivetext";
            DateTime deadline = DateTime.Now;
            List<string> filepaths = new List<string>();

            // Act
            Creator.CreateAssignmentDescription(course, description, deadline, filepaths);

            // Assert
            AssignmentDescription assignmentDescription = Getters.GetLatestAssignmentDescriptions(1).Single();
            Assert.IsTrue(course.Name.Equals(assignmentDescription.Course.Name));
            Assert.IsTrue(course.Description.Equals(assignmentDescription.Course.Description));
            Assert.IsTrue(description.Equals(assignmentDescription.Description));
            Assert.IsTrue(deadline.Equals(assignmentDescription.Date));
        }
        [TestMethod]
        public void CreatorCreateAssignmentDescription_NullParameters_ArgumentNullExceptionThrown()
        {
            // Arrange
            Course course = null;
            string description = null;
            DateTime deadline = DateTime.Now;
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
        public void CreatorCreateAssignment_ValidParameters_InputNameEqualsStudentName()
        {
            // Arrange Dummy AssignmentDescription
            string courseName = "TestCourse";
            string courseDescription = "TestDescription";
            Creator.CreateCourse(courseName, courseDescription);
            Course course = Getters.GetLatestCourses(1).Single();
            string description = "descriptivetext";
            DateTime deadline = DateTime.Now;
            List<string> filepaths = new List<string>();
            Creator.CreateAssignmentDescription(course, description, deadline, filepaths);
            AssignmentDescription assignmentDescription = Getters.GetLatestAssignmentDescriptions(1).Single();

            // Arrange Dummy Student
            string name = "John Doe";
            string username = "johndoe";
            string password = "1234";
            Creator.CreateStudent(name, username, password);
            Student student = Getters.GetLatestPersons(1).Single() as Student;

            // Arrange
            string comment = "comment";

            // Act
            Creator.CreateAssignment(assignmentDescription, student, comment, filepaths);

            // Assert
            Assignment assignment = Getters.GetLatestAssignments(1).Single();
            Assert.IsTrue(assignmentDescription.Description.Equals(assignment.AssignmentDescription.Description));
            Assert.IsTrue(student.Name.Equals(assignment.Student.Name));
            Assert.IsTrue(comment.Equals(assignment.Comment));
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
        public void CreatorCreateAssignmentGrade_ValidParameters_InputNameEqualsStudentName()
        {
            // Arrange Dummy Assignment
            string courseName = "TestCourse";
            string courseDescription = "TestDescription";
            Creator.CreateCourse(courseName, courseDescription);
            Course course = Getters.GetLatestCourses(1).Single();
            string description = "descriptivetext";
            DateTime deadline = DateTime.Now;
            List<string> filepaths = new List<string>();
            Creator.CreateAssignmentDescription(course, description, deadline, filepaths);
            AssignmentDescription assignmentDescription = Getters.GetLatestAssignmentDescriptions(1).Single();
            string name = "John Doe";
            string username = "johndoe";
            string password = "1234";
            Creator.CreateStudent(name, username, password);
            Student student = Getters.GetLatestPersons(1).Single() as Student;
            string comment = "comment";
            Creator.CreateAssignment(assignmentDescription, student, comment, filepaths);
            Assignment assignment = Getters.GetLatestAssignments(1).Single();

            // Arrange
            string grade = "12";

            // Act
            Creator.CreateAssignmentGrade(grade, comment, assignment);

            // Assert
            AssignmentGrade assignmentGrade = Getters.GetLatestAssignmentGrades(1).Single();
            Assert.IsTrue(grade.Equals(assignment.Grade));
            Assert.IsTrue(comment.Equals(assignment.Comment));
            Assert.IsTrue(assignment.Comment.Equals(assignmentGrade.Assignment.Comment));
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
            // Arrange Dummy Assignment
            string courseName = "TestCourse";
            string courseDescription = "TestDescription";
            Creator.CreateCourse(courseName, courseDescription);
            Course course = Getters.GetLatestCourses(1).Single();
            string description = "descriptivetext";
            DateTime deadline = DateTime.Now;
            List<string> filepaths = new List<string>();
            Creator.CreateAssignmentDescription(course, description, deadline, filepaths);
            AssignmentDescription assignmentDescription = Getters.GetLatestAssignmentDescriptions(1).Single();
            string name = "John Doe";
            string username = "johndoe";
            string password = "1234";
            Creator.CreateStudent(name, username, password);
            Student student = Getters.GetLatestPersons(1).Single() as Student;
            string comment = "comment";
            Creator.CreateAssignment(assignmentDescription, student, comment, filepaths);
            Assignment assignment = Getters.GetLatestAssignments(1).Single();

            // Arrange
            string grade = "11";

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
        public void CreatorCreateCourseGrade_ValidParameters_InputNameEqualsStudentName()
        {
            // Arrange Dummy Course
            string courseName = "coursename";
            string description = "descriptivetext";
            Creator.CreateCourse(courseName, description);
            Course course = Getters.GetLatestCourses(1).Single();

            // Arrange Dummy Student
            string studentName = "John Doe";
            string username = "johndoe";
            string password = "1234";
            Creator.CreateStudent(studentName, username, password);
            Student student = Getters.GetLatestPersons(1).Single() as Student;

            // Arrange
            string grade = "12";
            string comment = "comment";

            // Act
            Creator.CreateCourseGrade(grade, comment, course, student);

            // Assert
            CourseGrade courseGrade = Getters.GetLatestCourseGrades(1).Single();
            Assert.IsTrue(grade.Equals(courseGrade.AssignedGrade));
            Assert.IsTrue(comment.Equals(courseGrade.Comment));
            Assert.IsTrue(courseName.Equals(courseGrade.Course.Name));
            Assert.IsTrue(studentName.Equals(courseGrade.Student.Name));
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
            // Arrange Dummy Course
            string courseName = "coursename";
            string description = "descriptivetext";
            Creator.CreateCourse(courseName, description);
            Course course = Getters.GetLatestCourses(1).Single();

            // Arrange Dummy Student
            string studentName = "John Doe";
            string username = "johndoe";
            string password = "1234";
            Creator.CreateStudent(studentName, username, password);
            Student student = Getters.GetLatestPersons(1).Single() as Student;

            // Arrange
            string grade = "11";
            string comment = "comment";

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