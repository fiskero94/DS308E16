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
            Person person = Getters.GetPersonByID(1);
            Assert.AreEqual(Instances.Name, person.Name);
        }
        [TestMethod]
        public void CommandsInsertInto_ValidParameters_RoomInserted()
        {
            // Arrange
            int preActRoomCount = Lists.Rooms.Count;

            // Act
            Commands.InsertInto("Room", "NULL", Instances.Name);

            // Assert
            Assert.AreEqual(preActRoomCount + 1, Lists.Rooms.Count);
            Assert.AreEqual(Instances.Name, Getters.GetLatestRooms(1).Single().Name);
        }
        [TestMethod]
        public void CommandsDeleteFrom_ValidParameters_PersonRemoved()
        {
            // Arrange
            int preActPersonCount = Lists.Persons.Count;

            // Act
            Commands.DeleteFrom("Person", "ID=1");

            // Assert
            Assert.AreEqual(preActPersonCount - 1, Lists.Persons.Count);
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
            List<Room> Rooms = Getters.GetLatestRooms(3);

            // Assert
            Assert.AreEqual("4", Rooms[0].Name);
            Assert.AreEqual("3", Rooms[1].Name);
            Assert.AreEqual("2", Rooms[2].Name);
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