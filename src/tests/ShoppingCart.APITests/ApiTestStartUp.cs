using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.API;

namespace ShoppingCart.APITests
{
    public class ApiTestStartUp : Startup
    {
        public ApiTestStartUp(IWebHostEnvironment env) : base(env)
        {
        }

        protected override void OverrideServices(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();

            // var categoryReader = provider.GetService<ICategoryReader>();
            // services.AddScoped(sp => categoryReader);
            // var categoryRepository = provider.GetService<ICategoryRepository>();
            // services.AddScoped(sp => categoryRepository);
            //
            // var productReader = provider.GetService<IProductReader>();
            // services.AddScoped(sp => productReader);
            // var productRepository = provider.GetService<IProductRepository>();
            // services.AddScoped(sp => productRepository);
            //
            // var couponReader = provider.GetService<ICouponReader>();
            // services.AddScoped(sp => couponReader);
            // var couponRepository = provider.GetService<ICouponRepository>();
            // services.AddScoped(sp => couponRepository);
            //
            // var campaignReader = provider.GetService<ICampaignReader>();
            // services.AddScoped(sp => campaignReader);
            // var campaignRepository = provider.GetService<ICampaignRepository>();
            // services.AddScoped(sp => campaignRepository);
            //
            // var cartReader = provider.GetService<ICartReader>();
            // services.AddScoped(sp => cartReader);
            // var cartRepository = provider.GetService<ICartRepository>();
            // services.AddScoped(sp => cartRepository);
        }
    }
}