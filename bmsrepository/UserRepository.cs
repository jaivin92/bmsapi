using System.Data;
using bmslib.Config;
using bmsmodel.Common;
using bmsrepository.Common;
using bmsrepository.Interface;
using Dapper;

namespace bmsrepository
{
    internal class UserRepository(AppConfig appConfig) : BaseRepository(appConfig), IUserRepository
    {
        public async Task<bool> IsExists(UserModel model)
        {
            using (var conn = _connection)
            {
                try
                {
                    const string sql = @"
            SELECT COUNT(1)
            FROM Users
            WHERE
                (Name = @Name OR Mobile = @Mobile)
                AND (@Id = 0 OR Id <> @Id);";
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    var count = await conn.ExecuteScalarAsync<int>(
                        sql,
                        model);

                    return count > 0;

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    //await conn
                }
            }
        }

        public async Task Insert(UserModel model)
        {
            using (var conn = _connection)
            {
                try
                {
                    const string sql = @"
            INSERT INTO Users
            (
                Name,
                Email,
                Mobile,
                Password
            )
            VALUES
            (
                @Name,
                @Email,
                @Mobile,
                @Password
            );

            SELECT LAST_INSERT_ID();";
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    int id = await conn.ExecuteScalarAsync<int>(
                        sql,
                        model);

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    //await conn
                }
            }
        }

        public async Task Update(UserModel model)
        {
            using (var conn = _connection)
            {
                try
                {
                    const string sql = @"
            UPDATE Users
            SET
                Name = @Name,
                Email = @Email,
                Mobile = @Mobile,
                Password = @Password
            WHERE
                Id = @Id;";
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    await conn.ExecuteAsync(
                        sql,
                        model);

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    //await conn
                }
            }
        }

        public async Task<UserModel?> GetById(long id)
        {
            using (var conn = _connection)
            {
                try
                {
                    const string sql = @"
            SELECT
                Id,
                Name,
                Email,
                Mobile,
                Password
            FROM Users
            WHERE
                Id = @Id;";
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    return await conn.QueryFirstOrDefaultAsync<UserModel>(
                        sql,
                        new { Id = id });

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    //await conn
                }
            }
        }

        public async Task<List<UserModel>> GetAll()
        {
            using (var conn = _connection)
            {
                try
                {
                    const string sql = @"
            SELECT
                Id,
                Name,
                Email,
                Mobile,
                Password
            FROM Users;";
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    var users = await conn.QueryAsync<UserModel>(sql);
                    return users.ToList();

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    //await conn
                }
            }
        }


    }
}
