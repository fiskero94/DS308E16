using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class Message : Entity<Message>
    {
        private readonly uint _senderid;

        public Message(uint id, uint senderid, string title, string text, DateTime dateTimeSent) : base(id)
        {
            _senderid = senderid;
            Title = title;
            Text = text;
            DateTimeSent = dateTimeSent;
        }

        public Person Sender => Person.GetByID(_senderid);
        public string Title { get; }
        public string Text { get; }
        public DateTime DateTimeSent { get; }
        public List<Person> Recipients => GetRelations<Person>("MessageRecipient", "PersonID", "MessageID", ID);
        public List<string> Attachments => GetDocuments("MessageFile", "MessageID", ID);

        public void Remove() => Remover.RemoveMessage(this);
        public static Message New(Person sender, string title, string text, List<Person> recipients, List<string> filepaths)
        {
            Creator.CreateMessage(sender, title, text, recipients, filepaths);
            return GetLatest(1).Single();
        }
    }
}