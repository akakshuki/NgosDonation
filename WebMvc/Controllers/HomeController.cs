using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Repository;
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