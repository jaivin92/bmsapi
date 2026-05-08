
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
            config.AddTransient<IFoodCategoryRepository, FoodCategoryRepository>();
            config.AddTransient<IFoodRepository, FoodRepository>();
            config.AddTransient<IFoodTableRepository, FoodTableRepository>();
            config.AddTransient<IOrderRepository, OrderRepository>();
            config.AddTransient<IOrderItemRepository, OrderItemRepository>();

        }
    }
}
