using StudyPlatform.Classes.Database;
using System;

namespace StudyPlatform.Classes.Model
{
    public class Lesson
    {
        private uint _id;
        public uint ID
        {
            get
            {
                return _id;
            }
        }
        private DateTime _date;
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
        private string _description;
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
        private bool _online;
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

        private bool _active;
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
        public Lesson(uint id, DateTime date, string description, bool online, bool active)
        {
            _id = id;
            _date = date;
            _description = description;
            _online = online;
            _active = active;
        }
    }
}