using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmprMvcApp.DAL.Repository.Interface;
using SmprMvcApp.EntityLayer.Entities;
using SmprMvcApp.EntityLayer.ViewModels;

namespace SmprMvcApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;//ASP.NET Core uygulamalarında çalışma ortamını, dosya yollarını ve uygulama ile ilgili diğer çevresel bilgileri sağlar.Özellikle dosya işlemleri yaparken (örneğin, bir dosya yükleme işlemi sırasında fiziksel bir dosya yoluna erişim gerektiğinde) veya çalışma ortamını öğrenmek için kullanılır.

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> productsList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return View(productsList);
        }

        #region
        /*[HttpGet]
        public IActionResult Create()
        {
            *//*IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.
            GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });*//*
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
                    Text = c.Name,//Text: Kullanıcıya gösterilecek olan metin (Name).
                    Value = c.Id.ToString()//ToString() ile bu ID, metin formatına dönüştürülür, çünkü Value özelliği bir string türündedir. <option value="3">...</option>
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
        }*/

        /*[HttpGet]
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
        }*/
        #endregion

        //create ve update metotlarını tek bir fonksiyonda birleştirdim.
        [HttpGet]
        public IActionResult Upsert(int? id) //Update + Insert: Combine ettik yani iki metodu birleştirdik.
        {
            ProductViewModel productViewModel = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                //create
                return View(productViewModel);
            }
            else
            {
                //update
                productViewModel.Product = _unitOfWork.Product.Get(c => c.Id == id);
                return View(productViewModel);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel productViewModel, IFormFile? file)
        {
            if (ModelState.IsValid)//server side validations
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    //günceleme yaparken eski img'i silmek için
                    if (!string.IsNullOrEmpty(productViewModel.Product.ImageUrl))
                    {
                        //eğer img null ya da boş değilse eski img'i sil
                        var oldImgPath =
                            Path.Combine(wwwRootPath, productViewModel.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImgPath))
                        {
                            System.IO.File.Delete(oldImgPath);
                        }
                    }

                    //dosyayı kaydetmek için
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productViewModel.Product.ImageUrl = @"\images\product\" + fileName;
                }
                //create ve update için karar yapısını kuralım
                if (productViewModel.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productViewModel.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productViewModel.Product);
                }
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