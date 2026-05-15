using System.Text;
using System.Linq;
using bmslib.Config;
using bmsmodel.Common;
using bmsrepository.Common;
using bmsrepository.Interface;

namespace bmsrepository
{
    internal class OrderRepository(AppConfig appConfig) : BaseRepository(appConfig), IOrderRepository
    {
        public async Task<bool> IsExists(OrderModel model)
        {
            using var conn = _connection;
            const string sql = @"SELECT COUNT(1) FROM Orders WHERE Id!=@Id AND UserId = @UserId AND OrderDate = @OrderDate AND IsActive=1;";
            return await conn.ExecuteScalarAsync<int>(
                sql,
                model) > 0;
        }

        public async Task Insert(OrderModel model)
        {
            using (var conn = _connection)
            {
                try
                {
                    await conn.BeginTransactionAsync();
                    model.IsActive = true;
                    model.AddIgnore(nameof(model.OrderItemModels));
                    if(model.CustomerId == 0)
                    {
                        model.AddIgnore(nameof(model.CustomerId));
                    }
                    model.Id = await conn.InsertAsync("Orders", model);

                    if (model.OrderItemModels != null && model.OrderItemModels.Count > 0)
                    {
                        foreach (var orderItem in model.OrderItemModels)
                        {
                            orderItem.OrderId = model.Id;
                            orderItem.IsActive = true;
                            await conn.InsertAsync("OrderItems", orderItem);
                        }
                    }

                    await conn.CommitAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    await conn.RollbackAsync();
                }
            }
        }

        public async Task Update(OrderModel model)
        {
            using (var conn = _connection)
            {
                try
                {
                    await conn.BeginTransactionAsync();
                    model.AddIgnore(nameof(model.IsActive));
                    await conn.UpdateAsync("Orders", model);

                    if (model.OrderItemModels != null)
                    {
                        var existingItems = await conn.QueryAsync<OrderItemModel>(
                            "SELECT * FROM OrderItems WHERE OrderId=@OrderId AND IsActive=1",
                            new { OrderId = model.Id });

                        var existingItemMap = existingItems.ToDictionary(x => x.Id, x => x);

                        foreach (var orderItem in model.OrderItemModels)
                        {
                            orderItem.OrderId = model.Id;
                            orderItem.IsActive = true;

                            if (orderItem.Id > 0 && existingItemMap.ContainsKey(orderItem.Id))
                            {
                                await conn.UpdateAsync("OrderItems", orderItem);
                                existingItemMap.Remove(orderItem.Id);
                            }
                            else
                            {
                                orderItem.Id = 0;
                                await conn.InsertAsync("OrderItems", orderItem);
                            }
                        }

                        foreach (var deletedItem in existingItemMap.Values)
                        {
                            deletedItem.IsActive = false;
                            await conn.UpdateAsync("OrderItems", deletedItem);
                        }
                    }

                    await conn.CommitAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    await conn.RollbackAsync();
                }
            }
        }

        public async Task<OrderModel> GetById(long Id)
        {
            using var conn = _connection;
            string sql = @"select * from Orders where Id=@Id";
            return await conn.ExecuteScalarAsync<OrderModel>(sql, new
            {
                Id,
            });
        }

        public async Task<List<OrderModel>> GetAll(OrderModel model)
        {
            using var conn = _connection;
            StringBuilder sql = new StringBuilder("select *, COUNT(1) OVER () AS TotalRecord from Orders where 1=1");

            if (model != null)
            {
                if (model.Id > 0)
                {
                    sql.Append(" AND Id=@Id");
                }

                if (model.IsActive)
                {
                    sql.Append(" AND IsActive=@IsActive");
                }

                if (model.UserId > 0)
                {
                    sql.Append(" AND UserId=@UserId");
                }

                sql.Append(" and OrderStatus = @OrderStatus");
                sql.Append(" and OrderType = @OrderType");

                //if (model.OrderDate != default)
                //{
                //    sql.Append(" AND OrderDate=@OrderDate");
                //}

                //if (!string.IsNullOrEmpty(model.Notes))
                //{
                //    sql.Append(" and Notes like CONCAT('%', @Notes, '%')");
                //}
            }
            sql.Append(model.DataTableRequestModel.GetPagination("Id desc"));

            var orders = await conn.QueryAsync<OrderModel>(sql.ToString(), new
            {
                model.Id,
                model.IsActive,
                model.UserId,
                model.OrderStatus,
                model.OrderType,
                model.OrderDate,
                model.Notes
            });

            if (orders != null && orders.Count > 0)
            {
                var orderIds = orders.Select(x => x.Id).ToList();
                var orderItems = await conn.QueryAsync<OrderItemModel>(
                    "SELECT * FROM OrderItems WHERE IsActive=1 AND OrderId IN @OrderIds",
                    new { OrderIds = orderIds });

                var orderItemLookup = orderItems
                    .GroupBy(x => x.OrderId)
                    .ToDictionary(x => x.Key, x => x.ToList());

                foreach (var order in orders)
                {
                    order.OrderItemModels = orderItemLookup.TryGetValue(order.Id, out var items)
                        ? items
                        : [];
                }
            }

            return orders;
        }
    }
}
