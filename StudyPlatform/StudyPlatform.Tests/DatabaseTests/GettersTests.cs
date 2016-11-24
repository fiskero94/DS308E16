using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Model;
using StudyPlatform.Classes.Exceptions;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Reflection;

namespace StudyPlatform.Tests.ModelTests
{
    [TestClass]
    public class GettersTests
    {
        public GettersTests()
        {
            Common.ResetTables();
        }




        ///////////////////////////// TEST For ID /////////////////////////////////

        [TestMethod]
        public void GetPersonByID_1AsID_NoExceptionThrown()
        {
            // Act
            Person admin = Getters.GetPersonByID(1);

            // Assert
            Assert.AreEqual("Admin", admin.Name);
        }

        private static Dictionary<Type, Func<uint, object>> GetIDByTypes = new Dictionary<Type, Func<uint, object>>()
        {
            { typeof(Person), new Func<uint,Person>(Getters.GetPersonByID) },
            { typeof(Message), new Func<uint,Message>(Getters.GetMessageByID) },
            { typeof(News), new Func<uint,News>(Getters.GetNewsByID) },
            { typeof(Course), new Func<uint,Course>(Getters.GetCourseByID) },
            { typeof(Lesson), new Func<uint,Lesson>(Getters.GetLessonByID) },
            { typeof(Room), new Func<uint,Room>(Getters.GetRoomByID) },
            { typeof(AssignmentDescription), new Func<uint,AssignmentDescription>(Getters.GetAssignmentDescriptionByID) },
            { typeof(Assignment), new Func<uint,Assignment>(Getters.GetAssignmentByID) },
            { typeof(AssignmentGrade), new Func<uint,AssignmentGrade>(Getters.GetAssignmentGradeByID) },
            { typeof(CourseGrade), new Func<uint,CourseGrade>(Getters.GetCourseGradeByID) }
        };

        private void GetTypeByID_0AsID_InvalidIDExceptionThrown<T>()
        {
            //Act & Assert
            try
            {
                GetIDByTypes[typeof(T)].Invoke(0);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                if (!(ex is InvalidIDException))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void GetPersonByID_0AsID_InvalidIDExceptionThrown()
        {
            GetTypeByID_0AsID_InvalidIDExceptionThrown<Person>();
        }

        [TestMethod]
        public void GetMessageByID_0AsID_InvalidIDExceptionThrown()
        {
            GetTypeByID_0AsID_InvalidIDExceptionThrown<Message>();
        }

        [TestMethod]
        public void GetNewsByID_0AsID_InvalidIDExceptionThrown()
        {
            GetTypeByID_0AsID_InvalidIDExceptionThrown<News>();
        }

        [TestMethod]
        public void GetCourseByID_0AsID_InvalidIDExceptionThrown()
        {
            GetTypeByID_0AsID_InvalidIDExceptionThrown<Course>();
        }

        [TestMethod]
        public void GetLessoneByID_0AsID_InvalidIDExceptionThrown()
        {
            GetTypeByID_0AsID_InvalidIDExceptionThrown<Lesson>();
        }

        [TestMethod]
        public void GetRoomByID_0AsID_InvalidIDExceptionThrown()
        {
            GetTypeByID_0AsID_InvalidIDExceptionThrown<Room>();
        }

        [TestMethod]
        public void GetAssignmentDescriptionByID_0AsID_InvalidIDExceptionThrown()
        {
            GetTypeByID_0AsID_InvalidIDExceptionThrown<AssignmentDescription>();
        }

        [TestMethod]
        public void GetAssignmentByID_0AsID_InvalidIDExceptionThrown()
        {
            GetTypeByID_0AsID_InvalidIDExceptionThrown<Assignment>();
        }

        [TestMethod]
        public void GetAssignmentGradeByID_0AsID_InvalidIDExceptionThrown()
        {
            GetTypeByID_0AsID_InvalidIDExceptionThrown<AssignmentGrade>();
        }

        [TestMethod]
        public void GetCourseGradeByID_0AsID_InvalidIDExceptionThrown()
        {
            GetTypeByID_0AsID_InvalidIDExceptionThrown<CourseGrade>();
        }


        ///////////////////////////// TEST For Conditions /////////////////////////////////

        [TestMethod]
        public void GetPersonsByConditions_NullParameters_NullReferenceExceptionThrown()
        {
            //Act & Assert
            try
            {
                List<Person> personsList = Getters.GetPersonsByConditions(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                if (!(ex is NullReferenceException))
                    Assert.Fail();
            }
        }





        ///////////////////////////// TEST For Latest /////////////////////////////////

        private static Dictionary<Type, Func<uint, object>> GetLatestByTypes = new Dictionary<Type, Func<uint, object>>()
        {
            { typeof(Person), new Func<uint,List<Person>>(Getters.GetLatestPersons) },
            { typeof(Message), new Func<uint,List<Message>>(Getters.GetLatestMessages) },
            { typeof(News), new Func<uint,List<News>>(Getters.GetLatestNews) },
            { typeof(Course), new Func<uint,List<Course>>(Getters.GetLatestCourses) },
            { typeof(Lesson), new Func<uint,List<Lesson>>(Getters.GetLatestLessons) },
            { typeof(Room), new Func<uint,List<Room>>(Getters.GetLatestRooms) },
            { typeof(AssignmentDescription), new Func<uint,List<AssignmentDescription>>(Getters.GetLatestAssignmentDescriptions) },
            { typeof(Assignment), new Func<uint,List<Assignment>>(Getters.GetLatestAssignments) },
            { typeof(AssignmentGrade), new Func<uint,List<AssignmentGrade>>(Getters.GetLatestAssignmentGrades) },
            { typeof(CourseGrade), new Func<uint,List<CourseGrade>>(Getters.GetLatestCourseGrades) }
        };

        private void GetLatestByType_0AsID_InvalidIDExceptionThrown<T>()
        {
            //Act & Assert
            GetLatestByTypes[typeof(T)].Invoke(uint.MaxValue);
        }

        [TestMethod]
        public void GetLatestPersons________()
        {
            GetLatestByType_0AsID_InvalidIDExceptionThrown<CourseGrade>();
        }













    }
}
