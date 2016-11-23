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
        private DateTime _date;
        private string _description;
        private bool _online;
        private bool _active;

        public Lesson(uint id, uint courseid, DateTime date, string description, bool online, bool active)
        {
            _id = id;
            _courseid = courseid;
            _date = date;
            _description = description;
            _online = online;
            _active = active;
        }
        
        public uint ID
        {
            get
            {
                return _id;
            }
        }
        public Course Course
        {
            get
            {
                return Getters.GetCourseByID(_courseid);
            }
        }
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                else
                {
                    Commands.SetValue("lessons", ID, "date", value.ToString("yyyy-MM-dd HH:mm:ss"));
                    _date = value;
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
                    Commands.SetValue("lessons", ID, "description", value);
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

                Commands.SetValue("lessons", ID, "online", value.ToString().ToUpper());
                _online = value;
            }
        }
        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                Commands.SetValue("lessons", ID, "active", value.ToString().ToUpper());
                _active = value;
            }
        }
        public List<Room> Rooms
        {
            get
            {

            }
        }
        public List<Student> Absences
        {
            get
            {

            }
        }
        public List<string> Documents
        {

        }
    }
}