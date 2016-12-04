using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using StudyPlatformMVC.Exceptions;
using StudyPlatformMVC.Models;
using StudyPlatformMVC.ViewModels;

namespace StudyPlatformMVC.Controllers
{
    public class MessagesController : Controller
    {
        [Route("Messages/Sent")]
        public ActionResult Sent()
        {
            // Testing
            Session["user"] = Student.GetByConditions("name='Iver Clausen'").Single();
            // Actual
            Person person = (Person)Session["user"];
            if (person == null)
                return RedirectToAction("Index", "Login");
            return View(new MessageViewModel
            {
                Messages = person.SentMessages
            });
        }
        [Route("Messages/Sent/{id}")]
        public ActionResult Sent(int id)
        {
            // Testing
            Session["user"] = Student.GetByConditions("name='Iver Clausen'").Single();
            // Actual
            Person person = (Person)Session["user"];
            Message currentMessage;
            if (person == null)
                return RedirectToAction("Index", "Login");
            try { currentMessage = Message.GetByID(Convert.ToUInt32(id)); }
            catch (InvalidIDException) { return RedirectToAction("Sent", "Messages"); }
            if (currentMessage.Sender.ID != person.ID)
                return RedirectToAction("Sent", "Messages");
            return View(new MessageViewModel
            {
                Messages = person.SentMessages,
                CurrentMessage = currentMessage
            });
        }
        [Route("Messages/Recieved")]
        public ActionResult Recieved()
        {
            // Testing
            Session["user"] = Student.GetByConditions("name='Iver Clausen'").Single();
            // Actual
            Person person = (Person)Session["user"];
            if (person == null)
                return RedirectToAction("Index", "Login");
            return View(new MessageViewModel
            {
                Messages = person.RecievedMessages
            });
        }
        [Route("Messages/Recieved/{id}")]
        public ActionResult Recieved(int id)
        {
            // Testing
            Session["user"] = Student.GetByConditions("name='Iver Clausen'").Single();
            // Actual
            Person person = (Person)Session["user"];
            Message currentMessage;
            if (person == null)
                return RedirectToAction("Index", "Login");
            try { currentMessage = Message.GetByID(Convert.ToUInt32(id)); }
            catch (InvalidIDException) { return RedirectToAction("Recieved", "Messages"); }
            if (currentMessage.Recipients.Any(recipient => recipient.ID == person.ID))
                return View(new MessageViewModel
                {
                    Messages = person.RecievedMessages,
                    CurrentMessage = currentMessage
                });
            return RedirectToAction("Recieved", "Messages");
        }

        [Route("Messages/New")]
        public ActionResult New()
        {
            // Testing
            Session["user"] = Student.GetByConditions("name='Iver Clausen'").Single();
            // Actual
            Person user = (Person)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            List<SelectListItem> persons = Person.GetAll().Select(person => 
            new SelectListItem {Value = person.ID.ToString(), Text = person.Name}).ToList();
            return View(new MessageNewViewModel { Persons = persons });
        }

        [HttpPost]
        public ActionResult New(MessageNewViewModel model)
        {
            Person user = (Person)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            List<Person> recipients = model.SelectedPersons.Select(recipient => 
                Person.GetByID(Convert.ToUInt32(recipient))).ToList();
            Message.New(user, model.Title, model.Text, recipients, new List<string>());
            return RedirectToAction("Sent", "Messages");
        }
    }
}