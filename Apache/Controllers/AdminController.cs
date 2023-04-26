using Apache.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apache.Controllers
{
    [ErrorHandler]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            //Api.
            return View();
        }
    }
}