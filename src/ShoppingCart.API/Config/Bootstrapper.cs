using System.Collections.Generic;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Business.Campaign;
using ShoppingCart.Business.Category;
using ShoppingCart.Business.Coupon;
using ShoppingCart.Business.Product;
using ShoppingCart.Business.Validation;
using ShoppingCart.Infra.Persistence.Campaign;
using ShoppingCart.Infra.Persistence.Category;
using ShoppingCart.Infra.Persistence.Coupon;
using ShoppingCart.Infra.Persistence.Product;

namespace ShoppingCart.API.Config
{
    public class Bootstrapper
    {
        private readonly IServiceCollection services;

        private readonly ApiSettings settings;

        public Bootstrapper(IServiceCollection services, ApiSettings settings)
        {
            this.services = services;
            this.settings = settings;
        }

        public void RegisterServices()
        {
            var categories = new List<Business.Category.Category>();
            services.AddSingleton<ICategoryReader>(sp => new InMemoryCategoryReader(categories));
            services.AddSingleton<ICategoryRepository>(sp => new InMemoryCategoryRepository(categories));


            var products = new List<Business.Product.Product>();
            services.AddSingleton<IProductReader>(sp => new InMemoryProductReader(products));
            services.AddSingleton<IProductRepository>(sp => new InMemoryProductRepository(products));


            var coupons = new List<Business.Coupon.Coupon>();
            services.AddSingleton<ICouponReader>(sp => new InMemoryCouponReader(coupons));
            services.AddSingleton<ICouponRepository>(sp => new InMemoryCouponRepository(coupons));

            var campaigns = new List<Business.Campaign.Campaign>();
            services.AddSingleton<ICampaignReader>(sp => new InMemoryCampaignReader(campaigns));
            services.AddSingleton<ICampaignRepository>(sp => new InMemoryCampaignRepository(campaigns));


            services.AddMediatR(typeof(CreateCategoryCommand).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddTransient<IValidator<CreateCategoryCommand>, CreateCategoryValidator>();
            services.AddTransient<IValidator<CreateProductCommand>, CreateProductValidator>();
            services.AddTransient<IValidator<CreateCouponCommand>, CreateCouponValidator>();
            services.AddTransient<IValidator<CreateCampaignCommand>, CreateCampaignValidator>();
        }
    }
}