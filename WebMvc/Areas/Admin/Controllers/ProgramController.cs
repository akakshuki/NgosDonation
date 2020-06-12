using Domain.Repository;
using System;
using System.Collections.Generic;
using System.IO;
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
        //call IUnitOfWork to use functions of ProgramDao
        public ProgramController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            path = "~/FileImage/";
        }

        // GET: Admin/Program
        //display list of programs: hide or not hide
        public ActionResult Index()
        {
            var data = new ProgramDao(_unitOfWork).GetAll().OrderByDescending(s=>s.ProDateCreate).ToList();
            return View(data);
        }
        //get content of program by id
        [HttpGet]
        public ActionResult Detail(int id)
        {
            return View(new ProgramDao(_unitOfWork).GetByid(id));
        }
        //form create program
        public ActionResult Create()
        {
            ViewBag.TypeProgram = new TypeProgramDao(_unitOfWork).GetAll(); 
            return View();
        }
        //method create program
        [HttpPost]
        public ActionResult Create(ProgramDTO program)
        {
            ViewBag.TypeProgram = new TypeProgramDao(_unitOfWork).GetAll();
            if (!ModelState.IsValid) return View();

            program.ProDateCreate = DateTime.Now;
            program.ProHide = false;
            if (new ProgramDao(_unitOfWork).Create(program))
                return RedirectToAction("Index");

            TempData[MessageConst.ERROR] = "Create Failed!";
            return View();
        }
        //get content of program by id and form edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.TypeProgram = new TypeProgramDao(_unitOfWork).GetAll();
            var data = new ProgramDao(_unitOfWork).GetByid(id);
            if (data == null) return RedirectToAction("Page404", "Error");
            return View(data);
        }
        //method edit
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
        //method hide program or not hide
        public ActionResult HideProgram(int id)
        {
            new ProgramDao(_unitOfWork).HideProgram(id);
            return RedirectToAction("Index");
        }

        //Program Image
        // image list of program
        public ActionResult IndexPi(int id)
        {
            ViewBag.ImgMain = new ProgramImageDao(_unitOfWork).GetImgMain(id);
            ViewBag.lsImg = new ProgramImageDao(_unitOfWork).GetAll().Where(s => s.ProID == id).ToList();
            TempData["ProId"] = id;
            ViewBag.name = new ProgramDao(_unitOfWork).GetByid(id).ProName;
            return View();
        }
        //method create image
        [HttpPost]
        public ActionResult CreatePi(ProgramImageDTO programImage)
        {
            if (programImage.FileImage != null)
            {
                programImage.ImgFileName = DateTime.Now.Ticks + Path.GetFileName(programImage.FileImage.FileName);
                programImage.FileImage.SaveAs(Server.MapPath(path + programImage.ImgFileName));
                programImage.FileImage = null;
            }
            if (!ModelState.IsValid) return RedirectToAction("IndexPi", "Program", new { id = programImage.ProID });
            if (new ProgramImageDao(_unitOfWork).Create(programImage))
                return RedirectToAction("IndexPi","Program", new {id = programImage.ProID });
            return RedirectToAction("IndexPi", "Program", new { id = programImage.ProID });
        }
        //set image as the image main of program
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
        //method delete image
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