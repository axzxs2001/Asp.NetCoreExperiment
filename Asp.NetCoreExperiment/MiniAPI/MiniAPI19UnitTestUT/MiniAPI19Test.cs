using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;


namespace MiniAPI19UnitTestUT
{
    public class MiniAPI19Test
    {
        [Fact]
        public async Task GetOrderTest()
        {
            var orderNo = "abcd";
            var mock = new Mock<IOrderService>();
            mock.Setup(x => x.GetOrder(It.IsAny<string>())).Returns(orderNo);

            var myapp = new MyAppHostTest(services => services.AddSingleton(mock.Object));
            var client = myapp.CreateClient();
            var result = await client.GetStringAsync("/order");
            Assert.Equal($"Result:{orderNo}", result);


        }
       
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task PostOrderTest(bool backResult)
        {
            var mock = new Mock<IOrderService>();
            mock.Setup(x => x.AddOrder(It.IsAny<Order>())).Returns(backResult);

            var myapp = new MyAppHostTest(services => services.AddSingleton(mock.Object));
            var client = myapp.CreateClient();

            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(new Order{OrderNo = "abcd",Name = "Surface Pro 8",Price = 10000}),System.Text.Encoding.UTF8,"application/json");
            var response = await client.PostAsync("/order", content);
            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal($"Result:{backResult}", result);
        }
    }


    class MyAppHostTest : WebApplicationFactory<Program>
    {
        private readonly Action<IServiceCollection> _services;
        public MyAppHostTest(Action<IServiceCollection> services)
        {

            _services = services;
        }
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(_services);
            return base.CreateHost(builder);
        }
    }


}
