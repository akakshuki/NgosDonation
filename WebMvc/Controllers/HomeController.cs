using Domain.Repository;
using System.Web.Mvc;
using WebMvc.Models.Dao;

namespace WebMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _provider;

        public HomeController(IUnitOfWork provider)
        {
            _provider = provider;
        }

        public ActionResult Index()
        {
            ViewBag.data = new RoleDao(_provider).GetAllRole();

            return View();
        }
    }
}