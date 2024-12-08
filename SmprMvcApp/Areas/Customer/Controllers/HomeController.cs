using Microsoft.AspNetCore.Mvc;
using SmprMvcApp.DAL.Repository.Interface;
using SmprMvcApp.EntityLayer.Entities;
using SmprMvcApp.Models;
using System.Diagnostics;

namespace SmprMvcApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> productsList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return View(productsList);
        }

        public IActionResult ProductDetails(int productId)
        {
            Product getProductDetails = _unitOfWork.Product.Get(p => p.Id == productId, includeProperties: "Category");
            return View(getProductDetails);
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