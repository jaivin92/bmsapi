
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
            config.AddTransient<IFoodCategoryService, FoodCategoryService>();
            config.AddTransient<IFoodService, FoodService>();

            bmsrepository.Common.DependencyConfig.Configure(config, appConfig);
        }
    }
}
