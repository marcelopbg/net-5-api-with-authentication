using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using net_5_api_with_authentication;
using System.Net.Http;
using System.Net.Http.Headers;

namespace API.tests
{
    public class PrivateEndpointTestArranger
    {
        private WebApplicationFactory<Startup> factory;
        private WebApplicationFactory<Startup> factory1;

        public PrivateEndpointTestArranger(WebApplicationFactory<Startup> _factory)
        {
            factory = _factory;
        }

        public HttpClient Arrange()
        {
            var client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                            "Test", options => { });
                });
            })
          .CreateClient(new WebApplicationFactoryClientOptions
          {
              AllowAutoRedirect = false,
          });

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Test");
            return client;
        }
    }
}
