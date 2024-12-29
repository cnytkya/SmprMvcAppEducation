using SmprMvcApp.EntityLayer.Entities;

namespace SmprMvcApp.EntityLayer.ViewModels
{
    public class ShoppinCartViewModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public double OrderTotal { get; set; }
    }
}