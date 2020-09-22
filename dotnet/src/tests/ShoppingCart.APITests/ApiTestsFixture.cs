using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace ShoppingCart.APITests
{
    [SetUpFixture]
    internal class ApiTestsFixture
    {
        public static TestServer Server { get; private set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseStartup<ApiTestStartUp>();

            Server = new TestServer(webHostBuilder);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Server?.Dispose();
        }
    }
}