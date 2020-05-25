using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Repository;
using WebSite.Models.Dao;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unit;

        public HomeController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public ActionResult Index()
        {
           var data = new RoleDao(_unit).GetAll();
            return View(data.Result.ToList());
        }

       
    }
}