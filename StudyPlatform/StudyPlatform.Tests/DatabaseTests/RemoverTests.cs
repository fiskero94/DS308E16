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
        public void RemoverRemovePerson_Removed()
        {
            //Commands.DeleteFrom("persons", "id=" + person.ID);
            //Commands.DropTable("personsentmessages" + person.ID);
            //Commands.DropTable("personrecievedmessages" + person.ID);

            //foreach (Message message in Lists.Messages)
            //{
            //    Commands.DeleteFrom("messagerecipients" + message.ID, "personid=" + person.ID);
            //}

            //if (person is Student)
            //{
            //    foreach (Course course in Lists.Courses)
            //    {
            //        Commands.DeleteFrom("coursestudents" + course.ID, "studentid=" + person.ID);
            //    }
            //    foreach (Lesson lesson in Lists.Lessons)
            //    {
            //        Commands.DeleteFrom("lessonabsences" + lesson.ID, "studentid=" + person.ID);
            //    }
            //    Commands.DropTable("personcourses" + person.ID);
            //    Commands.DropTable("personassignments" + person.ID);
            //    Commands.DropTable("personabsences" + person.ID);
            //}
            //else if (person is Teacher)
            //{
            //    foreach (Course course in Lists.Courses)
            //    {
            //        Commands.DeleteFrom("courseteachers" + course.ID, "teacherid=" + person.ID);
            //    }
            //    Commands.DropTable("personcourses" + person.ID);
            //}
            //person = null;

            //STUDENT DELETION
            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);
            List<Person> allUsers = Lists.Persons;
            Student student = Getters.GetLatestPersons(1).Single() as Student;
            Remover.RemovePerson(student);
            List<Person> allUsersTest = Lists.Persons;

            Assert.AreEqual(allUsersTest.Count, allUsers.Count - 1);    //Person deletion
            Assert.AreEqual(0, student.RecievedMessages.Count);         //Recieved messages deletion
            Assert.AreEqual(0, student.SentMessages.Count);             //Sent messages deletion

            //TEACHER DELETION
            Creator.CreateTeacher(Instances.Name, Instances.Username, Instances.Password);

            //SECRETARY DELETION
            Creator.CreateSecretary(Instances.Name, Instances.Username, Instances.Password);
        }
        public void RemoverRemoveMessage_Removed()
        {

        }
        public void RemoverRemoveNews_Removed()
        {

        }
        public void RemoverRemoveCourse_Removed()
        {

        }
        public void RemoverRemoveLesson_Removed()
        {

        }
        public void RemoverRemoveRoom_Removed()
        {

        }
        public void RemoverRemoveAssignmentDescription_Removed()
        {

        }
        public void RemoverRemoveAssignment_Removed()
        {

        }
        public void RemoverRemoveAssignmentGrade_Removed()
        {

        }
        public void RemoverRemoveCourseGrade_Removed()
        {

        }
    }
}
