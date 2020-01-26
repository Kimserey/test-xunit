using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace WebApplication.Tests
{
    public class MyControllerTests: IClassFixture<MyWebApplicationFactory<Startup>>
    {
        private readonly MyWebApplicationFactory<Startup> _factory;

        public MyControllerTests(MyWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task MyControllerReturn1()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/my");
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<int>(body);
            Assert.Equal(1, res);
        }

        [Fact]
        public async Task MyControllerGetUserReturnsTheUser()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/my/user/" + Data.User1.Name);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<User>(body);
            Assert.Equal(Data.User1.Name, res.Name);
        }
    }
}