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
        //call IUnitOfWork to use functions of UserDao
        public ManageUserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Admin/MangageUser
        //get list of users: active, unactive, volunteer
        public ActionResult Index()
        {
            return View(new UserDao(_unitOfWork).GetAllUser());
        }
        // Get info of acoount user
        public ActionResult UserInfo(int id)
        {
            return View(new UserDao(_unitOfWork).GetUserById(id));
        }
        //method unactive or active account
        public ActionResult UnOrActiveAccount(int id)
        {
            new UserDao(_unitOfWork).UnOrActiveAccount(id);
            TempData[MessageConst.SUCCESS] = "Update Successfully !";
            return RedirectToAction("Index");
        }
        //method set volunteer for account user or not
        public ActionResult SetOrUnsetVolunteerAccount(int id)
        {
            new UserDao(_unitOfWork).SetOrUnsetVolunteerAccount(id);
            TempData[MessageConst.SUCCESS] = "Update Successfully !";
            return RedirectToAction("Index");
        }
    }
}