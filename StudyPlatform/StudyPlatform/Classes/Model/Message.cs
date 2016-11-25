using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class Message
    {
        private uint _id;
        private uint _senderid;
        private string _title;
        private string _text;
        private DateTime _date;

        public Message(uint id, uint senderid, string title, string text)
        {
            _id = id;
            _senderid = senderid;
            _title = title;
            _text = text;
            _date = DateTime.Now;
        }

        public uint ID
        {
            get
            {
                return _id;
            }
        }
        public Person Sender
        {
            get
            {
                return Getters.GetPersonByID(_senderid);
            }
        }
        public string Title
        {
            get
            {
                return _title;
            }
        }
        public string Text
        {
            get
            {
                return _text;
            }
        }
        public DateTime Date
        {
            get
            {
                return _date;
            }
        }
        public List<Person> Recipients
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.MessageRecipient" );
                uint[] ids = Extractor.ExtractIDs(query.Execute());
                List<Person> recipients = new List<Person>();
                foreach (uint id in ids)
                    recipients.Add(Getters.GetPersonByID(id));
                return recipients;
            }
        }
        public List<string> Attachments
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.messageattachments" + ID);
                string[] filepaths = Extractor.ExtractFilepaths(query.Execute());
                return filepaths.ToList();
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