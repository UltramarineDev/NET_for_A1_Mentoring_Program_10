namespace NorthwindDAL
{
    public class OrderDetailsHistoryModel
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public decimal ExtendedPrice { get; set; }
    }
}
