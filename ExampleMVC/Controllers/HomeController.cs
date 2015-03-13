using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Dwolla;

namespace ExampleMVC.Controllers
{
    public class HomeController : Controller
    {

        public Dwolla.OAuth o = new OAuth();

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Handles OAuth handshake
        /// </summary>
        /// <returns></returns>
        public ActionResult OAuth()
        {
            return Redirect(o.GenAuthUrl().ToString());
        }

    }
}
