using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class Message
    {
        private readonly uint _senderid;

        public Message(uint id, uint senderid, string title, string text, DateTime dateTime)
        {
            ID = id;
            _senderid = senderid;
            Title = title;
            Text = text;
            Date = dateTime;
        }

        public uint ID { get; }
        public Person Sender => Getters.GetPersonByID(_senderid);
        public string Title { get; }
        public string Text { get; }
        public DateTime Date { get; }
        public List<Person> Recipients
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public List<string> Attachments
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public static Message New(Person sender, string title, string text, List<Person> recipients, List<string> filepaths)
        {
            Creator.CreateMessage(sender, title, text, recipients, filepaths);
            return Getters.GetLatestMessages(1).Single();
        }
        public void Remove() => Remover.RemoveMessage(this);
    }
}