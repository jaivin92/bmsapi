
using bmsservice.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace bmsservice.Common
{
    public class DependencyConfig
    {
        public static void Configure(IServiceCollection config)
        {
            config.AddTransient<IUserService, UserService>();

            bmsrepository.Common.DependencyConfig.Configure(config);
        }
    }
}
