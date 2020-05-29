using Domain.Repository;
using System;
using System.Web.Mvc;
using WebMvc.Common;
using WebMvc.Controllers;
using WebMvc.Models.Dao;
using WebMvc.Models.ModelView;

namespace WebMvc.Areas.Admin.Controllers
{
    public class ManageDonateController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManageDonateController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View(new DonateDao(_unitOfWork).GetAll());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categories = new CategoryDao(_unitOfWork).GetAll();
            return View();
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            return View(new DonateDao(_unitOfWork).GetById(id));
        }

        [HttpPost]
        public ActionResult Create(DonateDTO donate)
        {
            ViewBag.Categories = new CategoryDao(_unitOfWork).GetAll();
            if (donate.EndDay < donate.StartDay || donate.StartDay >= DateTime.Now)
            {
                TempData[MessageConst.ERROR] = "Date time is invalid";
                return View();
            }
            if (!ModelState.IsValid) return View();

            if (new DonateDao(_unitOfWork).Create(donate))
            {
                TempData[MessageConst.SUCCESS] = "Success !";
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Categories = new CategoryDao(_unitOfWork).GetAll();
            var data = new DonateDao(_unitOfWork).GetByid(id);
            if (data == null) return RedirectToAction("Page404", "Error");
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(DonateDTO donate)
        {
            var data = new DonateDao(_unitOfWork).GetByid(donate.ID);
            ViewBag.Categories = new CategoryDao(_unitOfWork).GetAll();
            if (donate.EndDay < donate.StartDay || donate.StartDay >= DateTime.Now)
            {
                TempData[MessageConst.ERROR] = "Date time is invalid";
                return View(data);
            }
            if (!ModelState.IsValid) return View(data);

            if (new DonateDao(_unitOfWork).Edit(donate))
            {
                TempData[MessageConst.SUCCESS] = "Success !";
                return RedirectToAction("Index");
            }
            TempData[MessageConst.ERROR] = "Edit Failed";
            return View(data);
        }

        public ActionResult HideDonate(int id)
        {
            if (new DonateDao(_unitOfWork).GetById(id).UserDonates.Count > 0)
            {
                TempData[MessageConst.ERROR] = "Can't hide, because this had some donate by user";
                return RedirectToAction("Index");
            }
            new DonateDao(_unitOfWork).HideDonate(id);
            TempData[MessageConst.SUCCESS] = "Success !";
            return RedirectToAction("Index");
        }


    
    }
}