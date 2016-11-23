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
        public ListsTests()
        {
            Common.ResetTables();
        }

        Student actualStudent;
        Person actualPerson;
        Teacher actualTeacher;
        Secretary actualSecretary;

        [TestMethod]
        public void ListsPersons_ListParametersFilled_ContainsPersonDataFromDatabase()
        {
            // Arrange
            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);
            List<Person> persons = Lists.Persons;

            uint id = Instances.ID;
            string name = Instances.Name;
            Student student = new Student(id, name);

            foreach (Person item in persons)
            {
                if (item.Name == "Name")
                {
                    actualPerson = item;
                    break;
                }
            }

            // Act
            Assert.AreEqual(student.Name, actualPerson.Name);
            Assert.AreEqual(student.ID, actualPerson.ID);

        }

        [TestMethod]
        public void ListsStudents_ListParametersFilled_ContainsStudentDataFromDatabase()
        {
            // Arrange
            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);
            List<Student> students = Lists.Students;

            uint id = Instances.ID;
            string name = Instances.Name;
            Student student = new Student(id, name);

            foreach (Student item in students)
            {
                if (item.Name == "Name")
                    actualStudent = item;
                break;
            }

            // Act
            Assert.AreEqual(student.Name, actualStudent.Name);
            Assert.AreEqual(student.ID, actualStudent.ID);
        }

        [TestMethod]
        public void ListsTeachers_ListParametersFilled_ContainsTeacherDataFromDatabase()
        {
            // Arrange
            Creator.CreateTeacher(Instances.Name, Instances.Username, Instances.Password);
            List<Teacher> teachers = Lists.Teachers;

            uint id = Instances.ID;
            string name = Instances.Name;
            Teacher teacher = new Teacher(id, name);

            foreach (Teacher item in teachers)
            {
                if (item.Name == "Name")
                    actualTeacher = item;
                break;
            }

            // Act
            Assert.AreEqual(teacher.Name, actualTeacher.Name);
            Assert.AreEqual(teacher.ID, actualTeacher.ID);
        }

        [TestMethod]
        public void ListsSecretaries_ListParametersFilled_ContainsSecretaryDataFromDatabase()
        {
            // Arrange
            Creator.CreateSecretary(Instances.Name, Instances.Username, Instances.Password);
            List<Secretary> secretaries = Lists.Secretaries;

            uint id = Instances.ID;
            string name = Instances.Name;
            Secretary secretary = new Secretary(id, name);

            foreach (Secretary item in secretaries)
            {
                if (item.Name == "Name")
                    actualSecretary = item;
            }

            // Act
            Assert.AreEqual(secretary.Name, actualSecretary.Name);
            Assert.AreEqual(secretary.ID, actualSecretary.ID);
        }

    }
}
