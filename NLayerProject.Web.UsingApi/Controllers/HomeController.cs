﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLayerProject.Web.UsingApi.Filters;
using NLayerProject.Web.UsingApi.Helper;
using NLayerProject.Web.UsingApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.Web.UsingApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [SessionControl]//Bu belirttiğimiz session bilgisine göre çalışır
        public IActionResult Index()
        {
            return View();
        }

        [RolControl(RolesEnum.Admin)]//Burada da sessionda cektiğimiz rol bilgilerinde admin var ise girebilir.
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
