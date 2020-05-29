using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMvc.Common;
using WebMvc.Controllers;
using WebMvc.Models.Dao;
using WebMvc.Models.ModelView;

namespace WebMvc.Areas.Admin.Controllers
{
    public class ProgramController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private string path = "";

        public ProgramController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            path = "~/FileImage/";
        }

        // GET: Admin/Program
        public ActionResult Index()
        {
            var data = new ProgramDao(_unitOfWork).GetAll();
            return View(data);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            return View(new ProgramDao(_unitOfWork).GetByid(id));
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
            ViewBag.TypeProgram = new TypeProgramDao(_unitOfWork).GetAll();
            var data = new ProgramDao(_unitOfWork).GetByid(id);
            if (data == null) return RedirectToAction("Page404", "Error");
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(ProgramDTO program)
        {
            var data = new ProgramDao(_unitOfWork).GetByid(program.ID);
            ViewBag.TypeProgram = new TypeProgramDao(_unitOfWork).GetAll();
            if (!ModelState.IsValid) return View(data);
            if (new ProgramDao(_unitOfWork).Edit(program)) return RedirectToAction("Index");
            TempData[MessageConst.ERROR] = "Edit Failed";
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            new ProgramDao(_unitOfWork).Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult HideDonate(int id)
        {
            new ProgramDao(_unitOfWork).HideDonate(id);
            return RedirectToAction("Index");
        }

        //ProgramImage

        public ActionResult IndexPi(int id)
        {
            ViewBag.ImgMain = new ProgramImageDao(_unitOfWork).GetImgMain(id);
            ViewBag.lsImg = new ProgramImageDao(_unitOfWork).GetAll().Where(s => s.ProID == id).ToList();
            Session["ProId"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult CreatePi(ProgramImageDTO programImage)
        {
            if (programImage.FileImage != null)
            {
                programImage.ImgFileName = DateTime.Now.Ticks + programImage.ImgFileName + ".png";
                programImage.FileImage.SaveAs(Server.MapPath(path + programImage.ImgFileName));
                programImage.FileImage = null;
            }
            else
            {
                programImage.ImgFileName = "default.png";
            }
            if (!ModelState.IsValid) return View();
            if (new ProgramImageDao(_unitOfWork).Create(programImage))
                return RedirectToAction("Index");
            return View();
        }

        public ActionResult CheckMain(int id, int idPro)
        {
            if (new ProgramImageDao(_unitOfWork).CheckImgMain(id) == true)
            {
                TempData["success"] = "Uncheck/Check the main image successfully!";
            }
            else
            {
                TempData["error"] = "Uncheck/Check the main image failed!";
            }
            return RedirectToAction("IndexPi", new { id = idPro });
        }

        public ActionResult DelImg(int id)
        {
            ProgramImageDTO pi = new ProgramImageDao(_unitOfWork).GetByid(id);
            string fullPath = Request.MapPath("~/FileImage/" + pi.ImgFileName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            new ProgramImageDao(_unitOfWork).Delete(id);
            return RedirectToAction("IndexPi", new { id = pi.ProID });
        }
    }
}