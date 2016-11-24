using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Person person = Lists.Persons.Single();

            // Assert
            Assert.AreEqual(Instances.Name, person.Name);

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
            Secretary secretary = Lists.Secretaries.Single();

            // Assert
            Assert.AreEqual(Instances.Name, secretary.Name);
        }

        [TestMethod]
        public void ListsAssignmentDescriptions_ListParametersFilled_ContainsAssignmentDescriptionDataFromDatabase()
        {
            // Arrange
            List<string> filepaths = new List<string>();
            Creator.CreateAssignmentDescription(Instances.Course, Instances.Description, Instances.Date, filepaths);
            List<AssignmentDescription> assignmentdescriptions = Lists.AssignmentDescriptions;

            // Act
            AssignmentDescription assignmentdescription = Lists.AssignmentDescriptions.Single();

            // Assert
            Assert.AreEqual(Instances.ID, assignmentdescription.ID);
        }

    }
}
