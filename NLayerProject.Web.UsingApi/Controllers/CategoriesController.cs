using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLayerProject.Web.UsingApi.Dtos;
using NLayerProject.Web.UsingApi.Filters;
using NLayerProject.Web.UsingApi.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Web.UsingApi.Controllers
{
    public class CategoriesController : Controller
    {

        ApiAdress _api = new ApiAdress();
        public async Task<IActionResult> Index()
        {
            HttpClient client = _api.Initial();
            var result = "";
            var resp = await client.GetAsync("Categories");

            if (resp.IsSuccessStatusCode)
            {
                result =  resp.Content.ReadAsStringAsync().Result;

                var Categories = JsonConvert.DeserializeObject<List<CategoryDto>>(result);

                HttpContext.Session.SetObjectAsJson("Categories", Categories);//Extensions ile sessionda complex veri tutabiliyoruz örnek olarak koydum.
                HttpContext.Session.SetString("id", "10");//Bu da normal session kullanımı set

                return View(Categories);
            }
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {

            var categories = HttpContext.Session.GetObjectFromJson<List<CategoryDto>>("Categories");//örn olarak yapıldı. böylelikle indexte kullanılan session verisini böyle çağırabiliyoruz kodda nasıl kullanıldığını göstermek için koydum
            var id = HttpContext.Session.GetString("id");
            HttpClient client = _api.Initial();
            var json = JsonConvert.SerializeObject(categoryDto);
            var result = "";
            var resp = await client.PostAsync("Categories", new StringContent(json, Encoding.UTF8, "application/json"));

            if (resp.IsSuccessStatusCode)
            {
                result = resp.Content.ReadAsStringAsync().Result;
                
            }
            return RedirectToAction("Index");
        }

        //update/5
        public async Task<IActionResult> Update(int id)
        {
            HttpClient client = _api.Initial();
            var json = JsonConvert.SerializeObject(id);
            var result = "";
            var resp = await client.GetAsync($"Categories/{id}");

            if (resp.IsSuccessStatusCode)
            {
                result = resp.Content.ReadAsStringAsync().Result;
                var Category = JsonConvert.DeserializeObject<CategoryDto>(result);

                return View(Category);

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            HttpClient client = _api.Initial();
            var json = JsonConvert.SerializeObject(categoryDto);
            var result = "";
            var resp = await client.PutAsync("Categories", new StringContent(json, Encoding.UTF8, "application/json"));

            if (resp.IsSuccessStatusCode)
            {
                result = resp.Content.ReadAsStringAsync().Result;

            }

            return RedirectToAction("Index");
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = _api.Initial();
            var json = JsonConvert.SerializeObject(id);
            var result = "";
            var resp = await client.DeleteAsync($"Categories/{id}");

            if (resp.IsSuccessStatusCode)
            {
                result = resp.Content.ReadAsStringAsync().Result;

            }

            return RedirectToAction("Index");
        }
    }
}
