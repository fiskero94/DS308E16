using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyPlatform.Classes.Database;
using System.Configuration;
using StudyPlatformSQLSetup;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StudyPlatform.Tests
{
    static class Common
    {
        public static void ResetTables()
        {
            Query query = new Query("SHOW TABLES;");
            MySqlConnectionReader connectionReader = query.Execute();
            List<string> tables = new List<string>();
            while (connectionReader.Reader.Read())
                tables.Add(connectionReader.Reader.GetString(0));
            foreach (string table in tables)
                Query.ExecuteQueryString("DROP TABLE IF EXISTS " + table + ";");
            connectionReader.Connection.Close();
            Program.SetupTables(ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString());
            Program.SetupAdmin(ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString());
        }
        public static void TestActionForExceptionThrown<T1>
            (Exception exception, Action<T1> action, 
            T1 P1)
        {
            // Act & Assert
            try
            {
                action.Invoke(P1);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(exception.GetType() == ex.GetType()))
                    Assert.Fail(); // Exception thrown is not an ParameterException
            }
        }
        public static void TestActionForExceptionThrown<T1, T2>
            (Exception exception, Action<T1, T2> action, 
            T1 P1, T2 P2)
        {
            // Act & Assert
            try
            {
                action.Invoke(P1, P2);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(exception.GetType() == ex.GetType()))
                    Assert.Fail(); // Exception thrown is not an ParameterException
            }
        }
        public static void TestActionForExceptionThrown<T1, T2, T3>
            (Exception exception, Action<T1, T2, T3> action, 
            T1 P1, T2 P2, T3 P3)
        {
            // Act & Assert
            try
            {
                action.Invoke(P1, P2, P3);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(exception.GetType() == ex.GetType()))
                    Assert.Fail(); // Exception thrown is not an ParameterException
            }
        }
        public static void TestActionForExceptionThrown<T1, T2, T3, T4>
            (Exception exception, Action<T1, T2, T3, T4> action, 
            T1 P1, T2 P2, T3 P3, T4 P4)
        {
            // Act & Assert
            try
            {
                action.Invoke(P1, P2, P3, P4);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(exception.GetType() == ex.GetType()))
                    Assert.Fail(); // Exception thrown is not an ParameterException
            }
        }
        public static void TestActionForExceptionThrown<T1, T2, T3, T4, T5>
            (Exception exception, Action<T1, T2, T3, T4, T5> action, 
            T1 P1, T2 P2, T3 P3, T4 P4, T5 P5)
        {
            // Act & Assert
            try
            {
                action.Invoke(P1, P2, P3, P4, P5);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(exception.GetType() == ex.GetType()))
                    Assert.Fail(); // Exception thrown is not an ParameterException
            }
        }
        public static void TestActionForExceptionThrown<T1, T2, T3, T4, T5, T6>
            (Exception exception, Action<T1, T2, T3, T4, T5, T6> action, 
            T1 P1, T2 P2, T3 P3, T4 P4, T5 P5, T6 P6)
        {
            // Act & Assert
            try
            {
                action.Invoke(P1, P2, P3, P4, P5, P6);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(exception.GetType() == ex.GetType()))
                    Assert.Fail(); // Exception thrown is not an ParameterException
            }
        }
        public static void TestActionForExceptionThrown<T1, T2, T3, T4, T5, T6, T7>
            (Exception exception, Action<T1, T2, T3, T4, T5, T6, T7> action, 
            T1 P1, T2 P2, T3 P3, T4 P4, T5 P5, T6 P6, T7 P7)
        {
            // Act & Assert
            try
            {
                action.Invoke(P1, P2, P3, P4, P5, P6, P7);
                Assert.Fail(); // No exception thrown
            }
            catch (Exception ex)
            {
                if (!(exception.GetType() == ex.GetType()))
                    Assert.Fail(); // Exception thrown is not an ParameterException
            }
        }
    }
}
