using bmslib.Enmus;

namespace bmsmodel.Common
{
    public class OrderModel : BaseModel
    {
        public long UserId { get; set; }

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        public OrderType OrderType { get; set; } = OrderType.DineIn;

        public DateTime OrderDate { get; set; }

        public string? Notes { get; set; }
    }
}
