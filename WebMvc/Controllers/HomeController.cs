using System.Linq;
using Domain.Repository;
using System.Web.Mvc;
using WebMvc.Models.Dao;

namespace WebMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _provider;

        public HomeController(IUnitOfWork provider)
        {
            _provider = provider;
        }
        //Index Page
        public ActionResult Index()
        {
            return View();
        }
        //Donate Page
        public ActionResult Donate()
        {
            ViewBag.ListDonate = new DonateDao(_provider).GetAllDonateNoHide();

            return View();
        }
        //Donate Infomation Page
        public ActionResult DonateInfomation()
        {
            return View();
        }
        //Our Programmes Page
        public ActionResult Program()
        {
            return View();
        }
        //Program Infomation Page
        public ActionResult ProgramInfomation()
        {
            return View();
        }
        //About Us Page
        public ActionResult About()
        {
            return View();
        }
        //Contact Us Page
        public ActionResult Contact()
        {
            return View();
        }
        //Our Partners
        public ActionResult Partner()
        {
            return View();
        }
        //Signin, Signup page
        public ActionResult SignIn()
        {
            return View();
        }
        //Create new password page
        public ActionResult ForgotPwd()
        {
            return View();
        }
        //Personal Infomation
        public ActionResult PersonalInfo(string email)
        {
            return View();
        }
    }
}