using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyPlatformMVC.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        
        public ActionResult Index()
        {
            
            return View();
        }
        

            /*
        public string Index()
        {
            return "This is my default news action..";
        }

        //GET: Welcome 
        public string Welcome()
        {
            return "This is the news action method";
        }
        */
    }
}