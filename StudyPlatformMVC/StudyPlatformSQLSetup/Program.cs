﻿using System;
using MySql.Data.MySqlClient;
using StudyPlatformMVC.Database;
using StudyPlatformMVC.Models;
using System.Collections.Generic;

namespace StudyPlatformSQLSetup
{
    public class Program
    {
        private static string _rootPassword;
        private static string _connectionString;
        private static void Main(string[] args)
        {

            Console.WriteLine("This executable will setup the 'studyplatform' database on the local MySQL server. ");
            Console.WriteLine("If a 'studyplatform' database already exists, it will be dropped.");
            Console.Write("Input MySQL root password: ");
            _rootPassword = Console.ReadLine();
            _connectionString = @"server=localhost;user=root;port=3306;password=" + _rootPassword + ";";

            bool success = false;
            try
            {
                SetupDatabase();
                SetupMySqlUser();
                SetupTables(null);
                SetupAdmin(null);
                SetupData();
                success = true;
            }
            catch (MySqlException e)
            {
                WriteErrorMessage("Setup failed with the exception: " + e.Message);
                try { ExecuteQuery("DROP DATABASE IF EXISTS studyplatform;"); }
                catch (Exception) { }
            }

            Console.WriteLine(success ? "Setup completed, press any key to exit" : "Setup failed, press any key to exit");
            Console.ReadKey();
        }
        private static void SetupDatabase()
        {
            WriteSetupMessage("Dropping old studyplatform database if exists");
            ExecuteQuery("DROP DATABASE IF EXISTS studyplatform;");
            WriteSetupMessage("Creating new studyplatform database");
            ExecuteQuery("CREATE DATABASE studyplatform;");
            ExecuteQuery("USE studyplatform;");
        }
        private static void SetupMySqlUser()
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
                _connectionString = externalConnectionString;

            CreateTable("Person",
                "ID INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                "Name TEXT NOT NULL",
                "Username TEXT NOT NULL",
                "Password TEXT NOT NULL",
                "Type ENUM('Student','Teacher','Secretary') NOT NULL");
            CreateTable("PersonCourse",
                "PersonID INT UNSIGNED NOT NULL",
                "CourseID INT UNSIGNED NOT NULL");
            CreateTable("StudentAbsence",
                "StudentID INT UNSIGNED NOT NULL",
                "LessonID INT UNSIGNED NOT NULL");
            CreateTable("Message",
                "ID INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                "SenderID INT UNSIGNED NOT NULL",
                "Title TEXT NOT NULL",
                "Text TEXT NOT NULL",
                "DateTime DATETIME NOT NULL");
            CreateTable("MessageRecipient",
                "MessageID INT UNSIGNED NOT NULL",
                "PersonID INT UNSIGNED NOT NULL");
            CreateTable("MessageFile",
                "MessageID INT UNSIGNED NOT NULL",
                "Filepath TEXT NOT NULL");
            CreateTable("News",
                "ID INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                "AuthorID INT UNSIGNED NOT NULL",
                "Title TEXT NOT NULL",
                "Text TEXT NOT NULL",
                "DateTime DATETIME NOT NULL");
            CreateTable("Course",
                "ID INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                "Name TEXT NOT NULL",
                "Description TEXT NOT NULL");
            CreateTable("CourseFile",
                "CourseID INT UNSIGNED NOT NULL",
                "Filepath TEXT NOT NULL");
            CreateTable("Lesson",
                "ID INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                "CourseID INT UNSIGNED NOT NULL",
                "Description TEXT NOT NULL",
                "Online BOOL NOT NULL",
                "Cancelled BOOL NOT NULL",
                "DateTime DATETIME NOT NULL");
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
                "Cancelled BOOL NOT NULL",
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
                _connectionString = externalConnectionString;

            WriteSetupMessage("Setting up admin person");
            Creator.CreateSecretary("Admin", "admin", "password");
        }




        public static void SetupData()
        {
            Creator.CreateRoom("s1");
            Creator.CreateRoom("s2");
            Creator.CreateRoom("s3");

            Creator.CreateCourse("Matematik", "A-Level Kappa");


            List<string> filepaths = new List<string>();
            filepaths.Add("../C/Games");
            filepaths.Add("../C/Kappa");

            Creator.CreateLesson(Course.GetLatest(), "lektion 1", true, new DateTime(2016, 11, 29, 8, 10, 0), Room.GetLatest(1), filepaths);
            Creator.CreateLesson(Course.GetLatest(), "lektion 2", true, new DateTime(2016, 11, 29, 9, 10, 0), Room.GetLatest(2), filepaths);
            Creator.CreateLesson(Course.GetLatest(), "lektion 3", true, new DateTime(2016, 11, 29, 10, 10, 0), Room.GetLatest(1), filepaths);


            Creator.CreateStudent("name", "username", "password");
            Student student = Student.GetLatest();
            Creator.CreateCourse("coursename1", "course1");
            Creator.CreateCourse("coursename2", "course2");
            Course course1 = Course.GetByID(2);
            Course course2 = Course.GetByID(3);
            course1.AddStudent(student);
            course2.AddStudent(student);

            Creator.CreateCourseGrade(Course.GetLatest(), Student.GetLatest(), "grade", "comment");
        }





        private static void ExecuteQuery(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            using (MySqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = query;
                command.ExecuteNonQuery();
                // connection.Close();
            }
        }
        private static void CreateTable(string tableName, params string[] variables)
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
        private static void WriteSetupMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("> " + message);
            Console.ResetColor();
        }
        private static void WriteErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}