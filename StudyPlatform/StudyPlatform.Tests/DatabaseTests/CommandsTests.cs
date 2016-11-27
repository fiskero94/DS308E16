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
            // Act
            Commands.SetValue("Person", 1, "Name", Instances.Name);

            // Assert
            Person person = Person.GetByID(1);
            Assert.AreEqual(Instances.Name, person.Name);
        }
        [TestMethod]
        public void CommandsInsertInto_ValidParameters_RoomInserted()
        {
            // Arrange
            int preActRoomCount = Room.GetAll().Count;

            // Act
            Commands.InsertInto("Room", "NULL", Instances.Name);

            // Assert
            Assert.AreEqual(preActRoomCount + 1, Room.GetAll().Count);
            Assert.AreEqual(Instances.Name, Room.GetLatest(1).Single().Name);
        }
        [TestMethod]
        public void CommandsDeleteFrom_ValidParameters_PersonRemoved()
        {
            // Arrange
            int preActPersonCount = Person.GetAll().Count;

            // Act
            Commands.DeleteFrom("Person", "ID=1");

            // Assert
            Assert.AreEqual(preActPersonCount - 1, Person.GetAll().Count);
        }
        [TestMethod]
        public void CommandsGetLatestRows_FourRoomsCreated_RoomsReturnedInReverseOrder()
        {
            // Arrange
            Commands.InsertInto("Room", "NULL", "1");
            Commands.InsertInto("Room", "NULL", "2");
            Commands.InsertInto("Room", "NULL", "3");
            Commands.InsertInto("Room", "NULL", "4");

            // Act
            List<Room> rooms = Room.GetLatest(3);

            // Assert
            Assert.AreEqual("4", rooms[0].Name);
            Assert.AreEqual("3", rooms[1].Name);
            Assert.AreEqual("2", rooms[2].Name);
        }
        [TestMethod]
        public void CommandsCheckNull_NullVariable_TrueReturned()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void CommandsCheckNull_NonNullVariable_FalseReturned()
        {
            throw new NotImplementedException();
        }
    }
}