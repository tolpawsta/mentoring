namespace NorthwindDAL.Entities
{
    public class CustomerOrderDetail
    {
        public string ProductName { get; set; }
        public int Discount { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal ExtentedPrice { get; set; }
    }
}