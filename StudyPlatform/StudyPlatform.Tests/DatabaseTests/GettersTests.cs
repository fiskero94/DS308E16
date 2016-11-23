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
            uint id = Instances.ID;
            Person person = Getters.GetPersonByID(id);

            // Act & Assert
            try
            {
                foreach (Person item in Lists.Persons)
                {
                    if (item.Equals(person) != true)
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
                if (!(ex is ArgumentNullException))
                    Assert.Fail(); // Exception thrown is not an ArgumentNullException
            }
        }







        
    }
}
