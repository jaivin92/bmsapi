using System.Text;
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
                    model.Id = await conn.InsertAsync("Orders", model);
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

                if (model.OrderDate != default)
                {
                    sql.Append(" AND OrderDate=@OrderDate");
                }

                if (!string.IsNullOrEmpty(model.Notes))
                {
                    sql.Append(" and Notes like CONCAT('%', @Notes, '%')");
                }
            }
            sql.Append(model.DataTableRequestModel.GetPagination("Id desc"));
            return await conn.QueryAsync<OrderModel>(sql.ToString(), new
            {
                model.Id,
                model.IsActive,
                model.UserId,
                model.OrderStatus,
                model.OrderType,
                model.OrderDate,
                model.Notes
            });
        }
    }
}
