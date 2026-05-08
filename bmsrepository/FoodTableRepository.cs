using System.Text;
using bmslib.Config;
using bmsmodel.Common;
using bmsrepository.Common;
using bmsrepository.Interface;

namespace bmsrepository
{
    internal class FoodTableRepository(AppConfig appConfig) : BaseRepository(appConfig), IFoodTableRepository
    {
        public async Task<bool> IsExists(FoodTableModel model)
        {
            using var conn = _connection;
            const string sql = @"SELECT COUNT(1) FROM FoodTables WHERE Id!=@Id AND TableStatus = @TableStatus AND IsActive=1;";
            return await conn.ExecuteScalarAsync<int>(
                sql,
                model) > 0;
        }

        public async Task Insert(FoodTableModel model)
        {
            using (var conn = _connection)
            {
                try
                {
                    await conn.BeginTransactionAsync();
                    model.IsActive = true;
                    model.Id = await conn.InsertAsync("FoodTables", model);
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

        public async Task Update(FoodTableModel model)
        {
            using (var conn = _connection)
            {
                try
                {
                    await conn.BeginTransactionAsync();
                    model.AddIgnore(nameof(model.IsActive));
                    await conn.UpdateAsync("FoodTables", model);
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

        public async Task<FoodTableModel> GetById(long Id)
        {
            using var conn = _connection;
            string sql = @"select * from FoodTables where Id=@Id";
            return await conn.ExecuteScalarAsync<FoodTableModel>(sql, new
            {
                Id,
            });
        }

        public async Task<List<FoodTableModel>> GetAll(FoodTableModel model)
        {
            using var conn = _connection;
            StringBuilder sql = new StringBuilder("select *, COUNT(1) OVER () AS TotalRecord from FoodTables where 1=1");

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

                sql.Append(" and TableStatus = @TableStatus");

                if (model.BookTime != default)
                {
                    sql.Append(" AND BookTime=@BookTime");
                }
            }
            sql.Append(model.DataTableRequestModel.GetPagination("Id desc"));
            return await conn.QueryAsync<FoodTableModel>(sql.ToString(), new
            {
                model.Id,
                model.IsActive,
                model.TableStatus,
                model.BookTime
            });
        }
    }
}
