using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Model;

namespace StudyPlatform.Classes.Model
{
    public abstract class Person : Entity<Person>
    {
        private string _name;

        protected Person(uint id, string name) : base(id)
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
                Commands.SetValue("Person", ID, "name", value);
                _name = value;
            }
        }
        public List<Message> SentMessages => Message.GetByConditions("SenderID=" + ID);
        public List<Message> RecievedMessages
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.MessageRecipient WHERE PersonID=" + ID + ";");
                uint[] mailUints = Extractor.ExtractIDs(query.Execute(), "MessageID");
                List<Message> messages = new List<Message>();
                foreach (uint mailID in mailUints)
                {
                    messages.Add(Message.GetByID(mailID));
                }
                return messages;
            }
        }
        public void Remove() => Remover.RemovePerson(this);
    }
}