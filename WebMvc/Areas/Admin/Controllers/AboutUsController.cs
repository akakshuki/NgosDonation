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
    public class AboutUsController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private string path = "";

        public AboutUsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            path = "~/FileImage/";
        }
        // GET: Admin/AboutUs
        public ActionResult Index()
        {
            var data = new AboutUsDao(_unitOfWork).GetAll();
            return View(data);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            return View(new AboutUsDao(_unitOfWork).GetByid(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AboutUsDTO aboutUs)
        {
            if (aboutUs.FileImage != null)
            {
                aboutUs.AboutImage = DateTime.Now.Ticks + aboutUs.AboutName + ".png";
                aboutUs.FileImage.SaveAs(Server.MapPath(path + aboutUs.AboutImage));
                aboutUs.FileImage = null;
            }
            else
            {
                aboutUs.AboutImage = "default.png";
            }
            if (!ModelState.IsValid) return View();
            if (new AboutUsDao(_unitOfWork).Create(aboutUs))
                return RedirectToAction("Index");

            return View();
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = new AboutUsDao(_unitOfWork).GetByid(id);
            if (data == null) return RedirectToAction("Page404", "Error");
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(AboutUsDTO aboutUs)
        {
            //get Image have exist
            var currentFileName = new AboutUsDao(_unitOfWork).GetByid(aboutUs.ID).AboutImage;
            //check name have deafault if exist ==> dont delete
            if (currentFileName == "default.png")
            {
                //save file
                if (aboutUs.FileImage != null)
                {
                    aboutUs.AboutImage = DateTime.Now.Ticks.ToString() + aboutUs.AboutName + ".png";
                    aboutUs.FileImage.SaveAs(Server.MapPath(path + aboutUs.AboutImage));
                    aboutUs.FileImage = null;
                }
                else
                {
                    aboutUs.AboutImage = "default.png";
                }
            }
            else
            {
                if (aboutUs.FileImage != null)
                {
                    //delete file
                    var filePath = Server.MapPath(path + currentFileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    aboutUs.AboutImage = DateTime.Now.Ticks + aboutUs.AboutName + ".png";
                    aboutUs.FileImage.SaveAs(Server.MapPath(path + aboutUs.AboutImage));
                    aboutUs.FileImage = null;
                }
                else
                {
                    aboutUs.AboutImage = currentFileName;
                }

            }
            var data = new AboutUsDao(_unitOfWork).GetByid(aboutUs.ID);
            if (!ModelState.IsValid) return View(data);
            if (new AboutUsDao(_unitOfWork).Edit(aboutUs)) return RedirectToAction("Index");
            TempData[MessageConst.ERROR] = "Edit Failed";
            return View(data);
        }
    }
}