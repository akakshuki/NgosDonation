using Domain.Repository;
using System;
using System.Linq;
using System.Web.Mvc;
using WebMvc.Common;
using WebMvc.Controllers;
using WebMvc.Models.Dao;
using WebMvc.Models.Enum;
using WebMvc.Models.ModelView;

namespace WebMvc.Areas.Admin.Controllers
{
    public class ManageDonateController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        //call IUnitOfWork to use functions of DonateDao
        public ManageDonateController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //get list of donates: upcoming, ongoing, ended, hide
        public ActionResult Index()
        {
            return View(new DonateDao(_unitOfWork).GetAll().OrderByDescending(o=>o.DonateDateCreate).ToList());
        }
        //form create donate
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categories = new CategoryDao(_unitOfWork).GetAll();
            return View();
        }
        //get content of donate by id
        [HttpGet]
        public ActionResult Detail(int id)
        {
            return View(new DonateDao(_unitOfWork).GetById(id));
        }
        //method create
        [HttpPost]
        public ActionResult Create(DonateDTO donate)
        {
            ViewBag.Categories = new CategoryDao(_unitOfWork).GetAll();
            if (DateTime.Parse(donate.EndDay.ToString("yyyy/MM/dd")) < DateTime.Parse(donate.StartDay.ToString("yyyy/MM/dd")) || DateTime.Parse(donate.StartDay.ToString("yyyy/MM/dd")) < DateTime.Now)
            {
                TempData[MessageConst.ERROR] = "Date time is invalid";
                return View();
            }
            if (!ModelState.IsValid) return View();

            if (new DonateDao(_unitOfWork).Create(donate))
            {
                TempData[MessageConst.SUCCESS] = "Create Successfully !";
                return RedirectToAction("Index");
            }

            return View();
        }
        //get content of donate by id and form edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Categories = new CategoryDao(_unitOfWork).GetAll();
            var data = new DonateDao(_unitOfWork).GetByid(id);
            if (data == null) return RedirectToAction("Page404", "Error");
            return View(data);
        }
        //method edit donate
        [HttpPost]
        public ActionResult Edit(DonateDTO donate)
        {
            var data = new DonateDao(_unitOfWork).GetByid(donate.ID);
            ViewBag.Categories = new CategoryDao(_unitOfWork).GetAll();
            if (DateTime.Parse(donate.EndDay.ToString("yyyy/MM/dd")) < DateTime.Parse(donate.StartDay.ToString("yyyy/MM/dd")))
            {
                TempData[MessageConst.ERROR] = "Date time is invalid";
                return View(data);
            }

            if (!ModelState.IsValid) return View(data);

            if (new DonateDao(_unitOfWork).Edit(donate))
            {
                TempData[MessageConst.SUCCESS] = "Update Successfully !";
                return RedirectToAction("Index");
            }
            TempData[MessageConst.ERROR] = "Update Failed";
            return View(data);
        }
        //method hide donate
        public ActionResult HideDonate(int id)
        {
            var lsUD = new DonateDao(_unitOfWork).GetById(id).UserDonates.Count;
            var donate = new DonateDao(_unitOfWork).GetById(id);
            if ( lsUD > 0 && (donate.DonateStatus==DonateStatus.Ongoing || donate.DonateStatus== DonateStatus.Upcoming))
            {
                TempData[MessageConst.ERROR] = "Can't hide, because this had some donate by user";
                return RedirectToAction("Index");
            }
            new DonateDao(_unitOfWork).HideDonate(id);
            TempData[MessageConst.SUCCESS] = "Hide/Unhide Successfully !";
            return RedirectToAction("Index");
        }


    
    }
}