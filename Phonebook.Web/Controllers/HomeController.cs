﻿using System.Web.Mvc;

namespace Phonebook.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}