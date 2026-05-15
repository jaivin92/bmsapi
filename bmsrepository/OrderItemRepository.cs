using System.Text;
using bmslib.Config;
using bmsmodel.Common;
using bmsrepository.Common;
using bmsrepository.Interface;

namespace bmsrepository
{
    internal class OrderItemRepository(AppConfig appConfig) : BaseRepository(appConfig), IOrderItemRepository
    {
        public async Task<bool> IsExists(OrderItemModel model)
        {
            using var conn = _connection;
            const string sql = @"SELECT COUNT(1) FROM OrderItems WHERE Id!=@Id AND OrderId = @OrderId AND FoodId = @FoodId AND FoodTableId = @FoodTableId AND IsActive=1;";
            return await conn.ExecuteScalarAsync<int>(
                sql,
                new
                {
                    Id = model.Id,
                    OrderId = model.OrderId,
                    FoodId = model.FoodId,
                    FoodTableId = model.FoodTableId,

                }) > 0;
        }

        public async Task Insert(OrderItemModel model)
        {
            using (var conn = _connection)
            {
                try
                {
                    await conn.BeginTransactionAsync();
                    model.IsActive = true;
                    model.Id = await conn.InsertAsync("OrderItems", model);
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

        public async Task Update(OrderItemModel model)
        {
            using (var conn = _connection)
            {
                try
                {
                    await conn.BeginTransactionAsync();
                    model.AddIgnore(nameof(model.IsActive));
                    await conn.UpdateAsync("OrderItems", model);
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

        public async Task<OrderItemModel> GetById(long Id)
        {
            using var conn = _connection;
            string sql = @"select * from OrderItems where Id=@Id";
            return await conn.ExecuteScalarAsync<OrderItemModel>(sql, new
            {
                Id,
            });
        }

        public async Task<List<OrderItemModel>> GetAll(OrderItemModel model)
        {
            using var conn = _connection;
            StringBuilder sql = new StringBuilder("select *, COUNT(1) OVER () AS TotalRecord from OrderItems where 1=1");

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

                if (model.Quantity > 0)
                {
                    sql.Append(" AND Quantity=@Quantity");
                }

                if (model.FoodId > 0)
                {
                    sql.Append(" AND FoodId=@FoodId");
                }

                if (!string.IsNullOrEmpty(model.Notes))
                {
                    sql.Append(" and Notes like CONCAT('%', @Notes, '%')");
                }

                if (model.OrderId > 0)
                {
                    sql.Append(" AND OrderId=@OrderId");
                }

                //if (!string.IsNullOrEmpty(model.OrderStatus))
                //{
                //    sql.Append(" and OrderStatus like CONCAT('%', @OrderStatus, '%')");
                //}

                if (model.FoodTableId > 0)
                {
                    sql.Append(" AND FoodTableId=@FoodTableId");
                }
            }
            sql.Append(model.DataTableRequestModel.GetPagination("Id desc"));
            return await conn.QueryAsync<OrderItemModel>(sql.ToString(), new
            {
                model.Id,
                model.IsActive,
                model.Quantity,
                model.FoodId,
                model.Notes,
                model.OrderId,
                model.OrderItemStatus,
                model.FoodTableId
            });
        }
    }
}
