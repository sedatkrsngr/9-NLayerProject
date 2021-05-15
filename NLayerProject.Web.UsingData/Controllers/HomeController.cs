using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLayerProject.Core.Services;
using NLayerProject.Web.UsingData.Dtos;
using NLayerProject.Web.UsingData.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.Web.UsingData.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ISampleSqlService _sampleSqlService;

        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger,ISampleSqlService sampleSqlService, IMapper mapper)
        {
            _logger = logger;
            _sampleSqlService = sampleSqlService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _sampleSqlService.SqlQueryGetListData("select c.Name as CategoryName,p.Name as ProductName,p.Stock as ProductStock,p.Price as ProductPrice from Categories c join Products p on c.Id = p.CategoryId ");//Birden fazla değer dönen spler ve sorgular için kullanılabilir.

            return View(_mapper.Map<IEnumerable<SampleSqlDto>>(data));
        }

        public async Task<IActionResult> Privacy()
        {
            var data = await _sampleSqlService.SqlQueryGetData("select top 1 c.Name as CategoryName,p.Name as ProductName,p.Stock as ProductStock,p.Price as ProductPrice from Categories c join Products p on c.Id = p.CategoryId ");//tek değer dönen sorgular veya spler için kullanılabilir

            return View(_mapper.Map<SampleSqlDto>(data));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorDto errorDto)
        {
            return View(errorDto);
        }
    }
}
