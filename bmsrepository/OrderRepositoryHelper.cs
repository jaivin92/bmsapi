using bmsmodel;

namespace bmsrepository
{
    public static class OrderRepositoryHelper
    {
        public static List<OrderItemModel> MergeDuplicateOrderItems(List<OrderItemModel> items)
        {
            if (items == null || items.Count == 0)
            {
                return [];
            }

            return items
                .GroupBy(item => new { item.OrderId, item.FoodId })
                .Select(group => new OrderItemModel
                {
                    OrderId = group.Key.OrderId,
                    FoodId = group.Key.FoodId,
                    Quantity = group.Sum(x => x.Quantity <= 0 ? 1 : x.Quantity)
                })
                .ToList();
        }
    }
}
