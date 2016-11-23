using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Model;
using StudyPlatform.Classes.Exceptions;
using System.Linq;
using MySql.Data.MySqlClient;

namespace StudyPlatform.Tests.DatabaseTests
{
    [TestClass]
    public class GettersTests
    {
        // The methods tested here are generic, 
        // so if Person and Message works, 
        // the rest should work as well.
        public GettersTests()
        {
            Common.ResetTables();
        }

        [TestMethod]
        public void GettersGetPersonByID_1AsID_PersonReturned()
        {
            // Act
            Person admin = Getters.GetPersonByID(1);
            
            // Assert
            Assert.AreEqual("Admin", admin.Name);
        }
        [TestMethod]
        public void GettersGetPersonByID_0AsID_InvalidIDExceptionThrown()
        {
            //Act & Assert
            try
            {
                Person admin = Getters.GetPersonByID(0);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(ex is InvalidIDException))
                    Assert.Fail(); // Exception thrown is not an InvalidIDException
            }
        }
        [TestMethod]
        public void GettersGetPersonsByConditions_ValidParameters_PersonReturned()
        {
            // Act
            Person admin = Getters.GetPersonsByConditions("name='Admin'", "username='admin'", "password='password'").Single();

            // Assert
            Assert.AreEqual("Admin", admin.Name);
        }
        [TestMethod]
        public void GettersGetLatestPersons_FourPersonsCreated_ThreePersonsReturned()
        {
            // Arrange
            Commands.InsertInto("persons", "NULL", "username1", "password1", "name1", "secretary");
            Commands.InsertInto("persons", "NULL", "username2", "password2", "name2", "secretary");
            Commands.InsertInto("persons", "NULL", "username3", "password3", "name3", "secretary");
            Commands.InsertInto("persons", "NULL", "username4", "password4", "name4", "secretary");

            // Act
            List<Person> persons = Getters.GetLatestPersons(3);

            // Assert
            Assert.AreEqual("name4", persons[0].Name);
            Assert.AreEqual("name3", persons[1].Name);
            Assert.AreEqual("name2", persons[2].Name);
        }
        [TestMethod]
        public void GettersGetMessageByID_1AsID_MessageReturned()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void GettersGetMessageByID_0AsID_InvalidIDExceptionThrown()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void GettersGetMessagesByConditions_ValidParameters_MessageReturned()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void GettersGetLatestMessages_FourMessagesCreated_ThreeMessagesReturned()
        {
            throw new NotImplementedException();
        }
    }
}