using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    /// <summary>
    /// Controller for handling client-side errors.
    /// </summary>
    public class ErrorClientController : Controller
    {
        /// <summary>
        /// Displays the error page.
        /// </summary>
        /// <returns>The error view.</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}