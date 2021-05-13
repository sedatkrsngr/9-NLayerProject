using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using NLayerProject.Core.Services;
using NLayerProject.Web.UsingApi.Dtos;
using NLayerProject.Web.UsingApi.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NLayerProject.Web.UsingApi.Filters
{
    public class NotFoundFilter : ActionFilterAttribute//Category için bulunamadı kontrolü istersek bunu kullanabileceğimiz tüm kontrolerlar için de kulanabiliriz. Yapmamız gereken OnActionExecutionAsync methoduna girince context.ActionDescriptor.ControllerName ile ayrı ayrı kontrol edebiliriz. Ya da farklı bir Actiona girdikten sonra hata yönetimi kullanmak istiyorsak. Global hata yönetimi kullanabiliriz. Ama global hata yönetimini bizim  kontrolümüz dışındaki hatalar için kullanmak daha mantıklı
    {

        ApiAdress _api = new ApiAdress();
        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();

            HttpClient client = _api.Initial();
            var result = "";
            var resp = await client.GetAsync($"Categories/{id}");

            if (resp.IsSuccessStatusCode)
            {
                result = resp.Content.ReadAsStringAsync().Result;
                var Category = JsonConvert.DeserializeObject<CategoryDto>(result);

                if (Category != null)
                {
                    await next();
                }
                else
                {
                    ErrorDto errorDto = new ErrorDto();

                    errorDto.Status = 404;

                    errorDto.Errors.Add($"id'si {id} olan kategori veritabanında bulunamadı");

                    context.Result = new RedirectToActionResult("Error", "Home", errorDto);
                }

            }


          
        }
    }
}
