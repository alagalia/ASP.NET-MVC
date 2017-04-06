using System.Linq;
using System.Web.Mvc;
using EventsApp.Data;
using EventsApp.Models.EntityModels;

namespace EventsApp.Attributies
{
    public class MyAuthorizeAttribute :AuthorizeAttribute
    {
        //this method restricted controller or action depend on givven roles
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //get roles from Action annotation 
            var roles = this.Roles.Split(',');
            bool isauth = filterContext.HttpContext.Request.IsAuthenticated;
            bool isInRole = roles.Any(r => filterContext.HttpContext.User.IsInRole(r));
            if (isauth && !isInRole)
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "~/views/shared/Unauthorized.cshtml"
                };
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}