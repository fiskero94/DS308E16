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
            Commands.SetValue("persons", 1, "name", Instances.Name);

            // Assert
            Person person = Getters.GetPersonByID(1);
            Assert.AreEqual(Instances.Name, person.Name);
        }
        [TestMethod]
        public void CommandsCreateTable_ValidParameters_TableCreated()
        {
            // Act
            Commands.CreateTable("testtable", "testvariable TEXT");

            // Assert
            Query query = new Query("SHOW TABLES;");
            MySqlConnectionReader connectionReader = query.Execute();

            bool success = false;
            while (connectionReader.Reader.Read())
                if (connectionReader.Reader.GetString(0) == "testtable")
                    success = true;

            connectionReader.Connection.Close();
            Assert.IsTrue(success);
        }
        [TestMethod]
        public void CommandsInsertInto_ValidParameters_RoomInserted()
        {
            // Arrange
            int preActRoomCount = Lists.Rooms.Count;

            // Act
            Commands.InsertInto("rooms", "NULL", Instances.Name);

            // Assert
            Assert.AreEqual(preActRoomCount + 1, Lists.Rooms.Count);
            Assert.AreEqual(Instances.Name, Getters.GetLatestRooms(1).Single().Name);
        }
    }
}
