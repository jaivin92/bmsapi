namespace bmsmodel.Common
{
    public class OrderItemModel : BaseModel
    {
        public int Quantity { get; set; }

        public long FoodId { get; set; }

        public string? Notes { get; set; }

        public long OrderId { get; set; }

        public string OrderStatus { get; set; } = string.Empty;

        public long FoodTableId { get; set; }
    }
}
