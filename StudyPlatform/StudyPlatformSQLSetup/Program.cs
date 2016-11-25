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

            CreateTable("persons", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                   "username TEXT NOT NULL",
                                   "password TEXT NOT NULL",
                                   "name TEXT NOT NULL",
                                   "type ENUM('student','teacher','secretary') NOT NULL");
            CreateTable("personcourse", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                   "username TEXT NOT NULL",
                                   "password TEXT NOT NULL",
                                   "name TEXT NOT NULL",
                                   "type ENUM('student','teacher','secretary') NOT NULL");
            CreateTable("studentabsence", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                   "username TEXT NOT NULL",
                                   "password TEXT NOT NULL",
                                   "name TEXT NOT NULL",
                                   "type ENUM('student','teacher','secretary') NOT NULL");
            CreateTable("messages", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                    "senderid INT UNSIGNED NOT NULL",
                                    "title TEXT NOT NULL",
                                    "text TEXT NOT NULL",
                                    "date DATETIME NOT NULL");
            CreateTable("messagereci", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                   "username TEXT NOT NULL",
                                   "password TEXT NOT NULL",
                                   "name TEXT NOT NULL",
                                   "type ENUM('student','teacher','secretary') NOT NULL");
            CreateTable("messagefile", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                   "username TEXT NOT NULL",
                                   "password TEXT NOT NULL",
                                   "name TEXT NOT NULL",
                                   "type ENUM('student','teacher','secretary') NOT NULL");
            CreateTable("news", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                "authorid INT UNSIGNED NOT NULL",
                                "title TEXT NOT NULL",
                                "text TEXT NOT NULL",
                                "date DATETIME NOT NULL");
            CreateTable("courses", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                   "name TEXT NOT NULL",
                                   "description TEXT NOT NULL");
            CreateTable("coursefile", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                   "username TEXT NOT NULL",
                                   "password TEXT NOT NULL",
                                   "name TEXT NOT NULL",
                                   "type ENUM('student','teacher','secretary') NOT NULL");
            CreateTable("lessons", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                   "courseid INT UNSIGNED NOT NULL",
                                   "date DATETIME NOT NULL",
                                   "description TEXT NOT NULL",
                                   "online BOOL NOT NULL",
                                   "active BOOL NOT NULL");
            CreateTable("LessonRoom",
                "LessonID INT UNSIGNED NOT NULL",
                "RoomID INT UNSIGNED NOT NULL");
            CreateTable("LessonFile",
                "LessonID INT UNSIGNED NOT NULL",
                "Filepath TEXT NOT NULL");
            CreateTable("Room", 
                "ID INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                "Name TEXT NOT NULL");
            CreateTable("AssignmentDescription", 
                "ID INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                "CourseID INT UNSIGNED NOT NULL",
                "Description TEXT NOT NULL",
                "Deadline DATETIME NOT NULL");
            CreateTable("AssignmentDescriptionFile",
                "AssignmentDescriptionID INT UNSIGNED NOT NULL",
                "Filepath TEXT NOT NULL");
            CreateTable("Assignment", 
                "ID INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                "AssignmentDescriptionID INT UNSIGNED NOT NULL",
                "StudentID INT UNSIGNED NOT NULL",
                "GradeID INT UNSIGNED",
                "Comment TEXT NOT NULL",
                "Cancelled BOOL NOT NULL",
                "DateTime DATETIME NOT NULL");
            CreateTable("AssignmentFile",
                "AssignmentID INT UNSIGNED NOT NULL",
                "Filepath TEXT NOT NULL");
            CreateTable("AssignmentGrade", 
                "ID INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                "AssignmentID INT UNSIGNED NOT NULL",
                "Grade ENUM('12','10','7','4','02','00','-3') NOT NULL",
                "Comment TEXT NOT NULL");
            CreateTable("CourseGrade", 
                "ID INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                "CourseID INT UNSIGNED NOT NULL",
                "StudentID INT UNSIGNED NOT NULL",
                "Grade ENUM('12','10','7','4','02','00','-3') NOT NULL",
                "Comment TEXT NOT NULL");
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