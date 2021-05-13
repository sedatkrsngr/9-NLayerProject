using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.Web.UsingApi.Filters
{
    public class SessionControlAttribute : TypeFilterAttribute//Örneğin kullanıcıda sessiondan gelen bir bilginin doluluğuna göre ilgili actiona girsin
    {
        public SessionControlAttribute() : base(typeof(SessionControlFilter))
        {
        }
    }
    public class SessionControlFilter : IAuthorizationFilter
    {
        public SessionControlFilter()
        {
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var GUID = context.HttpContext.Session.GetString("GUID");//Guid doluysa kullanıcı ilgili methoda devam eder home controllerda örn var. [SessionControl] adında filter

            if (string.IsNullOrEmpty(GUID))
            { //logine yönlendiriliyor.
                context.Result = new RedirectToActionResult("Logout", "Home", null);
            }
        }

    }

}
