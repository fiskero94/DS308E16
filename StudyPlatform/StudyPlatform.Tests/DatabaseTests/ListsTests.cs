﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Model;
using System.Collections.Generic;
using System.Linq;

namespace StudyPlatform.Tests.ModelTests
{
    [TestClass]
    public class ListsTests
    {
        public ListsTests()
        {
            Common.ResetTables();
        }

        [TestMethod]
        public void ListsCourseGrades_ListsParametersFilled_ContainsCourseGradesData()
        {

            // Arrange
            List<string> filepaths = new List<string>();

            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);
            Person person = Getters.GetLatestPersons(1).Single();

            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Getters.GetLatestCourses(1).Single();

            Creator.CreateAssignmentDescription(course, Instances.Description, Instances.Date, filepaths);
            AssignmentDescription assignmentdescription = Getters.GetLatestAssignmentDescriptions(1).Single();

            Creator.CreateAssignment(assignmentdescription, Instances.Student, Instances.Comment, filepaths);
            Assignment assignment = Getters.GetLatestAssignments(1).Single();

            Creator.CreateAssignmentGrade(Instances.Grade, Instances.Comment, assignment);

            // Act
            AssignmentGrade assignmentgrade = Lists.AssignmentGrades.Single();

            // Assert
            Assert.AreEqual(Instances.Comment, assignmentgrade.Comment);
        }

        [TestMethod]
        public void ListsAssignmentGrades_ListsParametersFilled_ContainsAssignmentGradesData()
        {
            // Arrange

            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);
            Student student = Getters.GetLatestPersons(1).Single() as Student;

            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Getters.GetLatestCourses(1).Single();


            Creator.CreateCourseGrade(Instances.Grade, Instances.Comment, course, student);

            // Act
            CourseGrade coursegrade = Lists.CourseGrades.Single();

            // Assert
            Assert.AreEqual(Instances.Comment, coursegrade.Comment);
        }

        [TestMethod]
        public void ListsNews_ListsParametersFilled_ContainsNewsData()
        {
            // Arrange

            Creator.CreateSecretary(Instances.Name, Instances.Username, Instances.Password);
            Secretary secretary = Getters.GetLatestPersons(1).Single() as Secretary;
            Creator.CreateNews(secretary, Instances.Title, Instances.Text);
            // Act
            News news = Lists.News.Single();

            // Assert
            Assert.AreEqual(Instances.Title, news.Title);
        }

        [TestMethod]
        public void ListsMessage_ListsParametersFilled_ContainsMessageData()
        {
            // Arrange
            List<Person> recipients = Lists.Persons;
            List<string> filepaths = new List<string>();


            Creator.CreateMessage(Getters.GetPersonByID(1), Instances.Title, Instances.Text, recipients, filepaths);
            // Act

            Message message = Lists.Messages.Single();

            // Assert
            Assert.AreEqual(Instances.Title, message.Title);
            Assert.AreEqual(Instances.Text, message.Text);
        }
        [TestMethod]
        public void ListsPersons_ListParametersFilled_ContainsPersonDataFromDatabase()
        {

            // Arrange
            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);

            // Act
            List<Person> person = Lists.Persons;

            // Assert
            Assert.AreEqual("Admin", person[0].Name);
            Assert.AreEqual(Instances.Name, person[1].Name);

        }
        [TestMethod]
        public void ListsStudents_ListParametersFilled_ContainsStudentDataFromDatabase()
        {
            // Arrange
            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);

            // Act
            Student student = Lists.Students.Single();

            // Assert
            Assert.AreEqual(Instances.Name, student.Name);
        }
        [TestMethod]
        public void ListsTeachers_ListParametersFilled_ContainsTeacherDataFromDatabase()
        {

            // Arrange
            Creator.CreateTeacher(Instances.Name, Instances.Username, Instances.Password);

            // Act
            Teacher teacher = Lists.Teachers.Single();

            // Assert
            Assert.AreEqual(Instances.Name, teacher.Name);
        }
        [TestMethod]
        public void ListsSecretaries_ListParametersFilled_ContainsSecretaryDataFromDatabase()
        {
            // Arrange
            Creator.CreateSecretary(Instances.Name, Instances.Username, Instances.Password);

            // Act
            List<Secretary> secretary = Lists.Secretaries;

            // Assert
            Assert.AreEqual("Admin", secretary[0].Name);
            Assert.AreEqual(Instances.Name, secretary[1].Name);
        }

        [TestMethod]
        public void ListsAssignmentDescriptions_ListParametersFilled_ContainsAssignmentDescriptionDataFromDatabase()
        {
            // Arrange
            List<string> filepaths = new List<string>();

            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Getters.GetLatestCourses(1).Single();
            Creator.CreateAssignmentDescription(course, Instances.Description, Instances.Date, filepaths);

            // Act
            AssignmentDescription assignmentdescription = Lists.AssignmentDescriptions.Single();

            // Assert
            Assert.AreEqual(Instances.Description, assignmentdescription.Description);
        }

        [TestMethod]
        public void ListsCourse_ListParametersFilled_ContainsCourseDataFromDatabase()
        {
            // Arrange
            Creator.CreateCourse(Instances.Name, Instances.Description);

            // Act
            Course course = Getters.GetLatestCourses(1).Single();

            // Assert
            Assert.AreEqual(Instances.Description, course.Description);
        }

        public void ListsRoom_ListParameterFilled_ContainsCourseDataFromDatabase()
        {
            // Arrange
            Creator.CreateRoom(Instances.Name);

            // Act
            Room room = Getters.GetLatestRooms(1).Single();

            // Assert
            Assert.AreEqual(Instances.Name, room.Name);
        }

        public void ListsLessons()
        {
            // Arrange
            List<Room> rooms = new List<Room>();
            List<string> filepaths = new List<string>();

            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Getters.GetLatestCourses(1).Single();

            Creator.CreateLesson(Instances.Date, Instances.Description, Instances.Online,
                Instances.Active, rooms, filepaths, course);

            // Act
            Lesson lesson = Getters.GetLatestLessons(1).Single();

            // Assert
            Assert.AreEqual(Instances.Description, lesson.Description);
            Assert.AreEqual(course, lesson.Course);
        }

        public void ListsAssignment()
        {
            // Arrange
            List<string> filepaths = new List<string>();

            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Getters.GetLatestCourses(1).Single();

            Creator.CreateAssignmentDescription(course, Instances.Description, Instances.Date, filepaths);
            AssignmentDescription assignmentdescription = Getters.GetLatestAssignmentDescriptions(1).Single();

            Creator.CreateAssignment(assignmentdescription, Instances.Student, Instances.Comment, filepaths);

            // Act
            Assignment assignment = Getters.GetLatestAssignments(1).Single();

            // Assert
            Assert.AreEqual(Instances.Comment, assignment.Comment);
            Assert.AreEqual(course.AssignmentDescription, assignment.AssignmentDescription);
        }
    }
}
