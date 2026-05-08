using System.Text;
using bmslib.Config;
using bmsmodel.Common;
using bmsrepository.Common;
using bmsrepository.Interface;

namespace bmsrepository
{
    internal class FoodRepository(AppConfig appConfig) : BaseRepository(appConfig), IFoodRepository
    {
        public async Task<bool> IsExists(FoodModel model)
        {
            using var conn = _connection;
            const string sql = @"SELECT COUNT(1) FROM Foods WHERE Id!=@Id AND Name = @Name AND FoodCategoryId = @FoodCategoryId AND IsActive=1;";
            return await conn.ExecuteScalarAsync<int>(
                sql,
                model) > 0;
        }

        public async Task Insert(FoodModel model)
        {
            using (var conn = _connection)
            {
                try
                {
                    await conn.BeginTransactionAsync();
                    model.IsActive = true;
                    model.Id = await conn.InsertAsync("Foods", model);
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

        public async Task Update(FoodModel model)
        {
            using (var conn = _connection)
            {
                try
                {
                    await conn.BeginTransactionAsync();
                    model.AddIgnore(nameof(model.IsActive));
                    await conn.UpdateAsync("Foods", model);
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

        public async Task<FoodModel> GetById(long Id)
        {
            using var conn = _connection;
            string sql = @"select * from Foods where Id=@Id";
            return await conn.ExecuteScalarAsync<FoodModel>(sql, new
            {
                Id,
            });
        }

        public async Task<List<FoodModel>> GetAll(FoodModel model)
        {
            using var conn = _connection;
            StringBuilder sql = new StringBuilder("select *, COUNT(1) OVER () AS TotalRecord from Foods where 1=1");

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

                if (!string.IsNullOrEmpty(model.Name))
                {
                    sql.Append(" and Name like CONCAT('%', @Name, '%')");
                }

                if (!string.IsNullOrEmpty(model.Description))
                {
                    sql.Append(" and Description like CONCAT('%', @Description, '%')");
                }

                if (model.Price > 0)
                {
                    sql.Append(" AND Price=@Price");
                }

                if (model.FoodCategoryId > 0)
                {
                    sql.Append(" AND FoodCategoryId=@FoodCategoryId");
                }
            }
            sql.Append(model.DataTableRequestModel.GetPagination("Id desc"));
            return await conn.QueryAsync<FoodModel>(sql.ToString(), new
            {
                model.Id,
                model.IsActive,
                model.Name,
                model.Description,
                model.Price,
                model.FoodCategoryId
            });
        }
    }
}
