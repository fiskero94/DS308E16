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

        public uint ID => _id;

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
                uint[] ids = Extractor.ExtractIDs(query.Execute(), "field");
                List<Lesson> lessons = new List<Lesson>();
                foreach (uint id in ids)
                    lessons.Add(Getters.GetLessonByID(id));
                return lessons;
            }
        }

        public static Room New(string name)
        {
            Creator.CreateRoom(name);
            return Getters.GetLatestRooms(1).Single();
        }
        public void Remove() => Remover.RemoveRoom(this);
        public bool CheckAvailability(DateTime date)
        {
            // Should check the reservations to see if the date given is available. 
            // Use Lesson.LessonLength as TimeSpan of the reservations.

            // Throw RoomUnavailableException if not available.
            throw new NotImplementedException();
        }
    }
}