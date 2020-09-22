using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ShoppingCart.API.Campaign;
using ShoppingCart.API.Cart;
using ShoppingCart.API.Category;
using ShoppingCart.API.Coupon;
using ShoppingCart.API.Product;
using ShoppingCart.Business.Cart;

namespace ShoppingCart.APITests
{
    public class ApiTestHelper
    {
        private readonly HttpClient client;

        public ApiTestHelper()
        {
            client = ApiTestsFixture.Server.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Guid> CreateACart()
        {
            var response = await client.PostAsync(new Uri("/cart", UriKind.Relative), null);
            var body = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
            Guid id = body.id;

            return id;
        }

        public async Task AddItemToTheCart(Guid id, AddItemRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await client.PostAsync(new Uri($"/cart/id/{id}/add-item", UriKind.Relative), content);
        }

        public async Task ApplyCouponTheCart(Guid id, ApplyCouponRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await client.PostAsync(new Uri($"/cart/id/{id}/apply-coupon", UriKind.Relative), content);
        }

        public async Task<Cart> GetCartByID(Guid id)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CustomResolver()
            };

            var response = await client.GetAsync(new Uri($"/cart/id/{id}", UriKind.Relative));
            var content = await response.Content.ReadAsStringAsync();
            var cart = JsonConvert.DeserializeObject<Cart>(content, settings);

            return cart;
        }

        public async Task<Guid> CreateACategory(CreateCategoryRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(new Uri("/category", UriKind.Relative), content);
            var body = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
            Guid id = body.id;

            return id;
        }

        public async Task<Guid> CreateAProduct(CreateProductRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(new Uri("/product", UriKind.Relative), content);
            var body = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
            Guid id = body.id;

            return id;
        }

        public async Task<Guid> CreateACoupon(CreateCouponRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(new Uri("/coupon", UriKind.Relative), content);
            var body = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
            Guid id = body.id;

            return id;
        }

        public async Task<Guid> CreateACampaign(CreateCampaignRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(new Uri("/campaign", UriKind.Relative), content);
            var body = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
            Guid id = body.id;

            return id;
        }

        private class CustomResolver : DefaultContractResolver
        {
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var prop = base.CreateProperty(member, memberSerialization);

                var cartPropNames = new[] {"DeliveryCost", "AppliedCoupon", "CouponDiscount"};
                if (member.DeclaringType == typeof(Cart) && cartPropNames.Contains(prop.PropertyName))
                    prop.Writable = true;

                var lineItemPropNames = new[] {"AppliedCampaign", "CouponDiscount"};
                if (member.DeclaringType == typeof(LineItem) && lineItemPropNames.Contains(prop.PropertyName))
                    prop.Writable = true;

                return prop;
            }
        }
    }
}