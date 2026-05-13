using bmslib.Enmus;

namespace bmsmodel.Common
{
    public class OrderModel : BaseModel
    {
        public long UserId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public OrderType OrderType { get; set; }

        public DateTime OrderDate { get; set; }

        public string? Notes { get; set; }

        public List<OrderItemModel>? OrderItemModels { get; set; }
        //{"Id":0,"IsActive":true,"OrderId":1,"FoodId":1,"Quantity":1,"FoodTableId":1,"OrderStatus":1,"Notes":null}
    }
}
