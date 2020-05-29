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
    public class PartnerController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private string path = "";

        public PartnerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            path = "~/FileImage/";
        }
        // GET: Admin/Partner
        public ActionResult Index()
        {
            var data = new PartnerDao(_unitOfWork).GetAll();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PartnerDTO partner)
        {
            if (partner.FileImage != null)
            {
                partner.PartnerImage = DateTime.Now.Ticks + partner.PartnerName + ".png";
                partner.FileImage.SaveAs(Server.MapPath(path + partner.PartnerImage));
                partner.FileImage = null;
            }
            else
            {
                partner.PartnerImage = "default.png";
            }
            if (!ModelState.IsValid) return View();
            if (new PartnerDao(_unitOfWork).Create(partner))
                return RedirectToAction("Index");

            return View();
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = new PartnerDao(_unitOfWork).GetByid(id);
            if (data == null) return RedirectToAction("Page404", "Error");
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(PartnerDTO partner)
        {
            //get Image have exist
            var currentFileName = new PartnerDao(_unitOfWork).GetByid(partner.ID).PartnerImage;
            //check name have deafault if exist ==> dont delete
            if (currentFileName == "default.png")
            {
                //save file
                if (partner.FileImage != null)
                {
                    partner.PartnerImage = DateTime.Now.Ticks.ToString() + partner.PartnerName + ".png";
                    partner.FileImage.SaveAs(Server.MapPath(path + partner.PartnerImage));
                    partner.FileImage = null;
                }
                else
                {
                    partner.PartnerImage = "default.png";
                }
            }
            else
            {
                if (partner.FileImage != null)
                {
                    //delete file
                    var filePath = Server.MapPath(path + currentFileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    partner.PartnerImage = DateTime.Now.Ticks + partner.PartnerName + ".png";
                    partner.FileImage.SaveAs(Server.MapPath(path + partner.PartnerImage));
                    partner.FileImage = null;
                }
                else
                {
                    partner.PartnerImage = currentFileName;
                }

            }
            var data = new PartnerDao(_unitOfWork).GetByid(partner.ID);
            if (!ModelState.IsValid) return View(data);
            if (new PartnerDao(_unitOfWork).Edit(partner)) return RedirectToAction("Index");
            TempData[MessageConst.ERROR] = "Edit Failed";
            return View(data);
        }
    }
}