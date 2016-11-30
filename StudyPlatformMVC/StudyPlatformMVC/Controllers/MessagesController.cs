using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyPlatformMVC.Models;

namespace StudyPlatformMVC.Controllers
{
    public class MessagesController : Controller
    {
        public ViewResult Sent()
        {
            Student student = Student.GetByConditions("name='Iver Clausen'").Single();
            return View(student.SentMessages);
            // return View(((Person)Session["user"]).SentMessages);
        }
        public ViewResult Recieved()
        {
            Student student = Student.GetByConditions("name='Iver Clausen'").Single();
            return View(student.RecievedMessages);
            // return View(((Person)Session["user"]).RecievedMessages);
        }
    }
}