using StudyPlatform.Classes.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Model
{
    public class Room : Entity<Room>
    {
        private string _name;

        public Room(uint id, string name) : base(id)
        {
            _name = name;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                Commands.SetValue("rooms", ID, "name", value);
                _name = value;
            }
        }

        public List<Lesson> Reservations => GetRelations<Lesson>("LessonRoom", "LessonID", "RoomID", ID);

        public bool CheckAvailability(DateTime dateTime)
        {
            foreach (Lesson reservation in Reservations)
                if (reservation.DateTime.Date == dateTime.Date)
                    if (dateTime.CompareTo(reservation.DateTime) == 1 &&
                        dateTime.CompareTo(reservation.DateTime.Add(Lesson.Length)) == -1)
                        return false;
            return true;

            // Should check the reservations to see if the date given is available. 
            // Use Lesson.Length as TimeSpan of the reservations.

            // Throw RoomUnavailableException if not available.
            //throw new NotImplementedException();
        }
        public void Remove() => Remover.RemoveRoom(this);
        public static Room New(string name)
        {
            Creator.CreateRoom(name);
            return GetLatest(1).Single();
        }
    }
}