using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WUI.Models;

namespace WUI.Filters
{
    public class RoleFilter : ActionFilterAttribute
    {
        //Your Properties in Action Filter
        public int idRole { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            PersonneModel user = (PersonneModel) filterContext.HttpContext.Session["user"];
            if (user == null || user.Role != this.idRole)
            {
                if(this.idRole == 2 )
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "controller", "Register" },
                            { "action", "Index" }
                        });
                } else
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "controller", "Account" },
                            { "action", "Login" }
                        });
                }

            } else
            {
                base.OnActionExecuting(filterContext);

            }
                
        }
    }
}
