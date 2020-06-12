using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMvc.Models.Dao;

namespace WebMvc.Areas.Admin.Controllers
{
    public class NavigationController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public NavigationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Admin/Navigation
        //Display the number of questions the user needs admin to answer
        public PartialViewResult Index()
        {
            TempData["CountQuestion"] = new UserQuestionDao(_unitOfWork).GetAll().Count(s => s.QuesNew).ToString();
            return PartialView();
        }
    }
}