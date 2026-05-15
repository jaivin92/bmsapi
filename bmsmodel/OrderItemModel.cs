using bmslib.Enmus;

namespace bmsmodel.Common
{
    public class OrderItemModel : BaseModel
    {
        public int Quantity { get; set; }

        public long FoodId { get; set; }

        public string? Notes { get; set; }

        public long OrderId { get; set; }

        public OrderItemStatus OrderItemStatus { get; set; } = OrderItemStatus.Preparing;

        public long? FoodTableId { get; set; }

        //{ IsActive: true, OrderId: 8, FoodId: 28, Quantity: 1, FoodTableId: 1, OrderStatus: 1, Notes: "teststes" }
    }
}
