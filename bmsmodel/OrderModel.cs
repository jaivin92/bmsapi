namespace bmsmodel.Common
{
    public class OrderModel : BaseModel
    {
        public long UserId { get; set; }

        public string OrderStatus { get; set; } = string.Empty;

        public string OrderType { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public string? Notes { get; set; }
    }
}
