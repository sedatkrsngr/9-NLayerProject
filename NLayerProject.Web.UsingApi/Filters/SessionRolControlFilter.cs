using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayerProject.Web.UsingApi.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.Web.UsingApi.Filters
{
    public class RolControlAttribute : TypeFilterAttribute
    {
        public RolControlAttribute(RolesEnum rol) : base(typeof(SessionRolControlFilter))
        {
            Arguments = new object[] { rol };
        }
    }
    public class SessionRolControlFilter : IAuthorizationFilter
    {
        private readonly RolesEnum _rol;
        public SessionRolControlFilter(RolesEnum rol)
        {
            _rol = rol;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string result = "";
            var GUID = context.HttpContext.Session.GetString("GUID");

            if (!string.IsNullOrEmpty(GUID))
            {
                string RolAdı = _rol.ToString();

                // var roller = context.HttpContext.Session.GetObjectFromJson<List<Roller>>("userRoles");//Roller adında clasımızı sessionla doldurduğumuzu varsayalım ilgili duruma göre de veri şartları sağlıyorsa devam etsin. Böylelikle manuel yönetim sistemi yaptık Kullanımı Home controllerda mevcut [RolControl(RolesEnum.Admin)] örn kullanım

                //foreach (var item in roller)
                //{
                //    if (item.RolAdi == RolAdı)
                //    {
                //        result = "Basarili";
                //    }
                //}

                if (string.IsNullOrEmpty(result))
                {
                    context.Result = new RedirectToActionResult("UnauthorizedAccess", "Home", null);
                }

            }

        }
    }
}
