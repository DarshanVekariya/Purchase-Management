using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Data;

namespace Business.User
{
    public class Auth : ActionFilterAttribute
    {
        public string _roles { get; set; }

        public Auth() { }
        public Auth(string role)
        {
            this._roles = role;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string userId = context.HttpContext.Session.GetString("UserId");
            //context.HttpContext.;
            if (!string.IsNullOrEmpty(_roles))
            {
                string role = Convert.ToString(context.HttpContext.Session.GetString("Role"))!;
                bool isValid = false;
                if (this._roles.Contains(","))
                {
                    foreach (var item in this._roles.Split(','))
                    {
                        if (item.Trim().ToLower() == role.ToLower())
                        {
                            isValid = true;
                            break;
                        }
                    }
                }
                else
                {
                    if (this._roles.Trim().ToLower() == role.ToLower())
                    {
                        isValid = true;
                    }
                }
                if (!isValid)
                {
                    {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    {"Area","" }, { "Controller", "Home" }, { "Action", "NoAccess" },{ "error",Guid.NewGuid()}
                });
                    }
                }
            }
            if (string.IsNullOrEmpty(userId))
            {

                context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    {"Area","" }, { "Controller", "Home" }, { "Action", "Login" },{ "error",Guid.NewGuid()}
                    });
                //if (context.HttpContext.Request.Path.Value.Contains("/api"))
                //{
                //    context.Result = new JsonResult(new { message = "Unauthorized" })
                //    {
                //        StatusCode = StatusCodes.Status401Unauthorized
                //    };
                //}
                //else
                //{
                //    context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                //    {"Area","" }, { "Controller", "Home" }, { "Action", "Login" },{ "error",Guid.NewGuid()}
                //    });
                //}
            }
            else
            {
                await base.OnActionExecutionAsync(context, next);
            }
        }
    }
}
