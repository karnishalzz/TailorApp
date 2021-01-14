using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(TailorShopWebApp.Areas.Identity.IdentityHostingStartup))]
namespace TailorShopWebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}