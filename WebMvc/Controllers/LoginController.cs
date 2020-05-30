using Domain.Repository;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebMvc.Common;
using WebMvc.CommonHelper;
using WebMvc.Models.Dao;
using WebMvc.Models.ModelView;

namespace WebMvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SingIn(string email, string password)
        {
            var user = new UserDao(_unitOfWork).UserLogin(email, password);
            if (user == null)
            {
                TempData[MessageConst.ERROR] = "Email incorrect !";
            }
            else
            {
                if (user.UserPwd == Encrypt.EncryptPasswordMD5(password))
                {
                    //use cookie to store user's info
                    var cookie = new HttpCookie(MessageConst.USER_LOGIN);
                    var userData = JsonConvert.SerializeObject(new UserLogin()
                    {
                        UserName = user.UserName,
                        UserMail = user.UserMail
                    });
                    Session[user.UserMail] = user.RoleID;
                    cookie.Value = userData;
                    Response.Cookies.Add(cookie);
                    TempData[MessageConst.SUCCESS] = "Welcome " + user.UserName;
                    if (user.RoleID == 1) return RedirectToAction("Index", "Dashboard");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData[MessageConst.ERROR] = " Password incorrect !";
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult SignOut()
        {
            //remove session
            var cookie = Request.Cookies[MessageConst.USER_LOGIN];
            var user = JsonConvert.DeserializeObject<UserLogin>(cookie.Value);
            Session[user.UserMail] = null;
            Response.Cookies[MessageConst.USER_LOGIN].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Register(UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                TempData[MessageConst.ERROR] = " Register invalid ! ";
                return RedirectToAction("Index");
            }

            if (new UserDao(_unitOfWork).GetUserByEmail(user.UserMail) != null)
            {
                TempData[MessageConst.ERROR] = "This email have exist !";
            }
            var userRegister = new UserDao(_unitOfWork).Register(user);
            if (userRegister != null)
            {
                TempData[MessageConst.SUCCESS] = " Register Success!";
                return RedirectToAction("SingIn", "Login",
                    new { email = userRegister.UserMail, password = userRegister.UserPwd });
            }
            else
            {
                TempData[MessageConst.ERROR] = " Register fail! ";
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> ForgotPwd(string email)
        {
            try
            {
                var userData = new UserDao(_unitOfWork).GetUserByEmail(email);
                if (userData == null)
                {
                    TempData[MessageConst.ERROR] = "Email incorrect !";
                    return View("Index");
                }
                else
                {
                    var randomCode = new Random().Next(1000, 9999).ToString(); var MyMailMessage = new MailMessage
                    {
                        From = new MailAddress("giangbaccai1207@gmail.com")
                    };
                    MyMailMessage.To.Add(email);// Mail would be sent to this address
                    MyMailMessage.Subject = $"{userData.UserName} Reset password";
                    var smtpServer = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials =
                            new NetworkCredential("giangbaccai1207@gmail.com", "dinhhoang0712"),
                        EnableSsl = true
                    };

                    var body = new StringBuilder();
                    body.AppendFormat("Hi! , {0}\n", userData.UserName);
                    body.AppendLine(@"Your request reset Password are accepted !");
                    body.AppendLine($" Your secret code : {randomCode} ");

                    MyMailMessage.Body = body.ToString();

                    await smtpServer.SendMailAsync(MyMailMessage);

                    Session[randomCode] = userData.UserMail;
                    TempData[MessageConst.SUCCESS] = "Sent mail success! Let check you mail";
                    return View();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                TempData[MessageConst.ERROR] = "Something is wrong!";
                return View("Index");
            }
        }

        public ActionResult ResetPassword(string secretCode, string newPassword)
        {
            var email = Session[secretCode] as string;
            if (new UserDao(_unitOfWork).ResetPassword(email, newPassword))
            {
                Session.Remove(secretCode);
                TempData[MessageConst.SUCCESS] = "Reset Password Success !";
            }
            else
            {
                TempData[MessageConst.ERROR] = "Reset Password Fail!";
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ChangePassword(string oldpass, string newpass)
        {
            var cookie = Request.Cookies[MessageConst.USER_LOGIN];
            if (cookie == null)
            {
                TempData[MessageConst.ERROR] = " Please Login !";
                return RedirectToAction("Index");
            }
            var email= JsonConvert.DeserializeObject<UserLogin>(cookie.Value).UserMail;
            var user = new UserDao(_unitOfWork).GetUserByEmail(email);
            if (user == null)
            {
                TempData[MessageConst.ERROR] = "Email incorrect!";
                return RedirectToAction("SignOut");
            }
            if (user.UserPwd != Encrypt.EncryptPasswordMD5(oldpass))
            {
                TempData[MessageConst.ERROR] = "Password incorrect!";
                return RedirectToAction("PersonalInfo","Home");
            }
            if (!new UserDao(_unitOfWork).ChangePassword(newpass, email)) TempData[MessageConst.ERROR] = "Change password is failed";
            else TempData[MessageConst.ERROR] = "Change password is Success !";

            return RedirectToAction("PersonalInfo", "Home");
        }

        [HttpPost]
        public ActionResult EditUser(UserDTO user)
        {
            var res = new UserDao(_unitOfWork).UpdateInfo(user);
            if (res)
            {
                TempData[MessageConst.SUCCESS] = "Change Success!";
                var cookie = new HttpCookie(MessageConst.USER_LOGIN)
                {
                    Value = JsonConvert.SerializeObject(new UserLogin()
                    {
                        UserMail = user.UserMail, UserName = user.UserName
                    })
                };
                Response.Cookies.Add(cookie);
            }
            else
            {
                TempData[MessageConst.ERROR] = "Change Fail!";
            }
           
            return RedirectToAction("PersonalInfo", "Home");
        }
    }
}