using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;   // we will be displaying all the products ,so we need to get all the products form database ,for that we need IunitOfWork using dependency injection

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties:"Category");
            return View(productList);  // When we retrieve the productList in line 22 ,we can return back to the view and we will pass the productList
        }
        public IActionResult Details(int productId)
        {                                                         //it will be get because it'll be a single product
            Product product = _unitOfWork.Product.Get(u=>u.Id== productId, includeProperties: "Category");  // when we are getting a product , we need to add the like operator u=>u.Id==id based on that it will retrieve teh product details
            return View(product);  // When we retrieve the productList in line 22 ,we can return back to the view and we will pass the productList
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
