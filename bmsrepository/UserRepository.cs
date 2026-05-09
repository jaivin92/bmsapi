using System.Data;
using System.Text;
using bmslib.Config;
using bmsmodel.Common;
using bmsrepository.Common;
using bmsrepository.Interface;

namespace bmsrepository
{
    internal class UserRepository(AppConfig appConfig) : BaseRepository(appConfig), IUserRepository
    {
        public async Task<bool> IsExists(UserModel model)
        {
            using var conn = _connection;
            const string sql = @"SELECT COUNT(1)  FROM Users  WHERE   Id!=@Id AND Name = @Name AND IsActive=1;";
            return await conn.ExecuteScalarAsync<int>(
                sql,
                model) > 0;
        }

        public async Task Insert(UserModel model)
        {
            using (var conn = _connection)
            {
                try
                {   
                    await conn.BeginTransactionAsync();
                    model.IsActive = true;
                    model.Id = await conn.InsertAsync("Users",model);
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

        public async Task Update(UserModel model)
        {
            using (var conn = _connection)
            {
                try
                {
                    await conn.BeginTransactionAsync();
                    model.AddIgnore(nameof(model.IsActive));
                    await conn.UpdateAsync("Users", model);
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

        public async Task<UserModel> GetById(long Id)
        {

            using var conn = _connection;
            string sql = @"select * from Users where Id=@Id";
            return await conn.ExecuteScalarAsync<UserModel>(sql, new
            {
                Id,
            });
        }

        public async Task<UserModel?> Login(UserModel model)
        {
            using var conn = _connection;
            const string sql = @"select * from Users
                                 where IsActive=1
                                 and Password=@Password
                                 and (Mobile=@Mobile or (@Email is not null and @Email <> '' and Email=@Email))";
            return await conn.ExecuteScalarAsync<UserModel>(sql, new
            {
                model.Mobile,
                model.Email,
                model.Password
            });
        }

        public async Task<List<UserModel>> GetAll(UserModel model)
        {
            using var conn = _connection;
            StringBuilder sql = new StringBuilder("select *, COUNT(1) OVER () AS TotalRecord  from Users where 1=1");

            if (model != null) { 
                if(model.Id > 0)
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
            return await conn.QueryAsync<UserModel>(sql.ToString(), new
            {
                model.Id,
                model.IsActive,
                model.Name
            });
        }
    }
}
