using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatformMVC.Models;

namespace StudyPlatformMVC.ViewModels
{
    public class MessageViewModel
    {
        public List<Message> Messages;
        public Message CurrentMessage;
    }
}