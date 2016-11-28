using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatformMVC.Database;
using StudyPlatformMVC.Exceptions;

namespace StudyPlatformMVC.Models
{
    public class Lesson : Entity<Lesson>
    {
        private readonly uint _courseid;
        private string _description;
        private bool _online;
        private bool _cancelled;
        private DateTime _dateTime;

        public Lesson(uint id, uint courseid, string description, bool online, bool cancelled, DateTime dateTime) : base(id)
        {
            _courseid = courseid;
            _description = description;
            _online = online;
            _cancelled = cancelled;
            _dateTime = dateTime;
        }

        public Course Course => Course.GetByID(_courseid);
        public DateTime DateTime
        {
            get
            {
                return _dateTime;
            }
            set
            {
                foreach (Room room in Rooms)
                    if (!room.CheckAvailability(value))
                        throw new RoomUnavailableException(room.Name + " er ikke tilgængelig for tidspunktet " + value);
                Commands.SetValue("Lesson", ID, "DateTime", value.ToString("yyyy-MM-dd HH:mm:ss"));
                _dateTime = value;
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
                Commands.SetValue("Lesson", ID, "Description", value);
                _description = value;
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
        public List<Room> Rooms => GetRelations<Room>("LessonRoom", "RoomID", "LessonID", ID);
        public List<Student> Absences => GetRelations<Student>("StudentAbsence", "StudentID", "LessonID", ID);
        public List<string> Documents => GetDocuments("LessonFile", "LessonID", ID);
        public static readonly TimeSpan Length = new TimeSpan(0, 45, 0);

        public void Remove() => Remover.RemoveLesson(this);
        public void GiveAbsence(Student student) =>
            Commands.InsertInto("StudentAbsence", student.ID.ToString(), ID.ToString());
        public void RemoveAbsence(Student student) =>
            Commands.DeleteFrom("StudentAbsence", "StudentID=" + student.ID + " AND LessonID=" + ID);


        public static Lesson New(Course course, string description, bool online,
            DateTime dateTime, List<Room> rooms, List<string> filepaths)
        {
            Creator.CreateLesson(course, description, online, dateTime, rooms, filepaths);
            return GetLatest(1).Single();
        }
    }
}