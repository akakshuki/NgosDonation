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
    /// <summary>
    /// Controller for handling user login, registration, and authentication.
    /// </summary>
    public class LoginController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Displays the login page.
        /// </summary>
        /// <returns>The login view.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Handles user sign-in.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>A redirect to the appropriate dashboard or home page.</returns>
        [HttpPost]
        public ActionResult SignIn(string email, string password)
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
                    var cookie = new HttpCookie(MessageConst.USER_LOGIN);
                    var userData = JsonConvert.SerializeObject(new UserLogin()
                    {
                        UserName = user.UserName,
                        UserMail = user.UserMail,
                        UserVolunteer = user.UserVolunteer,
                        RoleId = user.RoleID
                    });
                    cookie.Value = userData;
                    Response.Cookies.Add(cookie);
                    TempData[MessageConst.SUCCESS] = "Welcome " + user.UserName + "!";
                    if (user.RoleID == 1) return RedirectToAction("Index", "Dashboard", new { Area = "Admin" });
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData[MessageConst.ERROR] = " Password incorrect !";
                }
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Handles user sign-out.
        /// </summary>
        /// <returns>A redirect to the login page.</returns>
        public ActionResult SignOut()
        {
            var cookie = Request.Cookies[MessageConst.USER_LOGIN];
            var user = JsonConvert.DeserializeObject<UserLogin>(cookie.Value);
            Session[user.UserMail] = null;
            Response.Cookies[MessageConst.USER_LOGIN].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Handles user registration.
        /// </summary>
        /// <param name="user">The user data.</param>
        /// <returns>A redirect to the login page.</returns>
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
                TempData[MessageConst.ERROR] = "This email already exists !";
                return RedirectToAction("Index");
            }
            var userRegister = new UserDao(_unitOfWork).Register(user);
            if (userRegister != null)
            {
                TempData[MessageConst.SUCCESS] = " Register successfully!";
                return Content("<form action='SignIn' id='frmTest' method='post'>" +
                               "<input type='hidden' name='email' value='" + userRegister.UserMail + "' />" +
                               "<input type='hidden' name='password' value='" + userRegister.UserPwd + "' />" +
                               "</form>" +
                               "<script>document.getElementById('frmTest').submit();</script>");
            }
            else
            {
                TempData[MessageConst.ERROR] = " Register failed! ";
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Displays the forgot password page and sends a password reset email.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="stt">The status.</param>
        /// <returns>The forgot password view.</returns>
        public async Task<ActionResult> ForgotPwd(string email, int stt = 0)
        {
            ViewBag.mail = email;
            if (stt == 0)
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
                        var randomCode = new Random().Next(1000, 9999).ToString();
                        var MyMailMessage = new MailMessage
                        {
                            From = new MailAddress("giangbaccai1207@gmail.com", "NGO")
                        };
                        MyMailMessage.To.Add(email);// Mail would be sent to this address
                        MyMailMessage.Subject = "NGO Support";
                        var smtpServer = new SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            Credentials =
                                new NetworkCredential("giangbaccai1207@gmail.com", "dinhhoang0712"),
                            EnableSsl = true
                        };

                        var body = new StringBuilder();
                        body.AppendFormat("Hi, {0}!\n", userData.UserName);
                        body.AppendLine(@"Your request reset password is accepted !");
                        body.AppendLine($" Your secret code : {randomCode} ");

                        MyMailMessage.Body = body.ToString();

                        await smtpServer.SendMailAsync(MyMailMessage);

                        Session[randomCode] = userData.UserMail;
                        TempData[MessageConst.SUCCESS] = "Send mail successfully! Let's check you mail";
                        return View();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    TempData[MessageConst.ERROR] = "Something is wrong. Please try again!";
                    return View("Index");
                }
            }
            return View();
        }

        /// <summary>
        /// Resets the user's password.
        /// </summary>
        /// <param name="secretCode">The secret code.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="email">The user's email.</param>
        /// <returns>A redirect to the login page.</returns>
        public ActionResult ResetPassword(string secretCode, string newPassword, string email)
        {
            var emailCheck = Session[secretCode] as string;
            if (email != emailCheck)
            {
                TempData[MessageConst.ERROR] = "Reset Password failed!";
                return RedirectToAction("ForgotPwd", new { email = email, stt = 1 });
            }
            if (new UserDao(_unitOfWork).ResetPassword(email, newPassword))
            {
                Session.Remove(secretCode);
                TempData[MessageConst.SUCCESS] = "Reset Password successfully !";
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Changes the user's password.
        /// </summary>
        /// <param name="oldpass">The old password.</param>
        /// <param name="newpass">The new password.</param>
        /// <returns>A redirect to the personal info page.</returns>
        [HttpPost]
        public ActionResult ChangePassword(string oldpass, string newpass)
        {
            var cookie = Request.Cookies[MessageConst.USER_LOGIN];
            if (cookie == null)
            {
                TempData[MessageConst.ERROR] = " Please Login!";
                return RedirectToAction("Index");
            }
            var email = JsonConvert.DeserializeObject<UserLogin>(cookie.Value).UserMail;
            var user = new UserDao(_unitOfWork).GetUserByEmail(email);
            if (user == null)
            {
                TempData[MessageConst.ERROR] = "Email incorrect!";
                return RedirectToAction("SignOut");
            }
            if (user.UserPwd != Encrypt.EncryptPasswordMD5(oldpass))
            {
                TempData[MessageConst.ERROR] = "Password incorrect!";
                return RedirectToAction("PersonalInfo", "Home");
            }
            if (!new UserDao(_unitOfWork).ChangePassword(newpass, email)) TempData[MessageConst.ERROR] = "Change password failed";
            else TempData[MessageConst.ERROR] = "Change password successfully !";

            return RedirectToAction("PersonalInfo", "Home");
        }

        /// <summary>
        /// Edits the user's information.
        /// </summary>
        /// <param name="user">The user data.</param>
        /// <returns>A redirect to the personal info page.</returns>
        [HttpPost]
        public ActionResult EditUser(UserDTO user)
        {
            var res = new UserDao(_unitOfWork).UpdateInfo(user);
            if (res)
            {
                TempData[MessageConst.SUCCESS] = "Change info successfully!";
                var cookie = new HttpCookie(MessageConst.USER_LOGIN)
                {
                    Value = JsonConvert.SerializeObject(new UserLogin()
                    {
                        UserMail = user.UserMail,
                        UserName = user.UserName
                    })
                };
                Response.Cookies.Add(cookie);
            }
            else
            {
                TempData[MessageConst.ERROR] = "Change info failed!";
            }

            return RedirectToAction("PersonalInfo", "Home");
        }
    }
}