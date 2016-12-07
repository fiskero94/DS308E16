using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyPlatform.Classes.Model;

namespace StudyPlatform.Tests
{
    static class Instances
    {
        public static Assignment Assignment => new Assignment(ID, ID, ID, Comment, ID, Date);
        public static AssignmentDescription AssignmentDescription => new AssignmentDescription(ID, ID, Title, Description, Cancelled, Date);
        public static AssignmentGrade AssignmentGrade => new AssignmentGrade(ID, ID, Grade, Comment);
        public static Course Course => new Course(ID, Name, Description);
        public static CourseGrade CourseGrade => new CourseGrade(ID, ID, ID, Grade, Comment);
        public static Lesson Lesson => new Lesson(ID, ID, Description, Online, Cancelled, Date);
        public static Message Message => new Message(ID, ID, Title, Text, Date);
        public static News News => new News(ID, ID, Title, Text, Date);
        public static Room Room => new Room(ID, Name);
        public static Secretary Secretary => new Secretary(ID, Name);
        public static Teacher Teacher => new Teacher(ID, Name);
        public static Student Student => new Student(ID, Name);
        public static uint ID => 1;
        public static DateTime Date => DateTime.Now;
        public static string Comment => "Comment";
        public static string Description => "Description";
        public static string Grade => "12";
        public static string Name => "Name";
        public static string Title => "Title";
        public static string Text => "Text";
        public static string Username => "Username";
        public static string Password => "Password";
        public static string EmptyString => "";
        public static bool Online => true;
        public static bool Cancelled => true;
        public static List<string> Filepaths => new List<string>();
        public static List<Person> Recipients => new List<Person>();
        public static List<Room> Rooms => new List<Room>();
    }
}