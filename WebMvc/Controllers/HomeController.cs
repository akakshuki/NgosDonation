using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Repository;
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


        [HttpPost]
        public ActionResult UserDonate(int donateId, int money)
        {
            var user = Request.Cookies[MessageConst.USER_LOGIN];
            if (user == null) return RedirectToAction("Index", "Login");
            var data = JsonConvert.DeserializeObject<UserLogin>(user.Value);
            var donateInfo = new DonateDao(_provider).GetById(donateId);

            var  orderData = new OrderData()
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
                        TempData[MessageConst.ERROR] = "Donate Fail !";
                        return RedirectToAction("Donate");

                    }
                }
            }
            catch (PayPal.PaymentsException ex)
            {
                PaypalLogger.Log("Error: " + ex.Message);
                TempData[MessageConst.ERROR] = "Donate Fail !";
                return RedirectToAction("Donate");

            }
            //save donate
            new DonateDao(_provider).AddUserDonate(order);
            TempData[MessageConst.SUCCESS] = "Donate Success";
          //  return RedirectToAction("Success", "Payment", new { code = apiContext.AccessToken });
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
                    email = "giangbaccai1207@gmail.com"
                }

            };
            var redictUrl = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            var detail = new Details() { tax = "1", shipping = "1", subtotal = order.Money.ToString() }; //subtotal : total order, note: sum(price*quantity)
            var amount = new Amount() { currency = "USD", details = detail, total = (order.Money + 2).ToString() }; //total= tax + shipping + subtotal
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
       
    }
}