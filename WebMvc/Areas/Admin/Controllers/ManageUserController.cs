using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.EF;
using Domain.Repository;
using WebMvc.Common;
using WebMvc.Controllers;
using WebMvc.Models.Dao;

namespace WebMvc.Areas.Admin.Controllers
{
    public class ManageUserController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public ManageUserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Admin/MangageUser
        public ActionResult Index()
        {
            return View(new UserDao(_unitOfWork).GetAllUser());
        }
        // Get info a use account
        public ActionResult UserInfo(int id)
        {
            return View(new UserDao(_unitOfWork).GetUserById(id));
        }

        public ActionResult UnOrActiveAccount(int id)
        {
            new UserDao(_unitOfWork).UnOrActiveAccount(id);
            TempData[MessageConst.SUCCESS] = "Update Success !";
            return RedirectToAction("Index");
        }
        public ActionResult SetOrUnsetVolunteerAccount(int id)
        {
            new UserDao(_unitOfWork).SetOrUnsetVolunteerAccount(id);
            TempData[MessageConst.SUCCESS] = "Update Success !";
            return RedirectToAction("Index");
        }
    }
}