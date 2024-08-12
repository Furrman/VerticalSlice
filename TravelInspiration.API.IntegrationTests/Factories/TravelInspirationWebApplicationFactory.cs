using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace TravelInspiration.API.IntegrationTests.Factories;

public class TravelInspirationWebApplicationFactory 
    : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, configBuilder) =>
        {
            configBuilder.AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "ConnectionStrings:TravelInspirationDbConnection",
                    "Server=(localdb)\\mssqllocaldb;Database=TravelInspirationDb;Trusted_Connection=true;MultipleActiveResultSets=true;"
                }
            });
        });
        base.ConfigureWebHost(builder);
    }
}
