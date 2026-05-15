namespace bmslib.Enmus
{
    public enum OrderStatus
    {
        Pending = 1,
        Accepted = 2,
        Preparing = 3,
        Ready = 4,
        Served = 5,
        Completed = 6,
        Cancelled = 7
    }

    public enum OrderItemStatus
    {
        Preparing = 1,
        Served = 2,
        Parcel = 3,
    }

    public enum OrderType
    {
        DineIn = 1,
        TakeAway = 2,
        Delivery = 3
    }

    public enum FoodTableType
    {
        Available = 1,
        Reserved = 2,
        Occupied = 3,
        Cleaning = 4
    }


}
