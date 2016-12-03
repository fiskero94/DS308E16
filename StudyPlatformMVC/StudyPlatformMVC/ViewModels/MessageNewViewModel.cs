using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyPlatformMVC.Models;

namespace StudyPlatformMVC.ViewModels
{
    public class MessageNewViewModel
    {
        public IEnumerable<SelectListItem> Persons;
        public string[] SelectedPersons;
        public string Title;
        public string Text;
        public Person Sender;
    }
}