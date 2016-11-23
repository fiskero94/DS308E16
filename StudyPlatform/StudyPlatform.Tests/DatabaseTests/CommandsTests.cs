using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Model;

namespace StudyPlatform.Tests.DatabaseTests
{
    [TestClass]
    public class CommandsTests
    {
        public CommandsTests()
        {
            Common.ResetTables();
        }

        [TestMethod]
        public void CommandsSetValue_StringAsValue_VariableChanged()
        {
            // Arrange
            string stringValue = Instances.Name;

            // Act
            Commands.SetValue("persons", 1, "name", "'" + Instances.Name + "'");

            // Assert
            Person person = Getters.GetPersonByID(1);
            Assert.AreEqual(person.Name, Instances.Name);
        }

        [TestMethod]
        public void CommandsCreateTable_()
        {
            // Arrange
            string stringValue = Instances.Name;

            // Act
            Commands.SetValue("persons", 1, "name", "'" + Instances.Name + "'");

            // Assert
            Person person = Getters.GetPersonByID(1);
            Assert.AreEqual(person.Name, Instances.Name);
        }
    }
}
