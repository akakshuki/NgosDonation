﻿using Newtonsoft.Json;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;
using WebMvc.Common;
using WebMvc.Models.ModelView;

namespace WebMvc.Controllers
{
    public class BaseController : Controller
    {
        //check cookie when start controller with implent this controller
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var cookie = Request.Cookies[MessageConst.USER_LOGIN];
            if (cookie == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index", area = "" }));
            }
            else
            {
                var user = JsonConvert.DeserializeObject<UserLogin>(cookie.Value);
                if (user.RoleId != 1 || user==null)
                {
                    filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "home", action = "Index", area = "" }));
                }
            }
        }
    }
}