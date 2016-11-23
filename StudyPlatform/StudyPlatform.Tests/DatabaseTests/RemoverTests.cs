using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Model;
using StudyPlatform.Classes.Exceptions;
using System.Linq;

namespace StudyPlatform.Tests.DatabaseTests
{
    [TestClass]
    public class RemoverTests
    {
        public RemoverTests()
        {
            Common.ResetTables();
        }

        [TestMethod]
        public void RemoverRemovePerson_StudentAsParameter_PersonRemoved()
        {
            //Commands.DeleteFrom("persons", "id=" + person.ID);

            //foreach (Message message in person.SentMessages)
            //    RemoveMessage(message);
            //foreach (Message message in person.RecievedMessages)
            //    Commands.DeleteFrom("messagerecipients" + message.ID, "personid=" + person.ID);

            //Commands.DropTable("personsentmessages" + person.ID);
            //Commands.DropTable("personrecievedmessages" + person.ID);

            //if (person is Student)
            //{
            //    foreach (Course course in ((Student)person).Courses)
            //        Commands.DeleteFrom("coursestudents" + course.ID, "studentid=" + person.ID);

            //    foreach (Lesson lesson in ((Student)person).Absences)
            //        Commands.DeleteFrom("lessonabscences" + lesson.ID, "studentid=" + person.ID);

            //    Commands.DropTable("personcourses" + person.ID);
            //    Commands.DropTable("personassignments" + person.ID);
            //    Commands.DropTable("personabscences" + person.ID);
            //}
            //else if (person is Teacher)
            //{
            //    foreach (Course course in ((Teacher)person).Courses)
            //        Commands.DeleteFrom("courseteachers" + course.ID, "teacherid=" + person.ID);

            //    Commands.DropTable("personcourses" + person.ID);
            //}
            //person = null;

            //STUDENT DELETION
            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);
            List<Person> allUsers = Lists.Persons;
            Student student = Getters.GetLatestPersons(1).Single() as Student;
            bool messageTest = false;
            Remover.RemovePerson(student);
            List<Person> allUsersTest = Lists.Persons;
            try
            {
                List<Message> messages = student.SentMessages;
            }
            catch
            {
                messageTest = true;
            }
            try
            {
                List<Message> messages = student.RecievedMessages;
            }
            catch
            {
                messageTest = true;
            }
            foreach (Message message in Lists.Messages)
            {
                if(message.Sender.ID == student.ID || message.Recipients.Contains(student))
                {
                    messageTest = true;
                }
            }
            Assert.AreEqual(allUsersTest.Count, allUsers.Count - 1);
            //Assert.AreEqual(0, student.RecievedMessages.Count);
            //Assert.AreEqual(0, student.SentMessages.Count);
            //Assert.AreEqual(0, student.Courses.Count);
            //Assert.AreEqual(0, student.Absences.Count);
            //Assert.AreEqual(0, student.Assignments.Count);
            Assert.AreEqual(false, messageTest);

            //TEACHER DELETION
            Creator.CreateTeacher(Instances.Name, Instances.Username, Instances.Password);

            //SECRETARY DELETION
            Creator.CreateSecretary(Instances.Name, Instances.Username, Instances.Password);
        }
        [TestMethod]
        public void RemoverRemovePerson_TeacherAsParameter_PersonRemoved()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void RemoverRemovePerson_SecretaryAsParameter_PersonRemoved()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void RemoverRemoveMessage_ValidParameters_MessageRemoved()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void RemoverRemoveNews_ValidParameters_NewsRemoved()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void RemoverRemoveCourse_ValidParameters_CourseRemoved()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void RemoverRemoveLesson_ValidParameters_LessonRemoved()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void RemoverRemoveRoom_ValidParameters_RoomRemoved()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void RemoverRemoveAssignmentDescription_ValidParameters_AssignmentDescriptionRemoved()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void RemoverRemoveAssignment_ValidParameters_AssignmentRemoved()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void RemoverRemoveAssignmentGrade_ValidParameters_AssignmentGradeRemoved()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void RemoverRemoveCourseGrade_ValidParameters_CourseGradeRemoved()
        {
            throw new NotImplementedException();
        }
    }
}
