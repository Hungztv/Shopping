namespace Shopping.Models
{
    public class CartItemModel
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total
        {
            get { return Quantity * Price; }
        }
        public string Image { get; set; }
        public CartItemModel()
        {

        }
        public CartItemModel(ProductModel product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null");
            }
            ProductId = product.Id;
            ProductName = product.Name;

            // Remove formatting from price string (e.g., "19.990.000 VNĐ" -> 19990000)
            string priceString = product.Price;
            if (!string.IsNullOrEmpty(priceString))
            {
                // Remove dots, commas, currency symbols and spaces
                priceString = priceString.Replace("VNĐ", "")
                                       .Replace("VND", "")
                                       .Replace(".", "")
                                       .Replace(",", "")
                                       .Replace(" ", "")
                                       .Trim();

                if (decimal.TryParse(priceString, out decimal parsedPrice))
                {
                    Price = parsedPrice;
                }
                else
                {
                    Price = 0;
                }
            }
            else
            {
                Price = 0;
            }

            Quantity = 1;
            Image = product.Image;
        }
    }
}
