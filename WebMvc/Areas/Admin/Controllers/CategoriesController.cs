using Domain.Repository;
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
        //call IUnitOfWork to use functions of CategoryDao
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //Display List category
        public ActionResult Index()
        {
            var data = new CategoryDao(_unitOfWork).GetAll();
            return View(data);
        }
        //form create category
        public ActionResult Create()
        {
            return View();
        }
        //method create category
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
                TempData[MessageConst.SUCCESS] = "Create Successfully !";
                return RedirectToAction("Index");
            }

            return View();
        }
        //get content of category by id and form edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = new CategoryDao(_unitOfWork).GetByid(id);
            if (data == null) return RedirectToAction("Page404", "Error");
            return View(data);
        }
        //method edit category
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
                TempData[MessageConst.SUCCESS] = "Update successfully!";
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}