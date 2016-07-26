using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebApplicat.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Blog()
        {
            return View();
        }
    }
}
