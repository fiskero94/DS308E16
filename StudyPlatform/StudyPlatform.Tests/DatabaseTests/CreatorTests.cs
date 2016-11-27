using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Model;
using StudyPlatform.Classes.Exceptions;
using System.Configuration;

namespace StudyPlatform.Tests.DatabaseTests
{
    [TestClass]
    public class CreatorTests
    {
        public CreatorTests()
        {
            Common.ResetTables();
        }

        [TestMethod]
        public void CreatorCreateStudent_ValidParameters_NoExceptionThrown() =>
            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);
        [TestMethod]
        public void CreatorCreateStudent_EmptyStringParameters_ArgumentExceptionThrown() =>
            Common.TestActionForExceptionThrown(new ArgumentException(),
            Creator.CreateStudent, "", "", "");
        [TestMethod]
        public void CreatorCreateStudent_NullParameters_ArgumentNullExceptionThrown() =>
            Common.TestActionForExceptionThrown(new ArgumentNullException(), 
            new Action<string, string, string>(Creator.CreateStudent), null, null, null);
        [TestMethod]
        public void CreatorCreateTeacher_ValidParameters_NoExceptionThrown() =>
            Creator.CreateTeacher(Instances.Name, Instances.Username, Instances.Password);
        [TestMethod]
        public void CreatorCreateTeacher_EmptyStringParameters_ArgumentExceptionThrown() =>
            Common.TestActionForExceptionThrown(new ArgumentException(),
            Creator.CreateTeacher, "", "", "");
        [TestMethod]
        public void CreatorCreateTeacher_NullParameters_ArgumentNullExceptionThrown() =>
            Common.TestActionForExceptionThrown(new ArgumentNullException(),
            new Action<string, string, string>(Creator.CreateTeacher), null, null, null);
        [TestMethod]
        public void CreatorCreateSecretary_ValidParameters_NoExceptionThrown() =>
            Creator.CreateSecretary(Instances.Name, Instances.Username, Instances.Password);
        [TestMethod]
        public void CreatorCreateSecretary_EmptyStringParameters_ArgumentExceptionThrown() =>
            Common.TestActionForExceptionThrown(new ArgumentException(),
            Creator.CreateSecretary, "", "", "");
        [TestMethod]
        public void CreatorCreateSecretary_NullParameters_ArgumentNullExceptionThrown() =>
            Common.TestActionForExceptionThrown(new ArgumentNullException(),
            new Action<string, string, string>(Creator.CreateSecretary), null, null, null);
        [TestMethod]
        public void CreatorCreateMessage_ValidParameters_NoExceptionThrown()
        {
            List<Person> recipients = Instances.Recipients;
            recipients.Add(Instances.Secretary);
            Creator.CreateMessage(Instances.Secretary, Instances.Title, 
            Instances.Text, recipients, Instances.Filepaths);
        }
        [TestMethod]
        public void CreatorCreateMessage_EmptyStringParameters_ArgumentExceptionThrown() =>
            Common.TestActionForExceptionThrown(new ArgumentException(),
            new Action<Person, string, string, List<Person>, List<string>>(Creator.CreateMessage), 
            Instances.Student, "", "", Instances.Recipients, Instances.Filepaths);
        [TestMethod]
        public void CreatorCreateMessage_NullParameters_ArgumentNullExceptionThrown() =>
            Common.TestActionForExceptionThrown(new ArgumentNullException(),
            new Action<Person, string, string, List<Person>, List<string>>(Creator.CreateMessage),
            null, null, null, null, null);
        [TestMethod]
        public void CreatorCreateMessage_NoRecipients_NoRecipientsExceptionThrown() =>
            Common.TestActionForExceptionThrown(new NoRecipientsException(),
            new Action<Person, string, string, List<Person>, List<string>>(Creator.CreateMessage), 
            Instances.Student, Instances.Title, Instances.Text, Instances.Recipients, Instances.Filepaths);
        [TestMethod]
        public void CreatorCreateNews_ValidParameters_NoExceptionThrown() =>
            Creator.CreateNews(Instances.Secretary, Instances.Title, Instances.Text);
        [TestMethod]
        public void CreatorCreateNews_EmptyStringParameters_ArgumentExceptionThrown() =>
            Common.TestActionForExceptionThrown(new ArgumentException(),
            Creator.CreateNews, Instances.Secretary, "", "");
        [TestMethod]
        public void CreatorCreateNews_NullParameters_ArgumentNullExceptionThrown() =>
            Common.TestActionForExceptionThrown(new ArgumentNullException(),
            new Action<Secretary, string, string>(Creator.CreateNews), null, null, null);
        [TestMethod]
        public void CreatorCreateCourse_ValidParameters_NoExceptionThrown() =>
            Creator.CreateCourse(Instances.Name, Instances.Description);
        [TestMethod]
        public void CreatorCreateCourse_EmptyStringParameters_ArgumentExceptionThrown() =>
            Common.TestActionForExceptionThrown(new ArgumentException(),
            Creator.CreateCourse, "", "");
        [TestMethod]
        public void CreatorCreateCourse_NullParameters_ArgumentNullExceptionThrown() =>
            Common.TestActionForExceptionThrown(new ArgumentNullException(),
            new Action<string, string>(Creator.CreateCourse), null, null);
        [TestMethod]
        public void CreatorCreateLesson_ValidParameters_NoExceptionThrown()
        {
            // Dependencies: CreateCourse
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Course.GetLatest(1).Single();
            Creator.CreateLesson(course, Instances.Description, Instances.Online, Instances.Date, Instances.Rooms, Instances.Filepaths);
        } 
        [TestMethod]
        public void CreatorCreateLesson_NullParameters_ArgumentNullExceptionThrown() =>
            Common.TestActionForExceptionThrown(new ArgumentNullException(),
            new Action<Course, string, bool, DateTime, List<Room>, List<string>>(Creator.CreateLesson),
            null, null, true, DateTime.Now, null, null);
        [TestMethod]
        public void CreatorCreateRoom_ValidParameters_InputNameEqualsRoomName() =>
            Creator.CreateRoom(Instances.Name);
        [TestMethod]
        public void CreatorCreateRoom_EmptyStringParameters_ArgumentExceptionThrown() =>
            Common.TestActionForExceptionThrown(new ArgumentException(),
            Creator.CreateRoom, "");
        [TestMethod]
        public void CreatorCreateRoom_NullParameters_ArgumentNullExceptionThrown() =>
            Common.TestActionForExceptionThrown(new ArgumentNullException(),
            new Action<string>(Creator.CreateRoom), null);
        [TestMethod]
        public void CreatorCreateAssignmentDescription_ValidParameters_NoExceptionThrown()
        {
            // Dependencies: CreateCourse
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Course.GetLatest(1).Single();
            Creator.CreateAssignmentDescription(course,
                Instances.Description, Instances.Date, Instances.Filepaths);
        }  
        [TestMethod]
        public void CreatorCreateAssignmentDescription_NullParameters_ArgumentNullExceptionThrown() =>
            Common.TestActionForExceptionThrown(new ArgumentNullException(),
            new Action<Course, string, DateTime, List<string>>(Creator.CreateAssignmentDescription),
            null, null, DateTime.Now, null);
        [TestMethod]
        public void CreatorCreateAssignment_ValidParameters_NoExceptionThrown()
        {
            // Dependencies: CreateAssignmentDescription, CreateCourse, CreateStudent
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Course.GetLatest(1).Single();
            Creator.CreateAssignmentDescription(course, Instances.Description, 
                Instances.Date, Instances.Filepaths);
            AssignmentDescription assignmentDescription = AssignmentDescription.GetLatest(1).Single();
            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);
            Student student = Student.GetLatest(1).Single();
            Creator.CreateAssignment(assignmentDescription,
                student, Instances.Comment, Instances.Filepaths);
        }
        [TestMethod]
        public void CreatorCreateAssignment_NullParameters_ArgumentNullExceptionThrown() =>
            Common.TestActionForExceptionThrown(new ArgumentNullException(),
            new Action<AssignmentDescription, Student, string, List<string>>(Creator.CreateAssignment),
            null, null, null, null);
        [TestMethod]
        public void CreatorCreateAssignmentGrade_ValidParameters_NoExceptionThrown() =>
            Creator.CreateAssignmentGrade(Instances.Grade, Instances.Comment, Instances.Assignment);
        [TestMethod]
        public void CreatorCreateAssignmentGrade_NullParameters_ArgumentNullExceptionThrown() =>
            Common.TestActionForExceptionThrown(new ArgumentNullException(),
            new Action<string, string, Assignment>(Creator.CreateAssignmentGrade),
            null, null, null);
        [TestMethod]
        public void CreatorCreateAssignmentGrade_11AsGrade_InvalidGradeExceptionThrown() =>
            Common.TestActionForExceptionThrown(new InvalidGradeException(),
            Creator.CreateAssignmentGrade,
            "11", Instances.Comment, Instances.Assignment);
        [TestMethod]
        public void CreatorCreateCourseGrade_ValidParameters_NoExceptionThrown()
        {
            // Dependencies: CreateCourse
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Course.GetLatest(1).Single();
            Creator.CreateCourseGrade(course, Instances.Student, Instances.Grade,
                Instances.Comment);
        }  
        [TestMethod]
        public void CreatorCreateCourseGrade_NullParameters_ArgumentNullExceptionThrown() =>
            Common.TestActionForExceptionThrown(new ArgumentNullException(),
            new Action<Course, Student, string, string>(Creator.CreateCourseGrade),
            null, null, null, null);
        [TestMethod]
        public void CreatorCreateCourseGrade_11AsGrade_InvalidGradeExceptionThrown() =>
            Common.TestActionForExceptionThrown(new InvalidGradeException(),
            Creator.CreateCourseGrade,
            Instances.Course, Instances.Student, "11", Instances.Comment);
    }
}