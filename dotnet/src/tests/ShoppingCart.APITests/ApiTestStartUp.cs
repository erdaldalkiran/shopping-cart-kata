using Microsoft.AspNetCore.Hosting;
using ShoppingCart.API;

namespace ShoppingCart.APITests
{
    public class ApiTestStartUp : Startup
    {
        public ApiTestStartUp(IWebHostEnvironment env) : base(env)
        {
        }
    }
}