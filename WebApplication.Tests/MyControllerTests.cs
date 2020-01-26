using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace WebApplication.Tests
{
    public class MyControllerTests: IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public MyControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task MyControllerReturn1()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/My");
            var body = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<int>(body);
            Assert.Equal(1, res);
        }
    }
}