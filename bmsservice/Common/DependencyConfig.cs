
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
            config.AddTransient<IFoodTableService, FoodTableService>();
            config.AddTransient<IOrderService, OrderService>();
            config.AddTransient<IOrderItemService, OrderItemService>();

            bmsrepository.Common.DependencyConfig.Configure(config, appConfig);
        }
    }
}
