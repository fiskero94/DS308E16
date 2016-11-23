using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Model;
using System.Linq;

namespace StudyPlatform.Tests.DatabaseTests
{
    [TestClass]
    public class ListsTests
    {
        public ListsTests()
        {
            Common.ResetTables();
        }

        private Message actualMessage;
        private Student actualStudent;
        private Person actualPerson;
        private Teacher actualTeacher;
        private Secretary actualSecretary;
        
        [TestMethod]
        public void ListsPersons_ListParametersFilled_ContainsPersonsData()
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
        public void ListsStudents_ListParametersFilled_ContainsStudentsData()
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
        public void ListsTeachers_ListParametersFilled_ContainsTeachersData()
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
        public void ListsSecretaries_ListParametersFilled_ContainsSecretariesData()
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
        [TestMethod]
        public void ListsMessages_ListsParametersFilled_ContainsMessagesData()
        {
            // Arrange
            List<Person> recipients = Lists.Persons;
            List<string> filepaths = new List<string>();
            filepaths.Add("");

            Creator.CreateMessage(Getters.GetPersonByID(1), Instances.Title, Instances.Text, recipients, filepaths);

            List<Message> messages = Lists.Messages;

            uint id = Instances.ID;
            uint _id = Instances.ID;
            string title = Instances.Title;
            string text = Instances.Text;
            Message message = new Message(id, _id, title, text);

            foreach (Message item in messages)
            {
                if (item.Title == "Title")
                {
                    actualMessage = item;
                    break;
                }
            }

            // Act
            Assert.AreEqual(message.Title, actualMessage.Title);
            Assert.AreEqual(message.ID, actualMessage.ID);
        }
        [TestMethod]
        public void ListsNews_ListParametersFilled_ContainsNewsData()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void ListsCourses_ListParametersFilled_ContainsCoursesData()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void ListsLessons_ListParametersFilled_ContainsLessonsData()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void ListsRooms_ListParametersFilled_ContainsRoomsData()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void ListsAssignmentDescriptions_ListParametersFilled_ContainsAssignmentDescriptionsData()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void ListsAssignments_ListParametersFilled_ContainsAssignmentsData()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void ListsAssignmentGrades_ListParametersFilled_ContainsAssignmentGradesData()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void ListsCourseGrades_ListParametersFilled_ContainsCourseGradesData()
        {
            throw new NotImplementedException();
        }
    }
}
