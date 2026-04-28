using bmslib.Config;
using bmsmodel.Common;
using bmsrepository.Common;
using bmsrepository.Interface;

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
                    await conn.BeginTransactionAsync();
                    model.IsActive = true;
                    model.Id = await conn.InsertAsync("User", model);
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
    }
}
