using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayerProject.Core.Services;
using NLayerProject.Web.UsingData.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.Web.UsingData.Filters
{
    public class NotFoundFilter : ActionFilterAttribute//Category için bulunamadı kontrolü istersek bunu kullanabileceğimiz tüm kontrolerlar için de kulanabiliriz. Yapmamız gereken OnActionExecutionAsync methoduna girince context.ActionDescriptor.ControllerName ile ayrı ayrı kontrol edebiliriz. Ya da farklı bir Actiona girdikten sonra hata yönetimi kullanmak istiyorsak. Global hata yönetimi kullanabiliriz. Ama global hata yönetimini bizim  kontrolümüz dışındaki hatalar için kullanmak daha mantıklı
    {
        private readonly ICategoryService _categoryService;

        public NotFoundFilter(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();

            var product = await _categoryService.GetByIdAsync(id);

            if (product != null)
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
