using System.Text;
using bmslib.Config;
using bmsmodel.Common;
using bmsrepository.Common;
using bmsrepository.Interface;

namespace bmsrepository
{
    internal class FoodCategoryRepository(AppConfig appConfig) : BaseRepository(appConfig), IFoodCategoryRepository
    {
        public async Task<bool> IsExists(FoodCategoryModel model)
        {
            using var conn = _connection;
            const string sql = @"SELECT COUNT(1) FROM FoodCategories WHERE Id!=@Id AND Name = @Name AND IsActive=1;";
            return await conn.ExecuteScalarAsync<int>(
                sql,
                model) > 0;
        }

        public async Task Insert(FoodCategoryModel model)
        {
            using (var conn = _connection)
            {
                try
                {
                    await conn.BeginTransactionAsync();
                    model.IsActive = true;
                    model.Id = await conn.InsertAsync("FoodCategories", model);
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

        public async Task Update(FoodCategoryModel model)
        {
            using (var conn = _connection)
            {
                try
                {
                    await conn.BeginTransactionAsync();
                    model.AddIgnore(nameof(model.IsActive));
                    await conn.UpdateAsync("FoodCategories", model);
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

        public async Task<FoodCategoryModel> GetById(long Id)
        {
            using var conn = _connection;
            string sql = @"select * from FoodCategories where Id=@Id";
            return await conn.ExecuteScalarAsync<FoodCategoryModel>(sql, new
            {
                Id,
            });
        }

        public async Task<List<FoodCategoryModel>> GetAll(FoodCategoryModel model)
        {
            using var conn = _connection;
            StringBuilder sql = new StringBuilder("select *, COUNT(1) OVER () AS TotalRecord from FoodCategories where 1=1");

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
            }
            sql.Append(model.DataTableRequestModel.GetPagination("Id desc"));
            return await conn.QueryAsync<FoodCategoryModel>(sql.ToString(), new
            {
                model.Id,
                model.IsActive,
                model.Name
            });
        }
    }
}
