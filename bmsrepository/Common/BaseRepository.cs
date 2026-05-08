using bmslib.Config;

namespace bmsrepository.Common
{
    internal abstract class BaseRepository
    {
        protected readonly AppConfig _appConfig;

        public BaseRepository(AppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public IDbConnection _connection
        {
            get
            {
                return new IDbConnection(_appConfig);
            }
        }
    }
}
