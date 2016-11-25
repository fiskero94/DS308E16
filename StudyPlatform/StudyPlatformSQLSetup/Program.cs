using System;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using StudyPlatform.Classes.Database;

namespace StudyPlatformSQLSetup
{
    public class Program
    {
        static string rootPassword;
        static string connectionString;
        static void Main(string[] args)
        {
            
            Console.WriteLine("This executable will setup the 'studyplatform' database on the local MySQL server. ");
            Console.WriteLine("If a 'studyplatform' database already exists, it will be dropped.");
            Console.Write("Input MySQL root password: ");
            rootPassword = Console.ReadLine();
            connectionString = @"server=localhost;user=root;port=3306;password=" + rootPassword + ";";

            bool success = false;
            try
            {
                SetupDatabase();
                SetupMySqlUser();
                SetupTables(null);
                SetupAdmin(null);
                success = true;
            }
            catch (MySqlException e)
            {
                WriteErrorMessage("Setup failed with the exception: " + e.Message);
                try { ExecuteQuery("DROP DATABASE IF EXISTS studyplatform;"); }
                catch (Exception) { }
            }

            if (success) Console.WriteLine("Setup completed, press any key to exit");
            else Console.WriteLine("Setup failed, press any key to exit");
            Console.ReadKey();
        }
        static void SetupDatabase()
        {
            WriteSetupMessage("Dropping old studyplatform database if exists");
            ExecuteQuery("DROP DATABASE IF EXISTS studyplatform;");
            WriteSetupMessage("Creating new studyplatform database");
            ExecuteQuery("CREATE DATABASE studyplatform;");
            ExecuteQuery("USE studyplatform;");
        }
        static void SetupMySqlUser()
        {
            WriteSetupMessage("Dropping old studyplatformuser if exists");
            ExecuteQuery("DROP USER IF EXISTS 'studyplatformuser'@'localhost';");
            WriteSetupMessage("Creating new MySQL user: studyplatformuser");
            ExecuteQuery("CREATE USER 'studyplatformuser'@'localhost' IDENTIFIED BY '7JPRFq9LeNet4DU2NNB4rwmP';");
            WriteSetupMessage("Granting all priviliges on database 'studyplatform' for user 'studyplatformuser'");
            ExecuteQuery("GRANT ALL PRIVILEGES ON studyplatform . * TO 'studyplatformuser'@'localhost';");
        }
        public static void SetupTables(string externalConnectionString)
        {
            if (externalConnectionString != null)
                connectionString = externalConnectionString;

            CreateTable("Person", "ID INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                   "Name TEXT NOT NULL",
                                   "Username TEXT NOT NULL",
                                   "Password TEXT NOT NULL",
                                   "Type ENUM('Student','Teacher','Secretary') NOT NULL");
            CreateTable("PersonCourse", "PersonID INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                   "CourseID INT UNSIGNED NOT NULL");
            CreateTable("StudentAbsence", "StudentID INT UNSIGNED NOT NULL",
                                   "LessonID INT UNSIGNED NOT NULL");
            CreateTable("Message", "ID INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                    "SenderID INT UNSIGNED NOT NULL",
                                    "Title TEXT NOT NULL",
                                    "Text TEXT NOT NULL",
                                    "DateTime DATETIME NOT NULL");
            CreateTable("MessageRecipient", "MessageID INT UNSIGNED NOT NULL",
                                   "PersonID INT UNSIGNED NOT NULL");
            CreateTable("MessageFile", "MessageID INT UNSIGNED NOT NULL",
                                   "Filepath TEXT NOT NULL");
            CreateTable("News", "ID INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                "AuthorID INT UNSIGNED NOT NULL",
                                "Title TEXT NOT NULL",
                                "Text TEXT NOT NULL",
                                "DateTime DATETIME NOT NULL");
            CreateTable("Course", "ID INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                   "Name TEXT NOT NULL",
                                   "Description TEXT NOT NULL");
            CreateTable("CourseFile", "CourseID INT UNSIGNED NOT NULL",
                                   "Filepath TEXT NOT NULL");
            CreateTable("Lesson", "ID INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                   "CourseID INT UNSIGNED NOT NULL",
                                   "Description TEXT NOT NULL",
                                   "Online BOOL NOT NULL",
                                   "Cancelled BOOL NOT NULL",
                                   "DateTime DATETIME NOT NULL");
            CreateTable("lessonroom", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                   "username TEXT NOT NULL",
                                   "password TEXT NOT NULL",
                                   "name TEXT NOT NULL",
                                   "type ENUM('student','teacher','secretary') NOT NULL");
            CreateTable("lessonfile", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                   "username TEXT NOT NULL",
                                   "password TEXT NOT NULL",
                                   "name TEXT NOT NULL",
                                   "type ENUM('student','teacher','secretary') NOT NULL");
            CreateTable("rooms", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                 "name TEXT NOT NULL");
            CreateTable("assignmentdescriptions", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                                  "courseid INT UNSIGNED NOT NULL",
                                                  "description TEXT NOT NULL",
                                                  "deadline DATETIME NOT NULL");
            CreateTable("assignmentdescriptionfile", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                   "username TEXT NOT NULL",
                                   "password TEXT NOT NULL",
                                   "name TEXT NOT NULL",
                                   "type ENUM('student','teacher','secretary') NOT NULL");
            CreateTable("assignments", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                       "assignmentdescriptionid INT UNSIGNED NOT NULL",
                                       "studentid INT UNSIGNED NOT NULL",
                                       "comment TEXT NOT NULL",
                                       "gradeid INT UNSIGNED",
                                       "date DATETIME NOT NULL");
            CreateTable("assignmentfile", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                   "username TEXT NOT NULL",
                                   "password TEXT NOT NULL",
                                   "name TEXT NOT NULL",
                                   "type ENUM('student','teacher','secretary') NOT NULL");
            CreateTable("assignmentgrades", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                            "grade ENUM('12','10','7','4','02','00','-3') NOT NULL",
                                            "comment TEXT NOT NULL",
                                            "assignmentid INT UNSIGNED NOT NULL");
            CreateTable("coursegrades", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                        "grade ENUM('12','10','7','4','02','00','-3') NOT NULL",
                                        "comment TEXT NOT NULL",
                                        "courseid INT UNSIGNED NOT NULL",
                                        "studentid INT UNSIGNED NOT NULL");
        }
        public static void SetupAdmin(string externalConnectionString)
        {
            if (externalConnectionString != null)
                connectionString = externalConnectionString;

            WriteSetupMessage("Setting up admin person");
            Creator.CreateSecretary("Admin", "admin", "password");
        }
        static void ExecuteQuery(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            using (MySqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = query;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        static void CreateTable(string tableName, params string[] variables)
        {
            WriteSetupMessage("Creating table for " + tableName);
            string query = "CREATE TABLE " + tableName + " (";
            foreach (string variable in variables)
            {
                query += variable;
                query += ", ";
            }
            query = query.TrimEnd(' ');
            query = query.TrimEnd(',');
            query += ");";
            ExecuteQuery(query);
        }
        static void WriteSetupMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("> " + message);
            Console.ResetColor();
        }
        static void WriteErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}