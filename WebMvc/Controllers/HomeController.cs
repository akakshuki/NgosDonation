using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Domain.Repository;
using System.Linq;
using System.Web.Mvc;
using Domain.EF;
using Newtonsoft.Json;
using WebMvc.Models.Dao;
using PayPal.Api;
using WebMvc.Common;
using WebMvc.Configurations;
using WebMvc.Models.ModelView;

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
            Session["menu"] = 1;
            ViewBag.money = new DonateDao(_provider).GetAll().Sum(s => s.TotalMoney);
            ViewBag.user = new UserDao(_provider).GetAllUser().Count(c => c.UserActive == true && c.RoleID==2);
            ViewBag.volunteer = new UserDao(_provider).GetAllUser().Count(c => c.UserActive && c.UserVolunteer && c.RoleID == 2);
            return View();
        }

        #region Donate
        //Donate Page
        public ActionResult Donate()
        {
            Session["menu"] = 2;
            ViewBag.ListDonate = new DonateDao(_provider).GetAllDonateNoHide();
            return View();
        }
        //Donate Infomation Page
        public ActionResult DonateInformation(int id)
        {
            var data = new DonateDao(_provider).GetById(id);
            ViewBag.DonateInfo = data;
            return View();
        }
        //User Donate with PayPal Payment
        [HttpPost]
        public ActionResult UserDonate(int donateId, int money)
        {
            var user = Request.Cookies[MessageConst.USER_LOGIN];
            if (user == null) {
                TempData[MessageConst.ERROR] = "Please login to donate!";
                return RedirectToAction("Index", "Login");
            }
            var data = JsonConvert.DeserializeObject<UserLogin>(user.Value);
            var donateInfo = new DonateDao(_provider).GetById(donateId);
            var orderData = new OrderData()
            {
                DonateName = donateInfo.DonateName,
                Money = money,
                UserMail = data.UserMail,
                DonateId = donateInfo.ID,
                UserName = data.UserName
            };
            Session[MessageConst.USER_SESSION] = orderData;
            if (donateInfo != null && data != null) return RedirectToAction("PaymentWithPaypal", "Home");

            return RedirectToAction("Donate");
        }
        #endregion

        #region Program
        //Our Programmes Page
        public ActionResult Program()
        {
            Session["menu"] = 5;
            var ls = new ProgramImageDao(_provider).GetAll().Where(w => w.Program.ProHide == false && w.ImgMain == true).ToList();
            return View(ls);
        }
        //Program Infomation Page
        public ActionResult ProgramInformation(int id)
        {
            var model = new ProgramDao(_provider).GetByid(id);
            ViewBag.imgMain = new ProgramImageDao(_provider).GetImgMain(id);
            ViewBag.ls = new ProgramImageDao(_provider).GetAll().Where(w => w.ProID == id).ToList();
            return View(model);
        }
        #endregion

        //About Us Page
        public ActionResult About()
        {
            Session["menu"] = 3;
            var ls = new AboutUsDao(_provider).GetAll().Where(w => w.AboutHide == false).ToList();
            return View(ls);
        }
        #region Contact
        //Contact Us Page
        public ActionResult Contact()
        {
            Session["menu"] = 4;
            ContactDTO dataContact = null;
            string path = Path.Combine(Server.MapPath("~/"), "DataContact.hat");
            if (System.IO.File.Exists(path))
            {
                //Deserialize
                Stream streamD = new FileStream(path, FileMode.OpenOrCreate);
                BinaryFormatter formatterD = new BinaryFormatter();
                dataContact = formatterD.Deserialize(streamD) as ContactDTO;
                streamD.Close();
            }
            return View(dataContact);
        }
        //CREATE QUESTION
        [HttpPost]
        public ActionResult CreateQuestion(UserQuestionDTO userQuestion)
        {
            if (!ModelState.IsValid) return Redirect("/");
            userQuestion.QuesDateCreate = DateTime.Now;
            userQuestion.QuesNew = true;
            if (new UserQuestionDao(_provider).Create(userQuestion))
            {
                TempData[MessageConst.SUCCESS] = "Send question successfully!";
                return RedirectToAction("Contact");
            }
            return Redirect("/");
        }
        #endregion

        //Our Partners
        public ActionResult Partner()
        {
            Session["menu"] = 6;
            ViewBag.lsPartner = new PartnerDao(_provider).GetAll();
            ViewBag.volunteer = new UserDao(_provider).GetAllUser().Where(w => w.UserVolunteer && w.UserActive && w.RoleID == 2).OrderByDescending(o=>o.MoneyDonate).ToList();
            return View();
        }
        //Personal Infomation Page
        public ActionResult PersonalInfo()
        {
            var cookie = Request.Cookies[MessageConst.USER_LOGIN];
            if (cookie == null)
            {
                TempData[MessageConst.ERROR] = "Please Login";
                return RedirectToAction("Index", "Login");
            }
            var userData= new UserDao(_provider).GetUserByEmail(JsonConvert.DeserializeObject<UserLogin>(cookie.Value).UserMail);
            if (userData == null)
            {
                TempData[MessageConst.ERROR] = "Email incorrect!";
                return RedirectToAction("SignOut","Login");
            }

            return View(userData);
        }

        #region PayPal Payment
        //create method
        public ActionResult PaymentWithPaypal()
        {
            var apiContext = PaypalConfiguration.GetAPIContext();
            var order = (OrderData)Session[MessageConst.USER_SESSION];
            try
            {
                string payerID = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerID))
                {
                    //create a payment
                    string baseUri = Request.Url.Scheme + "://" + Request.Url.Authority + "/home/PaymentWithPaypal?guid=";
                    string guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = CreatePayment(apiContext, baseUri + guid, order);

                    var link = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = string.Empty;
                    while (link.MoveNext())
                    {
                        Links links = link.Current;
                        if (links.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = links.href;
                        }
                    }
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var guid = Request.Params["guid"];
                    var executePayment = ExecutePayment(apiContext, payerID, Session[guid] as string);
                    if (executePayment.state.ToLower() != "approved")
                    {
                        TempData[MessageConst.ERROR] = "Donate Failed !";
                        return RedirectToAction("Donate");

                    }
                }
            }
            catch (PayPal.PaymentsException ex)
            {
                PaypalLogger.Log("Error: " + ex.Message);
                TempData[MessageConst.ERROR] = "Donate Failed !";
                return RedirectToAction("Donate");

            }
            //save donate
            new DonateDao(_provider).AddUserDonate(order);
            TempData[MessageConst.SUCCESS] = "Donate Successfully!";
          return RedirectToAction("Donate");
        }

        //work with paypal payment
        private Payment payment;

        //create a payment using an APIContext
        private Payment CreatePayment(APIContext apiContext, string redirectUrl, OrderData order)
        {
            var lsItem = new ItemList() { items = new List<Item>() };
            lsItem.items.Add(new Item { name = order.DonateName, currency = "USD", price = order.Money.ToString(), quantity = "1", sku = "sku" });


            var payer = new Payer()
            {
                payment_method = "paypal",
                payer_info = new PayerInfo
                {
                    email = ""
                }
            };
            var redictUrl = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            var detail = new Details() { tax = "0", shipping = "0", subtotal = order.Money.ToString() }; //subtotal : total order, note: sum(price*quantity)
            var amount = new Amount() { currency = "USD", details = detail, total = (order.Money).ToString() }; //total= tax + shipping + subtotal
            var transList = new List<Transaction>();
            transList.Add(new Transaction
            {
                description = "Donate to:" + order.DonateName,
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = lsItem,

            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transList,
                redirect_urls = redictUrl
            };
            return this.payment.Create(apiContext);
        }
        //create execute payment method
        private Payment ExecutePayment(APIContext apiContext, string payerID, string paymentID)
        {
            var paymentExecute = new PaymentExecution() { payer_id = payerID };
            this.payment = new Payment() { id = paymentID };
            return this.payment.Execute(apiContext, paymentExecute);
        }
        #endregion

        //Invite someone
        [HttpPost]
        public async Task<ActionResult> SendMailInvite( string email)
        {
            var user = new UserDao(_provider).GetUserByEmail(email);
            if (user != null && user.UserActive)
            {
                TempData[MessageConst.ERROR] = "This mail already exists in this website";
                return Redirect("/");
            }
            try
            {
                var cookie = Request.Cookies[MessageConst.USER_LOGIN];
                if (cookie == null)
                {
                    TempData[MessageConst.ERROR] = "Please Login to invite your friend!";
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    var userData = JsonConvert.DeserializeObject<UserLogin>(cookie.Value);
                    var randomCode = new Random().Next(1000, 9999).ToString();
                    var MyMailMessage = new MailMessage
                    {
                        From = new MailAddress("giangbaccai1207@gmail.com", "NGO")
                    };
                    MyMailMessage.To.Add(email);// Mail would be sent to this address
                    MyMailMessage.Subject = $"{userData.UserName} request Invite";
                    var smtpServer = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials =
                            new NetworkCredential("giangbaccai1207@gmail.com", "dinhhoang0712"),
                        EnableSsl = true
                    };

                    var body = new StringBuilder();
                    body.AppendFormat("Hi, {0}!\n", email);
                    body.AppendLine($"You have an invite request from {userData.UserMail}, with name {userData.UserName}.");
                    body.AppendLine($"Link to our website: https://localhost:44315/");

                    MyMailMessage.Body = body.ToString();

                    await smtpServer.SendMailAsync(MyMailMessage);

                    Session[randomCode] = userData.UserMail;
                    TempData[MessageConst.SUCCESS] = "Send mail successfully!";
                    return Redirect("/");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                TempData[MessageConst.ERROR] = "Something is wrong!";
                return View("Index");
            }
        }
       
    }
}