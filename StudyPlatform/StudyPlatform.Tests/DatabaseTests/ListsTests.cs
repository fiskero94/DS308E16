using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Model;
using System.Linq;

namespace StudyPlatform.Tests.ModelTests
{
    [TestClass]
    public class ListsTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: Add test logic here
            //
        }

        [TestMethod]
        public void ListsPersons_ListParametersFilled_ContainsPersonDataFromDatabase()
        {
            // Arrange
            List<Person> persons = new List<Person>();
            Query query = new Query("SELECT * FROM studyplatform.persons;");
            persons = Extractor.ExtractPersons(query.Execute());
            uint id = Instances.ID;
            string name = Instances.Name;
            string type = "student";

            // Act
            
        }

        [TestMethod]
        public void ListsStudents_ListParametersFilled_ContainsStudentData()
        {
            // Arrange
            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);
            List<Student> students = Lists.Students;

            uint id = Instances.ID;
            string name = Instances.Name;
            Student student = new Student(id, name);
            List<Student> expected = new List<Student>();

            expected.Add(student);

            // Act
            Assert.AreEqual(expected, students);
        }
    }
}
