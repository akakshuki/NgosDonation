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
    public class AboutUsController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private string path = "";
        //call IUnitOfWork to use functions of AboutUsDao
        public AboutUsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            path = "~/FileImage/";
        }
        // GET: Admin/AboutUs

        //Display List About us
        public ActionResult Index()
        {
            var data = new AboutUsDao(_unitOfWork).GetAll();
            return View(data);
        }

        //Get content of about us by ID
        [HttpGet]
        public ActionResult Detail(int id)
        {
            return View(new AboutUsDao(_unitOfWork).GetByid(id));
        }

        //Display form create about us
        public ActionResult Create()
        {
            return View();
        }
        //method create About us
        [HttpPost]
        public ActionResult Create(AboutUsDTO aboutUs)
        {
            if (aboutUs.FileImage != null)
            {
                aboutUs.AboutImage = DateTime.Now.Ticks + Path.GetFileName(aboutUs.FileImage.FileName);
                aboutUs.FileImage.SaveAs(Server.MapPath(path + aboutUs.AboutImage));
                aboutUs.FileImage = null;
            }
            if (!ModelState.IsValid) return View();
            if (new AboutUsDao(_unitOfWork).Create(aboutUs))
                return RedirectToAction("Index");

            TempData["error"] = "Create Failed!";
            return View();
        }
        //Get content of about us by ID and form edit about us
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = new AboutUsDao(_unitOfWork).GetByid(id);
            if (data == null) return RedirectToAction("Page404", "Error");
            return View(data);
        }
        //method edit about us
        [HttpPost]
        public ActionResult Edit(AboutUsDTO aboutUs)
        {
            //get Image have exist
            var currentFileName = new AboutUsDao(_unitOfWork).GetByid(aboutUs.ID).AboutImage;
            //check name have deafault if exist ==> dont delete
            if (aboutUs.FileImage==null)
            {
                aboutUs.AboutImage = currentFileName;
            }
            else
            {
                    //delete file
                    var filePath = Server.MapPath(path + currentFileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    aboutUs.AboutImage = DateTime.Now.Ticks + Path.GetFileName(aboutUs.FileImage.FileName);
                    aboutUs.FileImage.SaveAs(Server.MapPath(path + aboutUs.AboutImage));
                    aboutUs.FileImage = null;
            }
            var data = new AboutUsDao(_unitOfWork).GetByid(aboutUs.ID);
            if (!ModelState.IsValid) return View(data);
            if (new AboutUsDao(_unitOfWork).Edit(aboutUs)) return RedirectToAction("Index");
            TempData[MessageConst.ERROR] = "Edit Failed";
            return View(data);
        }
    }
}