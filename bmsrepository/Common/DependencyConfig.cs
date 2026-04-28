
using bmslib.Config;
using bmsrepository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace bmsrepository.Common
{
    public class DependencyConfig
    {
        public static void Configure(IServiceCollection config, AppConfig appConfig)
        {
            config.AddTransient<IUserRepository, UserRepository>();

        }
    }
}
