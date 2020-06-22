using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class ErrorClientController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }
    }
}