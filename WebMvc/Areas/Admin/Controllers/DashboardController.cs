using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Repository;
using WebMvc.Models.Dao;

namespace WebMvc.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            ViewBag.ListUserDonate = new UserDao(_unitOfWork).getUserDonateInCurrentDate();

            return View();
        }
    }
}