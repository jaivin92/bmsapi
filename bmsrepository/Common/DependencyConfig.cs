
using bmsrepository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace bmsrepository.Common
{
    public class DependencyConfig
    {
        public static void Configure(IServiceCollection config)
        {
            config.AddTransient<IUserRepository, UserRepository>();

        }
    }
}
