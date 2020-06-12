using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebMvc.Common;
using WebMvc.Controllers;
using WebMvc.Models.Dao;
using WebMvc.Models.ModelView;

namespace WebMvc.Areas.Admin.Controllers
{
    public class UserQuestionController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        //call IUnitOfWork to use functions of UserQuestionDao
        public UserQuestionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Admin/UserQuestion
        //get list of new questions and answered questions
        public ActionResult Index()
        {
            var data = new UserQuestionDao(_unitOfWork).GetAll();
            ViewBag.newQ = data.OrderByDescending(s=>s.QuesDateCreate).Where(s => s.QuesNew == true);
            ViewBag.reply = data.OrderByDescending(s => s.AnswerDateCreate).Where(s => s.QuesNew == false);
            return View();
        }
        //get content of question by id
        public ActionResult QuesDetail(int id)
        {
            var data = new UserQuestionDao(_unitOfWork).GetByid(id);
            return View(data);
        }
        //method answer question
        public ActionResult AnswerQuestion(UserQuestionDTO u)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("giangbaccai1207@gmail.com", "NGO");
                    var receiverEmail = new MailAddress(u.UserMail, u.UserName);
                    var password = "dinhhoang0712";
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
                    TempData[MessageConst.SUCCESS] = "Send mail to " + u.UserMail + " is successful!";
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