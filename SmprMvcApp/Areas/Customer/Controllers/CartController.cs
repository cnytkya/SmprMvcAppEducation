using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmprMvcApp.DAL.Repository.Interface;
using SmprMvcApp.EntityLayer.Entities;
using SmprMvcApp.EntityLayer.ViewModels;
using System.Security.Claims;

namespace SmprMvcApp.Areas.Customer.Controllers
{
    [Area("Customer")] //Bu attribute, controller'ın bir Area'ya (bölgeye) ait olduğunu belirtir. Bu durumda, controller "Customer" adlı alana aittir. Böylece URL rotaları şu şekilde yapılandırılabilir: /Customer/Cart/Index
    [Authorize] //Bu attribute, sadece yetkilendirilmiş kullanıcıların bu controller'a erişebileceğini belirtir.
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork; //Dependency Injection (Bağımlılık Enjeksiyonu) kullanarak IUnitOfWork arayüzünden bir nesne oluşturulur.
        public ShoppinCartViewModel ShoppingCartVM { get; set; } //ShoppinCartViewModel türünden bir property tanımlanır. Bu, alışveriş sepetiyle ilgili verileri View (görünüm) katmanına taşımak için bir ViewModel'dir.

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //Alışveriş sepeti verileri, ViewModel'e aktarılır. Bu veriler, View (görünüm) katmanında kullanılacaktır. Önce hangi kullanıcının sepetine bakılacağı belirlenir. Bunun için claim bilgileri kullanılır.
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.AppUserId == userId, includeProperties: "Product")//Kullanıcının sepetindeki ürünler, Product tablosundan çekilir. Bu işlem, Repository sınıfındaki GetAll metoduyla yapılır. Bu metod, filtreleme yapılmasına olanak tanır. Bu durumda, kullanıcının sepetindeki ürünler, AppUserId'ye göre filtrelenir.
            };
            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);//Her bir ürünün fiyatı, miktarına göre belirlenir. Bu değer, ShoppingCart nesnesindeki Price property'sine atanır.
                ShoppingCartVM.OrderTotal += (cart.Price * cart.Count);//Sepetteki ürünlerin toplam fiyatı hesaplanır. Her bir ürünün fiyatı, miktarıyla çarpılarak toplam fiyat bulunur. Bu değer, ViewModel'deki OrderTotal property'sine atanır.
            }
            return View(ShoppingCartVM);
        }

        public IActionResult Plus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            cartFromDb.Count += 1;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            if (cartFromDb.Count <= 1)
            {
                //remove that from cart
                _unitOfWork.ShoppingCart.Remove(cartFromDb);
            }
            else
            {
                cartFromDb.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);

            _unitOfWork.ShoppingCart.Remove(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary(int cartId)
        {
            return View();
        }

        private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            if (shoppingCart.Count <= 50) //Eğer üründen 50 adetten az varsa, ürünün normal fiyatı alınır.
            {
                return shoppingCart.Product.Price;
            }
            else
            {
                if (shoppingCart.Count <= 100) //Eğer üründen 50 adetten fazla, 100 adetten az varsa, ürünün 50 adetlik fiyatı alınır.
                {
                    return shoppingCart.Product.Price50;
                }
                else //Eğer üründen 100 adetten fazla varsa, ürünün 100 adetlik fiyatı alınır.
                {
                    return shoppingCart.Product.Price100;
                }
            }
        }
    }
}