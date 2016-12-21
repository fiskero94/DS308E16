using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyPlatform.Classes.Database;
using MySql.Data.MySqlClient;

namespace StudyPlatform.Tests.DatabaseTests
{
    [TestClass]
    public class QueryTests
    {
        public QueryTests()
        {
            WebHelper.ResetTables();
        }

        [TestMethod]
        public void QueryExecuteQueryString_UseAsQueryString_NoExceptionsThrown()
        {
            // Act
            Query.ExecuteQueryString("DESCRIBE Person;");
        }
        [TestMethod]
        public void QueryExecuteNonReader_DescribeAsQueryString_NoExceptionsThrown()
        {
            // Arrange
            Query query = new Query("DESCRIBE Person;");

            // Act
            query.ExecuteNonReader();
        }
        [TestMethod]
        public void QueryExecute_DescribeAsQueryString_NoExceptionsThrown()
        {
            // Arrange
            Query query = new Query("DESCRIBE Person;");

            // Act
            MySqlConnectionReader connectionReader = query.Execute();
            connectionReader.Connection.Close();
        }
        [TestMethod]
        public void SetQueryString_NullValue_ArgumentNullExceptionThrown()
        {
            // Act & Assert
            try
            {
                Query query = new Query(null);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(typeof(ArgumentNullException) == ex.GetType()))
                    Assert.Fail(); // Exception thrown is not an ArgumentNullException
            }
        }
    }
}
