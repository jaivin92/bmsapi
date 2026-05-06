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
        public async Task Insert(UserModel model)
        {
            using (var conn = _connection)
            {
                try
                {
                    const string sql = @"
            INSERT INTO Users
            (
                Name
            )
            VALUES
            (
                @Name
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


    }
}
