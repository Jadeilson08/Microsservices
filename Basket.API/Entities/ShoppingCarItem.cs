namespace Basket.API.Entities
{
    public class ShoppingCarItem
    {
        public int Quantity { get; set; }
        public decimal Price { get; private set; }
        public string ProductId { get; set; }
        public string ProductName {  get; set; }

        public void UpdatePrice(int amount)
        {
            Price -= amount;
        }
    }
}