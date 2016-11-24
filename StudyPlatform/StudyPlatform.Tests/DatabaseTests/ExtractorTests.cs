using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Tests.DatabaseTests
{
    [TestClass]
    public class ExtractorTests
    {

        // Only ExtractIDs and ExtractFilepaths
        public ExtractorTests()
        {
            Common.ResetTables();
        }

        [TestMethod]
        public void ExtractorExtractIDs_ValidParameters_InputIDsReturned() 
        {
            // Arrange
            Commands.InsertInto("personsentmessages1", "1");
            Commands.InsertInto("personsentmessages1", "2");
            Commands.InsertInto("personsentmessages1", "3");
            Query query = new Query("SELECT * FROM personsentmessages1");

            // Act
            uint[] ids = Extractor.ExtractIDs(query.Execute());

            // Assert
            Assert.AreEqual(1, ids[0]);
            Assert.AreEqual(2, ids[1]);
            Assert.AreEqual(3, ids[2]);
        }
        [TestMethod]
        public void ExtractorExtractFilepathss_ValidParameters_InputFilepathsReturned()
        {
            // Arrange
            Creator.CreateCourse(Instances.Name, Instances.Description);
            Commands.InsertInto("coursedocuments1", "C:/Path/file.txt");
            Commands.InsertInto("coursedocuments1", "C:/Path/file2.txt");
            Commands.InsertInto("coursedocuments1", "C:/Path/file3.txt");
            Query query = new Query("SELECT * FROM coursedocuments1");

            // Act
            string[] filepaths = Extractor.ExtractFilepaths(query.Execute());

            // Assert
            Assert.AreEqual("C:/Path/file.txt", filepaths[0]);
            Assert.AreEqual("C:/Path/file2.txt", filepaths[1]);
            Assert.AreEqual("C:/Path/file3.txt", filepaths[2]);
        }
    }
}
