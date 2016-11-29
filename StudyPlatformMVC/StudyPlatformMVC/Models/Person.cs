using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatformMVC.Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyPlatformMVC.Models
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
        public List<Message> RecievedMessages => GetRelations<Message>("MessageRecipient", "MessageID", "PersonID", ID);

        public void Remove() => Remover.RemovePerson(this);
    }
}