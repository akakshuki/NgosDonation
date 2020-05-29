using Domain.Repository;
using System.Web.Mvc;
using WebMvc.Common;
using WebMvc.Controllers;
using WebMvc.Models.Dao;
using WebMvc.Models.ModelView;

namespace WebMvc.Areas.Admin.Controllers
{
    public class TypeProgramController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public TypeProgramController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Admin/TypeProgram
        public ActionResult Index()
        {
            var data = new TypeProgramDao(_unitOfWork).GetAll();
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TypeProgramDTO typeProgram)
        {
            if (!ModelState.IsValid) return View();

            if (new TypeProgramDao(_unitOfWork).CheckHaveExist(typeProgram.TypeName))
            {
                TempData[MessageConst.ERROR] = "This name have exist!";
                return View();
            }

            if (new TypeProgramDao(_unitOfWork).Create(typeProgram)) 
                return RedirectToAction("Index");

            return View();
        }


        public ActionResult Delete(int id)
        {
            new TypeProgramDao(_unitOfWork).Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = new TypeProgramDao(_unitOfWork).GetByid(id);
            if (data == null) return RedirectToAction("Page404", "Error");
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(TypeProgramDTO typeProgram)
        {
            if (!ModelState.IsValid) return View();

            if (new TypeProgramDao(_unitOfWork).CheckHaveExist(typeProgram.TypeName))
            {
                TempData[MessageConst.ERROR] = "This name have exist!";
                return View();
            }

            if (new TypeProgramDao(_unitOfWork).Edit(typeProgram)) return RedirectToAction("Index");

            return View();
        }
    }
}