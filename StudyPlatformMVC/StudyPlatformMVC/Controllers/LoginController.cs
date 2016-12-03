using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyPlatformMVC.Exceptions;
using StudyPlatformMVC.Models;
using LoginViewModel = StudyPlatformMVC.ViewModels.LoginViewModel;

namespace StudyPlatformMVC.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["user"] != null)
                RedirectToAction("Index", "News");
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            try
            {
                Session["user"] = Person.GetByConditions("username=" + model.Username, "password=" + model.Password).Single();
                return RedirectToAction("Index", "News");
            }
            catch (InvalidIDException)
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}