﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyPlatformMVC.Controllers
{
    public class AssignmentsController : Controller
    {
        // GET: Assignments
        public ActionResult Index()
        {
            return View();
        }
    }
}