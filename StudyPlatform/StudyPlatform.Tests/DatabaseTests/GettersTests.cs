using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Model;
using StudyPlatform.Classes.Exceptions;
using System.Linq;
using MySql.Data.MySqlClient;

namespace StudyPlatform.Tests.ModelTests
{
    [TestClass]
    public class GettersTests
    {
        public GettersTests()
        {
            Common.ResetTables();
        }

        [TestMethod]
        public void GetPersonByID_1AsID_NoExceptionThrown()
        {
            // Act
            Person admin = Getters.GetPersonByID(1);
            
            // Assert
            Assert.AreEqual("Admin", admin.Name);
        }

        [TestMethod]
        public void GetPersonByID_0AsID_InvalidIDExceptionThrown()
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
                    Assert.Fail(); // Exception thrown is not an ArgumentNullException
            }
        }


        [TestMethod] //LUL
        public void GetPersonsByPredicates_ValidParameters_ArgumentNullExceptionThrown()
        {
            //Act & Assert
            try
            {
                List<Person> personsList = Getters.GetPersonsByPredicates(null);

                Assert.Fail(); // No exception thrown
            }

            catch (Exception ex)
            {
                if (!(ex is InvalidIDException) || !(ex is NullReferenceException))
                {
                    Assert.Fail(); // Exception thrown is not an ArgumentNullException
                }
            }
        }








        [TestMethod]
        public void GetLastestPersons_ValidParameters_ArgumentNullExceptionThrown()
        {
            // Arrange
            int amount = 3;
            List<Person> latestPersonsList = Getters.GetLatestPersons(amount);
            List<Person> personList = Lists.Persons;
            personList.Reverse();

            // Act & Assert
            try
            {
                for (int i = 0; i < amount - 1; i++)
                {
                    if (latestPersonsList[i].Equals(personList[i]) != true)
                    {
                        Assert.Fail(); // No exception thrown
                    }
                }
            }
            catch (Exception ex)
            {
                if ((ex is ArgumentNullException))
                    Assert.Fail(); // Exception thrown is not an ArgumentNullException
            }
        }


    }
}
