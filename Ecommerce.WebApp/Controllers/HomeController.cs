using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ecommerce.WebApp.Models;
using Ecommerce.Application.Services;
using Ecommerce.ViewModels.Catalog.Product;
using Ecommerce.Data.Entities;
using Ecommerce.Application.Catalog.Products;

namespace Ecommerce.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IRepository<Product> _ProductRepository;
        private IUnitOfWork _UOW;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork uow)
        {
            _logger = logger;
            _UOW = uow;
            _ProductRepository = _UOW.GetRepository<Product>();
        }
        
        public IActionResult Index()
        {
            var products = _ProductRepository.GetAll();
            return View(products);
        }

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
