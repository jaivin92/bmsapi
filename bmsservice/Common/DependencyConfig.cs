
using bmslib.Config;
using bmsservice.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace bmsservice.Common
{
    public class DependencyConfig
    {
        public static void Configure(IServiceCollection config, AppConfig appConfig)
        {
            config.AddTransient<IUserService, UserService>();

            bmsrepository.Common.DependencyConfig.Configure(config, appConfig);
        }
    }
}
