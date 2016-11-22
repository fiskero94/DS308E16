using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Tests.DatabaseTests
{
    [TestClass]
    public class QueryTests
    {
        [TestMethod]
        public void QueryExecuteQueryString_UseAsQueryString_NoExceptionsThrown()
        {
            // Act
            Query.ExecuteQueryString("USE studyplatform;");
        }
        [TestMethod]
        public void QueryExecute_DescribeAsQueryString_NoExceptionsThrown()
        {
            // Arrange
            Query query = new Query("DESCRIBE persons");

            // Act
            query.Execute();
        }
    }
}
