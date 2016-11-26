using StudyPlatform.Classes.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Model
{
    public class Lesson
    {
        private uint _id;
        private uint _courseid;
        private string _description;
        private bool _online;
        private bool _cancelled;
        private DateTime _dateTime;

        public Lesson(uint id, uint courseid, string description, bool online, bool cancelled, DateTime dateTime)
        {
            _id = id;
            _courseid = courseid;
            _description = description;
            _online = online;
            _cancelled = cancelled;
            _dateTime = dateTime;
        }
        
        public uint ID => _id;
        public Course Course => Getters.GetCourseByID(_courseid);
        public DateTime DateTime
        {
            get
            {
                return _dateTime;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                else
                {
                    foreach (Room room in Rooms)
                        room.CheckAvailability(value);
                    Commands.SetValue("Lesson", ID, "DateTime", value.ToString("yyyy-MM-dd HH:mm:ss"));
                    _dateTime = value;
                }
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                else
                {
                    Commands.SetValue("Lesson", ID, "Description", value);
                    _description = value;
                }
            }

        }
        public bool Online
        {
            get
            {
                return _online;
            }
            set
            {

                Commands.SetValue("Lesson", ID, "Online", value.ToString().ToUpper());
                _online = value;
            }
        }
        public bool Cancelled
        {
            get
            {
                return _cancelled;
            }
            set
            {
                Commands.SetValue("Lesson", ID, "Cancelled", value.ToString().ToUpper());
                _cancelled = value;
            }
        }
        public List<Room> Rooms
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public List<Student> Absences
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public List<string> Documents
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public static TimeSpan LessonLength => new TimeSpan(0, 45, 0);

        public void Remove() => Remover.RemoveLesson(this);
        public void GiveAbsence(Student student) =>
            Commands.InsertInto("StudentAbsence", student.ID.ToString(), ID.ToString());
        public void RemoveAbsence(Student student) =>
            Commands.DeleteFrom("StudentAbsence", "StudentID=" + student.ID + " AND LessonID=" + ID);

        public static Lesson GetByID(uint id) => Getters.GetLessonByID(id);
        public static List<Lesson> Find(params string[] conditions) => Getters.GetLessonsByConditions(conditions);
        public static List<Lesson> AllLessons => Lists.Lessons;
        public static Lesson New(Course course, string description, bool online,
            DateTime dateTime, List<Room> rooms, List<string> filepaths)
        {
            Creator.CreateLesson(course, description, online, dateTime, rooms, filepaths);
            return Getters.GetLatestLessons(1).Single();
        }
    }
}