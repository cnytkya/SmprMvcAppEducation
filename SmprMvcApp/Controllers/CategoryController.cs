using Microsoft.AspNetCore.Mvc;
using SmprMvcApp.Data;
using SmprMvcApp.Models;

namespace SmprMvcApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;//private readonly AppDbContext _appDbContext; ifadesi, bir sınıf içinde AppDbContext türünde yalnızca okunabilir (readonly) bir alan tanımlar. Bu tür bir alan genellikle bağımlılık enjeksiyonu (Dependency Injection) ile veri tabanı işlemlerini yapmak için kullanılır.

        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            // Veritabanından kategorileri listeye çekiyoruz
            List<Category> categoriesList = _appDbContext.Categories.ToList();

            // View'a kategoriler listesini model olarak geçiriyoruz
            return View(categoriesList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //Validation
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name and DisplayOrder cannot have the same value");
            }
            if (ModelState.IsValid)//server side validations
            {
                _appDbContext.Categories.Add(obj);
                _appDbContext.SaveChanges();
                TempData["success"] = "Category created successfully"; //eğer ekleme başarılı ise bana success mesajı versin.
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update(int? id)//Update ederken obj'yi id'ye göre çağıracağız. Nullable olabilir.
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Category? category = _appDbContext.Categories.Find(id);
            Category? category2 = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            //Category? category3 = _appDbContext.Categories.Where(c => c.Id == id).FirstOrDefault();
            if (category2 == null)
            {
                return NotFound();
            }
            return View(category2);
        }

        [HttpPost]
        public IActionResult Update(Category obj)
        {
            //Validation
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name and DisplayOrder cannot have the same value");
            }
            if (ModelState.IsValid)
            {
                _appDbContext.Categories.Update(obj);
                _appDbContext.SaveChanges();
                TempData["success"] = "Category updated successfully"; //eğer güncelleme başarılı ise bana success mesajı versin.
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
            //Category? category = _appDbContext.Categories.Find(id);
            Category? category2 = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            //Category? category3 = _appDbContext.Categories.Where(c => c.Id == id).FirstOrDefault();
            if (category2 == null)
            {
                return NotFound();
            }
            return View(category2);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteCategory(int? id)//bir nesneyi silmek için önce o nesneyi veritabanında bulmamız gerekir. nesnenin id'sini bulup silecektir.
        {
            Category? obj = _appDbContext.Categories.FirstOrDefault(obj => obj.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _appDbContext.Categories.Remove(obj);
            _appDbContext.SaveChanges();
            TempData["success"] = "Category deleted successfully"; //eğer silme başarılı ise bana success mesajı versin.
            return RedirectToAction("Index");
        }
    }
}