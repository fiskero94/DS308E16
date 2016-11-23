using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Model;
using StudyPlatform.Classes.Exceptions;
using System.Linq;

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
        public void TestMethod1()
        {
            //
            // TODO: Add test logic here
            //
        }

        [TestMethod]
        public void GetPersonByID_ValidParameters_ArgumentNullExceptionThrown()
        {
            // Arrange
            string name = Instances.Name;
            string username = Instances.Username;
            string password = Instances.Password;

            Creator.CreateStudent(name, username, password);
            Person lastestPerson = Getters.GetLatestPersons(1).Single();

            //Act & Assert
            try
            {
                Person person = Getters.GetPersonByID(lastestPerson.ID);

                foreach (var item in Lists.Persons)
                {
                    if (lastestPerson.Equals(person) != true)
                    {
                        Assert.Fail(); // No exception thrown
                    }
                }
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentNullException))
                    Assert.Fail(); // Exception thrown is not an ArgumentNullException
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


        [TestMethod]
        public void GetPersonsByPredicates_ValidParameters_ArgumentNullExceptionThrown()
        {
            // Arrange
            string name = Instances.Name;
            string username = Instances.Username;
            string password = Instances.Password;

            Creator.CreateStudent(name, username, password);
            Person lastestPerson = Getters.GetLatestPersons(1).Single();

            // Act & Assert
            try
            {
                List<Person> personList = Getters.GetPersonsByPredicates(lastestPerson.Name);

                foreach (var item in personList)
                {
                    if (lastestPerson.Equals(item) != true)
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
