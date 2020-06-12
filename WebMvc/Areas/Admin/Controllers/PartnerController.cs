using Domain.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using WebMvc.Common;
using WebMvc.Controllers;
using WebMvc.Models.Dao;
using WebMvc.Models.ModelView;

namespace WebMvc.Areas.Admin.Controllers
{
    public class PartnerController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string path = "";
        //call IUnitOfWork to use functions of PartnerDao
        public PartnerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            path = "~/FileImage/";
        }
        // GET: Admin/Partner
        //Display list of Partner
        public ActionResult Index()
        {
            List<PartnerDTO> data = new PartnerDao(_unitOfWork).GetAll();
            return View(data);
        }
        //form create partner
        public ActionResult Create()
        {
            return View();
        }
        //method create partner
        [HttpPost]
        public ActionResult Create(PartnerDTO partner)
        {
            if (partner.FileImage != null)
            {
                partner.PartnerImage = DateTime.Now.Ticks + Path.GetFileName(partner.FileImage.FileName);
                partner.FileImage.SaveAs(Server.MapPath(path + partner.PartnerImage));
                partner.FileImage = null;
            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (new PartnerDao(_unitOfWork).Create(partner))
            {
                return RedirectToAction("Index");
            }

            TempData[MessageConst.ERROR] = "Create failed!";
            return View();
        }
        //get content of partner by id and form edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            PartnerDTO data = new PartnerDao(_unitOfWork).GetByid(id);
            if (data == null)
            {
                return RedirectToAction("Page404", "Error");
            }

            return View(data);
        }
        //edit partner
        [HttpPost]
        public ActionResult Edit(PartnerDTO partner)
        {
            //get Image have exist
            string currentFileName = new PartnerDao(_unitOfWork).GetByid(partner.ID).PartnerImage;
            //check name have deafault if exist ==> dont delete
            if (partner.FileImage == null)
            {
                partner.PartnerImage = currentFileName;
            }
            else
            {
                //delete file
                string filePath = Server.MapPath(path + currentFileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                partner.PartnerImage = DateTime.Now.Ticks + Path.GetFileName(partner.FileImage.FileName);
                partner.FileImage.SaveAs(Server.MapPath(path + partner.PartnerImage));
                partner.FileImage = null;
            }
            PartnerDTO data = new PartnerDao(_unitOfWork).GetByid(partner.ID);
            if (!ModelState.IsValid)
            {
                return View(data);
            }

            if (new PartnerDao(_unitOfWork).Edit(partner))
            {
                return RedirectToAction("Index");
            }

            TempData[MessageConst.ERROR] = "Edit Failed";
            return View(data);
        }
    }
}