using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebMvc.Models.Dao;
using WebMvc.Models.ModelView;

namespace WebMvc.Areas.Admin.Controllers
{
    public class UserQuestionController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public UserQuestionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Admin/UserQuestion
        public ActionResult Index()
        {
            var data = new UserQuestionDao(_unitOfWork).GetAll();
            ViewBag.newQ = data.Where(s => s.QuesNew == true);
            ViewBag.reply = data.Where(s => s.QuesNew == false);
            return View();
        }
        public ActionResult QuesDetail(int id)
        {
            var data = new UserQuestionDao(_unitOfWork).GetByid(id);
            return View(data);
        }
        public ActionResult AnswerQuestion(UserQuestionDTO u)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("dtmt1610@gmail.com", "NGO");
                    var receiverEmail = new MailAddress(u.UserMail, u.UserName);
                    var password = "dtmt16101302";
                    var sub = "NGO support";
                    var body = u.AnswerContent;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 25,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    TempData["success"] = "Send mail to " + u.UserMail + " successfully!";
                    new UserQuestionDao(_unitOfWork).InsertAns(u.ID, u.AnswerContent);
                }
            }
            catch (Exception e)
            {
                TempData["error"] = "Something went wrong. Please try again!";
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("QuesDetail", new { id = u.ID });
        }
    }
}