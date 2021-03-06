﻿using MySql.Data.MySqlClient;
using StudyPlatform.Classes;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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
                SetupPseudoData();
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

            WriteSetupMessage("Creating Tables");
            CreateTable("Person",
                "ID INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY",
                "Name TEXT NOT NULL",
                "Username TEXT NOT NULL",
                "Password TEXT NOT NULL",
                "Type ENUM('Student','Teacher','Secretary') NOT NULL");
            CreateTable("StudentCourse",
                "StudentID INT UNSIGNED NOT NULL",
                "CourseID INT UNSIGNED NOT NULL");
            CreateTable("TeacherCourse",
                "TeacherID INT UNSIGNED NOT NULL",
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
                "Title TEXT NOT NULL",
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
        private static void SetupPseudoData()
        {
            WriteSetupMessage("Populating tables");
            //News
            WriteSetupMessageIndent("Creating news");
            Creator.CreateNews(Person.GetByID(1) as Secretary, "Informant Stuff", "Vi startede med at få informanten til at fortælle om hvordan hendes hverdag var, herefter spurgte vi ind til til de områder der blev nævnt som vi følte var relevante for et nyt system. Gennem interviewet kom vi frem til at informanten primært bruger skemaet, tjek af fravær, og aflevering af opgaver. Informanten bruger skemaet til at tjekke lokale og fag samt lektier. Derudover bruger informanten en funktion til at aflevere sine opgaver, hvor hun fortalte at det ville være hjælpsomt, hvis der kom en bekræftelse på at opgaverne er afleveret. Derudover fortalte hun, at systemet ikke skal logge af, således at den forbliver logget ind.");
            Creator.CreateNews(Person.GetByID(1) as Secretary, "Ændring af elev login.", "Der er sket en fejl i opretning af jeres elev logins, så jeres login virker ikke d. 3 Feb 2017, da jeres login skal opdateres.");
            Creator.CreateNews(Person.GetByID(1) as Secretary, "Der er ikke lektiecafé i matematik i dag torsdag d. 12/1-17 kl. 13.45-16.30.", "");
            Creator.CreateNews(Person.GetByID(1) as Secretary, "Administrationen i Aalborg afd.lukker kl. 13.30 fredag den 13.1.17", "");
            Creator.CreateNews(Person.GetByID(1) as Secretary, "Velkommen til vores afdeling i Aars!", "Her underviser vi både i HF enkeltfag, AVU (Almen voksenuddannelse), FVU(forberedende voksenuddannelse) og OBU(ordblindeundervisning).Vil du vide mere om vores uddannelser finder du dem på forsiden af hjemmesiden eller ved hjælp af den farvede bjælke herover for. Du kan ligeledes finder oplysninger om vores personale - dette finder du i menuen til venstre. Vi glæder os til at byde dig velkommen som kursist i vores flotte nye lokaler.");

            // Students
            WriteSetupMessageIndent("Creating students");
            var student01 = Student.New("Helle", "helle", "1234");
            var student02 = Student.New("Gunner Ebbesen", "gunnerebbesen", "1234");
            var student03 = Student.New("Kent Poulsen", "kentpoulsen", "1234");
            var student04 = Student.New("Bertel Greve", "bertelgreve", "1234");
            var student05 = Student.New("Esben Enevoldsen", "esbenenevoldsen", "1234");
            var student06 = Student.New("Olga Jansen", "olgajansen", "1234");
            var student07 = Student.New("Sanne Kruse", "sannekruse", "1234");
            var student08 = Student.New("Maybritt Erichsen", "maybritterichsen", "1234");
            var student09 = Student.New("Stine Haagensen", "stinehaagensen", "1234");
            var student10 = Student.New("Emilie Danielsen", "emiliedanielsen", "1234");
            var students = Group(student01, student02, student03, student04, student05, student06, student07, student08, student09, student10);
            // Teachers
            WriteSetupMessageIndent("Creating teachers");
            var teacher01 = Teacher.New("Henry Hoj", "henryhoj", "1234");
            var teacher02 = Teacher.New("Steffen Johansen", "steffenjohansen", "1234");
            var teacher03 = Teacher.New("Palle Jacobsen", "pallejacobsen", "1234");
            var teacher04 = Teacher.New("Bendt Morch", "bendtmorch", "1234");
            var teacher05 = Teacher.New("Alex Larsen", "alexlarsen", "1234");
            var teacher06 = Teacher.New("Christa Hoj", "christahoj", "1234");
            var teacher07 = Teacher.New("Lykke Pallesen", "lykkepallesen", "1234");
            var teacher08 = Teacher.New("Hjordis Frandsen", "hjordisfrandsen", "1234");
            var teacher09 = Teacher.New("Lilli Rask", "lillirask", "1234");
            var teacher10 = Teacher.New("Hjordis Villumsen", "hjordisvillumsen", "1234");
            var teachers = Group(teacher01, teacher02, teacher03, teacher04, teacher05, teacher06, teacher07, teacher08, teacher09, teacher10);
            // Rooms
            WriteSetupMessageIndent("Creating rooms");
            var room01 = Room.New("R301");
            var room02 = Room.New("R302");
            var room03 = Room.New("R303");
            var room04 = Room.New("R304");
            var room05 = Room.New("R305");
            var room06 = Room.New("A319");
            var room07 = Room.New("A320");
            var room08 = Room.New("A321");
            var room09 = Room.New("A322");
            var room10 = Room.New("A323");
            var rooms = Group(room01, room02, room03, room04, room05, room06, room07, room08, room09, room10);
            // Messages
            WriteSetupMessageIndent("Creating messages");
            var message01 = Message.New(student01, "Hjælp til dansk", "Tekst", Group<Person>(student02), Group<string>());
            var message02 = Message.New(student01, "Angående aflevering", "Tekst", Group<Person>(student03, student04, teacher01, teacher02), Group<string>());
            var message03 = Message.New(student01, "Kemi spørgsmål", "Tekst", Group<Person>(student05, student06, teacher03, teacher04), Group<string>());
            var message04 = Message.New(student01, "Idrætsdag", "Tekst", Group<Person>(student07, student08, teacher05, teacher06), Group<string>());
            var message05 = Message.New(student01, "Nye åbningstider ved studievejleder", "Tekst", Group<Person>(student09, student10, teacher07, teacher08), Group<string>());
            var message06 = Message.New(student02, "Åbent hus", "Tekst", Group<Person>(student01, teacher09), Group<string>());
            var messages = Group(message01, message02, message03, message04, message05, message06);

            //messages til billeder
            Student student1 = Student.GetByID(2);
            List<Person> recipients = new List<Person>();
            recipients.Add(student1);
            Creator.CreateMessage(student1, "Angående matematik eksamen", "Hej alle kursister på tredje år.  <br/> Lokale til matematik eksamen er skiftet til 301a, tidspunkt er ikke ændret.  <br/> Mvh,  <br/> Studieordning på VUC", recipients, new List<string>());
            // Courses
            WriteSetupMessageIndent("Creating courses");
            var course01 = Course.New("Dansk A", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque non ipsum nec libero tincidunt convallis quis nec turpis. Cras lobortis condimentum vestibulum. Integer felis lectus, imperdiet eu commodo vel, tristique accumsan elit. Aenean mattis metus nibh, quis vestibulum elit vulputate sed. Duis cursus suscipit velit, at egestas massa lacinia et. Phasellus elementum arcu lectus, et convallis massa dictum nec. Aliquam erat volutpat. Mauris congue nibh sapien, id facilisis nibh cras amet.");
            var course02 = Course.New("Matematik A", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque non ipsum nec libero tincidunt convallis quis nec turpis. Cras lobortis condimentum vestibulum. Integer felis lectus, imperdiet eu commodo vel, tristique accumsan elit. Aenean mattis metus nibh, quis vestibulum elit vulputate sed. Duis cursus suscipit velit, at egestas massa lacinia et. Phasellus elementum arcu lectus, et convallis massa dictum nec. Aliquam erat volutpat. Mauris congue nibh sapien, id facilisis nibh cras amet.");
            var course03 = Course.New("Fysik A", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque non ipsum nec libero tincidunt convallis quis nec turpis. Cras lobortis condimentum vestibulum. Integer felis lectus, imperdiet eu commodo vel, tristique accumsan elit. Aenean mattis metus nibh, quis vestibulum elit vulputate sed. Duis cursus suscipit velit, at egestas massa lacinia et. Phasellus elementum arcu lectus, et convallis massa dictum nec. Aliquam erat volutpat. Mauris congue nibh sapien, id facilisis nibh cras amet.");
            var course04 = Course.New("Kemi B", "I kemi B får du kendskab til kemiens centrale begreber og eksperimentelle arbejdsmetoder sammen med en forståelse for stoffers opbygning og kemiske reaktioner.");
            var course05 = Course.New("Engelsk A", "I engelsk A får du et solidt kendskab til det engelske sprogs mundtlige og skriftlige udtryksformer og lærer at begå dig sprogligt i en globaliseret verden.");
            var course06 = Course.New("Samfund B", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque non ipsum nec libero tincidunt convallis quis nec turpis. Cras lobortis condimentum vestibulum. Integer felis lectus, imperdiet eu commodo vel, tristique accumsan elit. Aenean mattis metus nibh, quis vestibulum elit vulputate sed. Duis cursus suscipit velit, at egestas massa lacinia et. Phasellus elementum arcu lectus, et convallis massa dictum nec. Aliquam erat volutpat. Mauris congue nibh sapien, id facilisis nibh cras amet.");
            var course07 = Course.New("Historie B", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque non ipsum nec libero tincidunt convallis quis nec turpis. Cras lobortis condimentum vestibulum. Integer felis lectus, imperdiet eu commodo vel, tristique accumsan elit. Aenean mattis metus nibh, quis vestibulum elit vulputate sed. Duis cursus suscipit velit, at egestas massa lacinia et. Phasellus elementum arcu lectus, et convallis massa dictum nec. Aliquam erat volutpat. Mauris congue nibh sapien, id facilisis nibh cras amet.");
            var course08 = Course.New("Idræt C", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque non ipsum nec libero tincidunt convallis quis nec turpis. Cras lobortis condimentum vestibulum. Integer felis lectus, imperdiet eu commodo vel, tristique accumsan elit. Aenean mattis metus nibh, quis vestibulum elit vulputate sed. Duis cursus suscipit velit, at egestas massa lacinia et. Phasellus elementum arcu lectus, et convallis massa dictum nec. Aliquam erat volutpat. Mauris congue nibh sapien, id facilisis nibh cras amet.");
            var course09 = Course.New("Geografi B", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque non ipsum nec libero tincidunt convallis quis nec turpis. Cras lobortis condimentum vestibulum. Integer felis lectus, imperdiet eu commodo vel, tristique accumsan elit. Aenean mattis metus nibh, quis vestibulum elit vulputate sed. Duis cursus suscipit velit, at egestas massa lacinia et. Phasellus elementum arcu lectus, et convallis massa dictum nec. Aliquam erat volutpat. Mauris congue nibh sapien, id facilisis nibh cras amet.");
            var course10 = Course.New("Religion B", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque non ipsum nec libero tincidunt convallis quis nec turpis. Cras lobortis condimentum vestibulum. Integer felis lectus, imperdiet eu commodo vel, tristique accumsan elit. Aenean mattis metus nibh, quis vestibulum elit vulputate sed. Duis cursus suscipit velit, at egestas massa lacinia et. Phasellus elementum arcu lectus, et convallis massa dictum nec. Aliquam erat volutpat. Mauris congue nibh sapien, id facilisis nibh cras amet.");
            var courses = Group(course01, course02, course03, course04, course05, course06, course07, course08, course09, course10);
            // Course Students
            WriteSetupMessageIndent("Adding students to courses");
            foreach (Course course in courses)
                foreach (Student student in students)
                    course.AddStudent(student);
            // Course Teachers
            WriteSetupMessageIndent("Adding teachers to courses");
            course01.AddTeacher(teacher01);
            course01.AddTeacher(teacher02);
            course02.AddTeacher(teacher02);
            course03.AddTeacher(teacher03);
            course04.AddTeacher(teacher04);
            course05.AddTeacher(teacher05);
            course06.AddTeacher(teacher06);
            course07.AddTeacher(teacher07);
            course08.AddTeacher(teacher08);
            course09.AddTeacher(teacher09);
            course09.AddTeacher(teacher10);
            course10.AddTeacher(teacher10);
            // Lessons
            GenerateLessons("05", "09", "2016", 40, courses, rooms);
            // CustomLesson
            Lesson customLesson = Lesson.GetByConditions("DateTime='2017-01-24 10:00:00'").Single();
            customLesson.Description = "Lav opgaverne i den vedhæftede fil";
            Commands.InsertInto("lessonfile", customLesson.ID.ToString(), "opgaver.pdf");


            //// AssignmentDescriptions --- KOMMENTERET UD TIL AT LAVE BILLEDER AF DESIGN
            //WriteSetupMessageIndent("Creating AssignmentDescriptions");
            //const string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent et nisl ipsum. Nunc nec eros vel dolor semper maximus. Suspendisse cursus in mi quis vehicula. Quisque elit risus, aliquet sit amet sem ut, facilisis eleifend libero. Donec ultricies nibh ut quam condimentum, non sollicitudin leo sed.";
            //var deadline = DateTime.Today;
            //var assignmentDescriptions = new List<AssignmentDescription>();
            //foreach (Course course in courses)
            //{
            //    assignmentDescriptions.Add(AssignmentDescription.New(course, "Title", description, deadline.AddDays(Rng.Next(-50, 50)), Group<string>()));
            //    assignmentDescriptions.Add(AssignmentDescription.New(course, "Title", description, deadline.AddDays(Rng.Next(-50, 50)), Group<string>()));
            //    assignmentDescriptions.Add(AssignmentDescription.New(course, "Title", description, deadline.AddDays(Rng.Next(-50, 50)), Group<string>()));
            //    assignmentDescriptions.Add(AssignmentDescription.New(course, "Title", description, deadline.AddDays(Rng.Next(-50, 50)), Group<string>()));
            //    assignmentDescriptions.Add(AssignmentDescription.New(course, "Title", description, deadline.AddDays(Rng.Next(-50, 50)), Group<string>()));
            //}
            //// Assignments
            //WriteSetupMessageIndent("Creating Assignments");
            //var assignments = new List<Assignment>();
            //foreach (AssignmentDescription assignmentDescription in assignmentDescriptions)
            //{
            //    List<Student> choices = new List<Student>(students);
            //    choices.RemoveAt(Rng.Next(0, choices.Count - 1));
            //    assignments.AddRange(choices.Select(student =>
            //        Assignment.New(assignmentDescription, student, "Lorem ipsum dolor sit amet, consectetur cras amet.", Group<string>())));
            //}
            //// AssignmentGrades
            //WriteSetupMessageIndent("Creating AssignmentGrades");
            //foreach (Assignment assignment in assignments)
            //    assignment.Grade = AssignmentGrade.New(TakeRandom(Common.ValidGrades.ToList()), "Vi gennemgår resultaterne af disse afleveringer i klassen", assignment);


            Creator.CreateAssignmentDescription(course01, "Dansk A, analyse af selvvalgt novelle", "Lav en analyse af en novelle, som følger analyse-kravene i kapitel 7", (DateTime.Now - TimeSpan.FromDays(5)), Group<string>());
            Creator.CreateAssignmentDescription(course02, "Matematik A, test af eksamenssæt", "Fuldfør til egen evne eksamenssættet fra sidste år", (DateTime.Now - TimeSpan.FromDays(1)), Group<string>());
            AssignmentDescription assignmentDescription1 = AssignmentDescription.GetLatest();
            List<string> aflevering = new List<string>();
            aflevering.Add("eksamenssæt.txt");
            Creator.CreateAssignment(assignmentDescription1, student1, "Vi gennemgår resultaterne af disse afleveringer i klassen", aflevering);
            Assignment.GetLatest().Grade = AssignmentGrade.New("10", "Vi gennemgår resultaterne af disse afleveringer i klassen", Assignment.GetLatest());
            Creator.CreateAssignmentDescription(course03, "Fysik A, tyngdekraft", "Lav en analyse af de forsøg vi foretog af tyngdekraften", DateTime.Now.AddDays(5), Group<string>());

            // CourseGrades
            WriteSetupMessageIndent("Creating CourseGrades");
            foreach (Course course in courses)
                foreach (Student student in students)
                    CourseGrade.New(course, student, TakeRandom(Common.ValidGrades.ToList()), "");
            List<CourseGrade> cg = CourseGrade.GetByConditions("StudentID=" + student01.ID);
            cg[0].Grade = "10";
            cg[0].Comment = "Godt gået, deltag lidt mere i timerne for at få 12.";
            cg[1].Grade = "02";
            cg[1].Comment = "Det er for dårligt, deltag mere i timerne og lav dine afleveringer.";
            cg[2].Grade = "7";
            cg[2].Comment = "Du klarer dig fint, jeg ved du kan gøre det bedre.";
            cg[3].Grade = "4";
            cg[3].Comment = "Deltag i undervisning og aflever dine afleveringer.";
            cg[4].Grade = "02";
            cg[4].Comment = "Ved du kan gøre det bedre.";
            cg[5].Grade = "-3";
            cg[5].Comment = "Tag dig sammen, og deltag i undervisningen.";
            cg[6].Grade = "7";
            cg[6].Comment = "Du klarer dig godt, brug lidt mere tid på dine afleveringer.";
            cg[7].Grade = "12";
            cg[7].Comment = "Du deltager aktivt i undervisning. Godt at se!";
            cg[8].Grade = "12";
            cg[8].Comment = "Godt klaret, godt at se.";
            cg[9].Grade = "00";
            cg[9].Comment = "Du skal begynde at deltage i undervisningen.";
        }
        private static List<T> Group<T>(params T[] objects) => objects.ToList();
        private static void GenerateLessons(string startDay, string startMonth, string startYear, int weeksToGenerate, List<Course> courses, List<Room> rooms)
        {
            DateTime dateTime = DateTime.ParseExact(startDay + "/" + startMonth + "/" + startYear + "-12:00", "dd/MM/yyyy-HH:mm", null);
            int daysToGenerate = weeksToGenerate * 5;
            int generatedDays = 0;
            int generatedWeeks = 0;
            while (generatedWeeks < weeksToGenerate)
            {
                GenerateLessonsForDay(dateTime.ToString("dd/MM/yyyy"), courses, rooms);
                dateTime = dateTime.AddDays(1);
                generatedDays += 1;
                WriteLessonsProgress(generatedDays, daysToGenerate);
                GenerateLessonsForDay(dateTime.ToString("dd/MM/yyyy"), courses, rooms);
                dateTime = dateTime.AddDays(1);
                generatedDays += 1;
                WriteLessonsProgress(generatedDays, daysToGenerate);
                GenerateLessonsForDay(dateTime.ToString("dd/MM/yyyy"), courses, rooms);
                dateTime = dateTime.AddDays(1);
                generatedDays += 1;
                WriteLessonsProgress(generatedDays, daysToGenerate);
                GenerateLessonsForDay(dateTime.ToString("dd/MM/yyyy"), courses, rooms);
                dateTime = dateTime.AddDays(1);
                generatedDays += 1;
                WriteLessonsProgress(generatedDays, daysToGenerate);
                GenerateLessonsForDay(dateTime.ToString("dd/MM/yyyy"), courses, rooms);
                dateTime = dateTime.AddDays(3);
                generatedDays += 1;
                WriteLessonsProgress(generatedDays, daysToGenerate);
                generatedWeeks += 1;
            }
            Console.WriteLine();
        }
        private static void WriteLessonsProgress(int progress, int goal)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\r  > Generating Lessons (" + progress + "/" + goal + " days)");
            Console.ResetColor();
        }
        private static readonly string[] TimeSlots = { "08:10", "09:05", "10:00", "10:55", "12:05", "13:00" };
        private static readonly string[] ExtraSlots = { "13:55", "14:50" };
        private static void GenerateLessonsForDay(string day, List<Course> courses, List<Room> rooms)
        {
            foreach (string timeSlot in TimeSlots)
            {
                List<Room> selectedRooms = new List<Room> { TakeRandom(rooms) };
                Lesson lesson = Lesson.New(TakeRandom(courses), "Lorem ipsum dolor sit amet.", true,
                    DateTime.ParseExact(day + "-" + timeSlot, "dd/MM/yyyy-HH:mm", null), selectedRooms, Group<string>());
                if (DateTime.Now > lesson.DateTime)
                    lesson.GiveAbsence(TakeRandom(lesson.Course.Students));
            }
            if (Rng.Next(1, 100) > 50)
            {
                List<Room> selectedRooms = new List<Room> { TakeRandom(rooms) };
                Lesson lesson = Lesson.New(TakeRandom(courses), "Lorem ipsum dolor sit amet.", false,
                    DateTime.ParseExact(day + "-" + ExtraSlots[0], "dd/MM/yyyy-HH:mm", null), selectedRooms, Group<string>());
                if (DateTime.Now > lesson.DateTime)
                    lesson.GiveAbsence(TakeRandom(lesson.Course.Students));
                selectedRooms.Clear();
                selectedRooms.Add(TakeRandom(rooms));
                Lesson lesson2 = Lesson.New(TakeRandom(courses), "Lorem ipsum dolor sit amet.", false,
                    DateTime.ParseExact(day + "-" + ExtraSlots[1], "dd/MM/yyyy-HH:mm", null), selectedRooms, Group<string>());
                if (DateTime.Now > lesson.DateTime)
                    lesson2.GiveAbsence(TakeRandom(lesson.Course.Students));
            }
        }
        private static readonly Random Rng = new Random(DateTime.Now.Millisecond);
        private static T TakeRandom<T>(IReadOnlyList<T> choices)
        {
            return choices[Rng.Next(0, choices.Count - 1)];
        }

        private static void ExecuteQuery(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            using (MySqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = query;
                command.ExecuteNonQuery();
            }
        }
        private static void CreateTable(string tableName, params string[] variables)
        {
            WriteSetupMessageIndent("Creating table " + tableName);
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
        private static void WriteSetupMessageIndent(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  > " + message);
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