using System.Data;
using bmslib.Config;
using MySqlConnector;

namespace bmsrepository.Common
{
    internal abstract class BaseRepository
    {
        protected readonly AppConfig _appConfig;


        public BaseRepository(AppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public IDbConnection create()
        {
            return new MySqlConnection(_appConfig.ConnectionStrings.CMSDBConnection);
        }

        public IDbConnection _connection
        {
            get
            {
                var conn = create();
                conn.Open();
                return conn;
            }
        }
    }
}
