using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Tên người nhận")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Số điện thoại")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Địa chỉ")]
        public string Address { get; set; }
        public decimal ShippingCost { get; set; }
        public string CouponCode { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
    }
}
