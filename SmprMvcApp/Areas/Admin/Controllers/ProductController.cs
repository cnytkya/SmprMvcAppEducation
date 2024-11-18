using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmprMvcApp.DAL.DbContextModel;
using SmprMvcApp.DAL.Repository.Interface;
using SmprMvcApp.EntityLayer.Entities;
using SmprMvcApp.EntityLayer.ViewModels;

namespace SmprMvcApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> productsList = _unitOfWork.Product.GetAll().ToList();
            return View(productsList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            /*IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.
            GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });*/
            //ViewBag kullanımı
            //ViewBag.CategoryList = CategoryList;
            //ViewData kullanımı
            //ViewData["CategoryList"] = CategoryList;
            ProductViewModel productViewModel = new()
            {
                CategoryList = _unitOfWork.Category
                .GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Product = new Product()
            };
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)//server side validations
            {
                _unitOfWork.Product.Add(productViewModel.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully"; //eğer ekleme başarılı ise bana success mesajı versin.
                return RedirectToAction("Index");
            }
            else
            {
                productViewModel.CategoryList = _unitOfWork.Category
                .GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
                return View(productViewModel);
            }
        }

        [HttpGet]
        public IActionResult Update(int? id)//Update ederken obj'yi id'ye göre çağıracağız. Nullable olabilir.
        {
            Product? product = _unitOfWork.Product.Get(c => c.Id == id);
            if (id == null || id == 0)//Eğer böyle bir kayıt yoksa, null döndür.
            {
                return NotFound();
            }

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully"; //eğer güncelleme başarılı ise bana success mesajı versin.
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)//Update ederken obj'yi id'ye göre çağıracağız. boş değer alabilir
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? product = _unitOfWork.Product.Get(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteProduct(int? id)//veritabanından id'yi bulup silecek.
        {
            Product? product = _unitOfWork.Product.Get(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully"; //eğer silme başarılı ise bana success mesajı versin.
            return RedirectToAction("Index");
        }
    }
}