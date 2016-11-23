using StudyPlatform.Classes.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Model
{
    public class Room
    {
        private uint _id;
        private string _name;

        public Room(uint id, string name)
        {
            _id = id;
            _name = name;
        }

        public uint ID
        {
            get
            {
                return _id;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if(value == null)
                {
                    throw new ArgumentNullException();
                }
                else
                {
                    Commands.SetValue("rooms", ID, "name", value);
                    _name = value;
                }
            }
        }
        public List<Lesson> Reservations
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.roomreservations" + ID);
                uint[] ids = Extractor.ExtractIDs(query.Execute());
                List<Lesson> lessons = new List<Lesson>();
                foreach (uint id in ids)
                    lessons.Add(Getters.GetLessonByID(id));
                return lessons;
            }
        }
    }
}