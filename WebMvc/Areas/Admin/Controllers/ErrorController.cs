using System.Web.Mvc;

namespace WebMvc.Areas.Admin.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Admin/Error
        public ActionResult Page404()
        {
            return View();
        }
    }
}