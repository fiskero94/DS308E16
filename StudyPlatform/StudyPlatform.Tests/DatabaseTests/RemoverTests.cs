﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Model;
using StudyPlatform.Classes.Exceptions;
using System.Linq;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

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
            //SETUP
            Creator.CreateStudent(Instances.Name, Instances.Username, Instances.Password);
            List<Person> allUsers = Lists.Persons;
            Student student = Getters.GetLatestPersons(1).Single() as Student;
            List<string> filepaths = new List<string>();
            filepaths.Add("");
            List<Person> recipients = new List<Person>();
            recipients.Add(Getters.GetPersonByID(1));
            Creator.CreateMessage(student, Instances.Title, Instances.Text, recipients, filepaths);
            recipients.Clear();
            recipients.Add(student);
            Creator.CreateMessage(Getters.GetPersonByID(1), Instances.Title, Instances.Text, recipients, filepaths);
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Course course = Getters.GetLatestCourses(1).Single();
            course.AddStudent(student);
            Creator.CreateRoom(Instances.Name);
            List<Room> rooms = new List<Room>();
            Room room = Getters.GetLatestRooms(1).Single();
            rooms.Add(room);
            Creator.CreateLesson(Instances.Date, Instances.Description, Instances.Online, Instances.Active, rooms, filepaths, course);
            Lesson lesson = Getters.GetLatestLessons(1).Single();
            Remover.RemovePerson(student);
            //MESSAGE RECIPIENTS
            foreach (Message item in Lists.Messages)
            {
                List<Person> items = item.Recipients;
                foreach (Person recipient in items)
                {
                    if(student.ID == recipient.ID)
                        Assert.Fail("deleted person still in recipients");
                }
            }
            //PERSON SENT/RECIEVED MESSAGES
            try
            {
                List<Message> sentMessages = student.SentMessages;
            }
            catch (Exception ex)
            {
                if (!(ex is MySqlException && ((MySqlException)ex).Number == 1146))
                    Assert.Fail(ex.Message);
            }
            try
            {
                List<Message> recievedMessages = student.RecievedMessages;
            }
            catch (Exception ex)
            {
                if (!(ex is MySqlException && ((MySqlException)ex).Number == 1146))
                    Assert.Fail(ex.Message);
            }
            course = Getters.GetCourseByID(course.ID);
            foreach (Student coursestudents in course.Students)
            {
                if(student.ID == coursestudents.ID)
                {
                    Assert.Fail("student is still in course");
                }
            }
            foreach (Lesson testLesson in course.Lessons)
            {
                foreach (Student item in testLesson.Absences)
                {
                    if (item.ID == student.ID)
                        Assert.Fail("Student still has absence");
                }
            }
            //    Commands.DropTable("personcourses" + person.ID);
            //    Commands.DropTable("personassignments" + person.ID);
            //    Commands.DropTable("personabscences" + person.ID);
            try
            {
                List<Course> courses = student.Courses;
            }
            catch (Exception ex)
            {
                if (!(ex is MySqlException && ((MySqlException)ex).Number == 1146))
                    Assert.Fail(ex.Message);
            }
            try
            {
                List<Assignment> assignments = student.Assignments;
            }
            catch (Exception ex)
            {
                if (!(ex is MySqlException && ((MySqlException)ex).Number == 1146))
                    Assert.Fail(ex.Message);
            }
            try
            {
                List<Lesson> absences = student.Absences;
            }
            catch (Exception ex)
            {
                if (!(ex is MySqlException && ((MySqlException)ex).Number == 1146))
                    Assert.Fail(ex.Message);
            }
            //PERSON DELETION
            List<Person> allUsersTest = Lists.Persons;
            Assert.AreEqual(allUsersTest.Count, allUsers.Count - 1);
            //TEACHER DELETION
            Creator.CreateTeacher(Instances.Name, Instances.Username, Instances.Password);
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
