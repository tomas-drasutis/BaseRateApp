using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace BaseRateApp.IntegratinTests
{
    public class Tests
    {

        [Test]
        public void Test1()
        {
            var hostBuilder = new HostBuilder()
                   .ConfigureWebHost(webHost =>
        {
            // Add TestServer
            webHost.UseTestServer();

            // Specify the environment
            webHost.UseEnvironment("Test");

            webHost.Configure(app => app.Run(async ctx => await ctx.Response.WriteAsync("Hello World!")));
        });
        }
    }
}