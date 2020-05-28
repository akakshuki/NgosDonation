using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMvc.Common;
using WebMvc.Models.Dao;
using WebMvc.Models.ModelView;

namespace WebMvc.Areas.Admin.Controllers
{
    public class ProgramController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public ProgramController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/Program
        public ActionResult Index()
        {
            var data = new ProgramDao(_unitOfWork).GetAll();
            return View(data);
        }

        public ActionResult Create()
        {
            ViewBag.TypeProgram = new TypeProgramDao(_unitOfWork).GetAll(); 
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProgramDTO program)
        {
            ViewBag.TypeProgram = new TypeProgramDao(_unitOfWork).GetAll();
            if (!ModelState.IsValid) return View();

            if (new ProgramDao(_unitOfWork).CheckHaveExist(program.ProName))
            {
                TempData[MessageConst.ERROR] = "This name have exist!";
                return View();
            }

            program.ProDateCreate = DateTime.Now;
            program.ProHide = false;
            if (new ProgramDao(_unitOfWork).Create(program))
                return RedirectToAction("Index");

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = new ProgramDao(_unitOfWork).GetByid(id);
            if (data == null) return RedirectToAction("Page404", "Error");
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(ProgramDTO program)
        {
            if (!ModelState.IsValid) return View();

            if (new ProgramDao(_unitOfWork).CheckHaveExist(program.ProName))
            {
                TempData[MessageConst.ERROR] = "This name have exist!";
                return View();
            }

            if (new ProgramDao(_unitOfWork).Edit(program)) return RedirectToAction("Index");

            return View();
        }

        public ActionResult Delete(int id)
        {
            new ProgramDao(_unitOfWork).Delete(id);
            return RedirectToAction("Index");
        }
    }
}