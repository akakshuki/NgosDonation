﻿using Domain.Repository;
using System.Web.Mvc;
using WebMvc.Common;
using WebMvc.Controllers;
using WebMvc.Models.Dao;
using WebMvc.Models.ModelView;

namespace WebMvc.Areas.Admin.Controllers
{
    public class CategoriesController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var data = new CategoryDao(_unitOfWork).GetAll();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoryDTO category)
        {
            if (!ModelState.IsValid) return View();

            if (new CategoryDao(_unitOfWork).CheckHaveExist(category.CateName))
            {
                TempData[MessageConst.ERROR] = "This category name already exists!";
                return View();
            }

            if (new CategoryDao(_unitOfWork).Create(category))
            {
                TempData[MessageConst.SUCCESS] = "Create Success !";
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Delete(int id)
        {
            new CategoryDao(_unitOfWork).Delete(id);
            TempData[MessageConst.SUCCESS] = "Delete Success !";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = new CategoryDao(_unitOfWork).GetByid(id);
            if (data == null) return RedirectToAction("Page404", "Error");
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(CategoryDTO category)
        {
            if (!ModelState.IsValid) return View();

            if (new CategoryDao(_unitOfWork).CheckHaveExist(category.CateName))
            {
                TempData[MessageConst.ERROR] = "This name already exists!";
                return View();
            }

            if (new CategoryDao(_unitOfWork).Edit(category))
            {
                TempData[MessageConst.SUCCESS] = "Update success!";
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}