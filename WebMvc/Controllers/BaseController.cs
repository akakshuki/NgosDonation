using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Domain.Repository;
using Newtonsoft.Json;
using WebMvc.Common;
using WebMvc.Models.Dao;
using WebMvc.Models.ModelView;

namespace WebMvc.Controllers
{
    public class BaseController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //check cookie when start controller with implent this controller
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var cookie = new HttpCookie(MessageConst.USER_LOGIN);
            
            if (cookie.Value == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index" , area=""}));
            }
        }
    }
}