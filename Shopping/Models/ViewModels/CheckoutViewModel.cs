using Shopping.Models.ViewModels;

namespace Shopping.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public OrderModel Order { get; set; }
        public CartItemViewModel CartSummary { get; set; }
    }
}
