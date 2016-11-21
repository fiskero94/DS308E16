using System;
using MySql.Data.MySqlClient;

namespace StudyPlatformSQLSetup
{
    class Program
    {
        static string rootPassword;
        static string connectionString { get { return @"server=localhost;user=root;port=3306;password=" + rootPassword + ";"; } }
        static void Main(string[] args)
        {
            
            Console.WriteLine("This executable will setup the 'studyplatform' database on the local MySQL server. ");
            Console.WriteLine("If a 'studyplatform' database already exists, it will be dropped.");
            Console.Write("Input MySQL root password: ");
            rootPassword = Console.ReadLine();

            bool success = false;
            try
            {
                Setup();
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
        static void Setup()
        {
            // Database creation
            WriteSetupMessage("Dropping old studyplatform database if exists");
            ExecuteQuery("DROP DATABASE IF EXISTS studyplatform;");
            WriteSetupMessage("Creating new studyplatform database");
            ExecuteQuery("CREATE DATABASE studyplatform;");
            ExecuteQuery("USE studyplatform;");
            // Table creation
            CreateTable("persons", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                   "username TEXT NOT NULL",
                                   "password TEXT NOT NULL",
                                   "name TEXT NOT NULL",
                                   "type ENUM('student','teacher','secretary') NOT NULL");
            CreateTable("messages", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                    "senderid INT UNSIGNED NOT NULL",
                                    "title TEXT NOT NULL",
                                    "text TEXT NOT NULL",
                                    "date DATE NOT NULL");
            CreateTable("news", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                "authorid INT UNSIGNED NOT NULL",
                                "title TEXT NOT NULL",
                                "text TEXT NOT NULL",
                                "date DATE NOT NULL");
            CreateTable("courses", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                   "name TEXT NOT NULL",
                                   "description TEXT NOT NULL");
            CreateTable("lessons", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                   "date DATE NOT NULL",
                                   "description TEXT NOT NULL",
                                   "online BOOL NOT NULL",
                                   "active BOOL NOT NULL");
            CreateTable("rooms", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                 "name TEXT NOT NULL");
            CreateTable("assignmentdescriptions", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                                  "courseid INT UNSIGNED NOT NULL",
                                                  "description TEXT NOT NULL",
                                                  "deadline DATE NOT NULL");
            CreateTable("assignments", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                       "assignmentdescriptionid INT UNSIGNED NOT NULL",
                                       "studentid INT UNSIGNED NOT NULL",
                                       "comment TEXT NOT NULL",
                                       "gradeid INT UNSIGNED NOT NULL",
                                       "date DATE NOT NULL");
            CreateTable("assignmentgrades", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                            "grade ENUM('12','10','7','4','02','00','-3') NOT NULL",
                                            "assignmentid INT UNSIGNED NOT NULL");
            CreateTable("coursegrades", "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                                        "grade ENUM('12','10','7','4','02','00','-3') NOT NULL",
                                        "courseid INT UNSIGNED NOT NULL",
                                        "studentid INT UNSIGNED NOT NULL");
            // Setting up Admin person
            WriteSetupMessage("Setting up admin person");
            ExecuteQuery("INSERT INTO persons VALUES(NULL,'admin','password','Admin','secretary');");
            ExecuteQuery("CREATE TABLE personsentmessages1 (messageid INT UNSIGNED NOT NULL);");
            ExecuteQuery("CREATE TABLE personrecievedmessages1 (messageid INT UNSIGNED NOT NULL);");
            // Database user creation
            WriteSetupMessage("Dropping old studyplatformuser if exists");
            ExecuteQuery("DROP USER IF EXISTS 'studyplatformuser'@'localhost';");
            WriteSetupMessage("Creating new MySQL user: studyplatformuser");
            ExecuteQuery("CREATE USER 'studyplatformuser'@'localhost' IDENTIFIED BY '7JPRFq9LeNet4DU2NNB4rwmP';");
            WriteSetupMessage("Granting all priviliges on database 'studyplatform' for user 'studyplatformuser'");
            ExecuteQuery("GRANT ALL PRIVILEGES ON studyplatform . * TO 'studyplatformuser'@'localhost';");
        }
        static void ExecuteQuery(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            using (MySqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = query;
                command.ExecuteNonQuery();
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